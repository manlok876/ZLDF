using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLDF.Core;
using ZLDF.DataAccess;

namespace ZLDF.Temp.Services
{
	internal class TestPeopleDatabase : IPeopleDatabase
	{
		private List<Person> people = new List<Person>()
		{
			new Person(),
			new Person()
		};

		Person IPeopleDatabase.AddNewPerson()
		{
			Person newPerson = new Person();
			people.Add(newPerson);
			return newPerson;
		}

		void IPeopleDatabase.AddOrUpdatePerson(Person person)
		{
		}

		public void RemovePerson(Person person)
		{
			if (people.Contains(person))
			{
				return;
			}

			people.Remove(person);
		}

		IEnumerable<Person> IPeopleDatabase.GetAllPeople()
		{
			return people;
		}

		public TestPeopleDatabase()
		{

		}
	}
}
