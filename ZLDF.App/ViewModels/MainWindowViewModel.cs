using Prism.Mvvm;

namespace ZLDF.App.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
		private string _title = "ZLDF";
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public MainWindowViewModel()
		{

		}
	}
}
