using AlphawolfSoftware.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphawolfSoftware.State
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(DatabaseField))]
    public class MainState
    {
        public string ConnectionString { get; set; }
        
        public bool IsList { get; set; }
        public bool IsCreate { get; set; }
        public bool IsRead { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsIntelligent { get; set; }

        public List<DatabaseField> ListFields { get; set; }

        public string Table { get; set; }
        
        public string OutputFolder { get; set; } 

    }
}
