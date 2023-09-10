using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using StockTicker;
using StockTicker.Service.Data.Models;
using StockTicker.Service.Data.Services;
using StockTickerUnitTests.Tests.Helpers;
using static StockTickerUnitTests.Tests.Helpers.TestUtils;

namespace StockTickerUnitTests
{
    [TestClass]
    public sealed class StockTickerUnitTests : IDisposable
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        private Uri _uri;

        public StockTickerUnitTests()
        {       
            _factory = new CustomWebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }

        [TestMethod]
        public void GetAllCompanies()
        {
            _uri = new Uri(_client.BaseAddress, $"/api/company/GetAllCompanies");
            var httpResponse = _client.GetAsync(_uri).Result;
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = httpResponse.Content.ReadAsStringAsync();
            var company = JsonConvert.DeserializeObject<IEnumerable<Company>>(stringResponse.Result);

            Assert.AreEqual(company.FirstOrDefault().Name, "ABC Inc.");
        }

        [TestMethod]
        public void FindCompanyById()
        {
            _uri = new Uri("/api/company/GetCompanyById/id=1", UriKind.Relative);
            var httpResponse = _client.GetAsync(_uri).Result;
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = httpResponse.Content.ReadAsStringAsync();
            var company = JsonConvert.DeserializeObject<Company>(stringResponse.Result);

            Assert.AreEqual(company.Name, "ABC Inc.");
        }

        [TestMethod]
        public void FindCompanyByIsin()
        {
            _uri = new Uri("/api/company/GetCompanyByIsin/Isin=AB123456789", UriKind.Relative);
            var httpResponse = _client.GetAsync(_uri).Result;
            httpResponse.EnsureSuccessStatusCode();            

            var stringResponse = httpResponse.Content.ReadAsStringAsync();
            var company = JsonConvert.DeserializeObject<Company>(stringResponse.Result);

            Assert.AreEqual(company.Isin, "AB123456789");
        }

        [TestMethod]
        public async Task AddNewCompany()
        {
            var request = new
            {
                Url = "api/company/PostCompany/",
                Body = new
                {                    
                    Name = "XYZ Inc.",
                    Exchange = "LSE",
                    Ticker = "XYZ",
                    Isin = "XY123456789",
                    website = "http://www.xyz.com"
                }
            };

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
        }

        [TestMethod]
        public async Task UpdateCompany()
        {
            var request = new
            {
                Url = "api/company/PutCompany/",
                Body = new
                {
                    Name = "XYZ Limited",
                    Exchange = "NYE",
                    Ticker = "XYZ",
                    Isin = "XY123456789",
                    website = "http://www.xyz.com"
                }
            };

            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }

        public void Dispose()
        {
            _factory?.Dispose();
            _client?.Dispose();
        }
    }
}
