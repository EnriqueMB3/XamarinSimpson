using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SimpsonApp.Models
{
   public class Temporada_M 
    {
        

    
        private int numTemp;
        private int episodio;
        private string titulo;

        public int Temporada
        {
            get { return numTemp; }
            set { numTemp = value;  }
        }

        public int Episodio
        {
            get { return episodio; }
            set { episodio = value;}
        }

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        public string imagen { get; set; }
    }
}
