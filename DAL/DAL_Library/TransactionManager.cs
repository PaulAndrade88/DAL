using System.Data;

namespace DAL_Library
{
    public class TransactionManager
    {
        public static IDbTransaction CurrentTransaction;
    }
}