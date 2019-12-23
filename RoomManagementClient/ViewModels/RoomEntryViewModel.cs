using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using RoomManagementClient.Helpers;
using RoomManagment.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RoomManagementClient.ViewModels
{
    public class RoomEntryViewModel : BindableBase
    {
        private string roomName;
        public string RoomName
        {
            get { return roomName; }
            set { SetProperty(ref roomName, value); }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        private string location;

        public string Location
        {
            get { return location; }
            set { SetProperty(ref location, value); }
        }

        private int capacity;
        public int Capacity
        {
            get { return capacity; }
            set { SetProperty(ref capacity, value); }
        }

        private string roomType;
        public string RoomType
        {
            get { return roomType; }
            set { SetProperty(ref roomType, value); }
        }

        private List<string> roomTypes = new List<string> { "Both", "Male Only", "Female Only", "Transgender Only" };
        public List<string> RoomTypes
        {
            get { return roomTypes; }
            set { SetProperty(ref roomTypes, value); }
        }

        private DelegateCommand saveCommand;
        public DelegateCommand SaveCommand =>
            saveCommand ?? (saveCommand = new DelegateCommand(ExecuteSaveCommand));

        private DelegateCommand clearCommand;
        public DelegateCommand ClearCommand =>
            clearCommand ?? (clearCommand = new DelegateCommand(ExecuteClearCommand));

        void ExecuteClearCommand()
        {
            ClearFields();
        }

        async void ExecuteSaveCommand()
        {
            RoomModel roomModel = new RoomModel
            {
                Address = this.Address,
                Capacity = this.Capacity,
                Location = this.Location,
                RoomName = this.RoomName
            };
            try
            {
                string response = await WebApiConsumer.ConsumePostAsJsonAsync("room", roomModel);
                
            }
            catch(Exception)
            {
                MessageBox.Show("Error occurs while saving. Retry Again!");
            }
        }

        bool CanExecuteSaveCommand()
        {
            return !string.IsNullOrEmpty(RoomName) && !string.IsNullOrEmpty(RoomType) && !string.IsNullOrEmpty(Location) && Capacity > 0;
        }

        void ClearFields()
        {
            this.Address = string.Empty;
            this.Capacity = 0;
            this.Location = string.Empty;
            this.RoomType = string.Empty;
            this.RoomName = string.Empty;
        }
    }
}
