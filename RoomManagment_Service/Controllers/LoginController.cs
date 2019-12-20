using RoomManagment.Core.Models;
using RoomManagment_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace RoomManagment_Service.Controllers
{
    public class LoginController : ApiController
    {

        [ResponseType(typeof(User))]
        public IHttpActionResult PostLogin(UserModel user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserModel userModel = null;
            using (Room_ManagmentEntities db = new Room_ManagmentEntities())
            {
                var validUser = db.Users.Where(i => i.Login == user.Login && i.Password == user.Password).FirstOrDefault();
                if (validUser == null)
                    return BadRequest("Not a valid request");
                else
                {
                    userModel = new UserModel
                    {
                        IsAdmin = validUser.IsAdmin.HasValue ? validUser.IsAdmin.Value : false,
                        Login = validUser.Login,
                        Password = validUser.Password,
                        UserId = validUser.UserId,
                        UserName = validUser.UserName
                    };
                }
            }
            return Ok(userModel);
        }
    }
}