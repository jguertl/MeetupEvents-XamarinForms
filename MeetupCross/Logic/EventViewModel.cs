using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using MeetupCross;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeetupCross
{
	public class EventViewModel:INotifyPropertyChanged
	{
		private readonly IMeetupService _meetup;
		private readonly ILocationService _location;

		public event PropertyChangedEventHandler PropertyChanged;

		private ObservableCollection<Event> _events;
		public ObservableCollection<Event> Events
		{
			get { return _events; }
			set
			{
				_events = value;
				OnPropertyChanged();
			}
		}

		private Coordinates _currentLocation;
		public Coordinates CurrentLocation
		{
			get { return _currentLocation; }
			set
			{
				_currentLocation = value;
				OnPropertyChanged();
			}
		}

		private bool _isBusy;
		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				OnPropertyChanged();
			}
		}

		public EventViewModel()
		{
			_events = new ObservableCollection<Event>();
			_meetup = new MeetupService();
			_location = DependencyService.Get<ILocationService>();
		}

		private Command _refresh;
		public Command Refresh
		{
			get { return _refresh ?? (_refresh = new Command(async () => await ExecuteRefresh())); }
		}

		private async Task ExecuteRefresh()
		{
			if (IsBusy) return;

			CurrentLocation = await _location.GetCoordinatesAsync();
			MessagingCenter.Send(this, "LocationSet");

			await LoadEvents();
		}

		private async Task LoadEvents()
		{
			if (IsBusy) return;
			IsBusy = true;


			if(Events != null)
				Events.Clear();

			try
			{
				var response = await _meetup.GetEvents(CurrentLocation);

				foreach (var v in response.Events)
					Events.Add(v);
					
				MessagingCenter.Send(this, "EventsLoaded");
			}
			finally
			{
				IsBusy = false;
			}
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
