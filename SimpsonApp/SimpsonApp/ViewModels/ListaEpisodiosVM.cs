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
    class ListaEpisodiosVM
    {
        List<Episodio_M> Season;
        public Command<string> FiltrarCommand { get; set; }
        public Command<Episodio_M> VerTemporadaCommand { get; set; }

        public ObservableCollection<Episodio_M> Lista { get; set; }

        public string Filtro { get; set; }

        public ListaEpisodiosVM()
        {
            Season = App.LosSimpsons.GetEpisodios();
            Lista = new ObservableCollection<Episodio_M>();

            Season.ForEach(x => Lista.Add(x));

            FiltrarCommand = new Command<string>(Filtrar);
            VerTemporadaCommand = new Command<Episodio_M>(Ver);
        }

        EpisodioView episodiosview;

        private async void Ver(Episodio_M obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Task.Run(async () => await App.LosSimpsons.DescargarInfoEpiodio(obj));
            }

            if (episodiosview == null) episodiosview = new EpisodioView();
            episodiosview.BindingContext = obj;

            //:(
           //  await App.Current.MainPage.Navigation.PushAsync(episodiosview);
        }

        private void Filtrar(string obj)
        {
            Lista.Clear();

            Season.Where(x => string.IsNullOrWhiteSpace(obj) || x.Nombre.Contains(obj.ToLower())).ToList().ForEach(x => Lista.Add(x));
        }
    }
}
