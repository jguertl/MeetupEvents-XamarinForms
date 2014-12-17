using System;
using Xamarin.Forms;

namespace MeetupCross
{
	public class Homepage : TabbedPage
	{
		private EventViewModel _vm
		{
			get { return BindingContext as EventViewModel; }
		}

		public Homepage()
		{
			BindingContext = new EventViewModel();

			var refresh = new ToolbarItem
			{
				Name = "refresh",
				Icon = "refresh.png",
				Command = _vm.Refresh,
				Priority = 0
			};

			ToolbarItems.Add(refresh);

			Title = "Meetup Event Finder";

			var list = new EventListView(_vm) { Title = "Events", Icon = "list.png" };
			var map = new EventMapView(_vm) { Title = "Map", Icon = "map.png"};

			Children.Add(list);
			Children.Add(map);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (_vm == null || _vm.IsBusy || _vm.Events.Count > 0)
				return;

			_vm.Refresh.Execute(null);
		}
	}
}

