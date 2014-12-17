using System;
using Xamarin.Forms;

namespace MeetupCross
{
	public class EventListView : ContentPage
	{
		public EventListView(EventViewModel viewModel)
		{
			BindingContext = viewModel;

			var stack = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(0, 10)
			};

			var progress = new ActivityIndicator
			{
				IsEnabled = true,
				Color = Color.White
			};

			progress.SetBinding(IsVisibleProperty, "IsBusy");
			progress.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

			stack.Children.Add(progress);

			var listView = new ListView {ItemsSource = viewModel.Events};

			var itemTemplate = new DataTemplate(typeof (TextCell));
			itemTemplate.SetBinding(TextCell.TextProperty, "Name");
			listView.ItemTemplate = itemTemplate;

			stack.Children.Add(listView);

			Content = stack;
		}
	}
}