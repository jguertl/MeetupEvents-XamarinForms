using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MeetupCross
{
	public class MeetupService:IMeetupService
	{
		#region Meetup API client props
		private const string _API_KEY = "7e2e46a3a16142773676771c5c431b";
		#endregion

		public async Task<EventsRoot> GetEvents(Coordinates coords)
		{
			var url = string.Format(@"https://api.meetup.com/2/groups?lat={0}&lon={1}&page=20&key={2}",coords.Latitude,coords.Longitude, _API_KEY);

			var client = new HttpClient();
			var response = await client.GetStringAsync(url);

			JObject o = JObject.Parse(response);

			return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<EventsRoot>(o.ToString()));
		}
	}
}

