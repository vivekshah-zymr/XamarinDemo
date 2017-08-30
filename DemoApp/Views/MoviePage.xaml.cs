using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using DemoApp.Models;
using System.Linq;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace DemoApp.Views
{
    public partial class MoviePage : ContentPage
    {
        FeedModel feeds;
        ObservableCollection<object> ListToBind = new ObservableCollection<object>();

        #region Basic Page Methods

        public MoviePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (feeds == null)
            {
                getFeed();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        async void getFeed()
        {
            UserDialogs.Instance.ShowLoading("", MaskType.Clear);
            feeds = await App.loginManager.getFeedAPICall();
            if (feeds != null)
            {
                foreach (var item in feeds.newsList)
                    ListToBind.Add(item);

                foreach (var item in feeds.movieList)
                    ListToBind.Add(item);

                feedListView.ItemsSource = ListToBind;
                BindingContext = feeds;
            }
            UserDialogs.Instance.HideLoading();
        }


        void didTapSlider(object sender, EventArgs e)
        {
            int i = 1;
            var mainPage = Xamarin.Forms.Application.Current.MainPage;
            while (mainPage.Navigation.NavigationStack.Count > 0 && mainPage.Navigation.NavigationStack.Count >= i)
            {
                var stackPage = mainPage.Navigation.NavigationStack[i - 1];
                System.Diagnostics.Debug.WriteLine("=======: " + stackPage);
                if (stackPage.GetType() == typeof(HomePage))
                {
                    ((HomePage)stackPage).IsPresented = true;
                    break;
                }
                i++;
            }
        }

        #endregion

        #region Table Related Methods

        public void cellItemAppeared(object sender, ItemVisibilityEventArgs e)
        {

        }

        #endregion

        #region News Items And Other Tap Events

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        void didTapWatchlist(object sender, EventArgs e)
        {

        }

        void didTapLike(object sender, EventArgs e)
        {
        }

        void didTapShare(object sender, EventArgs e)
        {

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
