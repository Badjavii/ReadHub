using backend.repositories;
using backend.services;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVue",
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

// Connection string
string connectionString = builder.Configuration.GetConnectionString("ReadHubDb")!;

// Repositorios
builder.Services.AddSingleton(new BookRepository(connectionString));
builder.Services.AddSingleton(new UserRepository(connectionString));
builder.Services.AddSingleton(new TransactionRepository(connectionString));

// Servicios
builder.Services.AddSingleton<BookService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<TransactionService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowVue");

app.UseAuthorization();
app.MapControllers();
app.Run();