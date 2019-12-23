using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagment.Core.Models
{
    /// <summary>
    /// Holds the properties of guest entity.
    /// </summary>
    public class GuestModel
    {
        public long GuestId { get; set; }
        public string GuestName { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public long RoomId { get; set; }

        public DateTime? CheckInDate { get; set; }

        public DateTime? CheckOutDate { get; set; }
    }
}
