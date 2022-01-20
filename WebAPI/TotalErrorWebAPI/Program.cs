using Data.Services.Implementations;
using Data.Services.Interfaces;
using Data.Services.MainServices;
using Data.Services.MapperProfiles;
using Data.TotalErrorDbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<TotalErrorDbContext>();
builder.Services.AddAutoMapper(typeof(MapProfile));
//builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IOrdersMainService, OrdersMainService>();

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
