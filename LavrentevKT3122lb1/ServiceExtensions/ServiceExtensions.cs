using LavrentevKT3122lb1.Interfaces;
using LavrentevKT3122lb1.Interfaces.LavrentevKT3122lb1.Interfaces;

namespace LavrentevKT3122lb1.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDbServices(this IServiceCollection services)
        {
            services.AddScoped<ITeachersService, TeachersService>();
            services.AddScoped<IDepartmentsService, DepartmentsService>();
        }
    }
}