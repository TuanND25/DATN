using DATN_Client.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using DATN_Client.Areas.Customer.Controllers;
using Blazored.Toast;
using DATN_Shared.Models;
using Microsoft.AspNetCore.Identity;
using DATN_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1000); 
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.Cookie.HttpOnly = true;
                  options.ExpireTimeSpan = TimeSpan.FromDays(30);
                  options.LoginPath = "/Login/Login";
                  options.SlidingExpiration = true;
              });


builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<BanOnlineController, BanOnlineController>();



builder.Services.AddAuthentication()
    .AddCookie()
    .AddGoogle(googleOptions =>
    {

        IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");
        googleOptions.ClientId = "83122337541-1p5sbfd774vu6tm6gak4u8jajdliaohh.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";


        googleOptions.CallbackPath = "https://localhost:7075/signin-google";

    });

builder.Services.AddBlazoredToast();
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
app.UseSession();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
      name: "Admin",
      areaName: "Admin",
      pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapAreaControllerRoute(
      name: "Customer",
      areaName: "Customer",
      pattern: "Customer/{controller=Home}/{action=Index}/{id?}"
    );


});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapBlazorHub();

app.Run();
