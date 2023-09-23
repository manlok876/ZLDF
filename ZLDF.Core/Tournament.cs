using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace ZLDF.Core
{
	public class Tournament : BindableBase
	{
		private Guid _id;
		public Guid Id
		{
			get => _id;
			set { SetProperty(ref _id, value); }
		}

		private string _title = "New Tournament";
		public string Title
		{
			get => _title;
			set { SetProperty(ref _title, value); }
		}

		private IList<Person> _participants = new List<Person>();
		public ICollection<Person> Participants
		{
			get => _participants;
			set { SetProperty(ref _participants, new List<Person>(value)); }
		}

		private IList<Nomination> _nominations = new List<Nomination>();
		public ICollection<Nomination> Nominations
		{
			get => _nominations;
			set { SetProperty(ref _nominations, new List<Nomination>(value)); }
		}

	}
}
