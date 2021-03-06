using Microsoft.EntityFrameworkCore;
using Serilog;
using BookStore.API.Data;
using BookStore.API.Configuration;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("BookStoreDbConnection");

builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseSqlServer(connString));
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddIdentityCore<APIUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BookStoreDbContext>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//setting up logger
//ctx-configuration context
//lc-logging configuration
builder.Host.UseSerilog((ctx, lc) => 
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
        b => b.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
