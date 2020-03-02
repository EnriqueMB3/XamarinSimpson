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
    public partial class FavsV : ContentPage
    {
        public FavsV()
        {
            InitializeComponent();
        }

        private void favview_Appearing(object sender, EventArgs e)
        {
            favview.InputTransparent = true;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            favview.InputTransparent = true;
        }
    }
}