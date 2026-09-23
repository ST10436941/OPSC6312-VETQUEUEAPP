using Microsoft.EntityFrameworkCore;
using VetQueue.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Add PostgreSQL Entity Framework Core
builder.Services.AddDbContext<VetQueueDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("VetQueueConnection")
    )
);

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirection
app.UseHttpsRedirection();

// CORS
app.UseCors("AllowAll");

// Authorization
app.UseAuthorization();

// Controllers
app.MapControllers();

app.Run();