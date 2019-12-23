using RoomManagment.Core;
using RoomManagment_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoomManagment_Service.Controllers
{
    public class RoomController : ApiController
    {
        
        /// <summary>
        /// Get room collection from database context.
        /// </summary>
        /// <returns>Room collection as Action Result.</returns>
        public IHttpActionResult GetRoom()
        {
            IEnumerable<RoomModel> rooms = null;
            using(var dbContext = new Room_ManagmentEntities())
            {
                rooms = dbContext.Rooms.Select(s => new RoomModel
                {
                    Address = s.Address,
                    Capacity = s.Capacity.HasValue ? s.Capacity.Value : 0,
                    Location = s.Location,
                    RoomId = s.RoomId,
                    RoomName = s.RoomName,
                    Status = dbContext.Guests.Count(j => j.RoomId == s.RoomId) < (s.Capacity.HasValue ? s.Capacity.Value : 0)

                }).ToList<RoomModel>();
                
            }
            return Ok(rooms);
        }

        /// <summary>
        /// Get room collection based on location.
        /// </summary>
        /// <param name="location">selected location in ui.</param>
        /// <returns>Room collection as Action Result.</returns>
        public IHttpActionResult GetRoom(string location)
        {
            IEnumerable<RoomModel> rooms = null;

            using (var dbContext = new Room_ManagmentEntities())
            {
                rooms = dbContext.Rooms.Where(i=>i.Location == location).Select(s => new RoomModel
                {
                    Address = s.Address,
                    Capacity = s.Capacity.HasValue ? s.Capacity.Value : 0,
                    Location = s.Location,
                    RoomId = s.RoomId,
                    RoomName = s.RoomName,
                    Status = dbContext.Guests.Count(j => j.RoomId == s.RoomId && j.CheckOutDate > DateTime.Now) < (s.Capacity.HasValue ? s.Capacity.Value : 0)

                }).ToList<RoomModel>();
            }
            return Ok(rooms);
        }

        /// <summary>
        /// Inserts the room details into the db context.
        /// </summary>
        public IHttpActionResult PostRoom(RoomModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data.");

            using(var dbContext = new Room_ManagmentEntities())
            {
                dbContext.Rooms.Add(new Room
                {
                    Address = value.Address,
                    Capacity = value.Capacity,
                    Location = value.Location,
                    RoomName = value.RoomName,
                    Status = true
                });
                dbContext.SaveChanges();
            }
            return Ok();
        }

        /// <summary>
        /// Updates the incoming room details in db context.
        /// </summary>
        /// <param name="id">Identity value of the room to be updated in db context.</param>
        /// <param name="value">Room details to be updated.</param>
        /// <returns>Http Action Result</returns>
        public IHttpActionResult PutRoom(int id, RoomModel value)
        {
            if (id <= 0)
                return BadRequest("Not a valid room id");
            using (var dbContext = new Room_ManagmentEntities())
            {
                var room = dbContext.Rooms.Where(i => i.RoomId == id).FirstOrDefault();
                if (room != null)
                {
                    room.RoomName = value.RoomName;
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
        /// Deletes the room based on incoming id.
        /// </summary>
        /// <param name="id">Identity value of the room to be deleted in db context.</param>
        /// <returns>Http Action Result</returns>
        public IHttpActionResult DeleteRoom(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid room id");

            using(var dbContext = new Room_ManagmentEntities())
            {
                var room = dbContext.Rooms.Where(i => i.RoomId == id).FirstOrDefault();
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
