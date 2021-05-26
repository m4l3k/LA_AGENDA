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
        



        bool dateIsSelected = false;
        DateTime _triggerTime; // variable para alarma, almacenará la fecha y hora
        TimeSpan timeSpan;       

        public Start metodPrincipal = new Start(); //crear nuevo objeto 
        

        public Pg_Agregar() 
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick); // establecer temporizador en cuanto se cambia de hora
            
            InitializeComponent();
        }


        //-----------------METODOS FECHA
        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            Recalculate();
            SetTriggerTime();            
            dateIsSelected = true; 
        }

        void OnSwitchToggled(object sender, ToggledEventArgs args) //el switch de alarma
        {
            Recalculate();
        }

        void Recalculate() //calcula los días faltantes hasta la fecha seleccionada
        {
            //TimeSpan timeSpan = endDatePicker.Date - startDatePicker.Date;
            timeSpan = endDatePicker.Date - startDatePicker.Date;
            resultLabel.Text = String.Format("{0} dia{1} hasta la fecha",timeSpan.Days, timeSpan.Days == 1 ? "" : "s");
        }

        private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        //--------------FIN METODOS FECHA



        //--------------METODO EDITOR para anotaciones
        public void onEditorCompleted(object sender, EventArgs e)
        {
            String text = ((Editor)sender).Text;
        }
        //-------------FIN METODO EDITOR



        //-------------BOTON GUARDAR
        public async void onGuardar(object sender, EventArgs e) //no valida campo de comentarios
        {
            try
            {
                if (Nombre.Text.Length != 0)
                {
                    if (Lugar.Text.Length != 0)
                    {
                        if (dateIsSelected == true) //sino cambio de fecha un día adelante no se selecciona
                        {
                            //Start.addReunion(Nombre.Text,Lugar.Text,_triggerTime.ToString(),Anotaciones.Text);
                            //método asíncrono para guardar en la base de datos

                            await App.Database.SaveReunionAsync(new Reuniones
                            {
                                nombre = Nombre.Text,
                                lugar = Lugar.Text,
                                fecha = _triggerTime.ToString(),
                                comentarios = Anotaciones.Text
                            });

                            await Navigation.PopAsync();

                            Nombre.Text = Lugar.Text = Anotaciones.Text = string.Empty;  //Limpiar el texto de los entrys
                                                                                         // Necesito regresar a la pagina principal despues de guardar  

                        }
                        else
                        {
                            await DisplayAlert("Fecha no seleccionada!!", "Seleccione una Fecha", "Entendido");
                        }
                        
                    }
                    else
                    {
                        await DisplayAlert("Lugar no ingresado!!", "Ingrese nombre del lugar", "Entendido");
                    }
                }
            }
            catch(System.NullReferenceException)
            {
                await DisplayAlert("Datos incompletos!!", "Debe llenar NOMBRE, LUGAR y FECHA", "Entendido");
            }
        }//--------------FIN BOTON GUARDAR


        //---------------METODOS HORA
        bool OnTimerTick() //Método para la alarma falta seleccionar el objeto que corresponda a la base, sólo selecciona el primer ingreso, por defecto marca la alarma
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

       
        void  SetTriggerTime()
        {
            Recalculate();
            
            _triggerTime = DateTime.Today + timeSpan + _timePicker.Time;

            // si se selecciona alarma...
            if (_switch.IsToggled)
            {
                //Fecha_resultante.Text = fecha;
                Hora_resultante.Text = _triggerTime.ToString();
                //resultLabel.Text = String.Format("{0} dia{1} hasta la fecha",timeSpan.Days, timeSpan.Days == 1 ? "" : "s");
                //Hora_resultante.Text = String.Format("Faltan {0} dias, con {1} horas",timeSpan.TotalDays,timeSpan.TotalHours);
            }


        }
        //--------------FIN METODOS HORA
        
    }
}