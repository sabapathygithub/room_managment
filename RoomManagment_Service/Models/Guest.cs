//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoomManagment_Service.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Guest
    {
        public long GuestId { get; set; }
        public string GuestName { get; set; }
        public string Sex { get; set; }
        public Nullable<int> Age { get; set; }
        public long RoomId { get; set; }
        public Nullable<System.DateTime> CheckInDate { get; set; }
        public Nullable<System.DateTime> CheckOutDate { get; set; }
    
        public virtual Room Room { get; set; }
    }
}