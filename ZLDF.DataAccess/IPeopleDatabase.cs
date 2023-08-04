using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLDF.Core;

namespace ZLDF.DataAccess
{
	public interface IPeopleDatabase
	{
		public Person CreatePerson();
		public void AddPerson(Person person);
		public void RemovePerson(Person person);

		public IEnumerable<Person> GetAllPeople();

	}
}
