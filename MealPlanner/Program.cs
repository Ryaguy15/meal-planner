using MealPlanner.DataSources;
using MealPlanner.Repositories.Interfaces;
using MealPlanner.Respositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddDbContext<MealPlanningContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("./settings.json");
}
else if (builder.Environment.IsProduction())
{
    builder.Configuration.AddEnvironmentVariables();
}


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
