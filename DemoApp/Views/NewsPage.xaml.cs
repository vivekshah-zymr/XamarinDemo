using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using DemoApp.Models;
using System.Linq;

namespace DemoApp.Views
{
    public partial class NewsPage : ContentPage
    {
        public ObservableCollection<NewsModel> newsArray { get; set; }
        public NewsPage()
        {
            InitializeComponent();
            newsArray = new ObservableCollection<NewsModel>();
            newsArray.Add(new NewsModel { newsID = 1, Name = "Shahid Kapoor Called Mira Rajput’s Father To Apologise – Here’s Why!", Type = "My daddy strongest!", Image = "https://dopypjfu5v10j.cloudfront.net/images/news/25224/dc98aec8-e332-40f8-8af6-eda2ed4bd8b2shahid-kapoor-759.jpg" });
            newsArray.Add(new NewsModel { newsID = 2, Name = "Abhay Deol Awarded The Valerian Of Racism For Raising His Voice Against Fairness Creams", Type = "Well done Abhay!", Image = "https://dopypjfu5v10j.cloudfront.net/images/news/25223/4d5038ec-2918-4110-9701-c488df36d2b4download2.jpeg" });
            newsArray.Add(new NewsModel { newsID = 3, Name = "Ranbir Kapoor on Sanjay Dutt biopic: I can't believe how much hell can a man go through in his life", Type = "Ranbir Kapoor opens up on Sanjay Dutt biopic.", Image = "https://dopypjfu5v10j.cloudfront.net/images/news/25222/cf3f1f8c-4f75-4dac-a3ad-f5e91716b6c1download1.jpeg" });
            newsListView.ItemsSource = newsArray;
        }

        void didTapBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        void didTapSearch(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        // void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //  if (e.SelectedItem == null)
        //  {
        //      return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
        //  }
        //          var displayTxt = ((NewsModel)e.SelectedItem).newsID.ToString() + " " + ((NewsModel)e.SelectedItem).Type;
        //  //DisplayAlert("Item Selected", displayTxt , "OK");
        //  var newsDetailsPage = new NewsDetailsPage(displayTxt);
        //  Navigation.PushAsync(newsDetailsPage);
        //}

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            ((ListView)sender).SelectedItem = null;
            var displayTxt = ((NewsModel)e.Item).newsID.ToString() + " " + ((NewsModel)e.Item).Type;
            //DisplayAlert("Item Tapped", displayTxt, "OK");

            var newsDetailsPage = new NewsDetailsPage();
            newsDetailsPage.BindingContext = new { NewsTitle = displayTxt };
            Navigation.PushAsync(newsDetailsPage);
        }

        void didTapWatchlist(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            NewsModel listitem = (from news in newsArray where news.newsID == (int)item.CommandParameter select news).FirstOrDefault<NewsModel>();
            DisplayAlert("News Watchlist", listitem.Type, "Ok");
        }

        void didTapLike(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            NewsModel listitem = (from news in newsArray where news.newsID == (int)item.CommandParameter select news).FirstOrDefault<NewsModel>();
            DisplayAlert("News Like", listitem.Type, "Ok");
        }

        void didTapShare(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            NewsModel listitem = (from news in newsArray where news.newsID == (int)item.CommandParameter select news).FirstOrDefault<NewsModel>();
            DisplayAlert("News Share", listitem.Type, "Ok");
        }
    }
}
