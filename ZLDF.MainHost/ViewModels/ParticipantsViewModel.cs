using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;

using ZLDF.Core;
using ZLDF.DataAccess;

namespace ZLDF.MainHost.ViewModels
{
	public class ParticipantsViewModel : BindableBase
	{
		private IPeopleDatabase _peopleDatabase;

		private ObservableCollection<Person> _people = new ObservableCollection<Person>();

		public ObservableCollection<Person> Fighters
		{
			get { return _people; }
			set { SetProperty(ref _people, value); }
		}

		private Person? _selectedFighter;
		public Person? SelectedFighter
		{
			get { return _selectedFighter; }
			set { SetProperty(ref _selectedFighter, value); }
		}

		private DelegateCommand _addFighterCommand;
		public DelegateCommand AddFighterCommand =>
			_addFighterCommand ?? (_addFighterCommand = new DelegateCommand(AddFighter));
		public void AddFighter()
		{
			Person newPerson = _peopleDatabase.AddNewPerson();
			Fighters.Add(newPerson);
		}

		private DelegateCommand _removeFighterCommand;
		public DelegateCommand RemoveFighterCommand =>
			_removeFighterCommand ?? (_removeFighterCommand = new DelegateCommand(RemoveFighter));
		public void RemoveFighter()
		{
			if (SelectedFighter is null)
			{
				return;
			}

			_peopleDatabase.RemovePerson(SelectedFighter);
			Fighters.Remove(SelectedFighter);
			SelectedFighter = Fighters.FirstOrDefault();
		}

		public ParticipantsViewModel(IPeopleDatabase peopleDB)
		{
			_peopleDatabase = peopleDB;

			Fighters = new ObservableCollection<Person>(_peopleDatabase.GetAllPeople());
			SelectedFighter = Fighters.FirstOrDefault();
		}

	}
}
