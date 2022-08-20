using System;

namespace AnexWebAPI.Models
{
    public class ReservationModel
    {
        public long Id { get; set; }
        public long HotelId { get; set; }
        public string HotelName { get; set; }
        public DateTime Date { get; set; }
        public long TotalPax { get; set; }
        public string ReservationNumber { get; set; }

        public ReservationModel()
        {
            Id = 0;
            HotelId = 0;
            HotelName = "";
            Date = new DateTime(1900, 1, 1);
            TotalPax = 0;
            ReservationNumber = "";
        }
    }
}