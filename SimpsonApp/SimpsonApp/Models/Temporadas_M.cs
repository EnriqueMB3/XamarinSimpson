using System;
using System.Collections.Generic;
using System.Text;

namespace SimpsonApp.Models
{
   public class Temporadas_M
    {
        private int temporadas;

        public int Temporadas
        {
            get { return temporadas; }
            set { temporadas = value; }
        }


        //list episodios count() ?
        private int totaldecapitulos;

        public int TotalDeCapitulos
        {
            get { return totaldecapitulos; }
            set { totaldecapitulos = value; }
        }

        private string periodo;


        public string Periodo
        {
            get { return periodo; }
            set { periodo = value; }
        }
    }
}
