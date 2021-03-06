﻿using StockTicker.Service.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTicker.Service.Data.Services
{
    public interface ICompanyDBService
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company> FindCompanyByIdAsync(int id);
        Task<Company> FindCompanyByIsinAsync(string isin);
        Task AddNewCompanyAsync(Company company);
        Task UpdateExsistingCompanyAsync(Company company);
    }
}
