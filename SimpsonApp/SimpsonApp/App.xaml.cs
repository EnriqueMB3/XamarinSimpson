using SimpsonApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimpsonApp.Views;

namespace SimpsonApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
        public static LosSimpsons LosSimpsons { get; set; } = new LosSimpsons();
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
