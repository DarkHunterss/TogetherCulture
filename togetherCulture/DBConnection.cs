using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace togetherCulture
{
    internal class DBConnection
    {
        private static DBConnection _instance;
        private string _connectionString;

        private DBConnection()
        {
            _connectionString = Globals.ConnectionString;
        }

        public static DBConnection getConnectionInstance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        // Executes a command that does not return any data (e.g., INSERT, UPDATE, DELETE)
        public int executeNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Executes a command and returns a single value (e.g., SELECT COUNT(*))
        public object executeScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }

        // Executes a command and returns a DataTable (e.g., SELECT queries)
        public DataTable executeQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable resultTable = new DataTable();
                        adapter.Fill(resultTable);
                        return resultTable;
                    }
                }
            }
        }

        // Method to test the database connection
        public bool testConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Database connection successful!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
                return false;
            }
        }
    }
}
