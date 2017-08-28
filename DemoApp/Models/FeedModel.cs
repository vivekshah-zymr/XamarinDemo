using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DemoApp.Models
{
    public class FeedModel
    {
        public ObservableCollection<NewsModel> newsList { get; set; }
        public ObservableCollection<PersonModel> personList { get; set; }
        public ObservableCollection<MovieModel> movieList { get; set; }
    }
}
