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
        readonly string ruta = $"{System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/datasimpsons";

        public LosSimpsons()
        {
            connection = new SQLiteConnection(ruta);
            connection.CreateTable<Temporadas_M>();
            connection.CreateTable<Temporada_M>();
            connection.CreateTable<Episodio_M>();


        }

        // Lista de temporadas
        public async  Task DescargarTemporadas()
        {
            if (connection.Table<Temporadas_M>().Count() == 0 && Connectivity.NetworkAccess == NetworkAccess.Internet)
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
                    DescargarPortadasCap(lista);
                }
            }
        }

        public async Task DescargarInfoEpiodio(int temporada, int cap)
        {
            HttpClient httpClient = new HttpClient();
            Episodio_M episodio = GetEpisodios(temporada, cap);

            var json = await httpClient.GetAsync($"http://itesrc.net/api/simpsons/episodio/{temporada}/{cap}");

            json.EnsureSuccessStatusCode();

            episodio = JsonConvert.DeserializeObject<Episodio_M>(await json.Content.ReadAsStringAsync());

            connection.InsertOrReplace(episodio);
         
        }



        public void DescargarPortadasCap(List<Temporada_M> lista)
        {

            string pathfolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string file;

            Directory.CreateDirectory(pathfolder);

            foreach (var item in lista)
            {
                file = $"{pathfolder}/{item.Temporada}x{item.Episodio.ToString("00")}.jpg";
                if (!File.Exists(file) && Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    WebClient webClient = new WebClient();
                    string uri = $"http://itesrc.net{item.imagen}";
                    webClient.DownloadFile(new Uri(uri), file);

                }
            }
        }

        public List<Temporadas_M> GetTempordas()
        {
            return new List<Temporadas_M>(connection.Table<Temporadas_M>());
        }
        public Episodio_M GetEpisodios(int temp, int ep)
        {
            Episodio_M episodio = new Episodio_M();
            episodio = connection.Table<Episodio_M>().Where(x => x.Temporada == temp && x.Episodio == ep).FirstOrDefault();
           
            return episodio;
        }
        public List<Episodio_M> GetAll5Episodio(int tem)
        {
            //return new List<Episodio_M>(connection.Table<Episodio_M>().OrderBy(x => x.Temporada).ThenBy(x => x.Episodio));
           return new List<Episodio_M>(connection.Table<Episodio_M>().Where(x=>x.Temporada ==tem).ToList());
          
        }

            public ObservableCollection<Temporada_M> GetTemporada(int numero)
        {
            return new ObservableCollection<Temporada_M>(connection.Table<Temporada_M>().Where(x => x.Temporada == numero));
        }

    }
}
