using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotel.Models
{
    public class Reservation
    {
        public RoomID RoomID { get; }
        public string Username { get; set; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public TimeSpan  Length => EndDate.Subtract(StartDate);

        public Reservation(RoomID roomId,string username, DateTime startTime, DateTime endTime)
        {
            RoomID = roomId;
            StartDate = startTime;
            EndDate = endTime;
            Username = username;
        }

    }
}
