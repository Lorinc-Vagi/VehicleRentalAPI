using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<VehicleRentalContext>(o=>o.UseSqlite
    (builder.Configuration.GetConnectionString("DefaultConnection"))
    );
builder.Services.AddControllers().AddNewtonsoftJson();

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins"); // Itt h�vj�k meg a CORS -t!

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
//test