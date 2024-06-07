using System;
using Microsoft.EntityFrameworkCore;
using Akademik.Domain.Entities;
using Akademik.Infrastructure.Persistence; 

namespace Akademik.Infrastructure.Data 
{
    public class InitialDataSeeder
    {
        private readonly AkademikDbContext _context;

        public InitialDataSeeder(AkademikDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (await _context.Database.CanConnectAsync())
            {

                if (!_context.ResidentsDetails.Any())
                {
                    var residentDetails = new List<ResidentDetails>
                {
                    new ResidentDetails
                    {
                        Email = "jan.kowalski@example.com",
                        StudentCardNumber = "12345",
                        PhoneNumber = "123-456-789",
                        Street = "Ul. Testowa 123",
                        City = "Testowo",
                        Country = "Polska",
                        PostalCode = "12-345",
                    },
                    // ... more resident details as needed
                };
                    _context.ResidentsDetails.AddRange(residentDetails);
                    _context.SaveChanges();
                }

                // Seed Rooms
                if (!_context.Rooms.Any())
                {
                    var rooms = new List<Room>
                {
                    new Room
                    {
                        RoomNumber = 101,
                        NumberOfBeds = 2,
                        IsAvailable = true,
                    },
                    // ... more rooms as needed
                };
                    _context.Rooms.AddRange(rooms);
                    _context.SaveChanges();
                }

                // Seed Residents - This is tricky, as it depends on having ResidentDetails and Rooms
                if (!_context.Residents.Any())
                {
                    var residents = new List<Resident>();

                    var resident = new Resident
                    {
                        PESEL = "12345678901", // Replace with valid PESELs
                        FirstName = "Jan",
                        LastName = "Kowalski",
                        ResidentDetails = _context.ResidentsDetails.First(), // Assumes at least one exists
                        RoomNumber = _context.Rooms.First() // Same assumption
                    };
                    resident.EncodeName();
                    residents.Add(resident);
                    // ... more residents
                

                    _context.Residents.AddRange(residents);
                    await _context.SaveChangesAsync();
                } 
            }
        }
    }
}
