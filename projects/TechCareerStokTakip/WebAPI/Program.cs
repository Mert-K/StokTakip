using DataAccess;
using Service;
using Service.Abstract;
using Service.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//KENDÝ IOC KAYITLARIM
builder.Services.AddDataAccessDependencies(builder.Configuration); //DataAccess katmanýndaki extension method, kendim oluþturdum
builder.Services.AddServiceDependencies(); //Service katmanýndaki extension method, kendim oluþturdum


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
