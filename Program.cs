using AutoMapper;
using Business;
using Business.ProviderBusiness.MapperProfile;
using Microsoft.EntityFrameworkCore;
using ToDo_Planning.Business;
using ToDo_Planning.Entities;

//using ToDo_Planning.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//dbcontext
builder.Services.AddDbContext<ToDoPlanningContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

//automapper
var configuration = new MapperConfiguration(cfg =>
{
    cfg.SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
    cfg.DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;
    cfg.AddProfile<ProviderProfile>();
    cfg.AddProfile<ProjectProfile>();
});

IMapper mapper = configuration.CreateMapper();
builder.Services.AddSingleton(mapper);

//services

builder.Services.AddMvc();
builder.Services.AddScoped<ICalculateFactory, CalculateFactory>();
builder.Services.AddScoped<IProviderService, ProviderService>();
builder.Services.AddScoped<IProjectBusiness, ProjectBusiness>();
builder.Services.AddScoped<IDeveloperBusiness, DeveloperBusiness>();
builder.Services.AddScoped<IScheduleBusiness, ScheduleBusiness>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});

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
app.UseCors("AllowAll");

app.Run();