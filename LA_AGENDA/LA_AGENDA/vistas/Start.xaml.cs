using LA_AGENDA.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LA_AGENDA.vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Start : ContentPage
    {
        public static IList<Reuniones> Reunion_list { get; private set; }
        public Start()
        {
            Reunion_list = new List<Reuniones>(); // lista para reuniones
            InitializeComponent();

            //BOTONES DE INTERFAZ
            btnNueva.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new Pg_Agregar());
            };

            btnRevisar.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new Pg_Listado());
            };

            btnSalir.Clicked += (sender, e) =>
            {
                return;
            };
            //BOTONES DE INTERFAZ

            BindingContext = this;  //enlace
        }


        // nuevo bloque de métodos para agregar los datos que se ingresen desde Pg_Agregar hacia este listado de objetos
        public static void addReunion(String name, String place, String date, String comment)
        {
            Reunion_list.Add(new Reuniones
            {

                nombre = name,
                lugar = place,
                fecha = date,
                comentarios = comment
                
            });           

        }
        //comment  2
        
        
        

    }
}