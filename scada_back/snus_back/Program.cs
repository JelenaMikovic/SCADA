using Microsoft.EntityFrameworkCore;
using scada_back.Database;
using scada_back.Repositories;
using scada_back.Services.IServices;
using scada_back.Services;
using scada_back.Handlers;
using scada_back.HandlersHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddCors();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseLazyLoadingProxies().
    UseSqlite("Data Source = scada.db");
}, ServiceLifetime.Transient);


//builder.Services.AddSingleton<DatabaseContext>();

// Services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<IAlarmService, AlarmService>();
builder.Services.AddTransient<IDeviceService, DeviceService>();
builder.Services.AddTransient<ScanService>();
builder.Services.AddTransient<SimulationService>();


// Repositories
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<TagRepository>();
builder.Services.AddTransient<AlarmRepository>();
builder.Services.AddTransient<DeviceRepository>();


//Sockets
builder.Services.AddSingleton<TagHandler>();
builder.Services.AddSingleton<AlarmHandler>();
builder.Services.AddSingleton<WebSocketConnectionManager>();

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapRazorPages();

app.MapHub<TagHub>("/hub/updateTag");
app.MapHub<AlarmHub>("/hub/updateAlarm");
app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(120),
});

app.MapRazorPages();

using var scope = app.Services.CreateScope();
scope.ServiceProvider.GetRequiredService<ScanService>().Run();

app.Run();
