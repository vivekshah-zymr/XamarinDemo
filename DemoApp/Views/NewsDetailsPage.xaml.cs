using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DemoApp.Views
{
    public partial class NewsDetailsPage : ContentPage
    {
        public NewsDetailsPage()
        {
			InitializeComponent();
        }

		void didTapBack(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
		void didTapSearch(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
    }
}
