using Data.Models.Models;
using Data.Services.DtoModels;
using Data.Services.Implementations;
using Data.Services.Interfaces;
using Data.Services.MainServices;
using Data.Services.MapperProfiles;
using Data.TotalErrorDbContext;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Quartz;
using Quartz.Impl;
using Quartz.Spi;

using System.Text;

using TotalErrorWebAPI.Scheduler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TotalErrorWebAPI",
        Version = "v1",
    });
});

builder.Services.AddScoped<TotalErrorDbContext>();
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped<IOrdersMainService, OrdersMainService>();
builder.Services.AddScoped<ICountriesMainService, CountriesMainService>();
builder.Services.AddScoped<IRegionsMainService, RegionsMainService>();
builder.Services.AddScoped<IItemTypesMainService, ItemTypesMainService>();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.Configure<TokenModel>(builder.Configuration.GetSection("JWT"));
var token = builder.Configuration.GetSection("JWT").Get<TokenModel>();
var secret = Encoding.ASCII.GetBytes(token.TokenSecret);

builder.Services.AddIdentity<User, UserRole>().AddEntityFrameworkStores<TotalErrorDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

// JWT start
builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(b =>
{
    b.SaveToken = true;
    b.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidateAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidateIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:TokenSecret"]))
    };
});

builder.Services.AddAuthorization();
// JWT end

// Quartz start
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

builder.Services.AddSingleton<SchedulerReader>();
builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(SchedulerReader),
    cronExpression: "0 * * ? * *"
    ));

builder.Services.AddHostedService<QuartzHostedService>();
// Quartz end

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

app.UseAuthentication();

app.UseAuthorization();