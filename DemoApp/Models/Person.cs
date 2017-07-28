using System;
using SQLite;
using System.ComponentModel;
using SQLite.Net.Attributes;

namespace DemoApp.Models
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
    }
}
