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

//KEND� IOC KAYITLARIM
builder.Services.AddDataAccessDependencies(builder.Configuration); //DataAccess katman�ndaki extension method, kendim olu�turdum
builder.Services.AddServiceDependencies(); //Service katman�ndaki extension method, kendim olu�turdum


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
