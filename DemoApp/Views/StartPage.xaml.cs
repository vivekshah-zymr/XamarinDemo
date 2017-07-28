using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DemoApp.Views
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }
		void didTapLogin(object sender, EventArgs e)
		{
            Navigation.PushAsync(new LoginPage());
		}
		void didTapSignup(object sender, EventArgs e)
		{
            Navigation.PushAsync(new SignUpPage());
		}
    }
}
