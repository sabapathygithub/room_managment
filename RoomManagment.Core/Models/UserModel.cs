using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagment.Core.Models
{
    /// <summary>
    /// Holds the properties of user entity.
    /// </summary>
    public class UserModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
