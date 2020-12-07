using HotelSWP.ASR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSWP.GUI.Models
{
    public class DatesModel : BaseModel
    {
        public string arrivalDay;

        public string arrivalMonth;

        public string arrivalYear;

        public string departureDay;

        public string departureMonth;

        public string departureYear;

        public override bool IsCompleted()
        {
            return arrivalDay != null && departureDay != null;
        }

        public DateTime GetStartDate()
        {
            return new DateTime(int.Parse(arrivalYear), GetMonthNumber(arrivalMonth), int.Parse(arrivalDay));
        }

        public DateTime GetEndDate()
        {
            return new DateTime(int.Parse(departureYear), GetMonthNumber(departureMonth), int.Parse(departureDay));
        }
        private int GetMonthNumber(string arrivalMonth)
        {
            var months = new List<string> { "stycznia", "lutego", "marca", "kwietnia", "maja", "czerwca", "lipca", "sierpnia", "września", "października", "listopada", "grudnia" };
            return months.IndexOf(arrivalMonth) + 1;
        }
    }
}
