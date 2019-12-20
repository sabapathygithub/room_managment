using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagment.Core.Models
{
    public class GuestModel
    {
        public long GuestId { get; set; }
        public string GuestName { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public long RoomId { get; set; }

        public Nullable<DateTime> CheckInDate { get; set; }

        public Nullable<DateTime> CheckOutDate { get; set; }
    }
}
