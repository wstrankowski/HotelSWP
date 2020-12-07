using Hotel.Models;
using Hotel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Hotel.DAL
{
    public class Repository
    {
        private static readonly string connectionString = "Server=DESKTOP-6H9P6RN\\SQLEXPRESS; Integrated Security=True; Database=Hotel;";
        private readonly SqlConnection _connection;

        public Repository()
        {
            _connection = GetConnection();
            _connection.Open();
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public IEnumerable<Convenience> GetConveniences()
        {
            var conveniences = new List<Convenience>();

            SqlCommand command = new SqlCommand("SELECT * FROM Convenience", _connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    conveniences.Add(new Convenience(
                        Convert.ToInt32(reader[0]),
                        reader[1].ToString(),
                        Convert.ToDecimal(reader[2])
                        )
                    );
                }
            }


            return conveniences;
        }

        public IEnumerable<Room> GetRooms()
        {
            var rooms = new List<Room>();

            SqlCommand command = new SqlCommand("SELECT * FROM Room", _connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rooms.Add(new Room(
                        Convert.ToInt32(reader[0]),
                        (RoomType)Enum.Parse(typeof(RoomType), reader[1].ToString(), true),
                        Convert.ToDecimal(reader[2])
                        )
                    );
                }
            }


            return rooms;
        }

        public void AddReservation(Reservation reservation)
        {
            using (SqlCommand sqlCommand = new SqlCommand { CommandText = "INSERT INTO [Client] ([Id], [Pesel]) VALUES (@id, @pesel)", Connection = _connection })
            {
                sqlCommand.Parameters.AddWithValue("@id", reservation.Client.GetId());
                sqlCommand.Parameters.AddWithValue("@pesel", reservation.Client.Pesel);
                sqlCommand.ExecuteNonQuery();
            }
            using (SqlCommand sqlCommand = new SqlCommand { CommandText = "INSERT INTO [Reservation] ([Id], [ClientId], [RoomId], [StartDate], [EndDate], [Price]) VALUES (@id, @clientId, @roomId, @startDate, @endDate, @price)", Connection = _connection })
            {
                sqlCommand.Parameters.AddWithValue("@id", reservation.GetId());
                sqlCommand.Parameters.AddWithValue("@clientId", 1);
                sqlCommand.Parameters.AddWithValue("@roomId", reservation.Room.Number);
                sqlCommand.Parameters.AddWithValue("@startDate", reservation.StartDate.ToString("yyyy-MM-dd"));
                sqlCommand.Parameters.AddWithValue("@endDate", reservation.EndDate.ToString("yyyy-MM-dd"));
                sqlCommand.Parameters.AddWithValue("@price", reservation.Price);
                sqlCommand.ExecuteNonQuery();
            }

            foreach (var item in reservation.Conveniences)
            {
                using (SqlCommand sqlCommand = new SqlCommand { CommandText = "INSERT INTO [ReservationConvenience] ([ReservationId], [ConvenienceId]) VALUES (@reservationId, @convenienceId)", Connection = _connection })
                {
                    sqlCommand.Parameters.AddWithValue("@reservationId", reservation.GetId());
                    sqlCommand.Parameters.AddWithValue("@convenienceId", item.Id);

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
