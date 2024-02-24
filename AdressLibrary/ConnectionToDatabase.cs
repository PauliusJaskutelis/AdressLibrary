using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdressLibrary
{
    public class ConnectionToDatabase
    {
        private SqlConnection connection;
        private ConnectionState state;

        public ConnectionToDatabase(String connectionString)
        {
            this.connection = new SqlConnection(connectionString);
            connect();
        }

        public ConnectionToDatabase() { }

        public SqlConnection GetSqlConnection() { return connection; }

        public void connect()
        {
            connection.Open();
        }

        public void disconnect()
        {
            connection.Close();
        }

        public bool isConnected()
        {
            state = connection.State;
            return state == ConnectionState.Open;
        }

        public void restartConnection()
        {
            connection.Close();
            connection.Open();
        }
    }
}
