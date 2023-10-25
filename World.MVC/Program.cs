using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;
using World.MVC.Contracts;
using World.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*builder.Services.AddAuthentication();*/
builder.Services.AddAuthentication().AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{

});
builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("https://localhost:7272"));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IContinentService, ContinentService>();

builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
