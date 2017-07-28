using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DemoApp.Views
{
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            this.MasterBehavior = MasterBehavior.Popover;
			//menuPage.ItemChanged += OnItemChanged;
        }

		//private void OnItemChanged(ViewModels.MenuItem item)
		//{
		//	Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
		//	if (Device.Idiom == TargetIdiom.Phone)
		//	{
		//		IsPresented = false;
		//	}
		//}
    }
}
