using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagment.Core
{
    public class RoomModel
    {
        public long RoomId { get; set; }
        public string RoomName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string RoomType { get; set; }
        public bool Status { get; set; }
    }
}
