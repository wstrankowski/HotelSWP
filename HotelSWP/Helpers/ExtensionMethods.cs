using Hotel.Models;
using Hotel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSWP.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<string> GetNames(this IEnumerable<Convenience> conveniences)
        {
            return conveniences.Select(x => x.Name);
        }

        public static Convenience FindConvenienceByName(this IEnumerable<Convenience> conveniences, string name)
        {
            return conveniences.FirstOrDefault(x => x.Name.Equals(name));
        }

        public static IEnumerable<Convenience> RemoveConvenienceByName(this IEnumerable<Convenience> conveniences, string name)
        {
            return conveniences.Where(x => !x.Name.Equals(name));
        }

        public static Room GetRoomByType(this IEnumerable<Room> rooms, RoomType roomType)
        {
            return rooms.FirstOrDefault(x => x.RoomType == roomType);
        }
    }
}
