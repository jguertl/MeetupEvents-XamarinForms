using System.Threading.Tasks;
using MeetupCross.iOS;
using Xamarin.Geolocation;

[assembly: Xamarin.Forms.Dependency(typeof(LocationService))]
namespace MeetupCross.iOS
{
	public class LocationService : ILocationService
	{
		public async Task<Coordinates> GetCoordinatesAsync()
		{
			var locator = new Geolocator() {DesiredAccuracy = 50};
			var position = await locator.GetPositionAsync(30000);

			var coords = new Coordinates
			{
				Latitude = position.Latitude,
				Longitude = position.Longitude
			};

			return coords;
		}
	}
}