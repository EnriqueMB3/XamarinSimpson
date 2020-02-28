﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SimpsonApp.Models;
using SimpsonApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SimpsonApp.ViewModels
{
   public class EpisodioVM: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void Actualizar([CallerMemberName]string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

      
        public ObservableCollection<Episodio_M> obsCollEpisodio { get; set; }
        List<Episodio_M> listEpisodio;

        public Command<string> FiltrarCommand { get; set; }
        public Command<Episodio_M> VerCommand { get; set; }

        public EpisodioVM()
        {
            //ep = App.LosSimpsons.GetAllEpisodio();
            obsCollEpisodio = new ObservableCollection<Episodio_M>();

            listEpisodio.ForEach(x => obsCollEpisodio.Add(x));

         //   FiltrarCommand = new Command<string>(Filtrar);
            VerCommand = new Command<Episodio_M>(Ver);
        }


        //private void Filtrar(string obj)
        //{
        //    ListaEpisodios.Clear();

        //   // ep.Where(x => string.IsNullOrWhiteSpace(obj) || x.Nombre.ToUpper().Contains(obj.ToUpper())).ToList().ForEach(x => ListaEpisodios.Add(x));
        //}

        EpisodioView episodioView = new EpisodioView();

        private async void Ver(Episodio_M episodioVer)
        {
           

            Episodio_M episodio = App.LosSimpsons.GetEpisodios(episodioVer.Temporada, episodioVer.Episodio);

            if (episodio == null)
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    
                    await Task.Run(() => App.LosSimpsons.DescargarInfoEpiodio(episodioVer.Temporada, episodioVer.Episodio));

                    episodio = App.LosSimpsons.GetEpisodios(episodioVer.Temporada, episodioVer.Episodio);
                }
            }
            episodioView.BindingContext = episodio;
            await App.Current.MainPage.Navigation.PushAsync(episodioView);
        }
    }
}
