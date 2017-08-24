using System;
namespace DemoApp.Models
{
    public class MovieModel
    {
		public int Id { get; set; }
		public string Title { get; set; }
		public string CoverImage { get; set; }
		public string PosterImage { get; set; }
		public double ReleasedDate { get; set; }
		public string ReleasedYear { get; set; }
    }
}
