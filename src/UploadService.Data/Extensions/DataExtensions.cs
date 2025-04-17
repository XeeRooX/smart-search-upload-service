using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UploadService.Data.Common;

namespace UploadService.Data.Extensions
{
    public static class DataExtensions
    {
        public static void ConfigureData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(connectionString));
        }
    }
}
