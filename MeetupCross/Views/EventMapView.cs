using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MeetupCross
{
	public class EventMapView : ContentPage
	{
		private EventViewModel _vm
		{
			get { return BindingContext as EventViewModel; }
		}

		public EventMapView(EventViewModel viewModel)
		{
			BindingContext = viewModel;

			var stack = new StackLayout();
			var map = new Map { IsShowingUser = true, VerticalOptions = LayoutOptions.FillAndExpand };

			MessagingCenter.Subscribe<EventViewModel>(this, "LocationSet", s =>
				{
					var currentLocation = _vm.CurrentLocation;
					map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(currentLocation.Latitude, currentLocation.Longitude),
						Distance.FromMiles(0.3)));
				});
					
			MessagingCenter.Subscribe<EventViewModel>(this, "EventsLoaded", s =>
				{
					map.Pins.Clear();
					foreach (var v in _vm.Events)
						if(v.Venue != null)
							map.Pins.Add(new Pin
								{
									Type = PinType.Place,
									Position = new Position(v.Venue.Latitude, v.Venue.Longitude),
									Address = v.Venue.Address1,
									Label = v.Name
								});
				});
			stack.Children.Add(map);

			Content = stack;
		}
	}
}
