﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.DTO

{
    public class ResidentDTO
    {
        public string PESEL { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string StudentCardNumber { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public IFormFile? PhotoData { get; set; }
        public byte[]? Photo { get; set; }
        public int RoomId { get; set; }

    }
}
