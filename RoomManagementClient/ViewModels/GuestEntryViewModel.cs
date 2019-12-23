using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace RoomManagementClient.ViewModels
{
    public class GuestEntryViewModel : BindableBase
    {
        private string guestName;
        public string GuestName
        {
            get { return guestName; }
            set { SetProperty(ref guestName, value); }
        }

        private string sex;
        public string Sex
        {
            get { return sex; }
            set { SetProperty(ref sex, value); }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set { SetProperty(ref age, value); }
        }

        private DateTime? checkInDate;
        public DateTime? CheckInDate
        {
            get { return checkInDate; }
            set { SetProperty(ref checkInDate, value); }
        }

        private DateTime? checkOutDate;
        public DateTime? CheckOutDate
        {
            get { return checkOutDate; }
            set { SetProperty(ref checkOutDate, value); }
        }

        private List<string> genderList = new List<string> { "Male", "Female", "Transgender" };
        public List<string> GenderList
        {
            get { return genderList; }
            set { SetProperty(ref genderList, value); }
        }

        private bool isEditable = true;
        public bool IsEditable
        {
            get { return isEditable; }
            set { SetProperty(ref isEditable, value); }
        }

        public long GuestId { get; set; }

        public long RoomId { get; set; }

    }
}
