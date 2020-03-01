using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SimpsonApp.Models;
using Xamarin.Forms;
using SimpsonApp.Views;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace SimpsonApp.ViewModels
{
    public class ListaTemporadasVM
    {



        public event PropertyChangedEventHandler PropertyChanged;
        void Actualizar([CallerMemberName]string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        private bool cargando;

        public bool Cargando
        {
            get { return cargando; }
            set { cargando = value; Actualizar(); }
        }

        private bool visible;

        public bool Visible
        {
            get { return visible; }
            set { visible = value; Actualizar(); }
        }





        List<Temporadas_M> Seasons;

        public Command<string> FiltrarCommand { get; set; }
        public Command<int> VerTemporadaCommand { get; set; }

        public ObservableCollection<Temporadas_M> Lista { get; set; }

        public string Filtro { get; set; }

        public ListaTemporadasVM()
        {
            Lista = new ObservableCollection<Temporadas_M>();
            Seasons = App.LosSimpsons.GetTempordas();
            Seasons.ForEach(x => Lista.Add(x));
            VerTemporadaCommand = new Command<int>(Ver);

            FiltrarCommand = new Command<string>(Filtrar);
        }

        private async void Ver(int obj)
        {



            Cargando = true;
            Visible = true;
            ObservableCollection<Temporada_M> temporada_Ms = App.LosSimpsons.GetTemporada(obj);

            if (temporada_Ms.Count() == 0)
            {
                await Task.Run(() => App.LosSimpsons.DescargarTemporadaInfo(obj));
            }

            temporada_Ms = App.LosSimpsons.GetTemporada(obj);
            ListaDeEpisodios ListaDeEpisodios = new ListaDeEpisodios(obj);

            Cargando = false;
            Visible = false;
            await App.Current.MainPage.Navigation.PushAsync(ListaDeEpisodios);
            ListaDeEpisodios.InputTransparent = false;
        }

        private void Filtrar(string obj)
        {
            Lista.Clear();

            if (obj != "ALL")
            {
                Seasons.Where(x => x.Periodo.StartsWith(obj)).ToList().ForEach(x => Lista.Add(x)); ;

            }
            else
            {

                Seasons.ForEach(x => Lista.Add(x));
            }
        }
    }
}
