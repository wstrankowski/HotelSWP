using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class ReservationConvenience
    {
        public int ReservationId { get; set; }
        public int ConvenienceId { get; set; }

        public ReservationConvenience(int reservationId, int convenienceId)
        {
            ReservationId = reservationId;
            ConvenienceId = convenienceId;
        }
    }
}
