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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpsonApp.ViewModels
{
    public class ListaEpisodiosVM
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

        public List<Temporada_M> listTemp { get; set; }
        public ObservableCollection<Temporada_M> episodiosTemporada { get; set; }
        //  public ObservableCollection<Temporada_M> ListaCapitulosTemporada { get; set; }
        public Command<string> FiltrarCommand { get; set; }
        public ObservableCollection<Temporada_M> ListaCapitulosTemporada { get; set; }
        public Command<Temporada_M> VerEpisodioCommand { get; set; }

        public string Filtro { get; set; }
        public int NumeroTemporada { get; set; }
        public ListaEpisodiosVM(int tem)
        {

            listTemp = App.LosSimpsons.GetTemporada(tem).ToList();
            ListaCapitulosTemporada = new ObservableCollection<Temporada_M>();
            listTemp.ForEach(x => ListaCapitulosTemporada.Add(x));
            NumeroTemporada = tem;
            FiltrarCommand = new Command<string>(Filtrar);

            VerEpisodioCommand = new Command<Temporada_M>(Ver);
        }

        EpisodioView episodiosview = new EpisodioView();

        private async void Ver(Temporada_M obj)
        {
            Visible = true;
            Cargando = true;

            Episodio_M em = App.LosSimpsons.GetEpisodios(obj.Temporada, obj.Episodio);
            if (em == null)
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    await Task.Run(async () => await App.LosSimpsons.DescargarInfoEpiodio(obj.Temporada, obj.Episodio));


                    em = App.LosSimpsons.GetEpisodios(obj.Temporada, obj.Episodio);
                }
            }

            if (episodiosview == null) episodiosview = new EpisodioView();


            episodiosview.BindingContext = em;
            Visible = false;
            Cargando = false;
            await App.Current.MainPage.Navigation.PushAsync(episodiosview);

        }

        private void Filtrar(string obj)
        {
            ListaCapitulosTemporada.Clear();
            listTemp.Where(x => x.Titulo.ToLower().Contains(obj.ToLower())).ToList().ForEach(x => ListaCapitulosTemporada.Add(x)); ;
        }



    }
}


