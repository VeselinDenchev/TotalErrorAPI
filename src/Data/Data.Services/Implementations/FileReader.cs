namespace Data.Services.Implementations
{
    using CsvHelper;

    using Data.Models.Models;
    using Data.Models.Enums;
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

                if (lastReadDate.Count() > 0)
                {
                    if (lastReadDate.First().LastReadFileDateTime < DateTime.Parse(fileName))
                    {
                        using (var reader = new StreamReader(currentFile))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            transferModels = csv.GetRecords<TransferModel>().ToList();
                        }

                        Order order;
                        Sale desiredSale;
                        ItemType itemType;
                        Country country;
                        Region region;

                        for (int i = 0; i < transferModels.Count; i++)
                        {
                            order = this.DbContext.Orders.FirstOrDefault(x => x.Id == transferModels[i].OrderId);
                            List<Sale> sales = new List<Sale>();

                            if (order is null)
                            {
                                foreach (Sale sale in order.Sales)
                                {
                                    sales.Add(sale);

                                    itemType = this.DbContext.ItemTypes.FirstOrDefault(it => it.ItemTypeName == transferModels[i].ItemType);
                                    if (itemType is null)
                                    {
                                        ItemType newItemType = new ItemType()
                                        {
                                            ItemTypeName = sale.ItemType.ItemTypeName
                                        };

                                        this.DbContext.ItemTypes.Add(newItemType);

                                        itemType = newItemType;
                                    }
                                }

                                this.DbContext.AddRange(sales);

                                country = this.DbContext.Countries.FirstOrDefault(c => c.Name == transferModels[i].Country);
                                if (country is null)
                                {
                                    region = this.DbContext.Regions.FirstOrDefault(r => r.Name == transferModels[i].Region);
                                    if (region is null)
                                    {
                                        Region newRegion = new Region()
                                        {
                                            Name = transferModels[i].Region
                                        };

                                        this.DbContext.Add(newRegion);

                                        region = newRegion;
                                    }

                                    Country newCountry = new Country()
                                    {
                                        Name = transferModels[i].Country,
                                        Region = region,
                                    };

                                    this.DbContext.Countries.Add(newCountry);

                                    country = newCountry;
                                }

                                string orderPriority = string.Empty;
                                switch (order.OrderPriority)
                                {
                                    case OrderPriorities.L:
                                        orderPriority = "L";
                                        break;

                                    case OrderPriorities.M:
                                        orderPriority = "M";
                                        break;

                                    case OrderPriorities.H:
                                        orderPriority = "H";
                                        break;

                                    case OrderPriorities.C:
                                        orderPriority = "C";
                                        break;
                                }
                                Enum.TryParse(orderPriority, out OrderPriorities priority);

                                string salesChannel = null;
                                switch (order.SalesChannel)
                                {
                                    case SalesChannels.Online:
                                        salesChannel = "Online";
                                        break;

                                    case SalesChannels.Offline:
                                        salesChannel = "Offline";
                                        break;
                                }
                                Enum.TryParse(salesChannel, out SalesChannels channel);

                                Order newOrder = new Order()
                                {
                                    OrderPriority = priority,
                                    OrderDate = order.OrderDate,
                                    SalesChannel = channel,
                                    Sales = sales,
                                    Country = order.Country,
                                };

                                this.DbContext.Orders.Add(newOrder);
                            }
                            else
                            {
                                foreach (Sale sale in order.Sales)
                                {
                                    desiredSale = this.DbContext.Sales
                                        .FirstOrDefault(x => (DateTime.Compare(x.ShipDate, DateTime.Parse(transferModels[i].ShipDate)) == 0
                                                        && x.TotalProfit == Decimal.Parse(transferModels[i].TotalProfit)));

                                    if (desiredSale is null)
                                    {
                                        itemType = this.DbContext.ItemTypes.FirstOrDefault(it => it.ItemTypeName == transferModels[i].ItemType);
                                        if (itemType is null)
                                        {
                                            ItemType newItemType = new ItemType()
                                            {
                                                ItemTypeName = sale.ItemType.ItemTypeName
                                            };

                                            this.DbContext.ItemTypes.Add(newItemType);

                                            itemType = newItemType;
                                        }
                                        this.DbContext.ItemTypes.Add(itemType);


                                        Sale newSale = new Sale()
                                        {
                                            ShipDate = sale.ShipDate,
                                            UnitsSold = sale.UnitsSold,
                                            UnitPrice = sale.UnitPrice,
                                            UnitCost = sale.UnitCost,
                                            TotalRevenue = sale.TotalRevenue,
                                            TotalCost = sale.TotalCost,
                                            TotalProfit = sale.TotalProfit,
                                            Order = order,
                                            ItemType = itemType
                                        };
                                        this.DbContext.Add(newSale);
                                    }

                                    country = this.DbContext.Countries.FirstOrDefault(c => c.Name == transferModels[i].Country);
                                    if (country is null)
                                    {
                                        region = this.DbContext.Regions.FirstOrDefault(r => r.Name == transferModels[i].Region);
                                        if (region is null)
                                        {
                                            Region newRegion = new Region()
                                            {
                                                Name = transferModels[i].Region
                                            };

                                            this.DbContext.Add(newRegion);

                                            region = newRegion;
                                        }

                                        Country newCountry = new Country()
                                        {
                                            Name = transferModels[i].Country,
                                            Region = region,
                                        };

                                        this.DbContext.Countries.Add(newCountry);

                                        country = newCountry;
                                    };
                                }
                            }
                        }
                    }

                    this.DbContext.LastReadFiles.Add(new LastReadFile() { LastReadFileDateTime = DateTime.Now });
                }
                else
                {
                    using (var reader = new StreamReader(currentFile))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        transferModels = csv.GetRecords<TransferModel>().ToList();
                    }
                    Order order;
                    Sale desiredSale;
                    ItemType itemType;
                    Country country;
                    Region region;

                    for (int i = 0; i < transferModels.Count; i++)
                    {
                        order = this.DbContext.Orders.FirstOrDefault(x => x.Id == transferModels[i].OrderId);
                        List<Sale> sales = new List<Sale>();

                        if (order is null)
                        {
                            foreach (Sale sale in transferModels[i].Sales)
                            {
                                sales.Add(sale);

                                itemType = this.DbContext.ItemTypes.FirstOrDefault(it => it.ItemTypeName == transferModels[i].ItemType);
                                if (itemType is null)
                                {
                                    ItemType newItemType = new ItemType()
                                    {
                                        ItemTypeName = sale.ItemType.ItemTypeName
                                    };

                                    this.DbContext.ItemTypes.Add(newItemType);

                                    itemType = newItemType;
                                }
                            }

                            this.DbContext.AddRange(sales);

                            country = this.DbContext.Countries.FirstOrDefault(c => c.Name == transferModels[i].Country);
                            if (country is null)
                            {
                                region = this.DbContext.Regions.FirstOrDefault(r => r.Name == transferModels[i].Region);
                                if (region is null)
                                {
                                    Region newRegion = new Region()
                                    {
                                        Name = transferModels[i].Region
                                    };

                                    this.DbContext.Add(newRegion);

                                    region = newRegion;
                                }

                                Country newCountry = new Country()
                                {
                                    Name = transferModels[i].Country,
                                    Region = region,
                                };

                                this.DbContext.Countries.Add(newCountry);

                                country = newCountry;
                            }

                            string orderPriority = string.Empty;
                            switch (order.OrderPriority)
                            {
                                case OrderPriorities.L:
                                    orderPriority = "L";
                                    break;

                                case OrderPriorities.M:
                                    orderPriority = "M";
                                    break;

                                case OrderPriorities.H:
                                    orderPriority = "H";
                                    break;

                                case OrderPriorities.C:
                                    orderPriority = "C";
                                    break;
                            }
                            Enum.TryParse(orderPriority, out OrderPriorities priority);

                            string salesChannel = null;
                            switch (order.SalesChannel)
                            {
                                case SalesChannels.Online:
                                    salesChannel = "Online";
                                    break;

                                case SalesChannels.Offline:
                                    salesChannel = "Offline";
                                    break;
                            }
                            Enum.TryParse(salesChannel, out SalesChannels channel);

                            Order newOrder = new Order()
                            {
                                OrderPriority = priority,
                                OrderDate = order.OrderDate,
                                SalesChannel = channel,
                                Sales = sales,
                                Country = order.Country,
                            };

                            this.DbContext.Orders.Add(newOrder);
                        }
                        else
                        {
                            foreach (Sale sale in order.Sales)
                            {
                                desiredSale = this.DbContext.Sales
                                    .FirstOrDefault(x => (DateTime.Compare(x.ShipDate, DateTime.Parse(transferModels[i].ShipDate)) == 0
                                                    && x.TotalProfit == Decimal.Parse(transferModels[i].TotalProfit)));

                                if (desiredSale is null)
                                {
                                    itemType = this.DbContext.ItemTypes.FirstOrDefault(it => it.ItemTypeName == transferModels[i].ItemType);
                                    if (itemType is null)
                                    {
                                        ItemType newItemType = new ItemType()
                                        {
                                            ItemTypeName = sale.ItemType.ItemTypeName
                                        };

                                        this.DbContext.ItemTypes.Add(newItemType);

                                        itemType = newItemType;
                                    }
                                    this.DbContext.ItemTypes.Add(itemType);


                                    Sale newSale = new Sale()
                                    {
                                        ShipDate = sale.ShipDate,
                                        UnitsSold = sale.UnitsSold,
                                        UnitPrice = sale.UnitPrice,
                                        UnitCost = sale.UnitCost,
                                        TotalRevenue = sale.TotalRevenue,
                                        TotalCost = sale.TotalCost,
                                        TotalProfit = sale.TotalProfit,
                                        Order = order,
                                        ItemType = itemType
                                    };
                                    this.DbContext.Add(newSale);
                                }

                                country = this.DbContext.Countries.FirstOrDefault(c => c.Name == transferModels[i].Country);
                                if (country is null)
                                {
                                    region = this.DbContext.Regions.FirstOrDefault(r => r.Name == transferModels[i].Region);
                                    if (region is null)
                                    {
                                        Region newRegion = new Region()
                                        {
                                            Name = transferModels[i].Region
                                        };

                                        this.DbContext.Add(newRegion);

                                        region = newRegion;
                                    }

                                    Country newCountry = new Country()
                                    {
                                        Name = transferModels[i].Country,
                                        Region = region,
                                    };

                                    this.DbContext.Countries.Add(newCountry);

                                    country = newCountry;
                                };
                            }
                        }
                    }
                    DbContext.LastReadFiles.Add(
                        new LastReadFile()
                        {
                            LastReadFileDateTime = DateTime.Parse(fileName)
                        }
                    );
                    //DbContext.SaveChanges();
                }

                //DbContext.SaveChanges();
            }

            return transferModels;
        }
    }
}
