using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SimpsonApp.Models
{
   public class Episodio_M : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void Actualizar([CallerMemberName]string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        private string img;

        public string Imagen
        {
            get { return img; }
            set { img = value; Actualizar(); }
        }

        private int id;

        [PrimaryKey]
        public int Id
        {
            get { return id; }
            set { id = value; Actualizar(); }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; Actualizar(); }
        }
        private string nomOriginal;

        public string NombreOriginal
        {
            get { return nomOriginal; }
            set { nomOriginal = value; Actualizar(); }
        }
        private DateTime fecha;

        public DateTime FechaEmision
        {
            get { return fecha; }
            set { fecha = value; Actualizar(); }
        }

        private int duracion;

        public int Duracion
        {
            get { return duracion; }
            set { duracion = value; Actualizar(); }
        }
        private string sinopsis;

        public string Sinopsis
        {
            get { return sinopsis; }
            set { sinopsis = value; Actualizar(); }
        }

        private int numTemporada;

        public int NumeroTemporada
        {
            get { return numTemporada; }
            set { numTemporada = value; Actualizar(); }
        }

        private int numEpisodio;

        public int NumeroEpisodio
        {
            get { return numEpisodio; }
            set { numEpisodio = value; Actualizar(); }
        }

    }
}
