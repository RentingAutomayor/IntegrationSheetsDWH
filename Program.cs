using IntegrationSheetsDWH.Controller;
using IntegrationSheetsDWH.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationSheetsDWH
{
		class Program
		{
				public static string sLine = "-------------------------------------------------------------------";
				static void Main(string[] args)
				{
						var message = "Apliación: IntegrationSheetsDWH\nDescripción: Esta aplicación permitira integrar la información de los reportes con hojas de calculo de google para que podamos visualizar la información en Data Studio";
						Console.WriteLine("{0}\n{1}\n{0}", sLine, message);
						ApplicationConfig cnf = new ApplicationConfig();

						var commandToExecute = "EXEC " + cnf.spGetReportToExport;

						var daoController = new DAOController(cnf.GetConnectionString());
						daoController.createConnection();
						var listReportToExport = daoController.executeQuery(commandToExecute);
						daoController.closeConnection();

						message = "Inicia procesamiento de reportes ...";
						Console.WriteLine("{0}\n{1}\n{0}", sLine, message);

						ReportProcesator reportProcesator = new ReportProcesator(daoController, cnf.spreadSheetId,cnf.pathNameSecrets,cnf.applicationName);

						foreach (DataRow row in listReportToExport.Rows)
						{
								var reportToExport = new Report();
								reportToExport.id = row[(int)ReportField.ID].ToString();
								reportToExport.name = row[(int)ReportField.NAME].ToString();
								reportToExport.description = row[(int)ReportField.DESCRIPTION].ToString();
								reportToExport.storedProcedure = row[(int)ReportField.STORED_PROCEDURE].ToString();
								reportToExport.sheetName = row[(int)ReportField.SHEET_NAME].ToString();

								reportProcesator.ProcessReport(reportToExport);

						}


						Console.Read();
				}
		}
}
