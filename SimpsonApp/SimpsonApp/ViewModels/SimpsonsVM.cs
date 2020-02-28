
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SimpsonApp.ViewModels
{
    public class SimpsonsVM : INotifyPropertyChanged
    {
        //Pokedex pokedex = new Pokedex(); - Se movio a App.Xaml.cs para que todos los viewmodels lo puedan utilizar

        public event PropertyChangedEventHandler PropertyChanged;

        void Actualizar([CallerMemberName]string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        private double valor;
        private string error;
        private bool running;

        public double Valor
        {
            get { return valor; }
            set { valor = value; Actualizar(); }
        }

        public string Error
        {
            get { return error; }
            set { error = value; Actualizar(); }
        }

        public bool Running
        {
            get { return running; }
            set { running = value; Actualizar(); }
        }

        private bool visible;
        public bool Visible
        {
            get { return visible; }
            set { visible = value; Actualizar(); }
        }
        public SimpsonsVM()
        {
     
            //Descargar();

            Task.Run(Descargar);
            ContinuarCommand = new Command(Continuar);
        }

      

        public event Action Pasar;
        public Command ContinuarCommand { get; set; }

        private void Continuar(object obj)
        {
            //throw new NotImplementedException();
            Application.Current.MainPage = new NavigationPage(new Views.ListaTemporadasView());
        }

        public async void Descargar()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Error = "No hay conexión a internet.";
                return;
            }

            try
            {
                Running = true;
                Visible = false;
                await App.LosSimpsons.DescargarTemporadas();

                Running = false;
                Visible = true;
               
                //error 404
         


            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }

       
    }
}
