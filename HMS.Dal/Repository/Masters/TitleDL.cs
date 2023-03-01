using HMS.Dal.Interfaces;
using HMS.Dal.Interfaces.Masters;
using HMS.Entity;
using HMS.Entity.Masters.titles.Request;
using HMS.Entity.Masters.titles.Response;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Dal.Repository.Masters
    {
    public class TitleDL : ITitleDL
        {
        public IConfiguration _configuration { get; }

        private readonly IDatabase _database;
        public TitleDL(IConfiguration configuration, IDatabase database)
            {
            _configuration = configuration;
            _database = database;
            }

        public async Task<List<TitleResponse>> GetTitleDetails()
            {

            return await GetAllTitleDetailsAsync();
            }

        public async Task<List<TitleResponse>> GetTitleDetailsByID(TitleRequest titleRequest)
            {

            return await GetAllTitleDetailsByIDAsync(titleRequest);
            }

        public async Task<TitleResponse> SaveTitle(TitleSaveRequest titleSaveRequest)
            {

            return await SaveTitleAsync(titleSaveRequest);
            }

        

        public async Task<List<TitleResponse>> GetAllTitleDetailsAsync()
            {
            string connectionString = _configuration.GetConnectionString("ConnectionString1");
           // string CommandType = "GET_DETAILS";
            OracleConnection oracleConnection = null;
            List<TitleResponse> titleResponses = new List<TitleResponse>();
            try
                {
                OracleDataReader reader;
                using (oracleConnection = new OracleConnection(connectionString))
                    {
                    OracleParameter[] param =
               {
                // _database.MakeInParam("P_COMMANDTYPE",OracleDbType.Varchar2,50,"GET_DETAILS"),
                _database.MakeOutParam("CV_1",OracleDbType.RefCursor,50),

              };
                    oracleConnection.Open();

                    _database.RunProcWithCon("MST_TITLE", param, oracleConnection, out reader);

                    while (await reader.ReadAsync())
                        {
                        TitleResponse _title = new TitleResponse();
                        _title.TitleID = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt32(reader.GetString(0));
                        _title.TitleName = await reader.IsDBNullAsync(1) ? null : reader.GetString(1);
                        _title.Gender = await reader.IsDBNullAsync(2) ? null : reader.GetString(2);
                        _title.AgeUnit = await reader.IsDBNullAsync(3) ? null : reader.GetString(3);


                        titleResponses.Add(_title);
                        }
                    await reader.DisposeAsync();
                    return titleResponses;
                    }


                }
            catch (Exception ex)
                {
                throw ex;
                }
            finally
                {
                _database.Close();

                }


            }

        public async Task<List<TitleResponse>> GetAllTitleDetailsByIDAsync(TitleRequest titleRequest)
            {
            string connectionString = _configuration.GetConnectionString("ConnectionString1");
            OracleConnection oracleConnection = null;
            List<TitleResponse> titleResponses = new List<TitleResponse>();
            try
                {
                OracleDataReader reader;
                using (oracleConnection = new OracleConnection(connectionString))
                    {
                    OracleParameter[] param =
               {
                         
                _database.MakeInParam("P_COMMANDTYPE",OracleDbType.Varchar2,50,titleRequest.CommandType.ToUpper()),
                _database.MakeInParam("P_TITLEID",OracleDbType.Int32,10,titleRequest.TitleID),
                //_database.MakeOutParam("P_RESULT",OracleDbType.Varchar2,50),
                _database.MakeOutParam("CV_1",OracleDbType.RefCursor,50),

              };
                    oracleConnection.Open();

                    _database.RunProcWithCon("SP_GET_MSTDETAILS", param, oracleConnection, out reader);

                    while (await reader.ReadAsync())
                        {
                        TitleResponse _title = new TitleResponse();
                        _title.TitleID = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt32(reader.GetString(0));
                        _title.TitleName = await reader.IsDBNullAsync(1) ? null : reader.GetString(1);
                        _title.Gender = await reader.IsDBNullAsync(2) ? null : reader.GetString(2);
                        _title.AgeUnit = await reader.IsDBNullAsync(3) ? null : reader.GetString(3);


                        titleResponses.Add(_title);
                        }
                    await reader.DisposeAsync();
                    return titleResponses;
                    }


                }
            catch (Exception ex)
                {
                throw ex;
                }
            finally
                {
                _database.Close();

                }


            }


        public async Task<TitleResponse> SaveTitleAsync(TitleSaveRequest titleSaveRequest)
            {
            string SP_Name = "SP_INSERT_UPDATE_TITLEDETAILS";
            //  string connectionString = _configuration.GetConnectionString("ConnectionString");
            OracleConnection oracleConnection = null;
            List<TitleResponse> titleResponses = new List<TitleResponse>();
            TitleResponse _titleResponses = new TitleResponse();
            try
                {


                OracleParameter[] param =
           {
                     
                _database.MakeInParam("P_COMMANDTYPE",OracleDbType.Varchar2,50,titleSaveRequest.CommandType.ToUpper()),
                 _database.MakeInParam("P_TITLEID",OracleDbType.Int32,10,titleSaveRequest.TitleID),
                 _database.MakeInParam("P_TITLENAME",OracleDbType.Varchar2,50,titleSaveRequest.TitleName),

                  _database.MakeInParam("P_GENDER",OracleDbType.Varchar2,50,titleSaveRequest.Gender),

                   _database.MakeInParam("P_AGEUNIT",OracleDbType.Varchar2,50,titleSaveRequest.AgeUnit),
                      _database.MakeOutParam("P_RESULT",OracleDbType.Varchar2,50),




            };

                _database.RunProc(SP_Name, param);
                _titleResponses.Result = Convert.ToString((string)param[5].Value.ToString());

                return _titleResponses;




                }
            catch (Exception ex)
                {
                throw ex;
                }
            finally
                {
                _database.Close();

                }


            }

       

        }
    }
