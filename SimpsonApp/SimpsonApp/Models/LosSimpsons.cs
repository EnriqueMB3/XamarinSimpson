using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SimpsonApp.Models
{
    public class LosSimpsons
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        SQLiteConnection connection;
        readonly string ruta = $"{System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/simpsons";

        public LosSimpsons()
        {
            connection = new SQLiteConnection(ruta);
            connection.CreateTable<Temporadas_M>();
            connection.CreateTable<Temporada_M>();
            connection.CreateTable<Episodio_M>();


        }

        // Lista de temporadas
        public async Task DescargarTemporadas()
        {
            if (connection.Table<Temporadas_M>().Count() == 0)
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
                            Numero = item.Numero,
                            TotalEpisodios = item.TotalEpisodios,
                            Periodo = item.Periodo

                        };

                        connection.Insert(Temporadas);

                    }

                }
            }
        }

        //Temporada info
        public async Task DescargarTemporadaInfo(int numero)
        {
            if ((connection.Table<Temporada_M>().Where(x => x.Temporada == numero).Count()) == 0)
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    HttpClient httpClient = new HttpClient();
                    var json = await httpClient.GetAsync($"http://itesrc.net/api/simpsons/temporada/{numero}");
                    json.EnsureSuccessStatusCode();
                    List<Temporada_M> lista = JsonConvert.DeserializeObject<List<Temporada_M>>(await json.Content.ReadAsStringAsync());
                    connection.InsertAll(lista);
                    //DescargarPortadasCap(lista);
                }
            }
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



        public void DescargarPortadasCap(List<Temporada_M> lista)
        {

            string pathfolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/simpson";
            string file;

            Directory.CreateDirectory(pathfolder);

            foreach (var item in lista)
            {
                file = $"{pathfolder}/{item.Temporada}x{item.Episodio}.jpg";
                if (!File.Exists(file) && Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(new Uri($"http://itesrc.net/{item.imagen}"), file);
                }
            }
        }

        public List<Temporadas_M> GetTempordas()
        {
            return new List<Temporadas_M>(connection.Table<Temporadas_M>());
        }
        public List<Episodio_M> GetEpisodios()
        {
            return new List<Episodio_M>(connection.Table<Episodio_M>());
        }

        public List<Temporada_M> GetTemporada(int numero)
        {
            return new List<Temporada_M>(connection.Table<Temporada_M>().Where(x => x.Temporada == numero));
        }

    }
}
