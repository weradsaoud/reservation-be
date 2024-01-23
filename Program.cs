using FirebaseAdmin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using resevation_be.models;
using resevation_be.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ).EnableSensitiveDataLogging()
);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"D:\werad\joka\resevation-be\credentials\reservationapp-150e9-firebase-adminsdk-kw0bf-8af92a0b4a.json");
builder.Services.AddSingleton(FirebaseApp.Create());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
