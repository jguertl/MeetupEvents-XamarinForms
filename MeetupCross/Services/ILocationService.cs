using System.Threading.Tasks;

namespace MeetupCross
{
	public interface ILocationService
	{
		Task<Coordinates> GetCoordinatesAsync();
	}
}
