using System;
using System.Collections.Generic;
using DemoApp.Models;
using Xamarin.Forms;

namespace DemoApp.Views
{
    public partial class PersonListPage : ContentPage
    {
        public PersonListPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var personList = App.Database.GetPersonsAsync();
            personListView.ItemsSource = personList;
        }

        async private void OnContactTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new AddPersonPage
            {
                BindingContext = e.Item as Person
            });
        }

        void didTapBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void didTapAdd(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPersonPage
            {
                BindingContext = new Person()
            });
        }
    }
}
