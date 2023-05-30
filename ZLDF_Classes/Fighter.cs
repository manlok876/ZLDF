using System;
using System.Collections.Generic;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public class Fighter : BindableBase
	{

		public Guid Id { get; set; }

		private string _firstName = "Ivan";
		public string FirstName
		{
			get { return _firstName; }
			set
			{
				SetProperty(ref _firstName, value);
			}
		}
		private string _lastName = "Ivanov";
		public string LastName
		{
			get { return _lastName; }
			set
			{
				SetProperty(ref _lastName, value);
			}
		}

		private string _city = "City";
		public string City
		{
			get { return _city; }
			set
			{
				SetProperty(ref _city, value);
			}
		}
		private Club? _club = null;
		public Club? Club
		{
			get { return _club; }
			set
			{
				SetProperty(ref _club, value);
			}
		}

		// These properties are used to support many-to-many relationship in EF
		// because I am a lazy bastard who cannot be bothered to do it right
		// and because damn EF Core 6.0 for not supporting unidirected many-to-many relationships
		// TODO: remove when EF 7 comes out (hopefully)
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable IDE0051 // Remove unused private members
		private List<Nomination> _nominationsEf { get; set; } = new List<Nomination>();
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore IDE1006 // Naming Styles

		public Fighter()
		{
			Id = Guid.NewGuid();
		}
	}
}
