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
        public Pg_Listado()
        {
            InitializeComponent();
            String Lista = Start.Reunion_list.ToString();
        }
        
    }
}