using System;
namespace DemoApp.Models
{
    public class MusicModel
    {
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Length { get; set; }
		public double PostedDate { get; set; }
		public string YouTubeUrl { get; set; }
		public string YouTubeId { get; set; }
		public string YouTubeView { get; set; }
		public string ThumbnailImageUrl { get; set; }
		public int ViewCount { get; set; }
		public string PublishStatus { get; set; }
		public string ActiveStatus { get; set; }
		public string LastUpdateTime { get; set; }
    }
}
