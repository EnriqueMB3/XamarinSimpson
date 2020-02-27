using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SimpsonApp.Models;
using Xamarin.Forms;
using SimpsonApp.Views;
using System.Threading.Tasks;
using System.Linq;

namespace SimpsonApp.ViewModels
{
 public   class ListaTemporadasVM
    {
        // List<Temporadas_M> Seasons;
        List<Temporada_M> Season;
        public Command<string> FiltrarCommand { get; set; }
        public Command<Temporada_M> VerTemporadaCommand { get; set; }

        public ObservableCollection<Temporada_M> Lista { get; set; }

        public string Filtro { get; set; }

        public ListaTemporadasVM()
        {
            Season = App.LosSimpsons.GetTempordas();
            Lista = new ObservableCollection<Temporada_M>();

            Season.ForEach(x => Lista.Add(x));

            FiltrarCommand = new Command<string>(Filtrar);
           VerTemporadaCommand = new Command<Temporada_M>(Ver);
        }

        ListaDeEpisodios episodioslistview;

        private async void Ver(Temporada_M obj)
        {
            if (string.IsNullOrWhiteSpace(obj.TituloEpisodio))
            {
                Task.Run(async () => await App.LosSimpsons.DescargarTemporadaInfo(obj));
            }

            if (episodioslistview == null) episodioslistview = new ListaDeEpisodios();
            episodioslistview.BindingContext = obj;
          
            //:(
            //  await App.Current.MainPage.Navigation.PushAsync(episodioslistview);
        }

        private void Filtrar(string obj)
        {
            Lista.Clear();

            Season.Where(x => string.IsNullOrWhiteSpace(obj) || x.TituloEpisodio.Contains(obj.ToLower())).ToList().ForEach(x => Lista.Add(x));
        }
    }
}
