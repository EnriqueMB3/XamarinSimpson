using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpsonApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaDeEpisodios : ContentPage
    {
        public ListaDeEpisodios()
        {
            InitializeComponent();
         

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            vepi.InputTransparent = true;
        }

        private void vepi_Appearing(object sender, EventArgs e)
        {
            vepi.InputTransparent = false;   
        }

        private void search1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(search1.Text))
            {
                search1.SearchCommand.Execute("");
            }
        }
    }
}