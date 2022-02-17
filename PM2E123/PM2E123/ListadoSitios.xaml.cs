using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E123.Models;

namespace PM2E123
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListadoSitios : ContentPage
    {
        public ListadoSitios()
        {
            InitializeComponent();
            Title = "Listado";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cargarListado();
        }

        public async void cargarListado()
        {
            var lista = await App.BaseDatos.ObtenerListaSitios();
            listaCasaPoin.ItemsSource = lista;
        }

        private async void lista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
               
                Sitios local = (Sitios)e.SelectedItem;

                
                BorrarOActualizar ventana = new BorrarOActualizar(local);
                await Navigation.PushModalAsync(ventana);


                listaCasaPoin.SelectedItem = null;

            }
        }
    }
}