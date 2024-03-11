using Microsoft.Data.SqlClient;
using RazorHotel24InClass.Interfaces;
using RazorHotel24InClass.Models;
using System.Data;

namespace RazorHotel24InClass.Services
{
    public class HotelService : Connection, IHotelService
    {
        private string queryString = "SELECT Hotel_No, Name, Address from Hotel";
        private string queryStringFromID = "SELECT * FROM Hotel where Hotel_No = @ID";
        private string insertSql = "INSERT INTO Hotel VALUES(@ID, @Navn, @Adresse)";
        private string deleteSql = "Delete from Hotel where Hotel_No = @ID";
        private string updateSql = "Update Hotel set Hotel_No = @ID, Name = @Navn, Address = @Adresse where Hotel_No = @ID_2";
        // lav selv sql strengene færdige og lav gerne yderligere sqlstrings
        //private string sqlHotelByName = "select * from Hotel where Name like '@Navn'";
        private string sqlHotelByName = "SELECT * FROM Hotel WHERE Name LIKE @Navn";

        public List<Hotel> GetAllHotel()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(queryString, connection);
                    commmand.Connection.Open();
                    SqlDataReader reader = commmand.ExecuteReader();
                    while (reader.Read())
                    {
                        int hotelNr = reader.GetInt32("Hotel_No");//.GetInt32(0);
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = (string)reader["Address"];
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
                finally
                {
                    //her kommer man altid
                }
            }
            return hoteller;
        }

        public Hotel GetHotelFromId(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(queryStringFromID, connection);
                    commmand.Parameters.AddWithValue("@ID", hotelNr);
                    commmand.Connection.Open();

                    SqlDataReader reader = commmand.ExecuteReader();
                    if (reader.Read())
                    {
                        int hotelNo = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNo, hotelNavn, hotelAdr);
                        return hotel;
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
                finally
                {
                    //her kommer man altid
                }
            }
            return null;
        }

        public bool CreateHotel(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
            }
            return false;
        }

        public bool UpdateHotel(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    command.Parameters.AddWithValue("@ID_2", hotelNr);

                    command.Connection.Open();

                    int noOfRows = command.ExecuteNonQuery();
                    if (noOfRows == 1)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

        public Hotel DeleteHotel(int hotelNr)
        {
            Hotel hotel = GetHotelFromId(hotelNr);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return hotel;
        }


        public List<Hotel> GetHotelsByName(string name)
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(sqlHotelByName, connection);
                    command.Parameters.AddWithValue("@Navn", "%" + name + "%");
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int hotelnr = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdresse = reader.GetString(2);
                        Hotel h = new Hotel(hotelnr, hotelNavn, hotelAdresse);
                        hoteller.Add(h);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Der skete en database fejl! " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Der skete en generel fejl! " + ex.Message);
                }
            }
            return hoteller;
        }
    }
}
