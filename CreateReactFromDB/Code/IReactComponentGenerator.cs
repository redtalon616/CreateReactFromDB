// React component Genertor
// By Martyn Lee  Date: 31/08/2021
// Do as you will with this, improvements always welcome

using AlphawolfSoftware.State;
using System.Collections.Generic;

namespace AlphawolfSoftware.Databases
{
    public interface IComponentGenerator
    {
        void Generate();
        List<DatabaseField> GetTableFields(string table);
        IList<string> GetTables();
        void Load(string folder, string filename);
        void Save(string folder, string filename);
    }
}