using System;
using System.Collections.Generic;

namespace Hotel.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public Room Room { get; set; }
        public Client Client { get; set; }
        public List<Convenience> Conveniences { get; set; }

        public Reservation(DateTime startDate, DateTime endDate, Room room)
        {
            Id = new Random().Next(1, int.MaxValue);
            StartDate = startDate;
            EndDate = endDate;
            Room = room;
            Price = CalculateCost(startDate, endDate, room.Price);
            Conveniences = new List<Convenience>();
        }

        public int GetId()
            => Id;

        public void AddConvenenienceToReservation(Convenience convenience)
        {
            Conveniences.Add(convenience);
            Price += convenience.Price;
        }

        public void AddClientToReservation(Client client)
        {
            Client = client;
        }

        private decimal CalculateCost(DateTime startDate, DateTime endDate, decimal roomPrice)
        {
            var day = (endDate - startDate).Days;

            var reservationValue = roomPrice * day;
           
            return reservationValue;
        }

        public string ConveniencesList
        {
            get
            {
                if(Conveniences.Count==0)
                {
                    return "";
                }

                var txt = "";

                foreach (var convenience in Conveniences)
                {
                    txt += convenience.Name + ", ";
                }

                return txt.Substring(0, txt.Length - 2);
            }
        }
    }
}
