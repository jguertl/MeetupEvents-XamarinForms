using System;
using System.Threading.Tasks;

namespace MeetupCross
{
	public interface IMeetupService
	{
		Task<EventsRoot> GetEvents(Coordinates coords);
	}
}

