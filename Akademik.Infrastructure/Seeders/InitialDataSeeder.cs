﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Akademik.Domain.Entities;
using Akademik.Infrastructure.Persistence;

namespace Akademik.Infrastructure.Data
{
    public class InitialDataSeeder
    {
        private readonly AkademikDbContext _context;
        private readonly Random _random = new Random();

        public InitialDataSeeder(AkademikDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.Database.CanConnectAsync())
            {
                // Seed ResidentDetails
                if (!await _context.ResidentsDetails.AnyAsync())
                {
                    var residentDetails = new List<ResidentDetails>();
                    for (int i = 1; i <= 3; i++)
                    {
                        residentDetails.Add(new ResidentDetails
                        {
                            Email = $"mieszkaniec{i}@example.com",
                            StudentCardNumber = $"SN{i:D5}",
                            PhoneNumber = GenerateRandomPhoneNumber(),
                            Street = $"Ulica Przykładowa {i}",
                            City = "Warszawa",
                            Country = "Polska",
                            PostalCode = "00-001"
                        });
                    }

                    await _context.ResidentsDetails.AddRangeAsync(residentDetails);
                    await _context.SaveChangesAsync();
                }

                // Seed Rooms
                if (!await _context.Rooms.AnyAsync())
                {
                    var rooms = new List<Room>
                    {
                        new Room { RoomNumber = 101, IsAvailable = true, NumberOfBeds = 2 },
                        new Room { RoomNumber = 102, IsAvailable = true, NumberOfBeds = 1 },
                        new Room { RoomNumber = 201, IsAvailable = true, NumberOfBeds = 3 }
                    };

                    await _context.Rooms.AddRangeAsync(rooms);
                    await _context.SaveChangesAsync();
                }

                // Seed Residents (z relacjami)
                if (!await _context.Residents.AnyAsync())
                {
                    var residentDetails = await _context.ResidentsDetails.ToListAsync();
                    var rooms = await _context.Rooms.ToListAsync();

                    var residents = new List<Resident>();
                    for (int i = 0; i < residentDetails.Count; i++)
                    {
                        int roomId = rooms[i % rooms.Count].Id; // Wybieramy pokój cyklicznie

                        residents.Add(new Resident
                        {
                            PESEL = GenerateRandomPesel(),
                            FirstName = $"Imię{i + 1}",
                            LastName = $"Nazwisko{i + 1}",
                            ResidentDetailsId = residentDetails[i].Id,
                            RoomId = roomId
                        });
                    }

                    await _context.Residents.AddRangeAsync(residents);
                    await _context.SaveChangesAsync();
                }
            }
        }

        // Prosta metoda do generowania losowego numeru PESEL (tylko przykład)
        private string GenerateRandomPesel()
        {
            string pesel = "";
            for (int i = 0; i < 11; i++)
            {
                pesel += _random.Next(0, 10).ToString();
            }
            return pesel;
        }

        // Prosta metoda do generowania losowego numeru telefonu (tylko przykład)
        private string GenerateRandomPhoneNumber()
        {
            return $"{_random.Next(100, 999)}-{_random.Next(100, 999)}-{_random.Next(100, 999)}";
        }
    }
}
