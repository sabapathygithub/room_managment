using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RoomManagment.Core.Models;
using RoomManagment_Service.Models;

namespace RoomManagment_Service.Controllers
{
    public class UsersController : ApiController
    {
        /// <summary>
        /// Gets the collection of user.
        /// </summary>
        public IHttpActionResult GetUsers()
        {
            IEnumerable<UserModel> users = null;
            using (Room_ManagmentEntities db = new Room_ManagmentEntities())
            {
                users = db.Users.Select(s => new UserModel
                {
                    IsAdmin = s.IsAdmin.HasValue ? s.IsAdmin.Value : false,
                    Login = s.Login,
                    Password = s.Password,
                    UserId = s.UserId,
                    UserName = s.UserName
                }).ToList<UserModel>();
            }
            return Ok(users);
        }

        /// <summary>
        /// Saves the user value into db context.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(UserModel user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserModel userModel = null;
            using (Room_ManagmentEntities db = new Room_ManagmentEntities())
            {
                bool isAdmin = !(db.Users.Count() > 0);
                db.Users.Add(new Models.User
                {
                    IsAdmin = isAdmin,
                    Login = user.Login,
                    Password = user.Password,
                    CreatedDate = DateTime.Now,
                    UserName = user.UserName
                });
                db.SaveChanges();                
            }
            return Ok(userModel);
        }
    }
}