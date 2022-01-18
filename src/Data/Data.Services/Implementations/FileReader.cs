namespace Data.Services.Implementations
{
    using CsvHelper;

    using Data.Models.Models;
    using Data.Services.DtoModels;
    using Data.Services.Interfaces;
    using Data.TotalErrorDbContext;

    using System.Globalization;

    public class FileReader : IFileReader
    {
        public FileReader(TotalErrorDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public TotalErrorDbContext DbContext { get; }

        public List<TransferModel> ReadFileFromDirectory(string dir)
        {
            string[] files = Directory.GetFiles(dir);

            List<TransferModel> transferModels = new List<TransferModel>();

            var lastReadDate = this.DbContext.LastReadFiles.OrderByDescending(x => x.LastReadFileDateTime);

            foreach (string currentFile in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(currentFile);

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

                HashSet<ItemType> newItemTypes = new HashSet<ItemType>();
                HashSet<Country> newCountries = new HashSet<Country>();
                HashSet<Region> newRegions = new HashSet<Region>();
                HashSet<Sale> newSales = new HashSet<Sale>();
                HashSet<Order> newOrders = new HashSet<Order>();

                if (lastReadDate.Count() > 0)
                {
                    if (lastReadDate.First().LastReadFileDateTime < DateTime.Parse(fileName))
                    {
                        using (var reader = new StreamReader(currentFile))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            transferModels = csv.GetRecords<TransferModel>().ToList();
                        }

                        for (int i = 0; i < transferModels.Count; i++)
                        {
                            List<Sale> sales = new List<Sale>();

                            order = dbOrders.FirstOrDefault(x => x.Id == transferModels[i].OrderId);
                            if (order is null)
                            {
                                order = newOrders.FirstOrDefault(x => x.Id == transferModels[i].OrderId);

                                if (order is null)
                                {
                                    order = new Order();

                                    itemType = dbItemTypes.FirstOrDefault(it => it.ItemTypeName == transferModels[i].ItemType);
                                    
                                    if (itemType is null)
                                    {
                                        itemType = newItemTypes.FirstOrDefault(it => it.ItemTypeName == transferModels[i].ItemType);

                                        if (itemType is null)
                                        {
                                            ItemType newItemType = new ItemType()
                                            {
                                                ItemTypeName = transferModels[i].ItemType
                                            };

                                            newItemTypes.Add(newItemType);

                                            itemType = newItemType;
                                        }
                                    }

                                    List<TransferModel> salesInOrder = transferModels.Where(m => m.OrderId == transferModels[i].OrderId).ToList();
                                    foreach (TransferModel saleInOrder in salesInOrder)
                                    {
                                        desiredSale = dbSales.FirstOrDefault(s => DateTime.Equals(s.ShipDate,
                                            DateTime.ParseExact(transferModels[i].ShipDate, "M/d/yyyy", CultureInfo.InvariantCulture))
                                        && s.TotalProfit == Decimal.Parse(transferModels[i].TotalProfit));

                                        if (desiredSale is null)
                                        {
                                            desiredSale = newSales.FirstOrDefault(s => DateTime.Equals(s.ShipDate,
                                            DateTime.ParseExact(transferModels[i].ShipDate, "M/d/yyyy", CultureInfo.InvariantCulture))
                                            && s.TotalProfit == Decimal.Parse(transferModels[i].TotalProfit));

                                            if (desiredSale is null)
                                            {
                                                Sale sale = new Sale()
                                                {
                                                    ShipDate = DateTime.ParseExact(transferModels[i].ShipDate, "M/d/yyyy", CultureInfo.InvariantCulture),
                                                    UnitsSold = int.Parse(transferModels[i].UnitsSold),
                                                    UnitPrice = Decimal.Parse(transferModels[i].UnitPrice),
                                                    UnitCost = Decimal.Parse(transferModels[i].UnitCost),
                                                    TotalRevenue = Decimal.Parse(transferModels[i].TotalRevenue),
                                                    TotalCost = Decimal.Parse(transferModels[i].TotalCost),
                                                    TotalProfit = Decimal.Parse(transferModels[i].TotalProfit),
                                                    Order = order,
                                                    ItemType = itemType
                                                };

                                                desiredSale = sale;
                                                
                                                sales.Add(desiredSale);

                                                newSales.Add(sale);
                                            }
                                        }
                                    }
                                    
                                    country = dbCountries.FirstOrDefault(c => c.Name == transferModels[i].Country);
                                    if (country is null)
                                    {
                                        country = newCountries.FirstOrDefault(c => c.Name == transferModels[i].Country);
                                        if (country is null)
                                        {
                                            region = dbRegions.FirstOrDefault(r => r.Name == transferModels[i].Region);
                                            
                                            if (region is null)
                                            {
                                                region = newRegions.FirstOrDefault(r => r.Name == transferModels[i].Region);
                                                if (region is null)
                                                {
                                                    Region newRegion = new Region()
                                                    {
                                                        Name = transferModels[i].Region
                                                    };

                                                    newRegions.Add(newRegion);

                                                    region = newRegion;
                                                }
                                            }

                                            Country newCountry = new Country()
                                            {
                                                Name = transferModels[i].Country,
                                                Region = region,
                                            };

                                            newCountries.Add(newCountry);

                                            country = newCountry;
                                        }
                                    }

                                    order.Id = transferModels[i].OrderId;
                                    order.OrderPriority = transferModels[i].OrderPriority;
                                    order.OrderDate = DateTime.ParseExact(transferModels[i].OrderDate, "M/d/yyyy", CultureInfo.InvariantCulture);
                                    order.SalesChannel = transferModels[i].SalesChannel;
                                    order.Sales = sales;
                                    order.Country = country;

                                    newOrders.Add(order);
                                }
                            }
                        }
                        this.DbContext.ItemTypes.AddRange(newItemTypes);
                        this.DbContext.Countries.AddRange(newCountries);
                        this.DbContext.Regions.AddRange(newRegions);
                        this.DbContext.Sales.AddRange(newSales);
                        this.DbContext.Orders.AddRange(newOrders);


                        DbContext.LastReadFiles.Add(
                            new LastReadFile()
                            {
                                LastReadFileDateTime = DateTime.Parse(fileName)
                            });
                    }
   
                }
                else
                {
                    using (var reader = new StreamReader(currentFile))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        transferModels = csv.GetRecords<TransferModel>().ToList();
                    }

                    for (int i = 0; i < transferModels.Count; i++)
                    {
                        order = newOrders.FirstOrDefault(x => x.Id == transferModels[i].OrderId);
                        List<Sale> sales = new List<Sale>();

                        if (order is null)
                        {
                            order = new Order();

                            itemType = newItemTypes.FirstOrDefault(it => it.ItemTypeName == transferModels[i].ItemType);
                            if (itemType is null)
                            {
                                ItemType newItemType = new ItemType()
                                {
                                    ItemTypeName = transferModels[i].ItemType
                                };

                                newItemTypes.Add(newItemType);

                                itemType = newItemType;

                            }

                            List<TransferModel> salesInOrder = transferModels.Where(m => m.OrderId == transferModels[i].OrderId).ToList();
                            foreach (TransferModel saleInOrder in salesInOrder)
                            {
                                desiredSale = newSales.FirstOrDefault(s => DateTime.Equals(s.ShipDate, 
                                    DateTime.ParseExact(transferModels[i].ShipDate, "M/d/yyyy", CultureInfo.InvariantCulture)) 
                                && s.TotalProfit == Decimal.Parse(transferModels[i].TotalProfit));

                                if (desiredSale is null)
                                {
                                    Sale sale = new Sale()
                                    {
                                        ShipDate = DateTime.ParseExact(transferModels[i].ShipDate, "M/d/yyyy", CultureInfo.InvariantCulture),
                                        UnitsSold = int.Parse(transferModels[i].UnitsSold),
                                        UnitPrice = Decimal.Parse(transferModels[i].UnitPrice),
                                        UnitCost = Decimal.Parse(transferModels[i].UnitCost),
                                        TotalRevenue = Decimal.Parse(transferModels[i].TotalRevenue),
                                        TotalCost = Decimal.Parse(transferModels[i].TotalCost),
                                        TotalProfit = Decimal.Parse(transferModels[i].TotalProfit),
                                        Order = order,
                                        ItemType = itemType
                                    };

                                    desiredSale = sale;

                                    sales.Add(desiredSale);

                                    newSales.Add(sale);
                                }
                            }

                            country = newCountries.FirstOrDefault(c => c.Name == transferModels[i].Country);
                            if (country is null)
                            {
                                region = this.DbContext.Regions.FirstOrDefault(r => r.Name == transferModels[i].Region);
                                if (region is null)
                                {
                                    region = newRegions.FirstOrDefault(r => r.Name == transferModels[i].Region);
                                    if (region is null)
                                    {
                                        Region newRegion = new Region()
                                        {
                                            Name = transferModels[i].Region
                                        };

                                        region = newRegion;

                                        newRegions.Add(newRegion);
                                    }
                                }

                                Country newCountry = new Country()
                                {
                                    Name = transferModels[i].Country,
                                    Region = region,
                                };

                                newCountries.Add(newCountry);

                                country = newCountry;
                            }

                            order.Id = transferModels[i].OrderId;
                            order.OrderPriority = transferModels[i].OrderPriority;
                            order.OrderDate = DateTime.ParseExact(transferModels[i].OrderDate, "M/d/yyyy", CultureInfo.InvariantCulture);
                            order.SalesChannel = transferModels[i].SalesChannel;
                            order.Sales = sales;
                            order.Country = country;

                            newOrders.Add(order);
                        }  
                    }

                    this.DbContext.ItemTypes.AddRange(newItemTypes);
                    this.DbContext.Countries.AddRange(newCountries);
                    this.DbContext.Regions.AddRange(newRegions);
                    this.DbContext.Sales.AddRange(newSales);
                    this.DbContext.Orders.AddRange(newOrders);

                    DbContext.LastReadFiles.Add(
                        new LastReadFile()
                        {
                            LastReadFileDateTime = DateTime.Parse(fileName)
                        }
                    );
                    DbContext.SaveChanges();
                }

                DbContext.SaveChanges();
            }

            return transferModels;
        }
    }
}
