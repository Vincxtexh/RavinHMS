using HMS.Dal.Interfaces;
using HMS.Entity;
using HMS.Entity.Request;
using HMS.Entity.Response;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Dal.Repository
{
    public class UserDetailsDL : IUserDetailsDL
    {
        public IConfiguration _configuration { get; }

        private readonly IDatabase _database;
        public UserDetailsDL(IConfiguration configuration, IDatabase database)
        {
            _configuration = configuration;
            _database = database;
        }


        public async Task<List<UserResponse>> GetUserDetails()
        {

            return await GetAllUserDetailsAsync();
        }
        public async Task<UserResponse> GetUserDetailsByIdAsync(int Id)
        {
            UserResponse result = new UserResponse();
            result = await GetUserDetailsAsync(Id);
            return result;
        }

        public async Task<bool> DeleteUserDetailsByIdAsync(int Id)
        {
            bool result = await DeleteUserDetailsAsync(Id);
            return result;
        }


        public async Task<List<UserResponse>> GetAllUserDetailsAsync()
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            OracleConnection oracleConnection = null;
            List<UserResponse> userDetail = new List<UserResponse>();
            try
            {
                OracleDataReader reader;
                using (oracleConnection = new OracleConnection(connectionString))
                {
                    OracleParameter[] param =
               {
                _database.MakeOutParam("CV_1",OracleDbType.RefCursor,50),
            
              };
                    oracleConnection.Open();

                    _database.RunProcWithCon("RW_FILL_DROPDOWN", param, oracleConnection, out reader);

                    while (await reader.ReadAsync())
                    {
                        UserResponse _user = new UserResponse();
                        _user.UserId = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt32(reader.GetString(0));
                        _user.UserName = await reader.IsDBNullAsync(1) ? null : reader.GetString(1);
                        _user.UserAge = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt32(reader.GetString(2));


                        userDetail.Add(_user);
                    }
                    await reader.DisposeAsync();
                    return userDetail;
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

        public async Task<CreateUserResponse> CreateCRMSUser(CreateUserRequest createUserRequest)
        {

            return await CreateCRMSUserAsync(createUserRequest); 
        }

        public async Task<bool> DeleteUserDetailsAsync(int CustomerID)
        {
            string connectionString = "User Id=SYSTEM;Password=admin;Data Source=DESKTOP-6PD0NT9:1521/XE:SYSTEM";
            OracleConnection oracleConnection = null;
            UserResponse userDetail = new UserResponse();
            try
            {
                using (oracleConnection = new OracleConnection(connectionString))
                {
                    oracleConnection.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = oracleConnection;
                        cmd.CommandText = "Delete from userdetails where userid = " + CustomerID;//"PKG_PAYU_MANAGER.PROC_PAYU_IS_CUSTOMER_EXISTS";
                                                                                                 // cmd.Parameters.Add("i_CUSTOMERID", OracleDbType.Int32, CustomerID, ParameterDirection.Input);

                        DbDataReader reader = await cmd.ExecuteReaderAsync();

                        await reader.DisposeAsync();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (oracleConnection != null)
                { oracleConnection.Close(); }
            }

        }

        public async Task<UserResponse> GetUserDetailsAsync(int CustomerID)
        {
            string connectionString = "User Id=SYSTEM;Password=admin;Data Source=DESKTOP-6PD0NT9:1521/XE:SYSTEM";
            OracleConnection oracleConnection = null;
            UserResponse userDetail = new UserResponse();
            try
            {
                using (oracleConnection = new OracleConnection(connectionString))
                {
                    oracleConnection.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = oracleConnection;
                        cmd.CommandText = "select * from userdetails where userid = " + CustomerID;//"PKG_PAYU_MANAGER.PROC_PAYU_IS_CUSTOMER_EXISTS";
                                                                                                   // cmd.Parameters.Add("i_CUSTOMERID", OracleDbType.Int32, CustomerID, ParameterDirection.Input);

                        DbDataReader reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            UserResponse _user = new UserResponse();
                            userDetail.UserId = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt32(reader.GetString(0));
                            userDetail.UserName = await reader.IsDBNullAsync(1) ? null : reader.GetString(1);
                            userDetail.UserAge = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt32(reader.GetString(2));


                            //userList.Add(_user);
                        }
                        await reader.DisposeAsync();
                        return userDetail;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (oracleConnection != null)
                { oracleConnection.Close(); }
            }

        }

        public async Task<CreateUserResponse> CreateCRMSUserAsync(CreateUserRequest createUserRequest)
        {
            string connectionString = "User Id=SYSTEM;Password=admin;Data Source=DESKTOP-6PD0NT9:1521/XE:SYSTEM";
            OracleConnection oracleConnection = null;
            CreateUserResponse userDetail = new CreateUserResponse();
            try
            {
                using (oracleConnection = new OracleConnection(connectionString))
                {
                    oracleConnection.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = oracleConnection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "CRMS_SP_INSERT_USERDETAILS";
                        cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2, createUserRequest.UserName, ParameterDirection.Input);
                        cmd.Parameters.Add("USERAGE", OracleDbType.Varchar2, createUserRequest.UserAge, ParameterDirection.Input);
                        cmd.Parameters.Add("USERID", OracleDbType.Int32, ParameterDirection.Output);

                        DbDataReader reader = await cmd.ExecuteReaderAsync();

                        userDetail.UserId = Convert.ToInt32(((Oracle.ManagedDataAccess.Types.OracleDecimal)cmd.Parameters[6].Value).Value);

                        await reader.DisposeAsync();
                        return userDetail;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (oracleConnection != null)
                { oracleConnection.Close(); }
            }

        }
    }
}
