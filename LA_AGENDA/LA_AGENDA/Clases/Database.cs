using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using LA_AGENDA.Clases;

namespace LA_AGENDA.Clases
{
    public class Database
    {
        
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Reuniones>().Wait();
        }

        public Task<List<Reuniones>> GetReunionesAsync()
        {
            return _database.Table<Reuniones>().ToListAsync();
        }

        public Task<int> SaveReunionAsync(Reuniones reunion)
        {
            return _database.InsertAsync(reunion);
        }
    }
}
