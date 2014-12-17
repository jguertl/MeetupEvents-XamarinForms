using System;
using Xamarin.Forms;

namespace MeetupCross
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			return new NavigationPage(new Homepage());
		}
	}
}

