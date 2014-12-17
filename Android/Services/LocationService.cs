using System;
using MeetupCross.Android;
using System.Threading.Tasks;
using Xamarin.Geolocation;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(LocationService))]
namespace MeetupCross.Android
{
	public class LocationService : ILocationService
	{
		public async Task<Coordinates> GetCoordinatesAsync()
		{
			var locator = new Geolocator(Forms.Context) { DesiredAccuracy = 30 };
			var position = await locator.GetPositionAsync(30000);

			var result = new Coordinates
			{
				Latitude = position.Latitude,
				Longitude = position.Longitude
			};

			return result;
		}
	}
}