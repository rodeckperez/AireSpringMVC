using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.PortableExecutable;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace AireSpringMVC.data
{
    public class Conection<T>
    {
        //private properties 
        private readonly IConfiguration _configuration;

        private string connectionChain = string.Empty;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader dataReader;

        //constructor

        public Conection(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.setConnection();
        }


        #region PrivateMethods
        private void setConnection()
        {
            this.connectionChain = _configuration.GetConnectionString("DefaultConnection");
        }

        private bool openConnection()
        {
            try
            {
                this.connection = new SqlConnection(connectionChain);
                this.connection.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void closeConnection()
        {
            this.connection.Close();
        }

        #endregion


        #region Public methods
        public void setCommand(string spName, List<SqlParameter> parameters)
        {
            this.command = new SqlCommand();
            this.command.CommandText = spName;
            if (parameters.Count > 0)
            {
                this.command.Parameters.AddRange(parameters.ToArray());
            }
        }

        public Task<List<T>> getReader()
        {
            List<T> newList = new List<T>();
            T obj = default(T);
            if (this.openConnection())
            {
                try
                {
                    this.command.Connection = this.connection;
                    using (var rdr = this.command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            obj = Activator.CreateInstance<T>();
                            foreach (PropertyInfo prop in obj.GetType().GetProperties())
                            {
                                if (!object.Equals(rdr[prop.Name], DBNull.Value))
                                {
                                    prop.SetValue(obj, Convert.ChangeType(rdr[prop.Name], prop.PropertyType), null);
                                }
                            }
                            newList.Add(obj);
                        }
                    }
                }
                catch (Exception e)
                { }
            }
            this.closeConnection();

            return Task.FromResult(newList);
        }

        public async Task executeNonQuery()
        {
            if (this.openConnection())
            {
                try
                {
                    this.command.Connection = this.connection;
                    this.command.CommandType = CommandType.StoredProcedure;
                    this.command.ExecuteNonQuery();
                }
                catch (Exception)
                { }
            }
            this.closeConnection();

        }

        #endregion

    }
}
