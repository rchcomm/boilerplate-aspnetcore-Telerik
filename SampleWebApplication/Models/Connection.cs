using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApplication.Models
{
    public class MyConnection
    {
        public string ConnectionString { get; set; }
        public SqliteConnection Instance { get; set; }

        public MyConnection(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.Instance = new SqliteConnection(connectionString);
        }
    }
}
