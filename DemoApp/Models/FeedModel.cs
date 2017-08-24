using System;
using System.Collections.Generic;

namespace DemoApp.Models
{
    public class FeedModel
    {
        public List<NewsModel> newsList { get; set; }
        public List<PersonModel> personList { get; set; }
        public List<MovieModel> movieList { get; set; }
    }
}
