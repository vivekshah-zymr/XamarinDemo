using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using DemoApp.Models;
using System.Linq;
using Acr.UserDialogs;

namespace DemoApp.Views
{
    public partial class NewsPage : ContentPage
    {
        List<NewsModel> newsList;
        ObservableCollection<NewsModel> newsCollection;
        int pageNumberNews = 1;
        bool isLoading;

        #region Basic Page Methods

        public NewsPage()
        {
            InitializeComponent();
            newsList = new List<NewsModel>();
            getNews();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        #endregion

        #region Table Related Methods

        async void getNews()
        {
            isLoading = true;
            newsLoading.IsRunning = true;
            UserDialogs.Instance.ShowLoading("", MaskType.Clear);
            List<NewsModel> currentNewsList = await App.loginManager.getNewsAPICall(pageNumberNews);
            if (currentNewsList != null && currentNewsList.Count() > 0)
            {
                newsList.AddRange(currentNewsList);
                newsCollection = new ObservableCollection<NewsModel>(newsList);
                newsListView.ItemsSource = newsCollection;
                isLoading = false;
            }
            newsLoading.IsRunning = false;
            UserDialogs.Instance.HideLoading();
        }

        // void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //  if (e.SelectedItem == null)
        //  {
        //      return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
        //  }
        //          var displayTxt = ((NewsModel)e.SelectedItem).Id.ToString() + " " + ((NewsModel)e.SelectedItem).Type;
        //  //DisplayAlert("Item Selected", displayTxt , "OK");
        //  var newsDetailsPage = new NewsDetailsPage(displayTxt);
        //  Navigation.PushAsync(newsDetailsPage);
        //}

        public void cellItemAppeared(object sender, ItemVisibilityEventArgs e)
        {
            if (isLoading || newsList.Count == 0)
                return;

            if (newsListView != null && e.Item == newsList[newsList.Count - 1])
            {
                pageNumberNews++;
                getNews();
            }
        }
        #endregion

        #region News Items And Other Tap Events

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            ((ListView)sender).SelectedItem = null;
            var displayTxt = ((NewsModel)e.Item).Id.ToString() + " " + ((NewsModel)e.Item).Title;
            //DisplayAlert("Item Tapped", displayTxt, "OK");

            var newsDetailsPage = new NewsDetailsPage();
            newsDetailsPage.newsDetails = (NewsModel)e.Item;
            newsDetailsPage.BindingContext = (NewsModel)e.Item;
            Navigation.PushAsync(newsDetailsPage);
        }

        void didTapWatchlist(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            NewsModel listitem = (from news in newsList where news.Id == (int)item.CommandParameter select news).FirstOrDefault<NewsModel>();
            DisplayAlert("News Watchlist", listitem.Title, "Ok");
        }

        void didTapLike(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            NewsModel listitem = (from news in newsList where news.Id == (int)item.CommandParameter select news).FirstOrDefault<NewsModel>();
            DisplayAlert("News Like", listitem.Title, "Ok");
        }

        void didTapShare(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            NewsModel listitem = (from news in newsList where news.Id == (int)item.CommandParameter select news).FirstOrDefault<NewsModel>();
            DisplayAlert("News Share", listitem.Title, "Ok");
        }

        void didTapBack(object sender, EventArgs e)
        {
			var mainPage = Xamarin.Forms.Application.Current.MainPage;
			var homePage = mainPage.Navigation.NavigationStack.LastOrDefault();
			if (homePage is HomePage)
			{
				TabbedPage tb = (TabbedPage)((HomePage)homePage).Detail;
				tb.CurrentPage = tb.Children[0];
			}
        }

        void didTapSearch(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        #endregion
    }
}
