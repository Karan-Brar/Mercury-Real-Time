using MercuryMVC.Hubs;
using Microsoft.EntityFrameworkCore;
using MercuryMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddDbContext<ChattingContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("ChatDb"))
);

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LoginForm}/");

app.MapControllerRoute(
    name: "ChatRoom",
    pattern: "{controller=ChatRoom}/{action=ChatRoom}/");

app.MapHub<ChatHub>("/chatHub");
app.MapHub<LoginHub>("/loginHub");

app.Run();
