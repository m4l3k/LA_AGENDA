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
    public partial class Pg_Revisar : ContentPage
    {
        public Pg_Revisar()
        {
            InitializeComponent();

            btnPendientes.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new Pg_Pendientes());
            };

            btnPasadas.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new Pg_Pasadas());
            };

            btnTodas.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new Pg_Listado());
            };

        }
    }
}