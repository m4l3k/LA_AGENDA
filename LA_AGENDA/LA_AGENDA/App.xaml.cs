using LA_AGENDA.Clases;
using LA_AGENDA.vistas;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LA_AGENDA
{
    public partial class App : Application
    {
        
       
        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new Start());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
