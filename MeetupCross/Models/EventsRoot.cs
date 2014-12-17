using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeetupCross
{
	public class EventsRoot
	{
		[JsonProperty("results")]
		public List<Event> Events { get; set; }
	}
}

