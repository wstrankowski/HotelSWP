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
    }
}
