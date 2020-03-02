using System;
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
  public  class FavVM
    {
        public List<Episodio_M> listaEpFav;
        public ObservableCollection<Episodio_M> obsCollFav{ get; set; }

        public Command<Episodio_M> AgregarCommand { get; set; }
        public Command<Episodio_M> EliminarCommand { get; set; }
        public Command<Episodio_M> verCommand { get; set; }

        FavVM()
        {
            // listaEpFav.Where(x => x.Favorito == true).ToList().OrderBy(x=>x.Temporada).ThenBy(x=>x.Episodio);
            listaEpFav = App.LosSimpsons.GetAll5Episodio();
            obsCollFav = new ObservableCollection<Episodio_M>();

            listaEpFav.ForEach(x => obsCollFav.Add(x));

           verCommand = new Command<Episodio_M>(ver);
            //AgregarCommand = new Command<Episodio_M>(Agregar);
            //EliminarCommand = new Command<Episodio_M>(Eliminar);
        }

        EpisodioView episodiosview = new EpisodioView();
        private async void ver(Episodio_M obj)
        {
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
        
            await App.Current.MainPage.Navigation.PushAsync(episodiosview);
        }

        //private void Eliminar(Episodio_M obj)
        //{
        //    var del = listaEpFav.Where(x => x.Id == obj.Id).FirstOrDefault();
        //    obsCollFav.Remove(del);
        //}

        //private void Agregar(Episodio_M obj)
        //{
        //  var add =  listaEpFav.Where(x => x.Id == obj.Id).FirstOrDefault();
        //    obsCollFav.Add(add);
        //}
    }
}
