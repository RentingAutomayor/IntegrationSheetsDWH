using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace IntegrationSheetsDWH.Controller
{
		public class SheetIntegrator
		{
				private UserCredential credential;
				private String spreadSheetId;
				static string[] Scopes = { SheetsService.Scope.Spreadsheets };
				private static string ApplicationName ;
				private SheetsService service;

				public SheetIntegrator(string spreadSheetId, string pathNameSecrets, string applicationName)
				{
						this.spreadSheetId = spreadSheetId;

						using (var stream = new FileStream(pathNameSecrets, FileMode.Open, FileAccess.Read))
						{
								ApplicationName = applicationName;

								this.credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
												GoogleClientSecrets.Load(stream).Secrets,
												Scopes,
												"user",
												CancellationToken.None
										).Result;


								Console.WriteLine("Se ha creado el token.");

								this.service = new SheetsService(new BaseClientService.Initializer()
								{
										HttpClientInitializer = this.credential,
										ApplicationName = ApplicationName,
								});

						}
				}


				public  void intergrateData(string sheetName,List<IList<object>> data) {
						try
						{
								string range = sheetName + "!A:XX";
								var valueRange = new ValueRange();
								valueRange.Values = data;
								var request = this.service.Spreadsheets.Values.Update(valueRange, this.spreadSheetId, range);
								request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

								var appendResponse = request.Execute();
						}
						catch (Exception ex)
						{
								Console.WriteLine(ex);
								throw;
						}
						
				}
		}
}
