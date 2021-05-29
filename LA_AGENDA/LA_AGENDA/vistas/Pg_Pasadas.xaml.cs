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
    public partial class Pg_Pasadas : ContentPage
    {

        DateTime fechaActual;
        public string selectedItem;
        public bool isSelected = false; // para segurar que algo fue seleccionado
        public Reuniones objReunion = new Reuniones(); //para intentar almacenar el objeto y poder borrar

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            fechaActual = DateTime.Now;
            collectionView.ItemsSource = await App.Database.GetPastReunionesAsync(fechaActual);
        }

        public Pg_Pasadas()
        {
            InitializeComponent();
        }


        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isSelected = true;
            string previous = (e.PreviousSelection.FirstOrDefault() as Reuniones)?.nombre;
            selectedItem = (e.CurrentSelection.FirstOrDefault() as Reuniones)?.nombre;
            objReunion = (e.CurrentSelection.FirstOrDefault() as Reuniones);
        }

        public async void EliminarPressed(object sender, EventArgs e)
        {
            if (isSelected)
            {
                if (selectedItem.Length != 0)
                {
                    await App.Database.DeleteReunionAsync(objReunion);
                    OnAppearing();
                }
            }
            else
            {
                await DisplayAlert("Nada Seleccionado!!", "Realice su selección...", "Entendido");
            }
        }

        public async void ModificarPressed(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new Pg_Modificar(objReunion));
        }




    }
}