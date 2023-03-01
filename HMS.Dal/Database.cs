using HMS.Dal.Interfaces;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Dal
{
    public class Database : IDatabase
    {
        private OracleConnection con;
        private readonly IConfiguration _configuration;

        public Database(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string strConnectionString
        {
            get
            {
                return  _configuration.GetConnectionString("ConnectionString");
            }
        }


        public int RunProc(string procName, OracleParameter[] prams)
        {

            int val = -1;
            OracleConnection connection;
            using (connection = new OracleConnection(strConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = CreateCommand(procName, prams, connection);
                    val = cmd.ExecuteNonQuery();
                    return val;
                }
                catch (OracleException ex)
                {
                    ex.ToString();
                    connection.Close();  ////
                    connection.Dispose();
                    return val;
                }
                finally
                {
                    connection.Close();  ////
                    connection.Dispose();
                }
            }
        }


        public String RunProcScalar(string procName, OracleParameter[] prams)
        {
            String Str = "";
            OracleConnection connection;
            using (connection = new OracleConnection(strConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = CreateCommand(procName, prams, connection);
                    Str = cmd.ExecuteScalar().ToString();

                    return Str;
                }
                catch (OracleException ex)
                {
                    ex.ToString();
                    connection.Close();  ////
                    connection.Dispose();
                    return Str;
                }
                finally
                {
                    connection.Close();  ////
                    connection.Dispose();
                }
            }
        }
        public int RunProcScalar(string procName, OracleParameter[] prams, out int i)
        {
            i = 0;
            OracleConnection connection;
            using (connection = new OracleConnection(strConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = CreateCommand(procName, prams, connection);
                    i = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    return i;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    connection.Close();  ////
                    connection.Dispose();
                }
                finally
                {

                    connection.Close();  ////
                    connection.Dispose();
                }
                return i;
            }


        }



        public void RunProcWithCon(string procName, OracleParameter[] prams, OracleConnection connection, out OracleDataReader dataReader)
        {
            dataReader = null;

            try
            {
                OracleCommand cmd = CreateCommand(procName, prams, connection);
                dataReader =  cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (OracleException ex)
            {
                ex.ToString();
                
            }
            finally
            {
                
            }
        }


        private OracleCommand CreateCommand(string procName, OracleParameter[] prams, OracleConnection con)
        {
            // make sure connection is open
            // Open();

            //command = new OracleCommand( sprocName, new OracleConnection( ConfigManager.DALConnectionString ) );
            OracleCommand cmd = new OracleCommand(procName, con);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 10000;
                // add proc parameters
                if (prams != null)
                {
                    foreach (OracleParameter parameter in prams)
                        cmd.Parameters.Add(parameter);
                }
                // return param
                //cmd.Parameters.Add(
                //      new OracleParameter("ReturnValue", OracleDbType.Int32, 4,
                //   ParameterDirection.Output, false, 0, 0,
                //   string.Empty, DataRowVersion.Default, null));
            }
            catch (OracleException ex)
            {
                ex.ToString();

            }
            finally
            {
                //this.Close();
                Dispose();
            }


            return cmd;
        }

        private void Open()
        {
            try
            {
                // open connection
                if (con == null)
                {
                    //OracleConnection.ClearAllPools();
                    con = new OracleConnection(strConnectionString);
                    OracleConnection.ClearPool(con);

                    //con = new OracleConnection(strConnectionString);
                    con.Open();
                }
                else
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                }

                //if (con != null && con.State == ConnectionState.Closed)
                //{
                //    //OracleConnection.ClearAllPools();
                //    //con = new OracleConnection(strConnectionString);
                //    //OracleConnection.ClearPool(con); 

                //    con.Open();
                //}
            }
            catch (OracleException ex)
            {
                ex.ToString();
            }
            finally
            {
                //   this.Close();
                Dispose();
            }
        }

        public void Close()
        {

            if (con != null)
            {
                con.Close();
                OracleConnection.ClearPool(con);
                // OracleConnection.ClearAllPools();
            }

        }


        public void Dispose()
        {
            // make sure connection is closed
            if (con != null)
            {
                con.Dispose();
                con = null;
            }
        }

        public OracleParameter MakeInParam(string ParamName, OracleDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }

        public OracleParameter MakeParam(string ParamName, OracleDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            OracleParameter param;

            if (Size > 0)
                param = new OracleParameter(ParamName, DbType, Size);
            else
                param = new OracleParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;

            return param;
        }


        public OracleParameter MakeOutParam(string ParamName, OracleDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }



    }
}
