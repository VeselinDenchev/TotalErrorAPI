namespace Data.Services.Implementations
{
    using CsvHelper;

    using Data.Models.Models;
    using Data.Services.DtoModels;
    using Data.Services.Interfaces;
    using Data.TotalErrorDbContext;

    using System.Globalization;

    public class BaseService : IBaseService
    {
        public BaseService(TotalErrorDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public TotalErrorDbContext DbContext { get; }

        public Dictionary<string, List<TransferModel>> ReadFilesFromDirectory(string dir)
        {
            string[] files = Directory.GetFiles(dir);
            //fileNames = new List<string>();

            List<TransferModel> transferModels = new List<TransferModel>();
            Dictionary<string, List<TransferModel>> transferModelDates = new Dictionary<string, List<TransferModel>>();

            var lastReadDate = this.DbContext.LastReadFiles.OrderByDescending(x => x.LastReadFileDateTime);

            foreach (var currentFile in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(currentFile);
                using (var reader = new StreamReader(currentFile))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    bool isRead = lastReadDate.Count() > 0 && lastReadDate.First().LastReadFileDateTime < DateTime.Parse(fileName);
                    bool anyFilesHasBeenRead = lastReadDate.Count() > 0;

                    if (!isRead || !anyFilesHasBeenRead)
                    {
                        transferModels = csv.GetRecords<TransferModel>().ToList();
                        transferModelDates[fileName] = transferModels;
                        //fileNames.Add(fileName);
                    }
                }
            }

            return transferModelDates;
        }

        public DataObject Convert(Dictionary<string, List<TransferModel>> transferModelsByFile)
        {
            Order order;
            Sale desiredSale;
            ItemType itemType;
            Country country;
            Region region;

            HashSet<ItemType> dbItemTypes = this.DbContext.ItemTypes.ToHashSet();
            HashSet<Country> dbCountries = this.DbContext.Countries.ToHashSet();
            HashSet<Region> dbRegions = this.DbContext.Regions.ToHashSet();
            HashSet<Sale> dbSales = this.DbContext.Sales.ToHashSet();
            HashSet<Order> dbOrders = this.DbContext.Orders.ToHashSet();

            HashSet<ItemType> tempItemTypes = new HashSet<ItemType>();
            HashSet<Country> tempCountries = new HashSet<Country>();
            HashSet<Region> tempRegions = new HashSet<Region>();
            HashSet<Sale> tempSales = new HashSet<Sale>();
            HashSet<Order> tempOrders = new HashSet<Order>();

            Queue<string> dates = new Queue<string>();

            foreach (string date in transferModelsByFile.Keys)
            {
                dates.Enqueue(date);
            }

                foreach (List<TransferModel> transferModelsList in transferModelsByFile.Values)
                {
                    string fileDate = dates.Dequeue();

                    foreach (TransferModel transferModel in transferModelsList)
                    {
                        List<Sale> sales = new List<Sale>();

                        order = dbOrders.FirstOrDefault(x => x.Id == transferModel.OrderId);
                        if (order is null)
                        {
                            order = tempOrders.FirstOrDefault(x => x.Id == transferModel.OrderId);

                            if (order is null)
                            {
                                order = new Order();

                                itemType = dbItemTypes.FirstOrDefault(it => it.ItemTypeName == transferModel.ItemType);

                                if (itemType is null)
                                {
                                    itemType = tempItemTypes.FirstOrDefault(it => it.ItemTypeName == transferModel.ItemType);

                                    if (itemType is null)
                                    {
                                        ItemType newItemType = new ItemType()
                                        {
                                            ItemTypeName = transferModel.ItemType
                                        };

                                        tempItemTypes.Add(newItemType);

                                        itemType = newItemType;
                                    }
                                }

                                List<TransferModel> salesInOrder = transferModelsList
                                    .Where(m => m.OrderId == transferModel.OrderId)
                                    .ToList();
                                foreach (TransferModel saleInOrder in salesInOrder)
                                {
                                    desiredSale = dbSales.FirstOrDefault(s => DateTime.Equals(s.ShipDate,
                                        DateTime.ParseExact(transferModel.ShipDate, "M/d/yyyy", CultureInfo.InvariantCulture))
                                    && s.TotalProfit == Decimal.Parse(transferModel.TotalProfit));

                                    if (desiredSale is null)
                                    {
                                        desiredSale = tempSales.FirstOrDefault(s => DateTime.Equals(s.ShipDate,
                                        DateTime.ParseExact(transferModel.ShipDate, "M/d/yyyy", CultureInfo.InvariantCulture))
                                        && s.TotalProfit == Decimal.Parse(transferModel.TotalProfit));

                                        if (desiredSale is null)
                                        {
                                            Sale sale = new Sale()
                                            {
                                                ShipDate = DateTime.ParseExact(transferModel.ShipDate, "M/d/yyyy", CultureInfo.InvariantCulture),
                                                UnitsSold = int.Parse(transferModel.UnitsSold),
                                                UnitPrice = Decimal.Parse(transferModel.UnitPrice),
                                                UnitCost = Decimal.Parse(transferModel.UnitCost),
                                                TotalRevenue = Decimal.Parse(transferModel.TotalRevenue),
                                                TotalCost = Decimal.Parse(transferModel.TotalCost),
                                                TotalProfit = Decimal.Parse(transferModel.TotalProfit),
                                                Order = order,
                                                ItemType = itemType,
                                                FileDate = fileDate
                                            };

                                            desiredSale = sale;

                                            sales.Add(desiredSale);

                                            tempSales.Add(sale);
                                        }
                                    }
                                }

                                country = dbCountries.FirstOrDefault(c => c.Name == transferModel.Country);
                                if (country is null)
                                {
                                    country = tempCountries.FirstOrDefault(c => c.Name == transferModel.Country);
                                    if (country is null)
                                    {
                                        region = dbRegions.FirstOrDefault(r => r.Name == transferModel.Region);

                                        if (region is null)
                                        {
                                            region = tempRegions.FirstOrDefault(r => r.Name == transferModel.Region);
                                            if (region is null)
                                            {
                                                Region newRegion = new Region()
                                                {
                                                    Name = transferModel.Region
                                                };

                                                tempRegions.Add(newRegion);

                                                region = newRegion;
                                            }
                                        }

                                        Country newCountry = new Country()
                                        {
                                            Name = transferModel.Country,
                                            Region = region,
                                        };

                                        tempCountries.Add(newCountry);

                                        country = newCountry;
                                    }
                                }

                                order.Id = transferModel.OrderId;
                                order.OrderPriority = transferModel.OrderPriority;
                                order.OrderDate = DateTime.ParseExact(transferModel.OrderDate, "M/d/yyyy", CultureInfo.InvariantCulture);
                                order.SalesChannel = transferModel.SalesChannel;
                                order.Sales = sales;
                                order.Country = country;
                                order.FileDate = fileDate;

                                tempOrders.Add(order);
                            }
                        }
                    }

                }

            DataObject data = new DataObject();
            data.Countries = tempCountries;
            data.ItemTypes = tempItemTypes;
            data.Orders = tempOrders;
            data.Regions = tempRegions;
            data.Sales = tempSales;

            return data;
        }

        public void SaveDataToDatabase(DataObject data)
        {
            this.DbContext.ItemTypes.AddRange(data.ItemTypes);
            this.DbContext.Countries.AddRange(data.Countries);
            this.DbContext.Regions.AddRange(data.Regions);
            this.DbContext.Sales.AddRange(data.Sales);
            this.DbContext.Orders.AddRange(data.Orders);

            List<LastReadFile> dates = new List<LastReadFile>();

            foreach (DateTime fileName in data.LastReadFiles)
            {
                LastReadFile lastReadFile = new LastReadFile()
                {
                    LastReadFileDateTime = fileName
                };

                dates.Add(lastReadFile);
            }

            this.DbContext.LastReadFiles.AddRange(dates);

            DbContext.SaveChanges();
        }
        
    }
}
