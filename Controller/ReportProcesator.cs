using IntegrationSheetsDWH.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationSheetsDWH.Controller
{
		class ReportProcesator
		{
				private DAOController daoController;
				private SheetIntegrator sheetIntegrator;
				private string spreadSheetId;
				private string line;
				private string pathNameSecrets;
				private string applicationName;


				public ReportProcesator(DAOController connection, string spreadSheetId,string pathNameSecrets,string applicationName)
				{
						this.daoController = connection;
						this.spreadSheetId = spreadSheetId;
						this.pathNameSecrets = pathNameSecrets;
						this.applicationName = applicationName;
						this.line = "-------------------------------------------------------------------";
				}

				public void ProcessReport(Report report) {
						try
						{
								var message = "Reporte: " + report.name + "\nDescripción: " + report.description + "\nStored procedure: " + report.storedProcedure + "\nSheet name: " + report.sheetName;
								Console.WriteLine("{0}\n{1}\n{0}", line, message);

								var dataReport = this.GetInfoReport(report.storedProcedure);
								var dataFormmated = this.FormatDataToExport(dataReport);
								this.ExportReport(report.sheetName, dataFormmated);
								
						}
						catch (Exception ex)
						{
								Console.WriteLine(ex.Message);
						}
				}

				private List<IList<object>> FormatDataToExport(DataTable data) {
						try
						{								
								List<IList<object>>lsRow = new List<IList<object>>();
								List<object> lsColumnsByRow = new List<object>(); 

								foreach (DataColumn column in data.Columns) { 
										lsColumnsByRow.Add(column.ColumnName);										
								}

								lsRow.Add(lsColumnsByRow);


								foreach (DataRow row in data.Rows)
								{
										
										object[] lsColumns = new object[row.ItemArray.Length];
										lsColumns = row.ItemArray;
										lsColumnsByRow = new List<object>();

										foreach (var col in lsColumns)
										{
												lsColumnsByRow.Add(col);
										}

										lsRow.Add(lsColumnsByRow);

								}

								return lsRow;
						}
						catch (Exception ex)
						{
								Console.WriteLine(ex.Message);
								return null;
						}
				}

				private DataTable GetInfoReport(string storedProcedure) {
						try
						{
								var query = "EXEC " + storedProcedure;
								this.daoController.createConnection();
								var dataReport = this.daoController.executeQuery(query);
								this.daoController.closeConnection();
								return dataReport;
						}
						catch (Exception ex)
						{
								Console.WriteLine(ex.Message);
								return null;
						}
				}

				private Boolean ExportReport(string sheetName, List<IList<object>> data) {
						try
						{
								this.sheetIntegrator = new SheetIntegrator(this.spreadSheetId,this.pathNameSecrets,this.applicationName);
								this.sheetIntegrator.intergrateData(sheetName, data);
							

								return true;
						}
						catch (Exception ex)
						{
								Console.WriteLine(ex.Message);
								return false;
						}
				}
		}
}
