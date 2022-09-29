using BowlingGame;
using BowlingGame.Interfaces;
using BowlingGame.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<BowlingDataStore>();

builder.Services.AddTransient<IGameRepository, GameRepository>();
builder.Services.AddTransient<IFrameRepository, FrameRepository>();
builder.Services.AddTransient<IRollRepository, RollRepository>();

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
