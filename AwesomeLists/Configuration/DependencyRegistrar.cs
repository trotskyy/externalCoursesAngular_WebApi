using AwesomeLIsts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AwesomeLists.Data.Abstract;
using AwesomeLists.Services.Task;
using AwesomeLists.Services.TaskList;
using AwesomeLists.Services.User;

namespace AwesomeLists.Configuration
{
    public static class DependencyRegistrar
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AppDb"));
            });

            services.AddScoped<ITaskListRepository, TaskListRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskListService, TaskListService>();
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<ITaskMapper, TaskMapper>();
            services.AddSingleton<ITaskListMapper, TaskListMapper>();
        }
    }
}
