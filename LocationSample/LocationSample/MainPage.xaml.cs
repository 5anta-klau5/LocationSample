using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace LocationSample
{
    public partial class MainPage : ContentPage
    {
        double lat;
        double lng;
        
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGetLocation_Clicked(object sender, EventArgs e)
        {
            ILocation loc = DependencyService.Get<ILocation>();
            loc.locationObtained += LocationObtained;

            loc.ObtainMyLocation();
            loc = null;

            //await Task.Delay(4000);

            Position position = new Position(lat, lng);
            Pin pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "My Pin",
            };

            MyMap.Pins.Add(pin);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(lat, lng), Distance.FromMeters(1000)));
        }

        private void LocationObtained(object sender, ILocationEventArgs e)
        {
            lat = e.lat;
            lng = e.lng;

            lblCoords.Text = $"{lat}, {lng}";
        }
    }
}
