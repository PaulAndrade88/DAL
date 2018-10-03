using System.Data;
using System.Data.Common;

namespace DAL_Library
{
    internal interface IAdoNetTransaction
    {
        IsolationLevel GetIsolationLevel();
        void SetTransaction(string connectionName, DbTransaction dbTransaction);
    }
}