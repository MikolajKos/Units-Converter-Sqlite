using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UnitsConverterApp.MVVM.Models.DataModels.Entities;

namespace UnitsConverterApp.MVVM.Models.DataModels
{
    public class MyDbContext : DbContext
    {
        public DbSet<UnitType> UnitsType { get; set; }
        public DbSet<Unit> Units { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = C:\Users\nikok\OneDrive\Dokumenty\PROGRAMOWANIE\.NET\WFP\MVVM\Units-Converter-Sqlite\UnitsConverterApp\UnitsConverterApp\UnitConverterDb.db");
        }
    }
}
