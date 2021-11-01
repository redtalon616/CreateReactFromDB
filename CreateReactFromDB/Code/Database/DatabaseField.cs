using AlphawolfSoftware.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphawolfSoftware.Databases
{
    public class DatabaseField
    {
        public string COLUMN_NAME { get; set; }
        public string Readable_Caption { get; set; }
        public int? ORDINAL_POSITION { get; set; }
        public string COLUMN_DEFAULT { get; set; }
        public string DATA_TYPE { get; set; }
        public int? CHARACTER_MAXIMUM_LENGTH { get; set; }
        public int? NUMERIC_PRECISION { get; set; }
        public int? NUMERIC_PRECISION_RADIX { get; set; }
        public int? NUMERIC_SCALE { get; set; }
        public int? DATETIME_PRECISION { get; set; }
        public bool IsVisible { get; set; }
        public bool IsValidated { get; set; }
        public bool IsInList { get; set; }
        public InputTypes.Categories Category { get; set; }
    }
}
