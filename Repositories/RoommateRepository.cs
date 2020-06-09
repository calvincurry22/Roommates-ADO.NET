using System;
using System.Text;
using Microsoft.Data.SqlClient;
using Roommates.Models;
using System.Collections.Generic;

namespace Roommates.Repositories
{
    class RoommateRepository : BaseRepository
    {

        public RoommateRepository(string connectionString) : base(connectionString) { }

        public List<Roommate> GetAll()
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, FirstName, LastName, RentPortion, MoveInDate, RoomId FROM Roommate";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Roommate> roommates = new List<Roommate>();

                    while(reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int rentPortionColumnPosition = reader.GetOrdinal("RentPortion");
                        int rentPortionValue = reader.GetInt32(rentPortionColumnPosition);

                        int movedInDateColumnPosition = reader.GetOrdinal("MoveInDate");
                        DateTime movedInDateValue = reader.GetDateTime(movedInDateColumnPosition);

                        int roomIdColumnPosition = reader.GetOrdinal("RoomId");
                        int roomIdValue = reader.GetInt32(roomIdColumnPosition);


                        Roommate roommate = new Roommate
                        {
                            Id = idValue,
                            Firstname = firstNameValue,
                            Lastname = lastNameValue,
                            RentPortion = rentPortionValue,
                            MovedInDate = movedInDateValue,
                            Room = null
                        };

                        roommates.Add(roommate);
                    }
                    reader.Close();

                    return roommates;
                }
            }
        }
        public Roommate GetById(int id)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, FirstName, LastName, RentPortion, MoveInDate, RoomId FROM Roommate WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Roommate roommate = null;

                    if (reader.Read())
                    {
                        roommate = new Roommate
                        {
                            Id = id,
                            Firstname = reader.GetString(reader.GetOrdinal("FirstName")),
                            Lastname = reader.GetString(reader.GetOrdinal("LastName")),
                            RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                            MovedInDate = reader.GetDateTime(reader.GetOrdinal("MoveInDate")),
                            Room = null
                        };
                    }
                    reader.Close();

                    return roommate;
                }
            }
        }
        public List<Roommate> GetAllWithRoom(int roomId)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT roommate.Id AS RoommateId, FirstName, LastName, RentPortion, MoveInDate, RoomId, room.Name AS RoomName, room.MaxOccupancy AS Occupancy
                                        FROM Roommate roommate 
                                        JOIN Room room ON room.Id = RoomId 
                                        WHERE RoomId = @id";
       
                    cmd.Parameters.AddWithValue("@id", roomId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Roommate> roommates = new List<Roommate>();
                    while(reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("RoommateId");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int rentPortionColumnPosition = reader.GetOrdinal("RentPortion");
                        int rentValue = reader.GetInt32(rentPortionColumnPosition);

                        int moveInDateColumnPosition = reader.GetOrdinal("MoveInDate");
                        DateTime moveInDateValue = reader.GetDateTime(moveInDateColumnPosition);

                        int roomIdColumnPosition = reader.GetOrdinal("RoomId");
                        int roomIdValue = reader.GetInt32(roomIdColumnPosition);

                        int roomNameColumnPosition = reader.GetOrdinal("RoomName");
                        string roomNameValue = reader.GetString(roomNameColumnPosition);

                        int occupancyColumnPosition = reader.GetOrdinal("Occupancy");
                        int occupancyValue = reader.GetInt32(occupancyColumnPosition);



                        Roommate newRoommate = new Roommate
                        {
                            Id = idValue,
                            Firstname = firstNameValue,
                            Lastname = lastNameValue,
                            RentPortion = rentValue,
                            MovedInDate = moveInDateValue,
                            Room = new Room
                            {
                                Id = roomIdValue,
                                Name = roomNameValue,
                                MaxOccupancy = occupancyValue
                            }
                        };

                        roommates.Add(newRoommate);
                    }
                    reader.Close();
                    return roommates;
                }
            }
        }
    }

}
