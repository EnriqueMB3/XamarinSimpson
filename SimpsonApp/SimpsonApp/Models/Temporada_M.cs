using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SimpsonApp.Models
{
   public class Temporada_M : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void Actualizar([CallerMemberName]string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
        private int numTemp;
        private int episodio;
        private string titulo;

        public int NumeroTemporada
        {
            get { return numTemp; }
            set { numTemp = value; Actualizar(); }
        }

        public int NumeroEpisodio
        {
            get { return episodio; }
            set { episodio = value; Actualizar(); }
        }

        public string TituloEpisodio
        {
            get { return titulo; }
            set { titulo = value; Actualizar(); }
        }

        // public string imagen { get; set; }
    }
}
