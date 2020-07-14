using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace gRPCService101.DataAccessor
{
    public class BaseDataAccessor
    {
        protected IDbConnection DbConnection { get; set; }

        public BaseDataAccessor(IConfiguration configuration, string connectionStringName)
        {
            var connectionString = configuration?.GetConnectionString(connectionStringName);
            this.DbConnection = new OracleConnection(connectionString);
        }

        protected virtual async Task<IEnumerable<T>> ExecuteCommandQuery<T>(IDbConnection conn,
                                                                       string qryString,
                                                                       CommandType commandType = CommandType.StoredProcedure,
                                                                       List<DbParameter> @params = null) where T : new()
        {
            return await ExecuteQuery<T>(conn, qryString, commandType, @params);
        }

        public virtual async Task<IEnumerable<T>> ExecuteCommandQuery<T>(string qryString,
                                                                        CommandType commandType = CommandType.StoredProcedure,
                                                                        List<DbParameter> @params = null) where T : new()
        {
            IEnumerable<T> qryresults = null;
            try
            {
                qryresults = await ExecuteQuery<T>(this.DbConnection, qryString, commandType, @params);
            }
            catch (Exception e)
            {
                throw e;
            }
            return qryresults;
        }

        protected virtual async Task<IEnumerable<T>> ExecuteQuery<T>(IDbConnection connection, string qryString, CommandType commandType, List<DbParameter> @params) where T : new()
        {
            // check db connection
            // check command qry string
            if (connection == null)
                throw new Exception("connection is null");

            List<T> genericObjects = new List<T>();
            OracleCommand command = new OracleCommand();
            command.Connection = connection as OracleConnection;
            command.CommandText = qryString;
            command.CommandType = commandType;
            @params?.ForEach((param) => command.Parameters.Add(param));

            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var asyncReader = (await command.ExecuteReaderAsync());

                using (asyncReader)
                {
                    while (await asyncReader.ReadAsync())
                    {
                        T genericObject = new T();
                        var databaseColumns = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(ColumnAttribute))).ToList();
                        databaseColumns.ForEach((property) =>
                        {
                            ColumnAttribute attribute = property.GetCustomAttribute<ColumnAttribute>();
                            var value = asyncReader[attribute.Name];
                            var converter = TypeDescriptor.GetConverter(property.PropertyType);
                            var convertedValue = converter.ConvertTo(value, property.PropertyType);
                            property.SetValue(genericObject, convertedValue);
                        });
                        genericObjects.Add(genericObject);
                    }
                }
            }
            return genericObjects;
        }
    }
}
