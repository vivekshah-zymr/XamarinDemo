using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using DemoApp.Models;
using System.Linq;
using Acr.UserDialogs;

namespace DemoApp.Views
{
    public partial class MusicPage : ContentPage
    {
        List<MusicModel> musicList;
        ObservableCollection<MusicModel> musicCollection;
        int pageNumberMusic = 1;
        bool isLoading;

        #region Basic Page Methods

        public MusicPage()
        {
            InitializeComponent();
            musicList = new List<MusicModel>();
            getMusic();
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

        async void getMusic()
        {
            isLoading = true;
            musicLoading.IsRunning = true;
            UserDialogs.Instance.ShowLoading("", MaskType.Clear);
            List<MusicModel> currentMusicList = await App.loginManager.getMusicAPICall(pageNumberMusic);
            if (currentMusicList != null && currentMusicList.Count() > 0)
            {
                musicList.AddRange(currentMusicList);
                musicCollection = new ObservableCollection<MusicModel>(musicList);
                musicListView.ItemsSource = musicCollection;
                isLoading = false;
            }
            musicLoading.IsRunning = false;
            UserDialogs.Instance.HideLoading();
        }

        public void cellItemAppeared(object sender, ItemVisibilityEventArgs e)
        {
            if (isLoading || musicList.Count == 0)
                return;

            if (musicListView != null && e.Item == musicList[musicList.Count - 1])
            {
                pageNumberMusic++;
                getMusic();
            }
        }
        #endregion

        #region Music Items And Other Tap Events

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            ((ListView)sender).SelectedItem = null;
            var displayTxt = ((MusicModel)e.Item).Id.ToString() + " " + ((MusicModel)e.Item).Title;
            //DisplayAlert("Item Tapped", displayTxt, "OK");

            //var newsDetailsPage = new NewsDetailsPage();
            //newsDetailsPage.newsDetails = (NewsModel)e.Item;
            //newsDetailsPage.BindingContext = (NewsModel)e.Item;
            //Navigation.PushAsync(newsDetailsPage);
        }

        void didTapWatchlist(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            MusicModel listitem = (from music in musicList where music.Id == (int)item.CommandParameter select music).FirstOrDefault<MusicModel>();
            DisplayAlert("Music Watchlist", listitem.Title, "Ok");
        }

        void didTapLike(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            MusicModel listitem = (from music in musicList where music.Id == (int)item.CommandParameter select music).FirstOrDefault<MusicModel>();
            DisplayAlert("Music Like", listitem.Title, "Ok");
        }

        void didTapShare(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            MusicModel listitem = (from music in musicList where music.Id == (int)item.CommandParameter select music).FirstOrDefault<MusicModel>();
            DisplayAlert("Music Share", listitem.Title, "Ok");
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

