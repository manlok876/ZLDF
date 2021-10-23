﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLDF.Classes
{
	class Fighter
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { 
			get
			{
				return String.Format("{0} {1}", LastName, FirstName);
			} 
		}
		public DateTime Birthdate { get; set; }

		public string City { get; set; }
		public Club Club { get; set; }

		public string Email { get; set; }
		public string Phone { get; set; }

		public Fighter()
		{
			FirstName = "Ivan";
			LastName = "Ivanov";
			Birthdate = new DateTime(2000, 1, 1);

			City = "City";
			Club = null;

			Email = "e@mail.com";
			Phone = "8 800 555 35 35";
		}
	}
}
