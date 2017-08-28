using System;
using System.Collections.Generic;
using DemoApp.Models;
using Xamarin.Forms;

namespace DemoApp.Views
{
    public partial class BoxofficePage : ContentPage
    {
        public BoxofficePage()
        {
            InitializeComponent();
            BindingContext = new MonkeysViewModel();
        }

		void didTapBack(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		void didTapSearch(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		async void didTapPush(object sender, EventArgs e)
		{

            await Navigation.PushAsync(new AddPersonPage());
		}

	}
}
