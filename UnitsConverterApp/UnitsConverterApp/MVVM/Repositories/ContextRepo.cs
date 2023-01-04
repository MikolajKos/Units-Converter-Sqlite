using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnitsConverterApp.MVVM.Models;
using UnitsConverterApp.MVVM.Models.DataModels;
using UnitsConverterApp.MVVM.Models.DataModels.Entities;
using UnitsConverterApp.MVVM.ViewModels;

namespace UnitsConverterApp.MVVM.Repositories
{
    public class ContextRepo
    {
        private ContextRepoModel model = new ContextRepoModel();
        

        #region Add Queries

        public void AddUnitType(string unitType)
        {
            using MyDbContext myContext = new MyDbContext();

            myContext.UnitsType.Add(
                new UnitType
                {
                    UnitTypeName = unitType
                });

            myContext.SaveChanges();
        }

        public void AddUnit(string name, string symbol, double ratio, int unitType)
        {
            using MyDbContext myContext = new MyDbContext();

            myContext.Units.Add(
                new Unit
                {
                    Name = name,
                    Symbol = symbol,
                    Ratio = ratio,
                    UnitTypeId = unitType
                });

            myContext.SaveChanges();
        }

        #endregion


        #region Get Queries
        public List<string> GetUnitTypeList()
        {
            using MyDbContext myContext = new MyDbContext();
            model.getUnitTypeList = new List<string>();

            var unitTypesName = myContext.UnitsType.Select(s => s.UnitTypeName).ToList();

            return unitTypesName;
        }

        public List<string> GetUnitList(int typeId = 1)
        {
            using MyDbContext myContext = new MyDbContext();
            model.getUnitList = new List<string>();

            var unitsName = myContext.Units.Where(k => k.UnitTypeId == typeId)?.Select(s => s.Name).ToList();
            return unitsName;
        }

        public string GetUnitSymbol(string unitName)
        {
            using MyDbContext myContext = new MyDbContext();

            var getSymbolQuery = myContext.Units.FirstOrDefault(k => k.Name == unitName)?.Symbol;
            return getSymbolQuery;
        }
        
        private double GetRatio(string unitName)
        {
            using MyDbContext myContext = new MyDbContext();

            var getRatioQuery = myContext.Units
                .FirstOrDefault(k => k.Name == unitName)?.Ratio.ToString();

            return double.Parse(getRatioQuery);
        }

        #endregion




        public string CalculateValue(string enteredValue, string fromUnit, string toUnit)
        {
            try
            {
                double result = (double.Parse(enteredValue) * GetRatio(toUnit)) / GetRatio(fromUnit);
                return result.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public List<Unit> FillUnitsDataGrid(int typeId = 1)
        {
            using MyDbContext myContext = new MyDbContext();
            model.tableDataList = new List<Unit>();

            var tableList = model.tableDataList = myContext.Units
                .Where(k => k.UnitTypeId == typeId)?
                .Select(s => new Unit() {Id = s.Id, Name = s.Name, Symbol = s.Symbol, Ratio = s.Ratio }).ToList();

            return tableList;
        }

        public List<UnitType> FillUnitsTypesDataGrid()
        {
            using MyDbContext myContext = new MyDbContext();
            model.UnitsTypesList = new List<UnitType>();

            var tableList = model.UnitsTypesList = myContext.UnitsType.Select(s => new UnitType() {Id = s.Id, UnitTypeName = s.UnitTypeName}).ToList();

            return tableList;
        }

        public void DeleteRow(IList selectedId)
        {
            using MyDbContext myContext = new MyDbContext();

            foreach (var selectedItem in selectedId)
                myContext.Remove(selectedItem);

            myContext.SaveChanges();
        }

        public void UpdateDatGrid()
        {
            using MyDbContext myContext = new MyDbContext();

            myContext.SaveChanges();
        }
    }
}


