using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpsonApp.Models
{
   public class Episodio_M
    {
        public string Imagen { get; set; }
        [PrimaryKey]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreOriginal { get; set; }
        public DateTime FechaEmision { get; set; }
        public int Duracion { get; set; }
        public string Sinopsis { get; set; }
        public int NumeroTemporada { get; set; }
        public int NumeroEpisodio { get; set; }
    }
}
