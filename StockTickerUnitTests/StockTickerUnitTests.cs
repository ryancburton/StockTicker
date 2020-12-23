using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using StockTicker.Service.Data.Models;
using StockTicker.Service.Data.Services;
using System.Linq;

namespace StockTickerUnitTests
{
    [TestClass]
    public sealed class StockTickerUnitTests : IDisposable
    {
        private CompanyContext _companyContext;

        public StockTickerUnitTests()
        {
            //Database should be mocked in Memory db
            //Calling Controller would be more of an integration test than Unit test
            InitContext();
        }

        private void InitContext()
        {
            var builder = new DbContextOptionsBuilder<CompanyContext>().UseInMemoryDatabase("CompanyDB");
            _companyContext = new CompanyContext(builder.Options);

            Company company = new Company
            {
                Name = "ABC Inc.",
                Exchange = "LSE",
                Ticker = "ABC",
                Isin = "AB123456789",
                website = "http://www.abc.com"
            };

            if (!_companyContext.Company.Any())
            {
                _companyContext.Company.Add(company);
                _companyContext.SaveChangesAsync();
            }
        }

        [TestMethod]
        public void FindCompanyById()
        {
            var companyDBService = new CompanyDBService(_companyContext);

            var firstCompany = companyDBService.GetAllCompaniesAsync().Result.FirstOrDefault<Company>();

            var company = companyDBService.FindCompanyByIdAsync(firstCompany.CompanyId);
            Assert.AreEqual(company.Result.Name, firstCompany.Name);
        }

        [TestMethod]
        public async Task FindCompanyByIsin()
        {
            var companyDBService = new CompanyDBService(_companyContext);

            var firstCompany1 = companyDBService.GetAllCompaniesAsync();
            var firstCompany = companyDBService.GetAllCompaniesAsync().Result.FirstOrDefault<Company>();

            var company = await companyDBService.FindCompanyByIsinAsync(firstCompany.Isin);
            Assert.AreEqual(company.Name, firstCompany.Name);
        }

        [TestMethod]
        public async Task AddNewCompany()
        {
            var companyDBService = new CompanyDBService(_companyContext);

            Company company = new Company
            {
                Name = "XYZ Inc.",
                Exchange = "LSE",
                Ticker = "XYZ",
                Isin = "XY123456789",
                website = "http://www.xyz.com"
            };

            await companyDBService.AddNewCompanyAsync(company).ConfigureAwait(false);
            var newCompany = await companyDBService.FindCompanyByIsinAsync(company.Isin);
            
            Assert.AreEqual(newCompany.Name, company.Name);
        }

        [TestMethod]
        public async Task UpdateCompany()
        {
            var companyDBService = new CompanyDBService(_companyContext);

            var firstCompany = companyDBService.GetAllCompaniesAsync().Result.FirstOrDefault<Company>();

            firstCompany.Name = "Hostile Take over";

            await companyDBService.UpdateExsistingCompanyAsync(firstCompany).ConfigureAwait(false);
            var updatedCompany = await companyDBService.FindCompanyByIsinAsync(firstCompany.Isin);

            Assert.AreEqual(firstCompany.Name, updatedCompany.Name);
        }

        public void Dispose()
        {
            _companyContext.Dispose();
        }
    }
}
