using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using DemoApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace DemoApp.Views
{
    public partial class BoxofficePage : ContentPage
    {
        FeedModel feeds;
        ObservableCollection<object> ListToBind = new ObservableCollection<object>();

        #region Basic Page Methods

        public BoxofficePage()
        {
            InitializeComponent();
            getFeed();
        }

        async void getFeed()
        {
            UserDialogs.Instance.ShowLoading("", MaskType.Clear);
            feeds = await App.loginManager.getFeedAPICall();
            if (feeds != null)
            {
				foreach (var item in feeds.newsList)
					ListToBind.Add(item);
					
                PersonModel p1 = new PersonModel();
                p1.lists = new List<PersonModel>();
                p1.lists.AddRange(feeds.personList);
                ListToBind.Add(p1);

                foreach (var item in feeds.movieList)
                    ListToBind.Add(item);

                feedListView.ItemsSource = ListToBind;
                //BindingContext = feeds;
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

        string getRandomImageURL(int i)
        {
            //Random rnd = new Random();
            //int valueNo = rnd.Next(1, 11);

            switch (i)
            {
                case 1:
                    return "https://cdn.pixabay.com/photo/2016/09/01/10/23/image-1635747_960_720.jpg";
                case 2:
                    return "https://cdn.pixabay.com/photo/2016/05/18/20/44/squirrel-1401509_960_720.jpg";
                case 3:
                    return "https://cdn.pixabay.com/photo/2017/01/06/19/15/soap-bubble-1958650_960_720.jpg";
                case 4:
                    return "https://cdn.pixabay.com/photo/2014/05/27/07/14/beauty-355157_960_720.jpg";
                case 5:
                    return "https://cdn.pixabay.com/photo/2016/08/05/19/29/rock-1573068_960_720.jpg";
                case 6:
                    return "https://cdn.pixabay.com/photo/2017/04/10/12/29/calm-2218409__340.jpg";
                case 7:
                    return "https://cdn.pixabay.com/photo/2015/07/28/13/15/life-864358__340.jpg";
                case 8:
                    return "https://cdn.pixabay.com/photo/2017/07/15/17/02/young-2507141__340.jpg";
                case 9:
                    return "https://cdn.pixabay.com/photo/2016/12/23/16/49/feng-shui-1927590__340.jpg";
                case 10:
                    return "https://cdn.pixabay.com/photo/2017/08/07/01/33/record-2598461__340.jpg";
                default:
                    return "https://cdn.pixabay.com/photo/2017/08/07/01/33/record-2598461__340.jpg";
            }
        }
        #endregion
    }
}
