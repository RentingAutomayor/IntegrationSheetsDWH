using IntegrationSheetsDWH.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationSheetsDWH.Controller
{
		class DAOController
		{
				private string ConnectionString;
				private SqlConnection connection;
				private SqlCommand command;
				private SqlDataAdapter dtAdapter;
				private DataTable tblResult;


				private DAO.DAO objDAO;


				public DAOController(string connectionString)
				{
						this.ConnectionString = connectionString;
						this.objDAO = new DAO.DAO();
				}

				public void createConnection()
				{
						try
						{
								this.connection = this.objDAO.GetDatabaseConnection(this.ConnectionString);

						}
						catch (Exception ex)
						{
								Console.WriteLine(ex.Message);
								throw;
						}

				}

				public DataTable executeQuery(string sCommand)
				{
						try
						{
								this.dtAdapter = this.objDAO.GetDataAdapter(sCommand, this.connection);
								this.tblResult = new DataTable();
								this.dtAdapter.Fill(this.tblResult);
								return this.tblResult;
						}
						catch (Exception ex)
						{
								Console.WriteLine(ex.Message);
								throw;
						}

				}

				public void executeProcedureWithParams(string procedureName, List<Param> lsParam)
				{
						try
						{
								this.connection.Open();
								using (SqlCommand cmd = new SqlCommand(procedureName, this.connection))
								{
										cmd.CommandType = CommandType.StoredProcedure;

										foreach (var parameter in lsParam)
										{

												switch (parameter.datatype.ToUpper())
												{
														case "VARCHAR":
																cmd.Parameters.Add(parameter.getParamName(), SqlDbType.VarChar).Value = parameter.paramContent;
																break;
														case "INT":
																cmd.Parameters.Add(parameter.getParamName(), SqlDbType.Int).Value = int.Parse(parameter.paramContent);
																break;
														case "BIGINT":
																cmd.Parameters.Add(parameter.getParamName(), SqlDbType.BigInt).Value = parameter.paramContent.ToString();
																break;
														case "DATETIME":
																cmd.Parameters.Add(parameter.getParamName(), SqlDbType.DateTime2).Value = DateTime.Parse(parameter.paramContent.ToString());
																break;
														default:
																cmd.Parameters.Add(parameter.getParamName(), SqlDbType.VarChar).Value = parameter.paramContent;
																break;
												}

										}

										cmd.ExecuteNonQuery();
								}
								this.connection.Close();

						}
						catch (SqlException exSql)
						{
								Console.WriteLine(exSql.Message);
								//throw;
						}
						catch (Exception ex)
						{
								Console.WriteLine(ex.Message);
						}
				}


				public void closeConnection()
				{
						try
						{
								this.connection.Close();
						}
						catch (Exception ex)
						{
								Console.WriteLine(ex.Message);
								throw;
						}
				}
		}
}
