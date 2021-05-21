using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationSheetsDWH.DAO
{
		public class DAO
		{
				public SqlConnection GetDatabaseConnection(string connectionString)
				{
						return new SqlConnection(connectionString);
				}
				public SqlCommand GetSqlCommand(string sqlquery, SqlConnection dbConnection)
				{
						return new SqlCommand(sqlquery, dbConnection);
				}
				public SqlDataAdapter GetDataAdapter(string sqlquery, SqlConnection dbConnection)
				{
						return new SqlDataAdapter(sqlquery, dbConnection);
				}


		}
}
