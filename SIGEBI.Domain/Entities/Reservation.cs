using System;

namespace SIGEBI.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime ReservationDate { get; set; }

        public bool Status { get; set; }
    }
    }
