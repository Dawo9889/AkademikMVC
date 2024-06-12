using Akademik.Domain.Interfaces;
using Akademik.Infrastructure.Data;
using Akademik.Infrastructure.Persistence;
using Akademik.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Akademik.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AkademikDbContext>(option => option.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(option => option.SignIn.RequireConfirmedEmail = false)
                .AddEntityFrameworkStores<AkademikDbContext>();

            services.AddScoped<InitialDataSeeder>();


            services.AddScoped<IResidentRepository, ResidentRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();

        }
    }
}
