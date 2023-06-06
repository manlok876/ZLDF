using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLDF.Classes
{
	public abstract class Role : BindableBase
	{
		public Guid Id
		{
			get;
			set;
		} = Guid.NewGuid();

		public Person? Person
		{
			get;
			set;
		}
	}
}
