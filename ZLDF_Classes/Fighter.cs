using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public class Fighter : BindableBase
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { 
			get
			{
				return $"{LastName} {FirstName}";
			} 
		}
		public DateTime Birthday { get; set; }

		public string City { get; set; }
		public Club? Club { get; set; }

		public string Email { get; set; }
		public string Phone { get; set; }

		public Fighter()
		{
			Id = "noID";
			FirstName = "Ivan";
			LastName = "Ivanov";
			Birthday = new DateTime(2000, 1, 1);

			City = "City";
			Club = null;

			Email = "e@mail.com";
			Phone = "8 800 555 35 35";
		}
	}
}
