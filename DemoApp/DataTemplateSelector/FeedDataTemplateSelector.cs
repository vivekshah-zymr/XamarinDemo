using System;
using Xamarin.Forms;
using DemoApp.Models;

namespace DemoApp
{
    public class FeedDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NewsTemplate { get; set; }
        public DataTemplate PersonTemplate { get; set; }
        public DataTemplate MovieTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is NewsModel)
            {
                return NewsTemplate;
            }
            else if (item is MovieModel)
			{
                return MovieTemplate;
			}
            else
            {
                return PersonTemplate;
            }
        }

    }
}
