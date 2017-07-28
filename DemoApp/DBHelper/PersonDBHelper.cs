using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using SQLite.Net.Async;
using DemoApp.Models;
using SQLite.Net;
using Xamarin.Forms;
using System.Linq;

namespace DemoApp.DBHelper
{
    public class PersonDBHelper
    {
        private SQLiteConnection _connection;

        public PersonDBHelper()
        {
            _connection = DependencyService.Get<IFileHelper>().GetConnection();
            _connection.CreateTable<Person>();
        }

        // GET ALL
        public List<Person> GetPersonsAsync()
        {
            return _connection.Table<Person>().ToList();
        }

        public IEnumerable<Person> GetPerson()
        {
            return (from t in _connection.Table<Person>()
                    select t).ToList();
        }

        // GET with ID
        public Person GetPerson(int id)
        {
            return _connection.Table<Person>().FirstOrDefault(t => t.ID == id);
        }

        // Delete with ID
        public void DeletePerson(int id)
        {
            _connection.Delete<Person>(id);
        }

        // add person
        public void AddPerson(Person person)
        {
            if (person.ID != 0)
            {
                _connection.Update(person);
            }
            else
            {
                _connection.Insert(person);
            }

        }
    }
}
