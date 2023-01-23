using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Nukitter.Web.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'NukeItterContextConnection' not found.");

builder.Services.AddDbContext<NukeItterContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<NukeItterUser>(options => options.SignIn.RequireConfirmedAccount = true)
  .AddEntityFrameworkStores<NukeItterContext>();

builder.WebHost.ConfigureKestrel(options => {
  options.Listen(IPAddress.Any, 5001, listenOptions => {
    var certPem = File.ReadAllText("/etc/letsencrypt/live/dev.fergl.ie/fullchain.pem");
    var keyPem = File.ReadAllText("/etc/letsencrypt/live/dev.fergl.ie/privkey.pem");
    var x509 = X509Certificate2.CreateFromPem(certPem, keyPem);
    listenOptions.UseHttps(x509);
  });
});
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthentication(options => {
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
  })
  .AddCookie()
  .AddTwitter(twitterOptions => {
    twitterOptions.ConsumerKey = builder.Configuration["Auth:Twitter:ConsumerKey"];
    twitterOptions.ConsumerSecret = builder.Configuration["Auth:Twitter:ConsumerSecret"];
    twitterOptions.RetrieveUserDetails = true;
  });
builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
  app.UseExceptionHandler("/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
