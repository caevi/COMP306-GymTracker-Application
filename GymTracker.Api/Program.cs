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
// Database (RDS SQL Server)
// ----------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
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
// Ensure DB created + seed
// ----------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // create schema if needed
    db.Database.EnsureCreated();

    // seed initial data (users, exercises, workouts)
    DbSeeder.Seed(db);
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
