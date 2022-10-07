using Microsoft.EntityFrameworkCore;
using TP_A16_ServiceAPI.models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddDbContext<ProduitsContext>(opt =>
//    opt.UseInMemoryDatabase("ProduitsListe"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProduitsContext>(x =>
{
    x.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}


// connection bd


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
