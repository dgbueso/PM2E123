using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace PM2E123
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {

           
            base.OnBackButtonPressed();
            return true;

        

        }
        private async void Guardarsitios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new GuardarSitios());
        }
        private async void btnList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ListadoSitios());
        }
    }
}

