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

            if (int.TryParse(value, out int _))
                return true;

            message = "is not a number";
            return false;
        }
    }
}
