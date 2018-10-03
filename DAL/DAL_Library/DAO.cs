using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DAL_Library
{
    public class DAO
    {
        private static readonly string DEFAULT_CONNECTION = "defaultConnection";
        private static readonly string CREDENTIAL_MANAGER_KEYS = "CredentialManagerKeys";
        private static readonly string CONNECTION_STRING_USER = "User ID";
        private static readonly string CONNECTION_STRING_PASSWORD = "password";
        private string connectionName;
        private Database database;

        public string ConnectionName
        {
            get
            {
                return this.connectionName;
            }
            set
            {
                this.connectionName = value;
                this.database = null;
            }
        }

        protected string AbstractDAO()
        {
            return "Here we will place internal method for runtime execution processes";
        }

        protected virtual DataSet Return (string spName)
        {
            DataSet result = null;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;

            try
            {
                Database database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbTransaction = this.GetTransaction();
                if(dbTransaction != null)
                {
                    result = database.ExecuteDataSet(dbCommand, dbTransaction);
                }
                else
                {
                    result = database.ExecuteDataSet(dbCommand);
                }
            }
            catch(Exception message)
            {
                Console.WriteLine(message);//Here I will be calling the loger method.
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if (dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }

            return result;
        }

        protected virtual DataSet Return(string spName, int commandTimeout)
        {
            DataSet result = null;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger artifact will be here

            try
            {
                Database database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbCommand.CommandTimeout = commandTimeout;
                dbTransaction = this.GetTransaction();
                if(dbTransaction != null)
                {
                    result = database.ExecuteDataSet(dbCommand, dbTransaction);
                }
                else
                {
                    result = database.ExecuteDataSet(dbCommand);
                }
            }
            catch (Exception message)
            {
                //Logger artifact
                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual DataSet Return(string spName, IList<IDbDataParameter> dbDataParameters)
        {
            DataSet result = null;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger component
            Database database = this.GetDatabase();

            try
            {
                if(dbDataParameters.Count > 0)
                {
                    dbCommand = database.GetStoredProcCommand(spName);
                    foreach(IDbDataParameter current in dbDataParameters)
                    {
                        this.CheckDbNull(current);
                        dbCommand.Parameters.Add(current);
                    }
                }
                else
                {
                    dbCommand = database.GetStoredProcCommand(spName);
                }
                dbTransaction = this.GetTransaction();
                if(dbTransaction != null)
                {
                    result = database.ExecuteDataSet(dbCommand, dbTransaction);
                }
                else
                {
                    result = database.ExecuteDataSet(dbCommand);
                }
            }
            catch (Exception message)
            {
                //Logger artifact will be implemented here
                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    if (dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual DataSet Return(string spName, IList<IDataParameter> dbParameters, int commandTimeout)
        {
            DataSet result = null;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger component will be executed here...
            try
            {
                Database database = this.GetDatabase();
                if(dbParameters.Count > 0)
                {
                    dbCommand = database.GetStoredProcCommand(spName);
                    foreach(IDbDataParameter current in dbParameters)
                    {
                        this.CheckDbNull(current);
                        dbCommand.Parameters.Add(current);
                    }
                }
                else
                {
                    dbCommand = database.GetStoredProcCommand(spName);
                }
                dbCommand.CommandTimeout = commandTimeout;
                dbTransaction = this.GetTransaction();
                if(dbTransaction != null)
                {
                    result = database.ExecuteDataSet(dbCommand, dbTransaction);
                }
                else
                {
                    result = database.ExecuteDataSet(dbCommand);
                }
            }
            catch (Exception message)
            {
                //Here we will execute the logger component
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual IDataReader ReturnReader(string spName)
        {
            IDataReader result = null;
            //Logger component will be here.
            
            try
            {
                Database database = this.GetDatabase();
                DbCommand storedProcCommand = database.GetStoredProcCommand(spName);
                DbTransaction transaction = this.GetTransaction();
                if(transaction != null)
                {
                    result = database.ExecuteReader(storedProcCommand, transaction);
                }
                else
                {
                    result = database.ExecuteReader(storedProcCommand);
                }
            }
            catch (Exception message)
            {
                //logger component will be here
                throw;
            }
            return result;
        }

        protected virtual IDataReader ReturnReader(string spName, int commandTimeout)
        {
            IDataReader result = null;
            //Logger component will be here

            try
            {
                Database database = this.GetDatabase();
                DbCommand storedProcCommand = database.GetStoredProcCommand(spName);
                storedProcCommand.CommandTimeout = commandTimeout;
                DbTransaction transaction = this.GetTransaction();
                if(transaction != null)
                {
                    result = database.ExecuteReader(storedProcCommand, transaction);
                }
                else
                {
                    result = database.ExecuteReader(storedProcCommand);
                }
            }
            catch(Exception message)
            {
                //Logger component will be here
                throw;
            }
            return result;
        }

        protected virtual IDataReader ReturnReader(string spName, IList<IDbDataParameter> dbParameters)
        {
            DbCommand dbCommand = null;
            IDataReader result = null;
            DbTransaction dbTransaction = null;
            //Logger component will be here
            try
            {
                Database database = this.GetDatabase();
                dbTransaction = this.GetTransaction();
                if(dbParameters.Count > 0)
                {
                    dbCommand = database.GetSqlStringCommand(spName);
                    foreach (IDbDataParameter current in dbParameters)
                    {
                        this.CheckDbNull(current);
                        dbCommand.Parameters.Add(current);
                    }
                }
                else
                {
                    dbCommand = database.GetStoredProcCommand(spName);
                }
                if(dbTransaction != null)
                {
                    result = database.ExecuteReader(dbCommand, dbTransaction);
                }
                else
                {
                    result = database.ExecuteReader(dbCommand);
                }
            }
            catch(Exception message)
            {
                //Logger component will be here
                throw;
            }
            return result;
        }

        protected virtual IDataReader ReturnReader(string spName, IList<IDbDataParameter> dbParameters, int commandTimeout)
        {
            DbCommand dbCommand = null;
            IDataReader result = null;
            DbTransaction dbTransaction = null;
            //Logger component will be here.
            try
            {
                Database database = this.GetDatabase();
                dbTransaction = this.GetTransaction();
                if(dbParameters.Count > 0)
                {
                    dbCommand = database.GetStoredProcCommand(spName);
                    foreach(IDbDataParameter current in dbParameters)
                    {
                        this.CheckDbNull(current);
                        dbCommand.Parameters.Add(current);
                    }
                }
                else
                {
                    dbCommand = database.GetStoredProcCommand(spName);
                }
                dbCommand.CommandTimeout = commandTimeout;
                if(dbTransaction != null)
                {
                    result = database.ExecuteReader(dbCommand, dbTransaction);
                }
                else
                {
                    result = database.ExecuteReader(dbCommand);
                }
            }
            catch(Exception message)
            {
                //Logger component will be here
                throw;
            }
            return result;
        }

        protected virtual long Create(string spName)
        {
            long result = 0L;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger component will be here
            
            try
            {
                Database database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbTransaction = this.GetTransaction();
                if(dbTransaction != null)
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand));
                }
            }
            catch(Exception message)
            {
                //Logger component
                throw;
            }
            finally
            {
                if(dbCommand != null)
                {
                    dbCommand.Connection.Close();
                    dbCommand.Dispose();
                }
            }

            return result;

        }

        protected virtual long Create(string spName, int commandTimeout)
        {
            long result = 0L;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger component will be called here

            try
            {
                Database database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbCommand.CommandTimeout = commandTimeout;
                dbTransaction = this.GetTransaction();
                if(dbTransaction != null)
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand));
                }
            }
            catch(Exception message)
            {
                //Logger component will be called here
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual long Create(string spName, IList<IDbDataParameter> dbParameters)
        {
            long result = 0L;
            Database database = null;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger artifact will be called here
            try
            {
                database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbTransaction = this.GetTransaction();
                foreach(IDbDataParameter current in dbParameters)
                {
                    this.CheckDbNull(current);
                    dbCommand.Parameters.Add(current);
                }
                if(dbTransaction != null)
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand));
                }
            }
            catch(Exception message)
            {
                //Logger component will be called here
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual long Create(string spName, IList<IDbDataParameter> dbParameters, int commandTimeout)
        {
            long result = 0L;
            Database database = null;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger artifact will be executed here

            try
            {
                database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbCommand.CommandTimeout = commandTimeout;
                dbTransaction = this.GetTransaction();
                foreach(IDbDataParameter current in dbParameters)
                {
                    this.CheckDbNull(current);
                    dbCommand.Parameters.Add(current);
                }
                if(dbTransaction != null)
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception message)
            {
                //Logger component will be here
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }

            return result;
        }

        protected virtual long update(string spName)
        {
            long result = 0L;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger component will be here

            try
            {
                Database database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbTransaction = this.GetTransaction();
                if(dbTransaction != null)
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand));
                }
            }
            catch(Exception message)
            {
                //Logger component will be here
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual long Update(string spName, int commandTimeout)
        {
            long result = 0L;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger component will be here

            try
            {
                Database database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbCommand.CommandTimeout = commandTimeout;
                dbTransaction = this.GetTransaction();
                if(dbTransaction != null)
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand));
                }
            }
            catch(Exception message)
            {
                //Logger component will be here
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual long Update(string spName, IList<IDbDataParameter> dbParameters)
        {
            long result = 0L;
            Database database = null;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger component will be here

            try
            {
                database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbTransaction = this.GetTransaction();
                foreach(IDbDataParameter current in dbParameters)
                {
                    this.CheckDbNull(current);
                    dbCommand.Parameters.Add(current);
                }
                if(dbTransaction != null)
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand));
                }
            }
            catch(Exception message)
            {
                //Logger component will be here
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual long Update(string spName, IList<IDbDataParameter> dbParameters, int commandTimeout)
        {
            long result = 0L;
            Database database = null;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger component will be here
            try
            {
                database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbCommand.CommandTimeout = commandTimeout;
                dbTransaction = this.GetTransaction();
                foreach(IDbDataParameter current in dbParameters)
                {
                    this.CheckDbNull(current);
                    dbCommand.Parameters.Add(current);
                }
                if(dbTransaction != null)
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand));
                }
            }
            catch(Exception message)
            {
                //Internal logger component will be executed here
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual long Delete(string spName)
        {
            long result = 0L;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger component will be here

            try
            {
                Database database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbTransaction = this.GetTransaction();
                if(dbTransaction != null)
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand));
                }
            }
            catch
            {
                //Logger component will be here
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual long Delete(string spName, int commandTimeout)
        {
            long result = 0L;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger component will be here
            try
            {
                Database database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbCommand.CommandTimeout = commandTimeout;
                dbTransaction = this.GetTransaction();
                if(dbTransaction != null)
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand));
                }
            }
            catch(Exception message)
            {
                //Logger component
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual long Delete(string spName, IList<IDbDataParameter> dbParameters)
        {
            long result = 0L;
            Database database = null;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger component will be here

            try
            {
                database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbTransaction = this.GetTransaction();
                foreach(IDbDataParameter current in dbParameters)
                {
                    this.CheckDbNull(current);
                    dbCommand.Parameters.Add(current);
                }
                if(dbTransaction != null)
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand));
                }
            }
            catch(Exception message)
            {
                //Logger component will be here
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual long Delete(string spName, IList<IDataParameter> dbParameters, int commandTimeout)
        {
            long result = 0L;
            Database database = null;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            //Logger component will be here
            try
            {
                database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbCommand.CommandTimeout = commandTimeout;
                dbTransaction = this.GetTransaction();
                foreach(IDbDataParameter current in dbParameters)
                {
                    this.CheckDbNull(current);
                    dbCommand.Parameters.Add(current);
                }
                if(dbTransaction != null)
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = Convert.ToInt64(database.ExecuteScalar(dbCommand));
                }
            }
            catch(Exception message)
            {
                //Internal logger component
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual T ReturnValue<T>(string spName)
        {
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            T result = default(T);
            //Internal component logger will be here
            try
            {
                Database database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbTransaction = this.GetTransaction();
                if(dbTransaction != null)
                {
                    result = (T)((object)database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = (T)((object)database.ExecuteScalar(dbCommand));
                }
            }
            catch(Exception message)
            {
                //internal logger component will be here
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual T ReturnValue<T>(string spName, int commandTimeout)
        {            
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            T result = default(T);
            //Internal component logger will be here
            try
            {
                Database database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbCommand.CommandTimeout = commandTimeout;
                dbTransaction = this.GetTransaction();
                if(dbTransaction != null)
                {
                    result = (T)((object)database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = (T)((object)database.ExecuteScalar(dbCommand, dbTransaction));
                }
            }
            catch(Exception message)
            {
                //Internal logger component will be here
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual T ReturnValue<T>(string spName, IList<IDbDataParameter> dbParameters)
        {
            Database database = null;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            T result = default(T);
            //Internal logger component will be here
            try
            {
                database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbTransaction = this.GetTransaction();
                foreach(IDbDataParameter current in dbParameters)
                {
                    this.CheckDbNull(current);
                    dbCommand.Parameters.Add(current);
                }
                if(dbTransaction != null)
                {
                    result = (T)((object)database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = (T)((object)database.ExecuteScalar(dbCommand));
                }
            }
            catch(Exception  message)
            {
                //Internal logger component will be here
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbTransaction != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        protected virtual T ReturnValue<T>(string spName, IList<IDbDataParameter> dbParameters, int commandTimeout)
        {
            Database database = null;
            DbCommand dbCommand = null;
            DbTransaction dbTransaction = null;
            T result = default(T);
            //Internalt logger component will be here
            try
            {
                database = this.GetDatabase();
                dbCommand = database.GetStoredProcCommand(spName);
                dbCommand.CommandTimeout = commandTimeout;
                dbTransaction = this.GetTransaction();
                foreach(IDbDataParameter current in dbParameters)
                {
                    this.CheckDbNull(current);
                    dbCommand.Parameters.Add(current);
                }
                if(dbTransaction != null)
                {
                    result = (T)((object)database.ExecuteScalar(dbCommand, dbTransaction));
                }
                else
                {
                    result = (T)((object)database.ExecuteScalar(dbCommand));
                }
            }
            catch(Exception message)
            {
                //Internal component error
                throw;
            }
            finally
            {
                if(dbTransaction == null)
                {
                    if(dbCommand != null)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }

        public void SetContextValue(string key, object value)
        {
            //Board.SetValue(key, value);
        }

        public object GetContextValue(string key)
        {
            //return Board.GetValue(key);
            return new object();
        }

        public void ClearContextValue(string key)
        {
            //Board.ClearValue(key);
        }

        private Database GetDatabase()
        {
            Database result = null;
            if(this.database != null)
            {
                result = this.database;
            }
            else
            {
                try
                {
                    this.database = this.GetDatabase();// We need to add a diferent method here
                    if(this.database == null)
                    {
                       //throw new DataBaseException("");// Pending to define logger method
                    }
                    result = this.database;
                }
                catch
                {

                }
            }

            return result;
        }

        private DbTransaction GetTransaction()
        {
            //Logger component
            DbTransaction dbTransaction = null;

            try
            {
                IDbTransaction currentTransaction = TransactionManager.CurrentTransaction;
                if(currentTransaction != null)
                {
                    IAdoNetTransaction adoNetTransaction = currentTransaction as IAdoNetTransaction;
                    if (adoNetTransaction != null)
                    {
                        IsolationLevel isolationLevel = adoNetTransaction.GetIsolationLevel();
                        DbConnection dbConnection = this.GetDatabase().CreateConnection();
                        dbConnection.Open();
                        dbTransaction = dbConnection.BeginTransaction(isolationLevel);
                        adoNetTransaction.SetTransaction(this.connectionName, dbTransaction);
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return dbTransaction;
        }

        private IsolationLevel GetIsolationLevel()
        {
            return new IsolationLevel();
        }

        protected virtual void CheckDbNull(IDbDataParameter dbParameters)
        {
            if(dbParameters != null && dbParameters.Value == null)
            {
                dbParameters.Value = DBNull.Value;
            }
        }
    }
}
