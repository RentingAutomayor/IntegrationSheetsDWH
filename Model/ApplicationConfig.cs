using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationSheetsDWH.Model
{
		public class ApplicationConfig
		{
				private string server;
				private string user;
				private string password;
				private string DB;
				private string connectionString;
				public string spGetReportToExport;
				public string spreadSheetId;
				public string pathNameSecrets;
				public string applicationName;

				public ApplicationConfig()
				{
						this.server = ConfigurationManager.AppSettings["server"];
						this.user = ConfigurationManager.AppSettings["user"];
						this.password = ConfigurationManager.AppSettings["password"];
						this.DB = ConfigurationManager.AppSettings["database"];
						this.connectionString = "data source = " + this.server + "; initial catalog = " + this.DB + "; user id = " + this.user + "; password = " + this.password + "";
						this.spGetReportToExport = ConfigurationManager.AppSettings["spGetReportsToExport"];
						this.spreadSheetId = ConfigurationManager.AppSettings["spreadSheetId"];
						this.pathNameSecrets = ConfigurationManager.AppSettings["pathNameSecrets"];
						this.applicationName = ConfigurationManager.AppSettings["applicationName"];
				}

				public string GetConnectionString()
				{
						return this.connectionString;
				}

		}
}
