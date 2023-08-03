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

		public Person AddNewPerson()
		{
			Person newPerson = new Person();
			people.Add(newPerson);
			return newPerson;
		}

		public void AddPerson(Person person)
		{
			if (people.Contains(person))
			{
				return;
			}

			people.Add(person);
		}

		public void RemovePerson(Person person)
		{
			if (people.Contains(person))
			{
				return;
			}

			people.Remove(person);
		}

		public IEnumerable<Person> GetAllPeople()
		{
			return people;
		}

		public TestPeopleDatabase()
		{

		}
	}
}
