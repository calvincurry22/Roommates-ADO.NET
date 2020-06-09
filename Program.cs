using System;
using System.Collections.Generic;
using Roommates.Models;
using Roommates.Repositories;

namespace Roommates
{
    class Program
    {
        /// <summary>
        ///  This is the address of the database.
        ///  We define it here as a constant since it will never change.
        /// </summary>
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);

            Console.WriteLine("Getting All Rooms:");
            Console.WriteLine();

            List<Room> allRooms = roomRepo.GetAll();

            foreach (Room room in allRooms)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }
            Console.WriteLine("----------------------------");
            Console.WriteLine("Getting Room with Id 1");

            Room singleRoom = roomRepo.GetById(1);

            Console.WriteLine($"{singleRoom.Id} {singleRoom.Name} {singleRoom.MaxOccupancy}");

            //Room bathroom = new Room
            //{
            //    Name = "Bathroom",
            //    MaxOccupancy = 1
            //};

            //roomRepo.Insert(bathroom);

            //Console.WriteLine("-------------------------------");
            //Console.WriteLine($"Added the new Room with id {bathroom.Id}");

            //bathroom.Name = "Hotty Potty";
            //bathroom.MaxOccupancy = 7;

            //roomRepo.Update(bathroom);

            //Room bath = roomRepo.GetById(8);
            //bath.Name = "Powder Room";
            //bath.MaxOccupancy = 3;
            //roomRepo.Update(bath);

            //roomRepo.Delete(9);

            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);
            List<Roommate> allRoommates = roommateRepo.GetAll();
            Console.WriteLine("-------------------------");
            foreach (Roommate roommate in allRoommates)
            {
                Console.WriteLine(roommate.Firstname);
                Console.WriteLine(roommate.Lastname);
                Console.WriteLine(roommate.RentPortion);
                Console.WriteLine(roommate.MovedInDate);
            }

            Roommate singleRoommate = roommateRepo.GetById(2);
            Console.WriteLine($"Roommate Name is {singleRoommate.Firstname}");

            List<Roommate> a = roommateRepo.GetAllWithRoom(3);
            foreach( Roommate roommate in a)
            {
                Console.WriteLine(roommate.Firstname);
                Console.WriteLine(roommate.Lastname);
                Console.WriteLine(roommate.RentPortion);
                Console.WriteLine(roommate.MovedInDate);
                Console.WriteLine(roommate.Room.Id);
                Console.WriteLine(roommate.Room.Name);
                Console.WriteLine(roommate.Room.MaxOccupancy);
            }





        }
    }
}
