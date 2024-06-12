using Akademik.Application.DTO.ResidentDTO;
using Akademik.Application.DTO.RoomDTO;
using Akademik.Application.Mappings;
using Akademik.Application.Services.ResidentService;
using Akademik.Application.Services.RoomService;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Akademik.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddApplication(this IServiceCollection services)
        {



            services.AddScoped<IResidentService, ResidentService>();
            services.AddScoped<IRoomService, RoomService>();

            services.AddAutoMapper(typeof(ResidentMappingProfiles));

            services.AddValidatorsFromAssemblyContaining<CreateResidentDTOValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<ResidentToEditDTOValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddAutoMapper(typeof(RoomMappingProfiles));

            services.AddValidatorsFromAssemblyContaining<RoomDTOValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();



            services.AddHealthChecks();
            services.AddRazorPages();
        }
    }
}
