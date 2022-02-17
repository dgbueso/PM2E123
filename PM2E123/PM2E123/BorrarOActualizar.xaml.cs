using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E123.Models;
using Plugin.Media;
using System.IO;
using Xamarin.Essentials;
namespace PM2E123
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BorrarOActualizar : ContentPage
    {
        
        string base64Val = "";
        Sitios a;
        public BorrarOActualizar(Sitios dato)
        {
            InitializeComponent();
            lblID.Text = dato.codigo + "";
            direccion.Text = dato.direccion;
            a = dato;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Datos_Ubicacion();
        }

        public async void Datos_Ubicacion()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("Error", "GPS no esta activo", "Ok");
                    lbllatitud.Text = "00.0";
                    lbllongitud.Text = "00.0";
                }


                if (location != null)
                {
                   
                    lbllatitud.Text = location.Latitude.ToString();
                    lbllongitud.Text = location.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {

                // Handle not enabled on device exception
            }
            catch (PermissionException)
            {
                // Handle permission exception
            }
            catch (Exception)
            {
                // Unable to get location
            }
        }


        private async void btnMapa_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new GPS(a));
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("Error", "GPS no esta activo", "Ok");
                }
                else
                {

                    var casas = new Models.Sitios
                    {
                        codigo = Convert.ToInt32(lblID.Text),
                        latitud = Convert.ToDouble(lbllatitud.Text),
                        longitud = Convert.ToDouble(lbllatitud.Text),
                        direccion = direccion.Text,
                        base64 = base64Val
                    };

                    var resultado = await App.BaseDatos.EliminarSitios(casas);

                    if (resultado == 1)
                    {
                        await DisplayAlert("Alerta", "Eliminado", "ok");
                        await Navigation.PushModalAsync(new ListadoSitios());
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo Eliminado", "ok");
                    }

                }

              
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {

                // Handle not enabled on device exception
            }
            catch (PermissionException)
            {
                // Handle permission exception
            }
            catch (Exception)
            {
                // Unable to get location
            }
        }

        private async void btnModif_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("Error", "GPS no esta activo", "Ok");
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(direccion.Text) == true)
                    {
                        await DisplayAlert("Error", "Debe escribir la direccion del sitio", "Ok");
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(direccion.Text) == true)
                        {
                            await DisplayAlert("Error", "Debe describir la direccion del sitio", "Ok");
                        }
                        else
                        {
                            if (String.IsNullOrWhiteSpace(base64Val) == true)
                            {
                                await DisplayAlert("Error", "Ingrese la foto", "Ok");
                            }
                            else
                            {
                                Guardar_Datos();
                            }

                        }

                    }


                }

               
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {

                // Handle not enabled on device exception
            }
            catch (PermissionException)
            {
                // Handle permission exception
            }
            catch (Exception)
            {
                // Unable to get location
            }

        }

        private async void btncargarimg_Clicked(object sender, EventArgs e)
        {
            var tomarfoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "miApp",
                Name = "Image.jpg"

            });

            if (tomarfoto != null)
            {
                imagen.Source = ImageSource.FromStream(() => { return tomarfoto.GetStream(); });
            }

            Byte[] imagenByte = null;

            using (var stream = new MemoryStream())
            {
                tomarfoto.GetStream().CopyTo(stream);
                tomarfoto.Dispose();
                imagenByte = stream.ToArray();

                base64Val = Convert.ToBase64String(imagenByte);
                
            }
        }


        public async void Guardar_Datos()
        {
            var casas = new Models.Sitios
            {
                codigo = Convert.ToInt32(lblID.Text),
                latitud = Convert.ToDouble(lbllatitud.Text),
                longitud = Convert.ToDouble(lbllatitud.Text),
                
                direccion = direccion.Text,
                base64 = base64Val
            };

            var resultado = await App.BaseDatos.GrabarSitios(casas);

            if (resultado == 1)
            {
                await DisplayAlert("Alerta", "Registro Actualizado", "ok");

                
                await Navigation.PushModalAsync(new ListadoSitios());
            }
            else
            {
                await DisplayAlert("Error", "No se pudo Modificos", "ok");
            }
        }



    }
 
}