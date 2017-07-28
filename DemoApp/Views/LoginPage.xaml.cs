using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using DemoApp.Models;
using DemoApp.ServiceManagers;

namespace DemoApp.Views
{
    
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
		
        void didTapLogin(object sender, EventArgs e)
		{
            User user = new User();
            user.FirstName = "Static data";

			//var homePage = new HomePage();
			//homePage.BindingContext = user;
			//Navigation.PushAsync(homePage);

            var homePage = new MasterPage();
			homePage.BindingContext = user;
			NavigationPage.SetHasNavigationBar(this, false);
			NavigationPage.SetHasNavigationBar(homePage, false);
			Navigation.PushAsync(homePage);


		}

        protected async void didTapLogin1(object sender, EventArgs e)
		{
            lblEmailError.IsVisible = false;
            lblPasswordError.IsVisible = false;
            if (txtEmail.Text == ""){
                lblEmailError.IsVisible = true;
                lblEmailError.Text = "Please enter email address.";
                return;
            }
            else if (!Utils.Utility.isValidEmail(txtEmail.Text)){
                lblEmailError.IsVisible = true;
                lblEmailError.Text = "Please enter valid email address.";    
                return;
            }
			if (txtPassword.Text == "")
			{
				lblPasswordError.IsVisible = true;
				return;
			}

            User user = new User();
            user.Email = txtEmail.Text;
            user.Password = txtPassword.Text;
            user = await App.loginManager.makeLoginAPICall(user);

            if (user.UserID != 0) {
				var homePage = new HomePage();
				homePage.BindingContext = user;
				await Navigation.PushAsync(homePage);
            }
            else{
				await DisplayAlert("MyApp", "Invalid email/password", "OK");
			}
		}

		void didTapBack(object sender, EventArgs e)
		{
            Navigation.PopAsync();
		}

		void didTapForgotPass(object sender, EventArgs e)
		{
            Navigation.PushAsync(new HomePage());
			//Navigation.PopAsync();
		}

		void didTapLoginWithFB(object sender, EventArgs e)
		{
            Navigation.PushAsync(new PersonListPage());
		}
    }
}
