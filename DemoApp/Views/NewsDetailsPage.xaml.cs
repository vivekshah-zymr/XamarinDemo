using System;
using System.Collections.Generic;
using Xamarin.Forms;
using DemoApp.Models;

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

		void webviewNavigating(object sender, WebNavigatingEventArgs e)
		{
			//this.labelLoading.IsVisible = true; //display the label when navigating starts
		}

		/// <summary>
		/// Called when the webview finished navigating. Hides the loading label.
		/// </summary>
		void webviewNavigated(object sender, WebNavigatedEventArgs e)
		{
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
