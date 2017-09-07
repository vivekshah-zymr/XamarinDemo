using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Octane.Xam.VideoPlayer.Constants;
using Octane.Xam.VideoPlayer.Events;
using DemoApp.Utils;

namespace DemoApp.Views
{
    public partial class MusicVideoPage : ContentPage
    {
        public MusicVideoPage()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
            //VideoPlayer.Source = YouTubeVideoIdExtension.Convert("5nyFfZnsyNY");  //5nyFfZnsyNY   RxPZh4AnWyk
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}

		void didTapBack(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		void didTapSearch(object sender, EventArgs e)
		{
			VideoPlayer.Source = YouTubeVideoIdExtension.Convert("RxPZh4AnWyk");
		}
		
        private void VideoPlayer_OnPlayerStateChanged(object sender, VideoPlayerStateChangedEventArgs e)
        {
            switch (e.CurrentState)
            {
                case PlayerState.Paused:
                case PlayerState.Prepared:
                case PlayerState.Completed:
                case PlayerState.Initialized:
                    //PauseButton.IsVisible = false;
                    //PlayButton.IsVisible = true;
                    break;
                default:
                    //PlayButton.IsVisible = false;
                    //PauseButton.IsVisible = true;
                    break;
            }
        }
    }
}
