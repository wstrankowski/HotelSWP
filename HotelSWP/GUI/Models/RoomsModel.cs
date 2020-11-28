using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSWP.GUI.Models
{
    public class RoomsModel : BaseModel
    {
        public int GuestsNumber { get; set; }
        public IEnumerable<string> SelectedConveniences { get; set; }

        public override bool IsCompleted()
        {
            return GuestsNumber != 0;
        }
    }
}
