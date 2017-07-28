using System;
using System.Collections.Generic;
using DemoApp.Models;
using Xamarin.Forms;

namespace DemoApp.Views
{
    public partial class AddPersonPage : ContentPage
    {
        public AddPersonPage()
        {
            InitializeComponent();
        }

        void didTapBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void didTapSave(object sender, EventArgs e)
        {
            var p = (Person)BindingContext;
            App.Database.AddPerson(p);
            Navigation.PopAsync();
        }

        void didTapDelete(object sender, EventArgs e)
        {
            var p = (Person)BindingContext;
            App.Database.DeletePerson(p.ID);
            Navigation.PopAsync();
        }
    }
}
