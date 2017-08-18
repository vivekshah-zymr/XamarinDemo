using System;
using System.Collections.Generic;
using Xamarin.Forms;
using DemoApp.Models;
using System.Diagnostics;

namespace DemoApp.Views
{

    public partial class NewsDetailsPage : ContentPage
    {
        public NewsModel newsDetails;

        public NewsDetailsPage()
        {
            InitializeComponent();
			
            //this.BindingContext = newsDetails;
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

            string htmlString = "";
			if (!String.IsNullOrEmpty(newsDetails.Text))
			{
				htmlString = newsDetails.Text;
			}

            var htmlSource = new HtmlWebViewSource();
			htmlSource.Html = htmlString;
			webviewNewsDetails.Source = htmlSource;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}

		void webviewNavigating(object sender, WebNavigatingEventArgs e)
		{
			var item = (Xamarin.Forms.WebView)sender;
			Debug.WriteLine("webviewNavigating ======= " + item.HeightRequest);

			//this.labelLoading.IsVisible = true; //display the label when navigating starts
		}

		
		void webviewNavigated(object sender, WebNavigatedEventArgs e)
		{
            
            var item = (Xamarin.Forms.WebView)sender;
            Debug.WriteLine("webviewNavigated === "+ item.HeightRequest);
			//this.labelLoading.IsVisible = false; //remove the loading indicator when navigating is finished
		}

        #region News Items And Other Tap Events

        void didTapWatchlist(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            DisplayAlert("News Watchlist", newsDetails.Title, "Ok");
        }

        void didTapLike(object sender, EventArgs e)
        {
            DisplayAlert("News Like", newsDetails.Title, "Ok");
        }

        void didTapShare(object sender, EventArgs e)
        {
            DisplayAlert("News Share", newsDetails.Title, "Ok");
        }

        void didTapBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void didTapSearch(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        #endregion
    }
}
