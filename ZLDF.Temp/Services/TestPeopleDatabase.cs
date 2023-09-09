using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLDF.Core;
using ZLDF.DataAccess;
using ZLDF.Temp.EF;

namespace ZLDF.Temp.Services
{
	public class TestPeopleDatabase : IPeopleDatabase
	{
		private readonly IDatabaseService _database;
		public DatabaseReference DbReference => _database.DbReference;

		public TestPeopleDatabase(IDatabaseService database)
		{
			_database = database;
		}

		public Person CreatePerson()
		{
			Person newPerson = new Person();
			AddPerson(newPerson);
			return newPerson;
		}

		public void AddPerson(Person person)
		{
			using (PeopleDbContext dbContext =
				new PeopleDbContext(DbReference))
			{
				dbContext.Add(person);
				dbContext.SaveChanges();
			}
		}

		public void UpdatePerson(Person person)
		{
			using (PeopleDbContext dbContext =
				new PeopleDbContext(DbReference))
			{
				dbContext.Update(person);
				dbContext.SaveChanges();
			}
		}

		public void RemovePerson(Person person)
		{
			using (PeopleDbContext dbContext =
				new PeopleDbContext(DbReference))
			{
				dbContext.Remove(person);
				dbContext.SaveChanges();
			}
		}

		public IEnumerable<Person> GetAllPeople()
		{
			List<Person> result;
			using (PeopleDbContext dbContext =
				new PeopleDbContext(DbReference))
			{
				result = dbContext.Participants.ToList();
			}
			return result;
		}
	}
}
