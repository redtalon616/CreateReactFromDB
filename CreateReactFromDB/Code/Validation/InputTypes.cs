using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphawolfSoftware.Validation
{
    public static class InputTypes
    {
        public enum Categories
        {
            None = 0,
            Required = 1,
            RegEX = 2,
            Date = 3,
            DateTime = 4,
            Time = 5,
            WholeNumber = 6,
            Currency = 7,
            Telephone = 8,
            Mobile = 9,
            Postcode = 10,
            DOB = 11,
            Age = 12,
            Email = 13,
            Password = 14,
            Url = 15
        }
    }
}
