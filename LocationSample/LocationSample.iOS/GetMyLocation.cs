using CoreLocation;
using LocationSample.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetMyLocation))]
namespace LocationSample.iOS
{
    public class LocationEventArgs : EventArgs, ILocationEventArgs
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class GetMyLocation : ILocation
    {
        public event EventHandler<ILocationEventArgs> locationObtained;

        public void ObtainMyLocation()
        {
            CLLocationManager lm = new CLLocationManager();
            lm.DesiredAccuracy = CLLocation.AccuracyBest;
            lm.DistanceFilter = CLLocationDistance.FilterNone;

            lm.LocationsUpdated += LocationsUpdated;
            lm.AuthorizationChanged += AuthorizationChanged;
            lm.RequestWhenInUseAuthorization();
        }

        protected void LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        {
            CLLocation[] locations = e.Locations;
            //string strLocation = locations[locations.Length - 1].Coordinate.Latitude.ToString();
            //strLocation = strLocation + "," + locations[locations.Length - 1].Coordinate.Longitude.ToString();

            LocationEventArgs args = new LocationEventArgs();
            args.lat = locations[locations.Length - 1].Coordinate.Latitude;
            args.lng = locations[locations.Length - 1].Coordinate.Longitude;

            locationObtained(this, args);
        }

        protected void AuthorizationChanged(object sender, CLAuthorizationChangedEventArgs e)
        {
            if (e.Status == CLAuthorizationStatus.AuthorizedWhenInUse)
            {
                (sender as CLLocationManager).StartUpdatingLocation();
            }
        }
    }
}
