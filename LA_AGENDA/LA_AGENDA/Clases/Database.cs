﻿using System;
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

        //----------Borrar TODO
        public Task<List<Reuniones>> DeleteReunionesAsync()
        {

            _database.DropTableAsync<Reuniones>().Wait();
            _database.CreateTableAsync<Reuniones>().Wait();
            return _database.Table<Reuniones>().ToListAsync();
        }


        //------------Modificar?

        /* public  Task<int> ModifyReunionAsync(Reuniones reunion)
         {
             //String query;
           /*  query = ("UPDATE Reuniones SET nombre = "+reunion.nombre+"" +
                 ", lugar = "+reunion.lugar+"" +
                 ", fecha = "+reunion.fecha+"" +
                 ", comentarios = "+reunion.comentarios+"" +
                 "  WHERE ID = "+reunion.ID+" ");*/
        //     "WHERE ID = ?", reunion.nombre, reunion.lugar, reunion.fecha, reunion.comentarios, reunion.ID);
        // _database.ExecuteAsync("UPDATE Reuniones SET nombre = @nombre, lugar = @lugar, fecha= @fecha, comentarios = @comentarios " +
        //     "WHERE ID = @ID", new {reunion.nombre, reunion.lugar, reunion.fecha, reunion.comentarios, reunion.ID });
        //return GetReunionesAsync();
        //return _database.UpdateAsync(reunion);
        //return _database.ExecuteAsync(query);
        // }


        public Task<int>  ModifyReunionAsync(Reuniones reunion)
        {
             _database.ExecuteAsync("UPDATE Reuniones SET nombre = @nombre, lugar = @lugar, fecha= @fecha, comentarios = @comentarios " +
                "WHERE ID = @ID", new { reunion.nombre, reunion.lugar, reunion.fecha, reunion.comentarios, reunion.ID });
            
            return _database.UpdateAsync(reunion);
            
        }

        




    }
}
