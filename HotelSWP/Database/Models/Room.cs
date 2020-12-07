using Hotel.Models.Enums;

namespace Hotel.Models
{
    public class Room
    {
        public Room()
        {

        }
        public Room(int number, RoomType roomType, decimal price)
        {
            Number = number;
            RoomType = roomType;
            Price = price;
        }

        public int Number { get; set; }
        public RoomType RoomType { get; set; }
        public decimal Price { get; set; }
    }
}
