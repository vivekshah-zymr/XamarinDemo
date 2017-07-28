using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DemoApp.Views
{
    public partial class MusicPage : ContentPage
    {
        public MusicPage()
        {
            InitializeComponent();
            this.BindingContext = new[] { "a", "b", "c", "d", "e", "f", "g", "h" };
        }
		void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e == null) return; // has been set to null, do not 'process' tapped event
            System.Diagnostics.Debug.WriteLine("Tapped: " + e.Item);
			((ListView)sender).SelectedItem = null; // de-select the row
		}
    }
}
