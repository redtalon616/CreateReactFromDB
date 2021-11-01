using System.Collections.Generic;

namespace AlphawolfSoftware.Databases
{
    public interface IExamineDatabase
    {
        List<DatabaseField> GetTableFields(string table, bool IsIntelligent);
        IList<string> GetTables();

        string ConnectionString { get; set; }
    }
}