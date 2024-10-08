using ManagmentApp.DbStorage;
using ManagmentApp.Middlewares;
using ManagmentApp.Repositories;
using ManagmentApp.Services;

namespace ManagmentApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<EmployeeSystemSettings>( 
              builder.Configuration.GetSection("MongoDB"));

            builder.Services.AddSingleton<EmployeeRepo>();
            builder.Services.AddSingleton<DepartmentRepo>();
            builder.Services.AddSingleton<CompensationRepo>();

            builder.Services.AddScoped<ExceptionHandlingMiddleware>();  

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();  
            builder.Services.AddScoped<ICompensationService, CompensationService>();        

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();
          

            app.MapControllers();

            app.Run();
        }
    }
}
