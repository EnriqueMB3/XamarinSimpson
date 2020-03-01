using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SimpsonApp.Models;
using Xamarin.Forms;
using SimpsonApp.Views;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Essentials;

namespace SimpsonApp.ViewModels
{
  public  class ListaEpisodiosVM
    {

        public ObservableCollection<Temporada_M> episodiosTemporada { get; set; }
        public Command<string> FiltrarCommand { get; set; }
        public Command<Temporada_M> VerEpisodioCommand { get; set; }
        public string Filtro { get; set; }
        public int NumeroTemporada { get; set; }
        public ListaEpisodiosVM()
        {
            episodiosTemporada = App.LosSimpsons.GetTemporada(NumeroTemporada);
            ListaCapitulosTemporada = new ObservableCollection<Temporada_M>();
            foreach (var item in episodiosTemporada)
            {
                ListaCapitulosTemporada.Add(item);
            }

            VerEpisodioCommand = new Command<Temporada_M>(Ver);

            FiltrarCommand = new Command<string>(Filtrar);
        }

        EpisodioView episodiosview = new EpisodioView();
        public ObservableCollection<Temporada_M> ListaCapitulosTemporada { get; set; }

        private async void Ver(Temporada_M obj)
        {
            Episodio_M em = App.LosSimpsons.GetEpisodios(obj.Temporada, obj.Episodio);
            if (em == null)
            {
               if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                   await Task.Run(async() => await App.LosSimpsons.DescargarInfoEpiodio(obj.Temporada, obj.Episodio));
               // Task.Run(async () => await App.LosSimpsons.DescargarInfoEpiodio(obj.NumeroTemporada,obj.NumeroEpisodio));

                    em = App.LosSimpsons.GetEpisodios(obj.Temporada, obj.Episodio);
                }
            }
            
            //episodiosview.BindingContext = em;
            
            if (episodiosview == null) episodiosview = new EpisodioView();
           
         
            episodiosview.BindingContext = em;
            await App.Current.MainPage.Navigation.PushAsync(episodiosview);
      
        }

        private void Filtrar(string obj)
        {
            ListaCapitulosTemporada.Clear();

            ListaCapitulosTemporada.Where(x => string.IsNullOrWhiteSpace(obj) || x.Titulo.Contains(obj.ToLower())).ToList().ForEach(x => ListaCapitulosTemporada.Add(x));
        }
    }
}
