using System;
using System.Collections.Generic;
using System.Text;

namespace LA_AGENDA.Clases
{
    public class Reuniones
    {
        public string nombre { get; set; }
        public string fecha { get; set; }
        public string lugar { get; set; }
        public string comentarios { get; set; }

        public bool dateIsSelected = false;


        public override string ToString()
        {
            return nombre;
        }
    }
}
