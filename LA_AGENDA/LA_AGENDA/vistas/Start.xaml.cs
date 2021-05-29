using LA_AGENDA.Clases;
using System;
using System.Collections.Generic;


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

            btnRevision.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new Pg_Revisar());
            };

            /*
            btnPendientes.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new Pendientes());
            };
            */

            btnSalir.Clicked += (sender, e) =>
            {

                System.Environment.Exit(0);
            };
            //BOTONES DE INTERFAZ

            BindingContext = this;  //enlace
        }       
    }
}