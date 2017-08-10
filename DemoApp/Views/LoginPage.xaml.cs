using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using DemoApp.Models;
using DemoApp.Utils;
using DemoApp.ServiceManagers;
using Acr.UserDialogs;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DemoApp.Views
{

    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            checkUserLoggedIn();
        }

        async void checkUserLoggedIn(){
			User user = Utility.getUserDetails();
			if (user.UserID != 0)
			{
                var continueLogin = await App.Current.MainPage.DisplayAlert("Hey " + user.FirstName, "Do you want to continue as " + user.FirstName +"? \n Or Sign in with different account?", "Continue as "+ user.FirstName, "New SignIn");
                if (continueLogin){
					var homePage = new HomePage();
					homePage.BindingContext = user;
					await Navigation.PushAsync(homePage);
                }
                else{
                    await Utils.Utility.clearAllApplicationProperty();
                }
			}
		}

        private async void didTapLogin1(object sender, EventArgs e)
        {
            // UserDialogs.Instance.AlertAsync("Test alert", "Alert Title");

            UserDialogs.Instance.ShowLoading("", MaskType.Clear);
            await Task.Delay(2000);
            UserDialogs.Instance.HideLoading();
            //await Task.Delay(1000);
            User user = new User();
            user.FirstName = "Static data";
            var homePage = new HomePage();
            homePage.BindingContext = user;
            await Navigation.PushAsync(homePage);
        }

        protected async void didTapLogin(object sender, EventArgs e)
        {
            lblEmailError.IsVisible = false;
            lblPasswordError.IsVisible = false;
            if (txtEmail.Text == "")
            {
                lblEmailError.IsVisible = true;
                lblEmailError.Text = "Please enter email address.";
                return;
            }
            else if (!Utils.Utility.isValidEmail(txtEmail.Text))
            {
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
            UserDialogs.Instance.ShowLoading("", MaskType.Black);
            user = await App.loginManager.makeLoginAPICall(user);
            UserDialogs.Instance.HideLoading();
            if (user.UserID != 0)
            {
                var homePage = new HomePage();
                homePage.BindingContext = user;
                await Navigation.PushAsync(homePage);
            }
            else
            {
                await DisplayAlert("MyApp", "Invalid email/password", "OK");
            }
        }

        void didTapBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void didTapForgotPass(object sender, EventArgs e)
        {
            checkUserLoggedIn();
            //Navigation.PushAsync(new HomePage());
            //Navigation.PopAsync();
        }

        void didTapLoginWithFB(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PersonListPage());
        }
    }
}
