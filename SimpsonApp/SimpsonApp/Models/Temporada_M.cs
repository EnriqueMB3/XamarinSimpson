using System;
using System.Collections.Generic;
using System.Text;

namespace SimpsonApp.Models
{
   public class Temporada_M
    {
        private int numTemp;

        public int NumeroTemporada
        {
            get { return numTemp; }
            set { numTemp = value; }
        }

        private int episodio;

        public int NumeroEpisodio
        {
            get { return episodio; }
            set { episodio = value; }
        }

        private string titulo;

        public string TituloEpisodio
        {
            get { return titulo; }
            set { titulo = value; }
        }

        // public string imagen { get; set; }
    }
}
