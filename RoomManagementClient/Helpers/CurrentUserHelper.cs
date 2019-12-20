using RoomManagment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagementClient.Helpers
{
    public sealed class CurrentUserHelper
    {
        private static CurrentUserHelper userHelper = null;
        private static readonly object padlock = new object();

        /// <summary>
        /// Gets or privately sets the value of current logged in user.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets or privately sets the value indicates whether the current logged in user is a admin or normal user.
        /// </summary>
        public bool IsAdmin { get; private set; }

        /// <summary>
        /// Gets or privately sets the value of user id.
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// Gets the instance of the singleton class
        /// </summary>
        public static CurrentUserHelper Instance
        {
            get
            {
                if(userHelper == null)
                {
                    lock(padlock)
                    {
                        if(userHelper == null)
                        {
                            userHelper = new CurrentUserHelper();
                        }
                    }
                }
                return userHelper;
            }
        }

        private CurrentUserHelper()
        {

        }

        /// <summary>
        /// Sets the user name and admim flag for the properties with in singleton class.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="isAdmin"></param>
        public void SetUserDetails(UserModel userModel)
        {
            UserName = userModel.UserName;
            IsAdmin = userModel.IsAdmin;
            UserId = userModel.UserId;
        }

    }
}
