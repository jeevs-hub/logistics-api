using logistics_api.Repository;
using logistics_api.Services;

var builder = WebApplication.CreateBuilder(args);

string driversFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets/drivers.json");
string menuFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets/menu.json");

var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

void EnsureDataFilesExist(string[] filePaths)
{

    foreach (var filePath in filePaths)
    {
        logger.LogInformation($"Checking {filePath} Exists");
        if (!File.Exists(filePath))
        {
            logger.LogError("file not found at: {FilePath}", menuFilePath);
            Environment.Exit(1);
        }
    }
}

EnsureDataFilesExist(new[] { driversFilePath, menuFilePath });


builder.Services.AddSingleton<IDriverRepository>(provider => new DriverRepository(driversFilePath, provider.GetRequiredService<ILogger<DriverRepository>>()));
builder.Services.AddSingleton<IMenuRepository>(provider => new MenuRepository(menuFilePath, provider.GetRequiredService<ILogger<MenuRepository>>()));


builder.Services.AddTransient<IMenuService, MenuService>();
builder.Services.AddTransient<IDriverService, DriverService>();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
