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
    public class ListaTemporadasVM
    {
        List<Temporadas_M> Seasons;

        public Command<string> FiltrarCommand { get; set; }
        public Command<int> VerTemporadaCommand { get; set; }

       public ObservableCollection<Temporadas_M> Lista { get; set; }

        public string Filtro { get; set; }

        public ListaTemporadasVM()
        {
            Seasons = App.LosSimpsons.GetTempordas();
            Lista = new ObservableCollection<Temporadas_M>();
            Seasons.ForEach(x => Lista.Add(x));
            VerTemporadaCommand = new Command<int>(Ver);

            //FiltrarCommand = new Command<string>(Filtrar);
        }

        ListaDeEpisodios ListaDeEpisodios = new ListaDeEpisodios();
        private async void Ver(int obj)
        {



            ObservableCollection<Temporada_M> temporada_Ms = App.LosSimpsons.GetTemporada(obj);

            if (temporada_Ms.Count() == 0)
            {
                await Task.Run(() => App.LosSimpsons.DescargarTemporadaInfo(obj));
            }

            temporada_Ms = App.LosSimpsons.GetTemporada(obj);
            ListaEpisodiosVM listaEpisodiosVM = new ListaEpisodiosVM();
            listaEpisodiosVM.ListaCapitulosTemporada = temporada_Ms;
            listaEpisodiosVM.NumeroTemporada = obj;
            ListaDeEpisodios.BindingContext = listaEpisodiosVM;


            await App.Current.MainPage.Navigation.PushAsync(ListaDeEpisodios);
            ListaDeEpisodios.InputTransparent = false;
        }

        //private void Filtrar(string obj)
        //{
        //    Lista.Clear();


        //}
    }
}
