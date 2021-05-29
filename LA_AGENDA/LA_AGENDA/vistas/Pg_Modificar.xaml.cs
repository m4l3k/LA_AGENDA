using LA_AGENDA.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LA_AGENDA.vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pg_Modificar : ContentPage
    {
        public Reuniones objReunion = new Reuniones();
        public String mapaBase;
        DateTime _triggerTime;
        TimeSpan timeSpan;

        public Pg_Modificar(Reuniones reunion)
        {
            InitializeComponent();
            objReunion = reunion;
            Nombre.Text = reunion.nombre;
            Lugar.Text = reunion.lugar;
            endDatePicker.Date = Convert.ToDateTime(reunion.fecha);
            Anotaciones.Text = reunion.comentarios;
            // dateIsSelected = true;
        }


        public async void onActualizar(object sender, EventArgs e)
        {
            try
            {
                if (!Nombre.Text.Equals(""))
                {
                    if (!Lugar.Text.Equals(""))
                    {
                        objReunion.nombre = Nombre.Text;
                        objReunion.lugar = Lugar.Text;
                        objReunion.fecha = _triggerTime.ToString();
                        objReunion.comentarios = Anotaciones.Text;

                        await App.Database.SaveReunionAsync(objReunion);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Lugar no ingresado!!", "Ingrese nombre del lugar", "Entendido");
                    }
                }
            }
            catch (System.NullReferenceException)
            {
                await DisplayAlert("Datos incompletos!!", "Debe llenar NOMBRE, LUGAR y FECHA", "Entendido");
            }
        }// FIN BOTON MODIFICAR



        public void onEditorCompleted(object sender, EventArgs e)
        {
            String text = ((Editor)sender).Text;
        }

        bool OnTimerTick() //Método para la alarma falta seleccionar el objeto que corresponda a la base, sólo selecciona el primer ingreso, por defecto marca la alarma
        {
            if (_switch.IsToggled && DateTime.Now >= _triggerTime)
            {
                _switch.IsToggled = false;
                DisplayAlert("ALERTA", "Reunión: '" + Nombre.Text + "' Ya es hora !!", "OK");
            }
            return true;
        }

        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            Recalculate();
            SetTriggerTime();           
        }

        void OnSwitchToggled(object sender, ToggledEventArgs args) //el switch de alarma
        {
            Recalculate();
        }

        void Recalculate() //calcula los días faltantes hasta la fecha seleccionada
        {
            timeSpan = endDatePicker.Date - startDatePicker.Date;
            resultLabel.Text = String.Format("{0} dia{1} hasta la fecha", timeSpan.Days, timeSpan.Days == 1 ? "" : "s");
        }

        void OnTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Time")
            {
                SetTriggerTime();
            }
        }

        void AlarmSwitchToggled(object sender, ToggledEventArgs args)
        {
            SetTriggerTime();
        }


        void SetTriggerTime()
        {
            Recalculate();
            _triggerTime = DateTime.Today + timeSpan + _timePicker.Time;
            // si se selecciona alarma...
            if (_switch.IsToggled)
            {
                Hora_resultante.Text = _triggerTime.ToString();
            }
        }
    }
}