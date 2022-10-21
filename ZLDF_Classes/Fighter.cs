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
		private string _city;
		private Club? _club;

		public Guid Id { get; private set; }
		public string FirstName
		{
			get { return _firstName; }
			set
			{
				SetProperty(ref _firstName, value);
				RaisePropertyChanged(nameof(FullName));
			}
		}
		public string LastName
		{
			get { return _lastName; }
			set
			{
				SetProperty(ref _lastName, value);
				RaisePropertyChanged(nameof(FullName));
			}
		}
		public string FullName
		{
			get
			{
				return $"{LastName} {FirstName}";
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
			_firstName = "Ivan";
			_lastName = "Ivanov";

			_city = "City";
			_club = null;
		}
	}
}
