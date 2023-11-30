using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1;
using WebApplication1.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryClients(Config.GetClients())
    .AddDeveloperSigningCredential();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "https://localhost:5116"; // Без /signin-oidc
    options.RequireHttpsMetadata = false; 
    options.ClientId = "mvc-client";
    options.ClientSecret = "secret"; // TODO поменять
    options.ResponseType = "code";
    options.CallbackPath = "/signin-oidc";


    options.SaveTokens = true;
});


// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();  
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Welcome}/{id?}");

app.Run();
