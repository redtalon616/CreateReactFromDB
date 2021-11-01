using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphawolfSoftware.Databases
{
    public class DatabaseTools
    {
        public string ConnectionString { get; set; }

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);

            if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }

        public DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
        {
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);
            command.CommandType = commandType;
            return command;
        }

        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = GetConnection();
                DbCommand mycommand = GetCommand(cnn, sql, CommandType.Text);
                mycommand.CommandText = sql;
                DbDataReader reader = mycommand.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                cnn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dt;
        }
    }
}
