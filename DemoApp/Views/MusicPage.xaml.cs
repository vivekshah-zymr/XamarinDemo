using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using DemoApp.Models;
using System.Linq;
using Acr.UserDialogs;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Splat;

namespace DemoApp.Views
{
    public partial class MusicPage : ContentPage
    {
        List<MusicModel> musicList;
        ObservableCollection<MusicModel> musicCollection;
        int pageNumberMusic = 1;
        bool isLoading;
        public bool isActive;

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
                //set dummy like and watchlist 
                int i = 0;
                foreach (MusicModel m in currentMusicList)
                {
                    if (i % 2 == 0)
                    {
                        m.isWatchlist = true;
                    }
                    if (i % 4 == 0)
                    {
                        m.isLike = true;
                    }
                    i++;
                }
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

            var musicDetailsPage = new MusicDetailsPage();
            musicDetailsPage.musicDetails = (MusicModel)e.Item;
            musicDetailsPage.BindingContext = (MusicModel)e.Item;
            Navigation.PushAsync(musicDetailsPage);
        }

        async void didTapWatchlist(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            MusicModel listitem = (from music in musicList where music.Id == (int)item.CommandParameter select music).FirstOrDefault<MusicModel>();
            listitem.isWatchlist = !listitem.isWatchlist;
            item.Image = listitem.isWatchlist ? "iconWatchlistSelected" : "iconWatchlistBlack";

            string msg = listitem.isWatchlist ? "Added to My List!" : "Remove from My List!";
            UserDialogs.Instance.ShowLoading(msg, MaskType.None);
            await Task.Delay(800);
            UserDialogs.Instance.HideLoading();
            //DisplayAlert("Music Watchlist",x listitem.Title, "Ok");
        }
		
		void didTapLike(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            MusicModel listitem = (from music in musicList where music.Id == (int)item.CommandParameter select music).FirstOrDefault<MusicModel>();
            listitem.isLike = !listitem.isLike;
            item.Image = listitem.isLike ? "iconLikeSelected" : "iconLikeBlack";
            //DisplayAlert("Music Like", listitem.Title, "Ok");
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

