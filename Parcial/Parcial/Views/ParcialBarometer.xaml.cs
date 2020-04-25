using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parcial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParcialBarometer : ContentPage
    {
        public ObservableCollection<ItemBarometer> Calculos { get; }
        public ParcialBarometer()
        {
            InitializeComponent();
            Calculos = new ObservableCollection<ItemBarometer>();
            Barometer.ReadingChanged += Barometer_ReadingChanged;
        }

        private void BarometerCheckButtom_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Barometer.IsMonitoring)
                {
                    Barometer.Stop();

                }
                else
                {
                    Barometer.Start(SensorSpeed.Default);


                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                DisplayAlert("Not Supported", "Error: " + fnsEx.Message, "Aceptar");
            }
            catch (Exception ex)
            {
                DisplayAlert("Exception", "Error: " + ex.Message, "Aceptar");
            }
        }

        void Barometer_ReadingChanged(object sender, BarometerChangedEventArgs e)
        {
            var data = e.Reading;
            var item = new ItemBarometer
            {
                DateInfo = DateTime.Now.ToString(),
                Pressure = "Pressure: " + data.PressureInHectopascals + " hectopascals"
            };

            Calculos.Insert(0, item);
            ListViewCheck.ItemsSource = Calculos;
            if (Compass.IsMonitoring) 
            {
                Compass.Stop();

            }
        }
    }
    public class ItemBarometer
    {
        public string DateInfo { get; set; }
        public string Pressure { get; set; }
    }

}