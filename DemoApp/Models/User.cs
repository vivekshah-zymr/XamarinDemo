using System;
namespace DemoApp.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string ProfileImage { get; set; }
        public string Authorization { get; set; }

        public byte[] ProfilePicData { get; set; }
    }
}
