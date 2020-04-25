using System;
using System.Collections.ObjectModel;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parcial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParcialOrientation : ContentPage
    {
        public ObservableCollection<ItemC> Calculos { get; }

        public ParcialOrientation()
        {
            InitializeComponent();
            Calculos = new ObservableCollection<ItemC>();
            OrientationSensor.ReadingChanged += OrientationSensor_ReadingChanged;
        }

        private void OrientationCheckButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (OrientationSensor.IsMonitoring)
                {
                    OrientationSensor.Stop();
                }
                else
                {
                    OrientationSensor.Start(SensorSpeed.Default);
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

        void OrientationSensor_ReadingChanged(object sender, OrientationSensorChangedEventArgs e)
        {
            var data = e.Reading;
            var item = new ItemC
            {
                DateInfo = DateTime.Now.ToString(),
                LabelX = "X: " + data.Orientation.X,
                LabelY = "Y: " + data.Orientation.Y,
                LabelZ = "Z: " + data.Orientation.Z,
                LabelW = "W: " + data.Orientation.W
            };
            Calculos.Insert(0, item);
            ListViewCheck.ItemsSource = Calculos;
            if (OrientationSensor.IsMonitoring)
            {
                OrientationSensor.Stop();
            }
        }
    }

    public class ItemC
    {
        public string DateInfo { get; set; }
        public string LabelX { get; set; }
        public string LabelY { get; set; }
        public string LabelZ { get; set; }
        public string LabelW { get; set; }
    }


}
