using Prism.Events;
using Prism.Mvvm;

namespace RoomManagementClient.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Room Management";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IEventAggregator ea)
        {

        }
    }
}
