using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Dal.Interfaces
{
    public interface IDatabase
    {

        OracleParameter MakeOutParam(string ParamName, OracleDbType DbType, int Size);

        OracleParameter MakeInParam(string ParamName, OracleDbType DbType, int Size, object Value);

        int RunProc(string procName, OracleParameter[] prams);
        String RunProcScalar(string procName, OracleParameter[] prams);

        void Close();

        void RunProcWithCon(string procName, OracleParameter[] prams, OracleConnection connection, out OracleDataReader dataReader);
    }
}
