using RoomManagment.Core.Models;
using RoomManagment_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RoomManagment_Service.Controllers
{
    public class GuestController : ApiController
    {
        /// <summary>
        /// Gets the collection of guest from database context.
        /// </summary>
        /// <returns>Collection of guest as action result.</returns>
        public IHttpActionResult GetGuest()
        {
            IEnumerable<GuestModel> guests = null;
            using (var dbContext = new Room_ManagmentEntities())
            {
                guests = dbContext.Guests.Select(s => new GuestModel
                {
                   Age = s.Age.HasValue ? s.Age.Value : 0,
                   GuestId = s.GuestId,
                   GuestName = s.GuestName,
                   RoomId = s.RoomId,
                   Sex = s.Sex,
                   CheckInDate = s.CheckInDate,
                   CheckOutDate = s.CheckOutDate
                }).ToList<GuestModel>();

            }
            return Ok(guests);
        }

        /// <summary>
        /// Gets the collection of guest based on incoming room id.
        /// </summary>
        /// <param name="id">Room Identity</param>
        /// <returns>Collection of guest as action result.</returns>
        public IHttpActionResult GetGuest(int id)
        {
            IEnumerable<GuestModel> guests = null;
            if (id <= 0)
                return BadRequest("Not a valid room id");

            using (var dbContext = new Room_ManagmentEntities())
            {
                var guest = dbContext.Guests.Where(i => i.RoomId == id);
                if (guest != null)
                {
                    guests = guest.Where(i => i.CheckOutDate > DateTime.Now).Select(s=> new GuestModel
                    {
                        Age = s.Age.HasValue ? s.Age.Value : 0,
                        GuestId = s.GuestId,
                        GuestName = s.GuestName,
                        RoomId = s.RoomId,
                        Sex = s.Sex,
                        CheckInDate = s.CheckInDate,
                        CheckOutDate = s.CheckOutDate
                    }).ToList<GuestModel>();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok(guests);
        }

        /// <summary>
        /// Saves the guest collection into db context.
        /// </summary>
        /// <param name="value">Guest collection from UI</param>
        /// <returns>Action result.</returns>
        public IHttpActionResult PostGuest(IEnumerable<GuestModel> value)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data.");

            using (var dbContenxt = new Room_ManagmentEntities())
            {
                foreach (var item in value)
                {
                    dbContenxt.Guests.Add(new Guest
                    {
                        Age = item.Age,
                        CheckInDate = item.CheckInDate,
                        CheckOutDate = item.CheckOutDate,
                        GuestName = item.GuestName,
                        RoomId = item.RoomId,
                        Sex = item.Sex
                    });
                }
                dbContenxt.SaveChanges();
            }
            return Ok();
        }

        /// <summary>
        /// Updates the incoming guest value based on identity value.
        /// </summary>
        public IHttpActionResult PutGuest(int id, GuestModel value)
        {
            if (id <= 0)
                return BadRequest("Not a valid room id");
            using (var dbContext = new Room_ManagmentEntities())
            {
                var room = dbContext.Guests.Where(i => i.RoomId == id).FirstOrDefault();
                if (room != null)
                {
                    dbContext.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }

        /// <summary>
        /// Deletes the guest value based on identity value.
        /// </summary>
        public IHttpActionResult DeleteGuest(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid room id");

            using (var dbContext = new Room_ManagmentEntities())
            {
                var room = dbContext.Guests.Where(i => i.RoomId == id).FirstOrDefault();
                if (room != null)
                {
                    dbContext.Entry(room).State = System.Data.Entity.EntityState.Deleted;
                    dbContext.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
    }
}