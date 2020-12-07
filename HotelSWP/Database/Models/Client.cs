using System;

namespace Hotel.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Pesel { get; set; }

        public Client(string pesel)
        {
            Id = new Random().Next(1, int.MaxValue);

            Pesel = pesel;
        }

        public int GetId() => Id;
    }
}
