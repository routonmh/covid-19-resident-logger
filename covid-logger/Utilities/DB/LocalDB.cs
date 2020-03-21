using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ResidentLog.Utilities.DB
{

    public class LocalDB : IDisposable
    {
        public MySqlConnection Connection { get; set; }

        public LocalDB()
        {
            Connection = new MySqlConnection(Program.DBConnectionString);
        }

        public Task OpenConnectionAsync()
        {
            return Connection.OpenAsync();
        }

        public MySqlCommand CreateCommand()
        {
            return Connection.CreateCommand();
        }

        public void Dispose()
        {
            Connection.CloseAsync();
        }
    }
}