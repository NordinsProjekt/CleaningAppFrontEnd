using CleaningApp.Application.Services;
using CleaningApp.Infrastructure.UnitOfWork;
using CleaningAppFrontEnd.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure MySQL with Pomelo provider.
builder.Services.AddDbContextFactory<CleaningDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Registrera UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Generisk Repository-registrering (kan anv�ndas om du vill injicera specifika repositories)
builder.Services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));
builder.Services.AddTransient<TaskService>();
builder.Services.AddTransient<CleaningTaskService>();
builder.Services.AddTransient<RoomService>();
builder.Services.AddTransient<FrontendDropdownService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();