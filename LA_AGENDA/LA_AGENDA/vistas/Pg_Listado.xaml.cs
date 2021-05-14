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
    public partial class Pg_Listado : ContentPage
    {
        public static IList<Reuniones> Reunion_list { get; private set; }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetReunionesAsync();
        }
        public Pg_Listado()
        {
            InitializeComponent();
            //((String Lista = Start.Reunion_list.ToString();
        }
        
    }
}