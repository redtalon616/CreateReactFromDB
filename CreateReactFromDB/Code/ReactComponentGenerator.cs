// React component Genertor
// By Martyn Lee  Date: 31/08/2021
// Do as you will with this, improvements always welcome

using AlphawolfSoftware.State;
using CreateReactFromDB.Code.OS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AlphawolfSoftware.Databases
{
    public class ReactComponentGenerator : IComponentGenerator
    {
        IExamineDatabase _edb;
        MainState _ms;

        #region Constructors

        public ReactComponentGenerator(MainState ms)
        {
            _ms = ms;
            _edb = new ExamineSQLServer();
            _edb.ConnectionString = ms.ConnectionString;
        }

        public ReactComponentGenerator(MainState ms, IExamineDatabase edb)
        {
            _ms = ms;
            _edb = edb;
            _edb.ConnectionString = ms.ConnectionString;
        }

        #endregion

        #region Database calls

        public IList<string> GetTables()
        {
            return _edb.GetTables();
        }

        public List<DatabaseField> GetTableFields(string table)
        {
            return _edb.GetTableFields(table, _ms.IsIntelligent);
        }

        #endregion

        public MainState State
        {
            get
            {
                return _ms;
            }
            set
            {
                _ms = value;
            }
        }

        public void Generate()
        {
            if (_ms.IsCreate || _ms.IsRead || _ms.IsUpdate || _ms.IsDelete)
            {
                GenerateEdit(_ms);
            }

            if (_ms.IsList)
            {
                GenerateList(_ms);
            }
        }

        #region Generation routines

        private void GenerateEdit(MainState ms)
        {
            string reactClass = GeneralReplace(OS.FileIO.LoadFile(AppContext.BaseDirectory, "Templates\\TemplateEdit.txt"), ms.Table, null);
            string reactFieldText = OS.FileIO.LoadFile(AppContext.BaseDirectory, "Templates\\TemplateInputText.txt");
            string reactFieldDate = OS.FileIO.LoadFile(AppContext.BaseDirectory, "Templates\\TemplateInputDate.txt");

            string entries = "";
            foreach (DatabaseField df in ms.ListFields.OrderBy(x => x.ORDINAL_POSITION).ToList())
            {
                if (df.IsVisible)
                {
                    if (df.DATA_TYPE == "nvarchar" || df.DATA_TYPE == "varchar")
                    {
                        entries += GeneralReplace(reactFieldText, ms.Table, df);
                    }

                    if (df.DATA_TYPE == "date")
                    {
                        entries += GeneralReplace(reactFieldDate, ms.Table, df);
                    }
                }
            }
            reactClass = reactClass.Replace("{entries}", entries, StringComparison.OrdinalIgnoreCase);

            if (ms.IsCreate || ms.IsUpdate)
            {
                string buttons = GeneralReplace(OS.FileIO.LoadFile(AppContext.BaseDirectory, "Templates\\TemplateButtons.txt"), ms.Table, null);
                reactClass = reactClass.Replace("{buttons}", buttons, StringComparison.OrdinalIgnoreCase);
            }

            OS.FileIO.SaveFile(ms.OutputFolder, ms.Table + "Edit.js", reactClass);
        }

        private string GeneralReplace(string reactFieldText, string table, DatabaseField df)
        {
            string retrn = reactFieldText.Replace("{table}", table, StringComparison.OrdinalIgnoreCase);
            if (df != null)
            {
                retrn = retrn.Replace("{Column_Name}", df.COLUMN_NAME, StringComparison.OrdinalIgnoreCase);
                retrn = retrn.Replace("{Data_Type}", df.DATA_TYPE, StringComparison.OrdinalIgnoreCase);
                retrn = retrn.Replace("{Column_Default}", df.COLUMN_DEFAULT, StringComparison.OrdinalIgnoreCase);
                retrn = retrn.Replace("{Character_Maximum_Length}", df.CHARACTER_MAXIMUM_LENGTH.ToString(), StringComparison.OrdinalIgnoreCase);
                retrn = retrn.Replace("{Ordinal_Position}", df.ORDINAL_POSITION.ToString(), StringComparison.OrdinalIgnoreCase);
                retrn = retrn.Replace("{Numeric_Precision_Radix}", df.NUMERIC_PRECISION_RADIX.ToString(), StringComparison.OrdinalIgnoreCase);
                retrn = retrn.Replace("{Readable_Caption}", df.Readable_Caption, StringComparison.OrdinalIgnoreCase);
                retrn = retrn.Replace("{Is_Visible}", GetTueFalse(df.IsVisible), StringComparison.OrdinalIgnoreCase);
                retrn = retrn.Replace("{Is_Validated}", GetTueFalse(df.IsValidated), StringComparison.OrdinalIgnoreCase);
                retrn = retrn.Replace("{Is_In_List}", GetTueFalse(df.IsInList), StringComparison.OrdinalIgnoreCase);
                retrn = retrn.Replace("{html_type}", GetHtmlFieldType(df), StringComparison.OrdinalIgnoreCase);
            }

            return retrn;
        }

        private string GetHtmlFieldType(DatabaseField df)
        {
            string ret = "text";

            if (df.DATA_TYPE.ToLower() == "date")
            {
                ret = "date";
            }

            if (df.DATA_TYPE.ToLower() == "bit")
            {
                ret = "checkbox";
            }

            if (df.DATA_TYPE.ToLower() == "nvarchar" || df.DATA_TYPE.ToLower() == "varchar")
            {
                if (df.Category == Validation.InputTypes.Categories.Email)
                {
                    ret = "email";
                }

                if (df.Category == Validation.InputTypes.Categories.Telephone)
                {
                    ret = "tel";
                }

                if (df.Category == Validation.InputTypes.Categories.Mobile)
                {
                    ret = "tel";
                }

                if (df.Category == Validation.InputTypes.Categories.WholeNumber)
                {
                    ret = "number";
                }

                if (df.Category == Validation.InputTypes.Categories.Url)
                {
                    ret = "url";
                }

                if (df.Category == Validation.InputTypes.Categories.Password)
                {
                    ret = "password";
                }
            }

            return ret;
        }

        private string GetTueFalse(bool IsIt)
        {
            if (IsIt)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        private void GenerateList(MainState ms)
        {
            string reactClass = GeneralReplace(OS.FileIO.LoadFile(AppContext.BaseDirectory, "Templates\\TemplateList.txt"), ms.Table, null);

            string headerTemplate = OS.FileIO.LoadFile(AppContext.BaseDirectory, "Templates\\TemplateRowHeader.txt");
            string rowTemplate = OS.FileIO.LoadFile(AppContext.BaseDirectory, "Templates\\TemplateRowMain.txt");

            string header = "";
            string row = "";
            foreach (DatabaseField df in ms.ListFields.OrderBy(x => x.ORDINAL_POSITION).ToList())
            {
                if (df.IsVisible && df.IsInList)
                {
                    header += GeneralReplace(headerTemplate, ms.Table, df);
                    if (df.DATA_TYPE == "nvarchar" || df.DATA_TYPE == "varchar")
                    {
                        row += GeneralReplace(rowTemplate, ms.Table, df);
                    }
                    else
                    {
                        row += GeneralReplace(rowTemplate, ms.Table, df);
                    }
                }
            }
            reactClass = reactClass.Replace("{RowHeader}", header, StringComparison.OrdinalIgnoreCase);
            reactClass = reactClass.Replace("{RowMain}", row, StringComparison.OrdinalIgnoreCase);

            OS.FileIO.SaveFile(ms.OutputFolder, ms.Table + "List.js", reactClass);
        }

        #endregion

        public void Load(string folder, string filename)
        {
            string fullpath = Path.Combine(folder, filename);
            MainState ms = Serializer.ReadFromXmlFile<MainState>(fullpath);

            if (ms != null)
            {
                _ms = ms;
            }
        }

        public void Save(string folder, string filename)
        {
            string fullpath = Path.Combine(folder, filename);
            Serializer.WriteToXmlFile<MainState>(fullpath, _ms);
        }

    }
}
