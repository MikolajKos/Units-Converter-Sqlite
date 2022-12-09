﻿using Microsoft.EntityFrameworkCore;
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
            optionsBuilder.UseSqlite(@"Data Source = UnitConverterDB.db");
        }
    }
}
