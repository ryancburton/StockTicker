using StockTicker.Service.Data.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace StockTickerUnitTests.Tests.Helpers
{
    public static class TestUtils
    {
        public static void InitializeDbForTests(CompanyContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            Company company = new Company
            {
                Name = "ABC Inc.",
                Exchange = "LSE",
                Ticker = "ABC",
                Isin = "AB123456789",
                website = "http://www.abc.com"
            };

            if (!dbContext.Company.Any())
            {
                dbContext.Company.Add(company);
                dbContext.SaveChangesAsync();
            }
        }

        public static class ContentHelper
        {
            public static StringContent GetStringContent(object obj)=> new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
        }
    }
}
