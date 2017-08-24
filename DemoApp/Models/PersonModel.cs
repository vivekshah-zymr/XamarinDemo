﻿using System;
namespace DemoApp.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string CoverImage { get; set; }
        public string AvatarImage { get; set; }
        public double DateOfBirth { get; set; }
        public string About { get; set; }
    }
}
