using Microsoft.EntityFrameworkCore;
using socialRunCommunity.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Add repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventParticipantRepository, EventParticipantRepository>();

//Add services
builder.Services.AddScoped<UserProfileService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:8080") // Замените на адрес фронтенда
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Настройка Kestrel для HTTPS с использованием сертификатов
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // HTTP

    options.ListenAnyIP(443, listenOptions =>
    {
        listenOptions.UseHttps(
            builder.Configuration["ASPNETCORE_Kestrel__Endpoints__Https__Certificate__Path"],
            builder.Configuration["ASPNETCORE_Kestrel__Endpoints__Https__Certificate__KeyPath"]);
    });
});

var app = builder.Build();

// Применение миграций при запуске
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); // Замените на ваше имя DbContext
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
