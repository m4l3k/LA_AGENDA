using LA_AGENDA.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LA_AGENDA.vistas;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LA_AGENDA.vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pg_Agregar : ContentPage
    {
        DateTime _triggerTime;
        TimeSpan timeSpan;
        public Reuniones Reunion { get; set; }

        public Start metodPrincipal = new Start();
        

        public Pg_Agregar()
        {
            Reunion = new Reuniones();//constructor inicial para la clase
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick); // establecer temporizador en cuanto se cambia de hora
            InitializeComponent();

        }

        //METODOS FECHA
        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            Recalculate();
            SetTriggerTime();
            Reunion.dateIsSelected = true;
        }

        void OnSwitchToggled(object sender, ToggledEventArgs args)
        {
            Recalculate();
        }

        void Recalculate()
        {
            //TimeSpan timeSpan = endDatePicker.Date - startDatePicker.Date;
            timeSpan = endDatePicker.Date - startDatePicker.Date;
            resultLabel.Text = String.Format("{0} dia{1} hasta la fecha",timeSpan.Days, timeSpan.Days == 1 ? "" : "s");
        }

        private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        //FIN METODOS FECHA



        //METODOS EDITOR
        public void onEditorCompleted(object sender, EventArgs e)
        {
            String text = ((Editor)sender).Text;
        }
        //FIN METODOS EDITOR

        //BOTON GUARDAR
        public void onGuardar(object sender, EventArgs e)
        {
            if (Nombre.Text.Length > 1)
            {
                if (Lugar.Text.Length > 1)
                {
                    if (Reunion.dateIsSelected == true)
                    {
                        Start.addReunion(Nombre.Text,Lugar.Text,_triggerTime.ToString(),Anotaciones.Text);
                        /*
                        Reunion.nombre = Nombre.Text;
                        Reunion.lugar = Lugar.Text;
                        Reunion.fecha = _triggerTime.ToString();
                        Reunion.comentarios = Anotaciones.Text;
                        */
                    }
                }
            }


        }//FIN BOTON GUARDAR

        //METODOS HORA
        bool OnTimerTick()
        {
            if (_switch.IsToggled && DateTime.Now >= _triggerTime)
            {
                _switch.IsToggled = false;
                DisplayAlert("ALERTA", "Reunión: '" + Nombre.Text + "' Ya es hora !!", "OK");
            }
            return true;
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

        // Alarma
        /*void SetTriggerTime()
        {
            if (_switch.IsToggled)
            {
                _triggerTime = DateTime.Today + _timePicker.Time;
                if (_triggerTime < DateTime.Now)
                {
                    _triggerTime += TimeSpan.FromDays(1);
                }
                FechayHora.Text = _triggerTime.ToString();
            }
        }*/

        //test alarma
        void  SetTriggerTime()
        {
            Recalculate();

            //string fecha = timeSpan.ToString();
            //string hora = _triggerTime.ToString();
            _triggerTime = DateTime.Today + timeSpan + _timePicker.Time;

            if (_switch.IsToggled)
            {

                //Fecha_resultante.Text = fecha;
                Hora_resultante.Text = _triggerTime.ToString();
            }
        }



        //FIN METODOS HORA





    }
}