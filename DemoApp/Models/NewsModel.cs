using System;
namespace DemoApp.Models
{
    public class NewsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string NewsType { get; set; }
        public string Url { get; set; }
        public double PostedDate { get; set; }
        public string Text { get; set; }
        public string Preview { get; set; }
        public string PosterImageUrl { get; set; }
        //public string Images { get; set; }
        public string LinkNews { get; set; }
        public string LinkMovies { get; set; }
        public string LinkPersons { get; set; }
        public string Trending { get; set; }
        public string ViewCount { get; set; }
        public string LikeCount { get; set; }
        public string FollowCount { get; set; }
        public string PublishStatus { get; set; }
        public string ActiveStatus { get; set; }
        public string LastUpdateTime { get; set; }
    }
}
