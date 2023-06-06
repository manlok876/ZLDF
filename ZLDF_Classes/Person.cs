using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLDF.Classes
{
	public class Person : BindableBase
	{
		public Guid Id
		{
			get;
			set;
		}

		private string _firstName = "Ivan";
		public string FirstName
		{
			get => _firstName;
			set
			{
				SetProperty(ref _firstName, value);
			}
		}

		private string _lastName = "Ivanov";
		public string LastName
		{
			get => _lastName;
			set
			{
				SetProperty(ref _lastName, value);
			}
		}

		private string _city = "City";
		public string City
		{
			get => _city;
			set
			{
				SetProperty(ref _city, value);
			}
		}

		#region Clubs

		private ObservableCollection<Club> _clubs = new();
		public IEnumerable<Club> Clubs
		{
			get => _clubs;
			private set
			{
				SetProperty(ref _clubs, new ObservableCollection<Club>(value));
			}
		}
		public Club? RepresentedClub
		{
			get => _clubs.FirstOrDefault();
		}

		public void AddClub(Club club)
		{
			if (_clubs.Contains(club))
			{
				return;
			}

			_clubs.Add(club);
			_clubs.Insert(0, club);
		}
		public void RemoveClub(Club club)
		{
			_clubs.Remove(club);
		}

		#endregion // Clubs

		public Person()
		{
			Id = Guid.NewGuid();
		}
	}
}
