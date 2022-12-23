using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotel.Models
{
    public class Hotel
    {
        private readonly ReservationBook _reservationBook;

        public string name { get; }

        public Hotel(string name, ReservationBook reservationBook)
        {
            this.name = name;

            _reservationBook = reservationBook;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationBook.GetAllReservations();
        }

        public async Task MakeReservation(Reservation reservation)
        {
            await _reservationBook.AddReservation(reservation);
        }


    }
}
