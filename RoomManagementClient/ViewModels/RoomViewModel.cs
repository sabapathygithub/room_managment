using Newtonsoft.Json;
using Prism.Events;
using Prism.Mvvm;
using RoomManagment.Core;
using RoomManagementClient.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using RoomManagementClient.Helpers;

namespace RoomManagementClient.ViewModels
{
    public class RoomViewModel : BindableBase
    {
        #region Fields
        private ObservableCollection<RoomModel> roomCollection = new ObservableCollection<RoomModel>();
        private RoomModel selectedRoom;
        private IEventAggregator ea;
        private ObservableCollection<string> locationCollection = new ObservableCollection<string>();
        private string selectedLocation;
        private bool showErrorRegion;
        private string errorMessage;
        private DelegateCommand retryCommand;
        #endregion

        #region Properties
        public ObservableCollection<string> LocationCollection
        {
            get { return locationCollection; }
            set { SetProperty(ref locationCollection, value); }
        }

        public string SelectedLocation
        {
            get { return selectedLocation; }
            set { 
                SetProperty(ref selectedLocation, value); 
                if(selectedLocation != null)
                {
                    GetRooms(selectedLocation);
                }
            }
        }

        public ObservableCollection<RoomModel> RoomCollection
        {
            get { return roomCollection; }
            set { SetProperty(ref roomCollection, value); }
        }

        public RoomModel SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                SetProperty(ref selectedRoom, value);
                if (ea != null)
                    ea.GetEvent<RoomSelectedEvent>().Publish(selectedRoom);
            }
        }
        
        public bool ShowErrorRegion
        {
            get { return showErrorRegion; }
            set { SetProperty(ref showErrorRegion, value); }
        }
        
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public DelegateCommand RetryCommand =>
            retryCommand ?? (retryCommand = new DelegateCommand(OnInitialize));

        #endregion

        #region Methods

        public RoomViewModel(IEventAggregator ea)
        {
            this.ea = ea;
            OnInitialize();
        }

        async void OnInitialize()
        {
            ErrorMessage = string.Empty;
            ShowErrorRegion = false;
            try
            {
                string response = await WebApiConsumer.ConsumeGetAsync("room");
                IEnumerable<RoomModel> locations = JsonConvert.DeserializeObject<IEnumerable<RoomModel>>(response);
                foreach (var item in locations)
                {
                    locationCollection.Add(item.Location);
                }                
            }
            catch (Exception)
            {
                ErrorMessage = "Error in fetching data. Retry Again.";
                ShowErrorRegion = true;
            }
        }

       async void GetRooms(string location)
        {
            roomCollection.Clear();
            string response = await WebApiConsumer.ConsumeGetAsync("room?location=" + location);
            IEnumerable<RoomModel> rooms = JsonConvert.DeserializeObject<IEnumerable<RoomModel>>(response);
            foreach (var item in rooms)
            {
                roomCollection.Add(item);                
            }
            SelectedRoom = roomCollection.FirstOrDefault();
        }

        #endregion
    }
}
