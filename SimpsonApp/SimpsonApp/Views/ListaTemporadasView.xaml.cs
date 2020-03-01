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
    public partial class ListaTemporadasView : ContentPage
    {
        public ListaTemporadasView()
        {
            InitializeComponent();
          
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            vtemporadas.InputTransparent = true;

        }

        private void vtemporadas_Appearing(object sender, EventArgs e)
        {
            vtemporadas.InputTransparent = false;
        }
    }
}