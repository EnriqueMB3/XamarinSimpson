using System;
using System.Collections.Generic;
using System.Text;

namespace SimpsonApp.Models
{
   public class Temporadas_M
    {
        private int numero;

        public int Numero
        {
            get { return numero; }
            set { numero= value; }
        }


        //list episodios count() ?
        private int totalepisodios;

        public int TotalEpisodios
        {
            get { return totalepisodios; }
            set { totalepisodios = value; }
        }

        private string periodo;


        public string Periodo
        {
            get { return periodo; }
            set { periodo = value; }
        }
    }
}
