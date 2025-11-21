using GymTracker.Api.Data;
using GymTracker.Api.Repositories;
using GymTracker.Api.Profiles;
using GymTracker.Core.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ----------------------
// CORS (Allow React)
// ----------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000", "https://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ----------------------
// Database (SQLite)
// ----------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("Data Source=gym.db");
});

// ----------------------
// Register Services / Repos
// ----------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();

// ----------------------
// AutoMapper
// ----------------------
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// ----------------------
// Controllers + Swagger
// ----------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ----------------------
// Ensure DB created
// ----------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

// ----------------------
// Middleware Pipeline
// ----------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ENABLE CORS BEFORE AUTH
app.UseCors("AllowReact");

app.UseAuthorization();

app.MapControllers();

app.Run();
