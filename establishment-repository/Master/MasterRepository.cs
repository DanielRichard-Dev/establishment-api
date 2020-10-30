using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace establishment_repository.Master
{
    public class MasterRepository
    {
        private readonly IConfiguration _configuration;

        public MasterRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected T ExecuteGetObj<T>(string query, string connectionString, object obj = null)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    T _objReturn;
                    if (obj != null)
                        _objReturn = conn.Query<T>(query, obj).FirstOrDefault();
                    else
                        _objReturn = conn.Query<T>(query).FirstOrDefault();
                    return _objReturn;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void ExecuteQuery(string query, string connectionString, object obj = null)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    if (obj != null)
                        conn.Execute(query, obj);
                    else
                        conn.Execute(query);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
