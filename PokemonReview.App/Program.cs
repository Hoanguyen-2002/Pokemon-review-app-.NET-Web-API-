using Microsoft.EntityFrameworkCore;
using PokemonReviewApp;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPokemonRepository , PokemonRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});// connect to Database

var app = builder.Build();

// Check if there is one argument and if it is "seeddata"
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    // Call the SeedData method to seed the database
    SeedData(app);

// Method to seed data into the database
void SeedData(IHost app)
{
    // Get the IServiceScopeFactory from the app's services
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    // Create a new scope for the seeding operation
    using (var scope = scopedFactory.CreateScope())
    {
        // Get the Seed service from the scope's service provider
        var service = scope.ServiceProvider.GetService<Seed>();
        
        // Call the SeedDataContext method to seed the data
        service.SeedDataContext();
    }
}



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
