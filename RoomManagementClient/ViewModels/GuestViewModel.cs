using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using RoomManagementClient.Events;
using RoomManagementClient.Helpers;
using RoomManagment.Core;
using RoomManagment.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;

namespace RoomManagementClient.ViewModels
{
    public class GuestViewModel : BindableBase
    {
        #region Fields
        private ObservableCollection<GuestEntryViewModel> guestCollection = new ObservableCollection<GuestEntryViewModel>();
        private RoomModel selectedRoom;
        private string roomName;
        private string location;        
        private bool showErrorRegion;
        private string errorMessage;
        private bool showGuestList;
        private DelegateCommand retryCommand;
        private DelegateCommand addGuestCommand;
        private DelegateCommand saveGuestCommand;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value which indicates the selected room.
        /// </summary>
        public RoomModel SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                SetProperty(ref selectedRoom, value);
                if (AddGuestCommand != null)
                    AddGuestCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets a string value which indicates the room name.
        /// </summary>
        public string RoomName
        {
            get { return roomName; }
            set { SetProperty(ref roomName, value); }
        }

        /// <summary>
        /// Gets or sets a string value which indicates the location.
        /// </summary>
        public string Location
        {
            get { return location; }
            set { SetProperty(ref location, value); }
        }

        /// <summary>
        /// Gets or sets the collection of guest which entered in the UI.
        /// </summary>
        public ObservableCollection<GuestEntryViewModel> GuestCollection
        {
            get { return guestCollection; }
            private set
            {
                SetProperty(ref guestCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets a boolean value which indicates whether to show/hide the error details.
        /// </summary>
        public bool ShowErrorRegion
        {
            get { return showErrorRegion; }
            set { SetProperty(ref showErrorRegion, value); }
        }

        /// <summary>
        /// Gets or sets a string value which indicates the error message.
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        /// <summary>
        /// Gets or sets a boolean value which indicates whether to show guest list or not.
        /// </summary>
        public bool ShowGuestList
        {
            get { return showGuestList; }
            set { SetProperty(ref showGuestList, value); }
        }

        #endregion

        #region Commands
        public DelegateCommand RetryCommand =>
            retryCommand ?? (retryCommand = new DelegateCommand(GetGuestCollection));

        
        public DelegateCommand AddGuestCommand =>
            addGuestCommand ?? (addGuestCommand = new DelegateCommand(ExecuteAddGuestCommand, CanExecuteAddGuestCommand));

        
        public DelegateCommand SaveGuestCommand =>
            saveGuestCommand ?? (saveGuestCommand = new DelegateCommand(ExecuteSaveGuestCommand));

        #endregion

        #region Methods

        public GuestViewModel(IEventAggregator ea)
        {
            ea.GetEvent<RoomSelectedEvent>().Unsubscribe(RoomSelectionChanged);
            ea.GetEvent<RoomSelectedEvent>().Subscribe(RoomSelectionChanged);
        }

        /// <summary>
        /// Called when AddGuest command is executed.
        /// </summary>
        private void ExecuteAddGuestCommand()
        {
            guestCollection.Add(new GuestEntryViewModel());
            ShowGuestList = guestCollection.Count > 0;
        }

        //Provides confirmation to AddGuest command whether to execute it or not.
        private bool CanExecuteAddGuestCommand()
        {
            return SelectedRoom != null && !(SelectedRoom.Capacity > 0 && guestCollection.Count == SelectedRoom.Capacity);
        }

        //Called when the SaveGuest command is executed,
        private void ExecuteSaveGuestCommand()
        {
            try
            {
                List<GuestModel> guestModels = new List<GuestModel>();
                foreach (var item in guestCollection)
                {
                    guestModels.Add(new GuestModel
                    {
                        Age = item.Age,
                        CheckInDate = item.CheckInDate,
                        CheckOutDate = item.CheckOutDate,
                        GuestName = item.GuestName,
                        RoomId = selectedRoom.RoomId,
                        Sex = item.Sex
                    });
                }
                var response = WebApiConsumer.ConsumePostAsJsonAsync("guest", guestModels);
                MessageBox.Show("Record saved successfully");
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs while saving. Retry Again!");
            }
        }

        //Called when the room selection is changed.
        private void RoomSelectionChanged(RoomModel obj)
        {
            guestCollection.Clear();
            ShowGuestList = false;
            if (obj != null)
            {
                SelectedRoom = obj;
                RoomName = obj.RoomName;
                Location = obj.Location;
                GetGuestCollection();
            }
        }


        // Calls the web api and gets the guest collection based on selected room.
        private async void GetGuestCollection()
        {
            if (SelectedRoom == null)
                return;
            ErrorMessage = string.Empty;
            ShowErrorRegion = false;
            try
            {
                string navigateUrl = "guest?id=" + SelectedRoom.RoomId;
                string response = await WebApiConsumer.ConsumeGetAsync(navigateUrl);
                IEnumerable<GuestModel> rooms = JsonConvert.DeserializeObject<IEnumerable<GuestModel>>(response);
                foreach (var item in rooms)
                {
                    guestCollection.Add(new GuestEntryViewModel
                    {
                        Age = item.Age,
                        CheckInDate = item.CheckInDate,
                        CheckOutDate = item.CheckOutDate,
                        GuestName = item.GuestName,
                        Sex = item.Sex,
                        GuestId = item.GuestId,
                        RoomId = item.RoomId,
                        IsEditable = false
                    });
                }
                ShowGuestList = guestCollection.Count > 0;
            }
            catch (Exception)
            {
                ErrorMessage = "Error while fetching data. Retry Again!.";
                ShowErrorRegion = true;
            }
        }
        #endregion
    }
}
