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

		public ParticipantsViewModel(IPeopleDatabase peopleDB)
		{
			_peopleDatabase = peopleDB;

			People = new ObservableCollection<Person>(_peopleDatabase.GetAllPeople());
			SelectedPerson = People.FirstOrDefault();
		}

		private ObservableCollection<Person> _people = new ObservableCollection<Person>();

		public ObservableCollection<Person> People
		{
			get { return _people; }
			set { SetProperty(ref _people, value); }
		}

		private Person? _selectedPerson;
		public Person? SelectedPerson
		{
			get { return _selectedPerson; }
			set { SetProperty(ref _selectedPerson, value); }
		}

		private DelegateCommand? _addFighterCommand;
		public DelegateCommand AddFighterCommand =>
			_addFighterCommand ??= new DelegateCommand(AddFighter);
		public void AddFighter()
		{
			Person newPerson = _peopleDatabase.CreatePerson();
			People.Add(newPerson);
			SelectedPerson = newPerson;
		}

		public void UpdateFighter()
		{
			if (SelectedPerson is null)
			{
				return;
			}

			_peopleDatabase.UpdatePerson(SelectedPerson);
		}

		private DelegateCommand? _removeFighterCommand;
		public DelegateCommand RemoveFighterCommand =>
			_removeFighterCommand ??= new DelegateCommand(RemoveFighter);
		public void RemoveFighter()
		{
			if (SelectedPerson is null)
			{
				return;
			}

			_peopleDatabase.RemovePerson(SelectedPerson);
			People.Remove(SelectedPerson);
			SelectedPerson = People.FirstOrDefault();
		}

	}
}
