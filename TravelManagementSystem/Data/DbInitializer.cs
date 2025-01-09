using TravelManagementSystem.Models;

namespace TravelManagementSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Seed Agents
            if (!context.Agents.Any())
            {
                var agents = new Agent[]
                {
                    new Agent { Name = "Tahir Khan", WhatsApp = "+1234567890", Phone = "1234567890", Email = "TahirKhane@example.com", OfficeAddress = "Mardan, KPK" },
                    new Agent { Name = "Mohibullah", WhatsApp = "+0987654321", Phone = "9876543210", Email = "Mohibullah@example.com", OfficeAddress = "Pesawar Ring Road" },
                    new Agent { Name = "Ali Zaman", WhatsApp = "+1112233445", Phone = "5554443333", Email = "AliZaman@example.com", OfficeAddress = "Islamabad" },
                    new Agent { Name = "Aslam Khan", WhatsApp = "+2223344556", Phone = "6665554444", Email = "AslamKhan@example.com", OfficeAddress = "Karachi" },
                    new Agent { Name = "Hassan Shah", WhatsApp = "+3334455667", Phone = "7776665555", Email = "HassanShah@example.com", OfficeAddress = "Lahore" }
                };

                foreach (var agent in agents)
                {
                    context.Agents.Add(agent);
                }
                context.SaveChanges();
            }

            // Seed Customers
            if (!context.Customers.Any())
            {
                var customers = new Customer[]
                {
                    new Customer { Name = "Ali", Phone = "1112223333", PassportNo = "A123456", Address = "Bara", Country = CountryEnum.UAE, ScanDocumentPath = "path/to/document1.pdf" },
                    new Customer { Name = "Shahab", Phone = "4445556666", PassportNo = "B654321", Address = "Peshawar", Country = CountryEnum.KSA, ScanDocumentPath = "path/to/document2.pdf" },
                    new Customer { Name = "Faizan", Phone = "1234567890", PassportNo = "C987654", Address = "Rawalpindi", Country = CountryEnum.Qatar, ScanDocumentPath = "path/to/document3.pdf" },
                    new Customer { Name = "Samir", Phone = "5556667777", PassportNo = "D135791", Address = "Quetta", Country = CountryEnum.UAE, ScanDocumentPath = "path/to/document4.pdf" },
                    new Customer { Name = "Omar", Phone = "8889990000", PassportNo = "E246810", Address = "Faisalabad", Country = CountryEnum.KSA, ScanDocumentPath = "path/to/document5.pdf" }
                };

                foreach (var customer in customers)
                {
                    context.Customers.Add(customer);
                }
                context.SaveChanges();
            }

            // Seed SalesTable
            if (!context.SalesTables.Any())
            {
                var salesTables = new SalesTable[]
                {
                    new SalesTable
                    {
                        AgentId = 1, // Link to Agent
                        CustomerId = 1, // Link to Customer
                        Company = "TransGuard",
                        Trade = Trade.Labour,
                        SubTrade = "Warehouse Worker",
                        FlightOn = DateTime.Parse("2024-01-10"),
                        Destination = "ISB-ABD",
                        Country = CountryEnum.UAE,
                        Credit = 500000,
                        Debit = 550000,
                        Balance = 50000,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new SalesTable
                    {
                        AgentId = 1, // Link to Agent
                        CustomerId = 4, // Link to Customer
                        Company = "Qatar Security",
                        Trade = Trade.Labour,
                        SubTrade = "Security Guard",
                        FlightOn = DateTime.Parse("2024-01-10"),
                        Destination = "ISB-DIA",
                        Country = CountryEnum.Qatar,
                        Credit = 600000,
                        Debit = 500000,
                        Balance = 100000,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new SalesTable
                    {
                        AgentId = 1, // Link to Agent
                        CustomerId = 5, // Link to Customer
                        Company = "TransGuard",
                        Trade = Trade.Labour,
                        SubTrade = "Warehouse Worker",
                        FlightOn = DateTime.Parse("2024-01-10"),
                        Destination = "ISB-ABD",
                        Country = CountryEnum.UAE,
                        Credit = 1000,
                        Debit = 500,
                        Balance = 500,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new SalesTable
                    {
                        AgentId = 2, // Link to Agent
                        CustomerId = 2, // Link to Customer
                        Company = "IRC",
                        Trade = Trade.Driver,
                        SubTrade = "BUS",
                        FlightOn = DateTime.Parse("2024-02-15"),
                        Destination = "PWD-JED",
                        Country = CountryEnum.KSA,
                        Credit = 2000,
                        Debit = 1500,
                        Balance = 500,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new SalesTable
                    {
                        AgentId = 2, // Link to Agent
                        CustomerId = 3, // Link to Customer
                        Company = "IRC",
                        Trade = Trade.Driver,
                        SubTrade = "BUS",
                        FlightOn = DateTime.Parse("2024-02-15"),
                        Destination = "PWD-JED",
                        Country = CountryEnum.KSA,
                        Credit = 4000,
                        Debit = 2000,
                        Balance = 2000,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new SalesTable
                    {
                        AgentId = 3, // Link to Agent
                        CustomerId = 3, // Link to Customer
                        Company = "Qatar Security",
                        Trade = Trade.Labour,
                        SubTrade = "Security",
                        FlightOn = DateTime.Parse("2024-03-20"),
                        Destination = "KHI-DIA",
                        Country = CountryEnum.Qatar,
                        Credit = 1500,
                        Debit = 1000,
                        Balance = 500,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new SalesTable
                    {
                        AgentId = 4, // Link to Agent
                        CustomerId = 4, // Link to Customer
                        Company = "Company D",
                        Trade = Trade.Driver,
                        SubTrade = "Heavy Driver",
                        FlightOn = DateTime.Parse("2024-04-25"),
                        Destination = "LRH-DXB",
                        Country = CountryEnum.UAE,
                        Credit = 3000,
                        Debit = 2500,
                        Balance = 500,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new SalesTable
                    {
                        AgentId = 5, // Link to Agent
                        CustomerId = 5, // Link to Customer
                        Company = "Al-maria",
                        Trade = Trade.Labour,
                        SubTrade = "Food Packing",
                        FlightOn = DateTime.Parse("2024-05-30"),
                        Destination = "PWD-RUH",
                        Country = CountryEnum.KSA,
                        Credit = 2500,
                        Debit = 2000,
                        Balance = 500,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    }
                };

                foreach (var sales in salesTables)
                {
                    context.SalesTables.Add(sales);
                }
                context.SaveChanges();
            }
            // Seed PurchTable
            if (!context.PurchTables.Any())
            {
                var purchTables = new PurchTable[]
                {
                    new PurchTable
                    {
                        AgentId = 1, // Link to Agent
                        CustomerId = 1, // Link to Customer
                        Company = "TransGuard",
                        Trade = Trade.Labour,
                        SubTrade = "Warehouse Worker",
                        FlightOn = DateTime.Parse("2024-01-10"),
                        Destination = "ISB-ABD",
                        Country = CountryEnum.UAE,
                        Credit = 500000,
                        Debit = 550000,
                        Balance = 50000,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new PurchTable
                    {
                        AgentId = 2, // Link to Agent
                        CustomerId = 1, // Link to Customer
                        Company = "Qatar Security",
                        Trade = Trade.Labour,
                        SubTrade = "Security Guard",
                        FlightOn = DateTime.Parse("2024-01-10"),
                        Destination = "ISB-DIA",
                        Country = CountryEnum.Qatar,
                        Credit = 600000,
                        Debit = 500000,
                        Balance = 100000,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new PurchTable
                    {
                        AgentId = 3, // Link to Agent
                        CustomerId = 1, // Link to Customer
                        Company = "TransGuard",
                        Trade = Trade.Labour,
                        SubTrade = "Warehouse Worker",
                        FlightOn = DateTime.Parse("2024-01-10"),
                        Destination = "ISB-ABD",
                        Country = CountryEnum.UAE,
                        Credit = 1000,
                        Debit = 500,
                        Balance = 500,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new PurchTable
                    {
                        AgentId = 2, // Link to Agent
                        CustomerId = 2, // Link to Customer
                        Company = "IRC",
                        Trade = Trade.Driver,
                        SubTrade = "BUS",
                        FlightOn = DateTime.Parse("2024-02-15"),
                        Destination = "PWD-JED",
                        Country = CountryEnum.KSA,
                        Credit = 2000,
                        Debit = 1500,
                        Balance = 500,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new PurchTable
                    {
                        AgentId = 3, // Link to Agent
                        CustomerId = 2, // Link to Customer
                        Company = "IRC",
                        Trade = Trade.Driver,
                        SubTrade = "BUS",
                        FlightOn = DateTime.Parse("2024-02-15"),
                        Destination = "PWD-JED",
                        Country = CountryEnum.KSA,
                        Credit = 4000,
                        Debit = 2000,
                        Balance = 2000,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new PurchTable
                    {
                        AgentId = 3, // Link to Agent
                        CustomerId = 3, // Link to Customer
                        Company = "Qatar Security",
                        Trade = Trade.Labour,
                        SubTrade = "Security",
                        FlightOn = DateTime.Parse("2024-03-20"),
                        Destination = "KHI-DIA",
                        Country = CountryEnum.Qatar,
                        Credit = 1500,
                        Debit = 1000,
                        Balance = 500,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new PurchTable
                    {
                        AgentId = 4, // Link to Agent
                        CustomerId = 3, // Link to Customer
                        Company = "Company D",
                        Trade = Trade.Driver,
                        SubTrade = "Heavy Driver",
                        FlightOn = DateTime.Parse("2024-04-25"),
                        Destination = "LRH-DXB",
                        Country = CountryEnum.UAE,
                        Credit = 3000,
                        Debit = 2500,
                        Balance = 500,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    },
                    new PurchTable
                    {
                        AgentId = 5, // Link to Agent
                        CustomerId = 5, // Link to Customer
                        Company = "Al-maria",
                        Trade = Trade.Labour,
                        SubTrade = "Food Packing",
                        FlightOn = DateTime.Parse("2024-05-30"),
                        Destination = "PWD-RUH",
                        Country = CountryEnum.KSA,
                        Credit = 2500,
                        Debit = 2000,
                        Balance = 500,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    }
                };

                foreach (var purchase in purchTables)
                {
                    context.PurchTables.Add(purchase);
                }
                context.SaveChanges();
            }
        }
    }
}
