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
    public class GuestViewModel :BindableBase
    {

        private RoomModel selectedRoom;
        public RoomModel SelectedRoom
        {
            get { return selectedRoom; }
            set { SetProperty(ref selectedRoom, value);
                if (AddGuestCommand != null)
                    AddGuestCommand.RaiseCanExecuteChanged();
            }
        }
        private string roomName;

        public string RoomName
        {
            get { return roomName; }
            set { SetProperty(ref roomName, value); }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set { SetProperty(ref location, value); }
        }

        private ObservableCollection<GuestEntryViewModel> guestCollection = new ObservableCollection<GuestEntryViewModel>();
        public ObservableCollection<GuestEntryViewModel> GuestCollection
        {
            get { return guestCollection; }
            private set
            {
                SetProperty(ref guestCollection, value);
            }
        }


        private bool showErrorRegion;
        public bool ShowErrorRegion
        {
            get { return showErrorRegion; }
            set { SetProperty(ref showErrorRegion, value); }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        private bool showGuestList;
        public bool ShowGuestList
        {
            get { return showGuestList; }
            set { SetProperty(ref showGuestList, value); }
        }

        private DelegateCommand retryCommand;
        public DelegateCommand RetryCommand =>
            retryCommand ?? (retryCommand = new DelegateCommand(GetGuestCollection));

        private DelegateCommand addGuestCommand;
        public DelegateCommand AddGuestCommand =>
            addGuestCommand ?? (addGuestCommand = new DelegateCommand(ExecuteAddGuestCommand, CanExecuteAddGuestCommand));

        void ExecuteAddGuestCommand()
        {
            guestCollection.Add(new GuestEntryViewModel());
            ShowGuestList = guestCollection.Count > 0;
        }

        bool CanExecuteAddGuestCommand()
        {            
            return SelectedRoom != null && !(SelectedRoom.Capacity > 0 && guestCollection.Count == SelectedRoom.Capacity);
        }

        private DelegateCommand saveGuestCommand;
        public DelegateCommand SaveGuestCommand =>
            saveGuestCommand ?? (saveGuestCommand = new DelegateCommand(ExecuteSaveGuestCommand));

        void ExecuteSaveGuestCommand()
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

        public GuestViewModel(IEventAggregator ea)
        {
            ea.GetEvent<RoomSelectedEvent>().Unsubscribe(RoomSelectionChanged);
            ea.GetEvent<RoomSelectedEvent>().Subscribe(RoomSelectionChanged);
        }

        private void RoomSelectionChanged(RoomModel obj)
        {
            guestCollection.Clear();
            ShowGuestList = false;
            if(obj != null)
            {
                SelectedRoom = obj;
                RoomName = obj.RoomName;
                Location = obj.Location;
                GetGuestCollection();
            }
        }

        private void GetGuestCollection()
        {
            if (SelectedRoom == null)
                return;
            ErrorMessage = string.Empty;
            ShowErrorRegion = false;
            try
            {
                string url = "https://localhost:44392/api/";
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var responseTask = client.GetAsync("guest?id=" + SelectedRoom.RoomId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        var response = readTask.Result;
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
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Error while fetching data. Retry Again!.";
                ShowErrorRegion = true;
            }
           
        }
    }
}
