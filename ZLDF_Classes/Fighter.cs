using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public class Fighter : BindableBase
	{
		private string _firstName;
		private string _lastName;
		private DateOnly _birthday;
		private string _city;
		private Club? _club;
		private string _email;
		private string _phone;

		public Guid Id { get; private set; }
		public string FirstName
		{
			get { return _firstName; }
			set
			{
				SetProperty(ref _firstName, value);
			}
		}
		public string LastName
		{
			get { return _lastName; }
			set
			{
				SetProperty(ref _lastName, value);
			}
		}
		public string FullName
		{
			get
			{
				return $"{LastName} {FirstName}";
			} 
		}
		public DateOnly Birthday
		{
			get { return _birthday; }
			set
			{
				SetProperty(ref _birthday, value);
			}
		}

		public string City
		{
			get { return _city; }
			set
			{
				SetProperty(ref _city, value);
			}
		}
		public Club? Club
		{
			get { return _club; }
			set
			{
				SetProperty(ref _club, value);
			}
		}

		public string Email
		{
			get { return _email; }
			set
			{
				SetProperty(ref _email, value);
			}
		}

		public string Phone
		{
			get { return _phone; }
			set
			{
				SetProperty(ref _phone, value);
			}
		}

		// These properties are used to support many-to-many relationship in EF
		// because I am a lazy bastard who cannot be bothered to do it right
		// and because damn EF Core 6.0 for not supporting unidirected many-to-many relationships
		// TODO: remove when EF 7 comes out (hopefully)
		private List<Nomination> _nominationsEf { get; set; } = new List<Nomination>();

		public Fighter()
		{
			_firstName = "Ivan";
			_lastName = "Ivanov";
			_birthday = new DateOnly(2000, 1, 1);

			_city = "City";
			_club = null;

			_email = "e@mail.com";
			_phone = "8 800 555 35 35";
		}
	}
}
