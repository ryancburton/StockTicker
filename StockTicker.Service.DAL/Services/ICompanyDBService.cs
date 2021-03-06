﻿using StockTicker.Service.DAL.Models;
using CarParts.Service.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTicker.Service.DAL.Services
{
    public interface ICompanyDBService
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company> FindCompanyByIdAsync(int id);
        Task<Company> FindCompanyByIsinAsync(string isin);
        Task AddNewCarPartAsync(CarPart carPart);
        Task AddNewCompanyAsync(Company company);
        Task UpdateExsistingCompanyAsync(Company company);
    }
}
