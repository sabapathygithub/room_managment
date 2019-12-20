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
        // GET api/values
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

        // GET api/values/5
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
                    guests = guest.Select(s=> new GuestModel
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

        // POST api/values
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

        // PUT api/values/5
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

        // DELETE api/values/5
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