using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                // Seed ResidentDetails
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
                            PostalCode = "12-345"
                        },
                        // ... more resident details as needed
                    };

                    _context.ResidentsDetails.AddRange(residentDetails);
                    await _context.SaveChangesAsync();
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
                    await _context.SaveChangesAsync();
                }

                // Seed Residents 
                if (!_context.Residents.Any())
                {
                    var residents = new List<Resident>();
                    var residentDetails = await _context.ResidentsDetails.FirstAsync(); // Get the first ResidentDetails
                    var room = await _context.Rooms.FirstAsync(); // Get the first Room

                    var resident = new Resident
                    {
                        PESEL = "12345678901", // Replace with valid PESELs
                        FirstName = "Jan",
                        LastName = "Kowalski",
                        Room = room, // Assign the Room directly
                        ResidentDetailsId = residentDetails.Id // Przypisanie Id
                    };
                    residents.Add(resident);
                    // ... more residents
                    _context.Residents.AddRange(residents);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
