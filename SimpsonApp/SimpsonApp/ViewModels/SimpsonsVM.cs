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

		public SimpsonsVM()
		{
			App.LosSimpsons.ActualizarProgreso += LosSimpsons_ActualizarProgreso;
			//Descargar();
			Task.Run(Descargar);
		}

		private void LosSimpsons_ActualizarProgreso(int arg1, int arg2)
		{
			
			Valor = arg1 / (double)arg2;
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
				await App.LosSimpsons.DescargarInfoGeneral();
				Running = false;
				App.LosSimpsons.ActualizarProgreso -= LosSimpsons_ActualizarProgreso;
				//error 404
				//Application.Current.MainPage = new NavigationPage(new SimpsonApp.Views.LosSimpsonMainPageView());

			}
			catch (Exception ex)
			{
				Error = ex.Message;
			}
		}
	}
}
