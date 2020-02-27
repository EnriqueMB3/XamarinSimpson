using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SimpsonApp.Models
{
  public  class LosSimpsons
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        SQLiteConnection connection;
        readonly string ruta = $"{System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/simpsons";

        public LosSimpsons()
        {
            connection = new SQLiteConnection(ruta);
            connection.CreateTable<Temporada_M>();
        }

        // Lista de temporadas
        public async Task DescargarInfoGeneral()
        {
            if (connection.Table<Temporada_M>().Count() == 0)
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    HttpClient httpClient = new HttpClient();

                    var json = await httpClient.GetAsync("http://itesrc.net/api/simpsons/temporadas");

                    json.EnsureSuccessStatusCode();
                    List<Temporadas_M> lista = JsonConvert.DeserializeObject<List<Temporadas_M>>(await json.Content.ReadAsStringAsync());

                    foreach (var item in lista)
                    {
                        Temporadas_M Temporadas = new Temporadas_M()
                        {
                            Temporadas = item.Temporadas,
                            TotalDeCapitulos = item.TotalDeCapitulos,
                            Periodo = item.Periodo

                        };

                        connection.Insert(Temporadas);
                    }

                }
            }
        }

        //Temporada info
        public async Task DescargarTemporadaInfo(Temporada_M obj)
        {
            HttpClient httpClient = new HttpClient();

            var json = await httpClient.GetAsync($"http://itesrc.net/api/simpsons/temporada/{obj.NumeroTemporada}");

            json.EnsureSuccessStatusCode();

            Temporada_M Temp = JsonConvert.DeserializeObject<Temporada_M>(await json.Content.ReadAsStringAsync());

            obj.NumeroTemporada = Temp.NumeroTemporada;
            obj.NumeroEpisodio = Temp.NumeroEpisodio;
            obj.TituloEpisodio = Temp.TituloEpisodio;


            connection.Update(obj);
        }

        public async Task DescargarInfoEpiodio(Episodio_M obj)
        {
            HttpClient httpClient = new HttpClient();

            var json = await httpClient.GetAsync($"http://itesrc.net/api/simpsons/episodio/{obj.NumeroTemporada}/{obj.NumeroEpisodio}");

            json.EnsureSuccessStatusCode();

            Episodio_M e = JsonConvert.DeserializeObject<Episodio_M>(await json.Content.ReadAsStringAsync());

            obj.NombreOriginal = e.NombreOriginal;
            obj.Nombre = e.Nombre;
            obj.Id = e.Id;
            obj.Duracion = e.Duracion;
            obj.FechaEmision = e.FechaEmision;
            obj.NumeroEpisodio = e.NumeroEpisodio;
            obj.NumeroTemporada = e.NumeroTemporada;
            obj.Sinopsis = e.Sinopsis;


            connection.Update(obj);
        }

        //public event Action<int, int> ActualizarProgreso;

        public void DescargarPortadaCap(List<Episodio_M> lista)
        {
            var path = $"{System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)}simpsons/";

            Directory.CreateDirectory(path);

            WebClient webClient = new WebClient();
            foreach (var item in lista)
            {
                webClient.DownloadFile(item.Imagen, $"{path}{item.NumeroTemporada}x{item.NumeroEpisodio}.jpg");

            }
        }

        public List<Temporada_M> GetTempordas()
        {
            return new List<Temporada_M>(connection.Table<Temporada_M>());
        }

    }
}
