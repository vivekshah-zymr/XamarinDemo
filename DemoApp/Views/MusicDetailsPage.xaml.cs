using System;
using System.Collections.Generic;
using DemoApp.Models;
using Xamarin.Forms;
using Octane.Xam.VideoPlayer;

namespace DemoApp.Views
{
    public partial class MusicDetailsPage : ContentPage
    {
        public MusicModel musicDetails;
        public MusicDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "allowLandScapePortrait");
        }

        //during page close setting back to portrait
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send(this, "preventLandScape");
        }

        #region News Items And Other Tap Events

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
