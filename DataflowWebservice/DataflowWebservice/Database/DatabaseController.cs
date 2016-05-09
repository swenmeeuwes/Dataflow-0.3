using DataflowWebservice.Models.Attributes;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataflowWebservice.Database
{
    public class DatabaseController
    {
        public NpgsqlConnection connection { get; private set; }
        public DatabaseController()
        {
            Connect();
            OpenConnection();
        }

        public void CloseConnection()
        {
            connection.Close();
        }
        public T Find<T>(T missing)
        {
            var tableName = missing.GetType().Name;
            string primaryKey = "";
            object primaryKeyValue = "";

            var properties = missing.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.IsDefined(typeof(PrimaryKey), false))
                {
                    primaryKeyValue = property.GetValue(missing, null);
                    primaryKey = property.Name;
                }
            }

            object found = null;

            using (var cmd = new NpgsqlCommand(string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", tableName, primaryKey, primaryKeyValue), connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        object[] parameters = new object[missing.GetType().GetProperties().Length];
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            parameters[i] = reader[i];
                        }
                        found = (T)Activator.CreateInstance(typeof(T), parameters);
                    }
                }
            }

            if (found == null)
                throw new Exception(string.Format("Could not find {0}.", missing.GetType()));

            return (T)found;
        }

        public T[] FindAll<T>(Type missing)
        {
            var tableName = missing.Name;
            List<T> found = new List<T>();

            using (var cmd = new NpgsqlCommand(string.Format("SELECT * FROM {0}", tableName), connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object[] parameters = new object[missing.GetProperties().Length];
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            parameters[i] = reader[i];
                        }
                        found.Add((T)Activator.CreateInstance(typeof(T), parameters));
                    }
                }
            }
            return found.ToArray();
        }

        public bool Persist(object obj)
        {
            var tableName = obj.GetType().Name;

            var parameterStrings = obj.GetType().GetProperties().Select(x => x.Name);
            var valueStrings = "";
            foreach (var item in parameterStrings)
            {
                valueStrings += string.Format("{0}, ", item);
            }
            valueStrings = valueStrings.Substring(0, valueStrings.Length - 2);

            var parameterValues = obj.GetType().GetProperties().Select(x => x.GetValue(obj, null));
            var values = "";
            foreach (var item in parameterValues)
            {
                values += string.Format("'{0}', ", item);
            }
            values = values.Substring(0, values.Length - 2);

            using (var cmd = new NpgsqlCommand(string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, valueStrings, values), connection))
            {
                try {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        void Connect()
        {
            connection = new NpgsqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString);
        }
        void OpenConnection()
        {
            connection.Open();
        }
    }
}