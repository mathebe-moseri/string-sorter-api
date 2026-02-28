var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins(
            "http://localhost:4200",
            "https://black-mud-0ba3c2e03.2.azurestaticapps.net"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Use Cors (must be before MapControllers)
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();