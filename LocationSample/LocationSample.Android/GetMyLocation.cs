using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LocationSample.Droid;
using Android.Locations;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetMyLocation))]
namespace LocationSample.Droid
{
    public class LocationEventArgs : EventArgs, ILocationEventArgs
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class GetMyLocation : Java.Lang.Object, ILocation, ILocationListener
    {
        //public IntPtr Handle => throw new NotImplementedException();

        public event EventHandler<ILocationEventArgs> locationObtained;

        public void ObtainMyLocation()
        {
            LocationManager lm = Android.App.Application.Context.GetSystemService(Context.LocationService) as LocationManager;
            lm.RequestLocationUpdates
                (
                    provider: LocationManager.NetworkProvider,
                    minTime: 0,
                    minDistance: 0,
                    listener: this
                );
        }

        //public void Dispose()
        //{
            
        //}

        public void OnLocationChanged(Location location)
        {
            if (location != null)
            {
                LocationEventArgs args = new LocationEventArgs();
                args.lat = location.Latitude;
                args.lng = location.Longitude;
                locationObtained(this, args);
            }
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
            
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            
        }
    }
}