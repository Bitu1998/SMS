using ExceptionHandling.Middlewares;
using SMS.Core.Security.HeaderPolicy;
using SMS.Model.Common;
using SMS.Repository.Container;
using SMS.Web;
using SMS.Web.Middleware.SecurityMiddleware;
using SMS.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddControllersWithViews();
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));
builder.Services.Configure<CustomVarModel>(builder.Configuration.GetSection("CustomVar"));
builder.Services.AddCustomContainer(builder.Configuration);
builder.Services.AddCoreContextProvider(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.UserAuthenConfig();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
});
var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseDeveloperExceptionPage();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        if (ctx.File.Name.EndsWith(".css"))
        {
            ctx.Context.Response.Headers["Content-Type"] = "text/css";
        }
    }
});
app.UseHttpsRedirection();
app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder().AddDefaultSecurePolicy());
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
