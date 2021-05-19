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

        //--------METODOS Ins,Del,Sel 

        //----------Mostrar Todo
        public Task<List<Reuniones>> GetReunionesAsync()
        {
            return _database.Table<Reuniones>().ToListAsync();
        }

       

        //----------Guardar
        public Task<int> SaveReunionAsync(Reuniones reunion)
        {
            if (reunion.ID != 0)
            {
                return _database.UpdateAsync(reunion);
            }
            else
            {
                return _database.InsertAsync(reunion);
            }            
        }

        //-----------Borrar
        public Task<int> DeleteReunionAsync(Reuniones reunion)
        {
            return _database.DeleteAsync(reunion);
        }
    }
}
