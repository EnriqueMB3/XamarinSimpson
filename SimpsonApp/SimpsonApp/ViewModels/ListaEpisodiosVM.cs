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
  public  class ListaEpisodiosVM
    {
     
        public Command<string> FiltrarCommand { get; set; }
        public Command<Episodio_M> VerEpisodioCommand { get; set; }
        public string Filtro { get; set; }
        public int NumeroTemporada { get; set; }
        public ListaEpisodiosVM()
        {
            
            VerEpisodioCommand = new Command<Episodio_M>(Ver);
 
        }

        EpisodioView episodiosview;
        public List<Temporada_M> ListaCapitulosTemporada { get; set; }

        private async void Ver(Episodio_M obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Task.Run(async () => await App.LosSimpsons.DescargarInfoEpiodio(obj));
            }

            if (episodiosview == null) episodiosview = new EpisodioView();
            episodiosview.BindingContext = obj;
            await App.Current.MainPage.Navigation.PushAsync(episodiosview);
        }

        //private void Filtrar(string obj)
        //{
        //    Lista.Clear();

        //    Season.Where(x => string.IsNullOrWhiteSpace(obj) || x.Nombre.Contains(obj.ToLower())).ToList().ForEach(x => Lista.Add(x));
        //}
    }
}
