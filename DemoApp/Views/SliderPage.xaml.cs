using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DemoApp.Views
{
    public partial class SliderPage : ContentPage
    {
        public SliderPage()
        {
            InitializeComponent();
        }

        void didTapLogout(object sender, EventArgs e)
        {
			System.Diagnostics.Debug.WriteLine("=====NavigationStack===== " + Navigation.NavigationStack.Count);
			int i = 1;
			while (Navigation.NavigationStack.Count > 0 && Navigation.NavigationStack.Count >= i)
			{
				System.Diagnostics.Debug.WriteLine("=======: " + Navigation.NavigationStack[i - 1]);
				i++;
			}

			System.Diagnostics.Debug.WriteLine("=====ModalStack===== " + Navigation.ModalStack.Count);
			int j = 1;

			while (Navigation.ModalStack.Count > 1 && Navigation.ModalStack.Count >= i)
			{
				System.Diagnostics.Debug.WriteLine("=======: " + Navigation.ModalStack[i - 1]);
				j++;
			}
			//Navigation.RemovePage(this);

			Application.Current.MainPage = new NavigationPage(new StartPage());
        }
    }
}
