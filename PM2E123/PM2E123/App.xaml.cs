using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using PM2E123.Models;

namespace PM2E123
{
    public partial class App : Application
    {
        static BDSitios basedatos;
        public static BDSitios BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new BDSitios(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Recuperacion.db3"));
                }


                return basedatos;
            }

        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

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
