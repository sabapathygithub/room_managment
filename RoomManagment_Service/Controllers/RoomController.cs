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
        // GET api/values
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
                    Status = s.Status.HasValue ? s.Status.Value : false

                }).ToList<RoomModel>();
                
            }
            return Ok(rooms);
        }

        // GET api/values/5
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
                    Status = s.Status.HasValue ? s.Status.Value : false

                }).ToList<RoomModel>();
            }
            return Ok(rooms);
        }

        // POST api/values
        public IHttpActionResult PostRoom(RoomModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data.");

            using(var dbContenxt = new Room_ManagmentEntities())
            {
                dbContenxt.Rooms.Add(new Room
                {
                    Address = value.Address,
                    Capacity = value.Capacity,
                    Location = value.Location,
                    RoomName = value.RoomName,
                    Status = true
                });
                dbContenxt.SaveChanges();
            }
            return Ok();
        }

        // PUT api/values/5
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

        // DELETE api/values/5
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
