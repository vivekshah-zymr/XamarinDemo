using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DemoApp.Views
{
    public partial class MoviePage : ContentPage
    {
        public MoviePage()
        {
            InitializeComponent();
        }

        void didTapSlider(object sender, EventArgs e)
        {
			int i = 1;

			while (Navigation.NavigationStack.Count > 0 && Navigation.NavigationStack.Count >= i)
			{
                var stackPage = Navigation.NavigationStack[i - 1];
				System.Diagnostics.Debug.WriteLine("=======: " + stackPage);
                if(stackPage.GetType() == typeof(HomePage))
                {
                    ((HomePage)stackPage).IsPresented = true;
                    break;
                }
				i++;
			}
        }

        void didTapSearch(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new HomePage());
            Navigation.PopAsync();
            //Navigation.PopAsync();
        }
    }
}
