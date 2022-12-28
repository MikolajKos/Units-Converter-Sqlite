using System;
using System.Collections.Generic;
using System.Text;

namespace UnitsConverterApp.Validation.TypesOfValidation
{
    public class ValidateStringIsDouble : ISpecyficValidation<string>
    {
        public bool Validate(string value, out string message)
        {
            message = "";

            if (double.TryParse(value, out double _))
                return true;

            message = "is not a number";
            return false;
        }
    }
}
