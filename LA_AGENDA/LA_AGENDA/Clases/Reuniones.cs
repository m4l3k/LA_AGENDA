using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace LA_AGENDA.Clases
{
    public class Reuniones
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string nombre { get; set; }
        public string fecha { get; set; }
        public DateTime fecha1 { get; set; }
        public string lugar { get; set; }
        public string comentarios { get; set; }

        
        public override string ToString()
        {
            return nombre +" " + lugar + "" + fecha;
        }
    }
}
