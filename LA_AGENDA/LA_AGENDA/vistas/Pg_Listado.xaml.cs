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
        //public static IList<Reuniones> Reunion_list { get; private set; }
        public string selectedItem;
        public bool isSelected = false; // para segurar que algo fue seleccionado
        public Reuniones objReunion = new Reuniones(); //para intentar almacenar el objeto y poder borrar


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

        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isSelected = true;
            string previous = (e.PreviousSelection.FirstOrDefault() as Reuniones)?.nombre;
            selectedItem = (e.CurrentSelection.FirstOrDefault() as Reuniones)?.nombre;
            objReunion = (e.CurrentSelection.FirstOrDefault() as Reuniones);
        }

        public  async void EliminarPressed(object sender, EventArgs e)
        {
            if(isSelected)
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








    }//end class
}