using Hotel.Models;
using Hotel.Models.Enums;
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
        public IEnumerable<Convenience> SelectedConveniences { get; set; }

        public override bool IsCompleted()
        {
            return GuestsNumber != 0;
        }

        public RoomType GetRoomType()
        {
            switch (GuestsNumber)
            {
                case 1:
                    return RoomType.SGL;
                case 2:
                    return RoomType.DBL;
                case 3:
                    return RoomType.TRIPLE;
                default: 
                    return default;
            }
        }
    }
}
