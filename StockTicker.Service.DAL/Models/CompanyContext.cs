﻿using Microsoft.EntityFrameworkCore;
using CarParts.Service.DAL.Models;

namespace StockTicker.Service.DAL.Models
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<CarPart> CarPart { get; set; }
    }
}
