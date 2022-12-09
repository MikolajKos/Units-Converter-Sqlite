  using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UnitsConverterApp.MVVM.Models.DataModels.Entities
{
    public class Unit
    {
        [Key][Browsable(false)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Ratio { get; set; }
        public int UnitTypeId { get; set; }
        [Browsable(false)]
        public UnitType UnitType { get; set; }
    }
}
