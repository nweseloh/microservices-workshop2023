using System.Text.Json.Serialization;
using Musicals;
using Musicals.Models;
using Musicals.Repositories;
using Musicals.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options  =>
{
    options.EnableAnnotations();
});
builder.Services.AddAuthentication();


//builder.Services.AddTransient<IShowsRepository, ShowsRepository>();
builder.Services.AddScoped<IReservationUseCase, ReservationUseCase>();
builder.Services.AddSingleton<IRepository<Show>, ShowsRepository>();
builder.Services.AddSingleton<IRepository<Reservation>, ReservationRepository>();
  
builder.Services.AddOptions<ReservationOptions>().BindConfiguration("Reservation");
//builder.Services.AddSingleton(new ReservationOptions() { DurationMinutes = 5 });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
