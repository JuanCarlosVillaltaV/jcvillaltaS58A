using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using UIsraelCRUD.Models;



namespace UIsraelCRUD.Repositories
{
 public class PersonRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public PersonRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Person>().Wait();
        }

        public Task<List<Person>> 
            GetAllPersonsAsync() => 
            _database.Table<Person>().ToListAsync();

        public Task<Person>
            GetPersonAsync(int id) =>
            _database.FindAsync<Person>(id);

        public Task<int> 
            AddPersonAsync(Person person) =>
            _database.InsertAsync(person);

        public Task<int> UpdatePersonAsync(Person person) => 
            _database.UpdateAsync(person);

        public Task<int> DeletePersonAsync(Person person) =>
            _database.DeleteAsync(person);
    }
}
