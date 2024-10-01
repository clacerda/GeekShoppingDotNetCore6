using GeekShopping.web.Services;
using GeekShopping.web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args); 
string serviceUrl = "ServiceUrls";
 

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation().AddViewOptions(opt => {
    opt.HtmlHelperOptions.ClientValidationEnabled = true;
});


builder.Services.AddHttpClient<IProductService, ProductService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration[$"{serviceUrl}:ProductAPI"]);
});

builder.Services.AddHttpClient<ICartService, CartService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration[$"{serviceUrl}:CartAPI"]);
});

builder.Services.AddHttpClient<ICouponService, CouponService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration[$"{serviceUrl}:CouponAPI"]);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies", cookie => cookie.ExpireTimeSpan = TimeSpan.FromMinutes(10))
  .AddOpenIdConnect("oidc", options =>
  {
      options.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
      options.GetClaimsFromUserInfoEndpoint = true;
      options.ClientId = "geek_shopping";
      options.ClientSecret = "my_super_secret";
      options.ResponseType = "code";
      options.ClaimActions.MapJsonKey("role", "role", "role");
      options.ClaimActions.MapJsonKey("sub", "sub", "sub");
      options.TokenValidationParameters.NameClaimType = "name";
      options.TokenValidationParameters.RoleClaimType = "role";
      options.Scope.Add("geek_shopping");
      options.SaveTokens = true;
  });

var app = builder.Build();

if (!app.Environment.IsEnvironment("Development"))
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
