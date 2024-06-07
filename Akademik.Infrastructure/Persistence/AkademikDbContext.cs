using Akademik.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Infrastructure.Persistence
{
    public class AkademikDbContext : DbContext
    {
        public DbSet<Resident> Residents { get; set; }
        public DbSet<ResidentDetails> ResidentsDetails { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public AkademikDbContext(DbContextOptions<AkademikDbContext> options) : base(options)
        {
            
        }



    }
}
