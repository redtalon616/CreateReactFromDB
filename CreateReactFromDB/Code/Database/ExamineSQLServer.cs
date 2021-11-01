using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphawolfSoftware.Databases
{
    public class ExamineSQLServer : IExamineDatabase
    {
        private DatabaseTools _db;

        public ExamineSQLServer()
        {
            _db = new DatabaseTools();
        }

        public string ConnectionString
        {
            get => _db.ConnectionString;
            set => _db.ConnectionString = value;
        }

        public IList<string> GetTables()
        {
            List<string> tables = new List<string>();

            string sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
            DataTable dtTables = _db.GetDataTable(sql);

            for (int i = 0; i < dtTables.Rows.Count; i++)
            {
                tables.Add((string)dtTables.Rows[i].ItemArray[0]);
            }

            return tables;
        }

        public List<DatabaseField> GetTableFields(string table, bool IsIntelligent)
        {
            List<DatabaseField> fields = new List<DatabaseField>();

            string sql = @"SELECT COLUMN_NAME, ORDINAL_POSITION, COLUMN_DEFAULT, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_PRECISION_RADIX, NUMERIC_SCALE, DATETIME_PRECISION, IS_NULLABLE
                           FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + table + "'";
            DataTable dtFields = _db.GetDataTable(sql);

            for (int i = 0; i < dtFields.Rows.Count; i++)
            {
                DatabaseField df = new DatabaseField();

                if (!dtFields.Rows[i].IsNull("COLUMN_NAME"))
                {
                    df.COLUMN_NAME = (string)dtFields.Rows[i].ItemArray[0];
                    df.Readable_Caption = ConvertToReadable(df.COLUMN_NAME.Trim());
                }

                if (!dtFields.Rows[i].IsNull("ORDINAL_POSITION"))
                {
                    df.ORDINAL_POSITION = (int)dtFields.Rows[i].ItemArray[1];
                }

                if (!dtFields.Rows[i].IsNull("COLUMN_DEFAULT"))
                {
                    df.COLUMN_DEFAULT = (string)dtFields.Rows[i].ItemArray[2];
                }

                if (!dtFields.Rows[i].IsNull("DATA_TYPE"))
                {
                    df.DATA_TYPE = (string)dtFields.Rows[i].ItemArray[3];
                }

                if (!dtFields.Rows[i].IsNull("CHARACTER_MAXIMUM_LENGTH"))
                {
                    df.CHARACTER_MAXIMUM_LENGTH = (int)dtFields.Rows[i].ItemArray[4];
                }

                if (!dtFields.Rows[i].IsNull("NUMERIC_PRECISION"))
                {
                    df.NUMERIC_PRECISION = (byte)dtFields.Rows[i].ItemArray[5];
                }

                if (!dtFields.Rows[i].IsNull("NUMERIC_PRECISION_RADIX"))
                {
                    df.NUMERIC_PRECISION_RADIX = (Int16)dtFields.Rows[i].ItemArray[6];
                }

                if (!dtFields.Rows[i].IsNull("NUMERIC_SCALE"))
                {
                    df.NUMERIC_SCALE = (int)dtFields.Rows[i].ItemArray[7];
                }

                if (!dtFields.Rows[i].IsNull("DATETIME_PRECISION"))
                {
                    df.DATETIME_PRECISION = (Int16)dtFields.Rows[i].ItemArray[8];
                }

                df.Category = Validation.InputTypes.Categories.None;
                if (!dtFields.Rows[i].IsNull("IS_NULLABLE"))
                {
                    if (((string)dtFields.Rows[i].ItemArray[9]).ToLower() == "no")
                    {
                        df.Category = Validation.InputTypes. Categories.Required;
                        df.IsValidated = true;
                    }
                }

                df.IsVisible = true;
                df.IsInList = true;

                if (df.Category != Validation.InputTypes.Categories.Required && 
                    (df.DATA_TYPE == "text" || df.DATA_TYPE == "ntext"))
                {
                    df.IsVisible = false;
                    df.IsValidated = false;
                }

                if (df.DATA_TYPE == "text" || df.DATA_TYPE == "ntext")
                {
                    df.IsInList = false;
                }

                if (IsIntelligent)
                {
                    CleverWordAssociation(ref df);
                }

                fields.Add(df);
            }

            return fields;
        }

        private void CleverWordAssociation(ref DatabaseField df)
        {
            if (!string.IsNullOrEmpty(df.Readable_Caption))
            {
                if (df.Readable_Caption.Length == 2 && df.Readable_Caption.ToLower() == "id")
                {
                    df.IsVisible = false;
                    df.IsInList = false;
                    df.IsValidated = false;
                }

                if (df.Readable_Caption.Length > 2 && df.Readable_Caption.ToLower().IndexOf(@" id") > -1)
                {
                    df.IsVisible = false;
                    df.IsInList = false;
                    df.IsValidated = false;
                }

                //RegEX

                if (df.DATA_TYPE.ToLower() == "datetime")
                {
                    df.Category = Validation.InputTypes.Categories.DateTime;

                    if (df.Readable_Caption.ToLower().IndexOf(@"date") > -1 &&
                        df.Readable_Caption.ToLower().IndexOf(@"time") == -1)
                    {
                        df.Category = Validation.InputTypes.Categories.Date;
                    }

                    if (df.Readable_Caption.ToLower().IndexOf(@"time") > -1 &&
                        df.Readable_Caption.ToLower().IndexOf(@"date") == -1)
                    {
                        df.Category = Validation.InputTypes.Categories.Time;
                    }
                }

                if (df.DATA_TYPE.ToLower() == "int")
                {
                    df.Category = Validation.InputTypes.Categories.WholeNumber;
                }

                if (df.DATA_TYPE.ToLower() == "decimal")
                {
                    df.Category = Validation.InputTypes.Categories.Currency;
                }

                if (df.Readable_Caption.ToLower().IndexOf(@"telephone") > -1)
                {
                    df.Category = Validation.InputTypes.Categories.Telephone;
                }

                if (df.Readable_Caption.ToLower().IndexOf(@"mobile") > -1)
                {
                    df.Category = Validation.InputTypes.Categories.Mobile;
                }

                if (df.Readable_Caption.ToLower().IndexOf(@"postcode") > -1)
                {
                    df.Category = Validation.InputTypes.Categories.Postcode;
                }

                if (df.Readable_Caption.ToLower().IndexOf(@"dob") > -1)
                {
                    df.Category = Validation.InputTypes.Categories.DOB;
                }

                if (df.Readable_Caption.ToLower().IndexOf(@"age") > -1 &&
                    df.Readable_Caption.ToLower().IndexOf(@"image") == -1)
                {
                    df.Category = Validation.InputTypes.Categories.Age;
                }

                if (df.Readable_Caption.ToLower().IndexOf(@"email") > -1)
                {
                    df.Category = Validation.InputTypes.Categories.Email;
                }

                if (df.Readable_Caption.ToLower().IndexOf(@"password") > -1)
                {
                    df.Category = Validation.InputTypes.Categories.Password;
                }

                if (df.Readable_Caption.ToLower().IndexOf(@"url") > -1)
                {
                    df.Category = Validation.InputTypes.Categories.Url;
                }
            }
        }

        private string ConvertToReadable(string text)
        {
            string retrn = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (i == 0)
                {
                    retrn += text.Substring(i, 1).ToUpper();
                }
                else
                {
                    if (text.Substring(i, 1).ToUpper() == text.Substring(i, 1) &&
                        text.Substring(i - 1, 1) != text.Substring(i - 1, 1).ToUpper())
                    {
                        retrn += " " + text.Substring(i, 1);
                    }
                    else
                    {
                        retrn += text.Substring(i, 1).ToLower();
                    }
                }
            }

            return retrn;
        }

    }
}
