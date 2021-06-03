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
        public String dbbQuery;
        /*Task<TableMapping> mapaDeTabla;*/

        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Reuniones>().Wait();
        }

        //--------METODOS Ins,Del,Update 
        //----------Mostrar Todo
        public Task<List<Reuniones>> GetReunionesAsync()
        {

            return _database.Table<Reuniones>().OrderBy(t => t.fecha).ToListAsync();
            //return _database.Table<Reuniones>().OrderBy(t => t.nombre).ToListAsync();  CONSULTA OK
            //return _database.Table<Reuniones>().Where(t => t.nombre == "4564Mxx").OrderBy(t => t.nombre).ToListAsync();
        }

        //----------Mostrar solo las reuniones pasadas
        public Task<List<Reuniones>> GetPastReunionesAsync(DateTime fechaActual)
        {
            return _database.Table<Reuniones>().Where(t => t.fecha1<fechaActual).OrderBy(t => t.fecha).ToListAsync();
            
        }

        //-------Mostrar solo reuniones pendientes
        public Task<List<Reuniones>> GetFutureReunionesAsync(DateTime fechaActual)
        {
            return _database.Table<Reuniones>().Where(t => t.fecha1 > fechaActual).OrderBy(t => t.fecha).ToListAsync();

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

        //----------Borrar TODO
        public Task<List<Reuniones>> DeleteReunionesAsync()
        {
            _database.DropTableAsync<Reuniones>().Wait();
            _database.CreateTableAsync<Reuniones>().Wait();
            return _database.Table<Reuniones>().ToListAsync();
        }  
    }
}
