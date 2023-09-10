using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockTicker.Service.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace StockTicker.Service.Data.Services
{
    public class CompanyDBService : ICompanyDBService
    {
        private readonly CompanyContext _companyContext;
        public CompanyDBService(CompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync() => await _companyContext.Company.ToListAsync();

        public async Task<Company> FindCompanyByIdAsync(int id) => await _companyContext.Company.SingleOrDefaultAsync(c => c.CompanyId == id);

        public async Task<Company> FindCompanyByIsinAsync(string isin) => await _companyContext.Company.SingleOrDefaultAsync(c => c.Isin == isin);

        public async Task AddNewCompanyAsync(Company company)
        {
            if (FindCompanyByIsinAsync(company.Isin).Result != null)
            {
                throw new Exception("Isin Code already exists.");
            }
            await _companyContext.Company.AddAsync(company).ConfigureAwait(false);
            await _companyContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateExsistingCompanyAsync(Company company)
        {
            try
            {
                var local = _companyContext.Set<Company>().Local
                            .FirstOrDefault(entry => entry.CompanyId.Equals(entry.CompanyId));

                if (local != null)
                {
                    _companyContext.Entry(local).State = EntityState.Detached;
                }
                _companyContext.Entry(company).State = EntityState.Modified;
                await _companyContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
