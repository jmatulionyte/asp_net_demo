using Microsoft.EntityFrameworkCore;
using pingPongAPI;
using pingPongAPI.Data;
using pingPongAPI.Repository;
using pingPongAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("TestDBConnection");

//providing connection to ApplicationDbContextClass
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(connectionString);
});

//add services
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

//mapper for model-dto mapping
builder.Services.AddAutoMapper(typeof(MappingConfig));
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

