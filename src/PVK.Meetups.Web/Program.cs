using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PVK.Meetups.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// configure forwarded headers so the ap works behind a load balancer
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var features = new PVK.Meetups.Web.FeatureFlags(builder.Configuration);
builder.Services.AddSingleton<PVK.Meetups.Web.FeatureFlags>(features);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("LocalPostgresConnection") ?? throw new InvalidOperationException("Connection string 'LocalPostgresConnection' not found.");
builder.Services.AddDbContext<PVKMeetupsDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<PVKMeetupsDbContext>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();

if (features.EnableWeakPasswords)
{
    builder.Services.AddDefaultIdentity<PVKMeetupsUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 4;

    })
    .AddEntityFrameworkStores<PVKMeetupsDbContext>();
}
else
{
    builder.Services.AddDefaultIdentity<PVKMeetupsUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 12;
    })
    .AddEntityFrameworkStores<PVKMeetupsDbContext>();
}

var config = builder.Configuration;
builder.Services.AddAuthentication()
        .AddGoogle(options =>
        {
            options.ClientId = config.GetSection("Authentication").GetValue<string>("GoogleClientId");
            options.ClientSecret = config.GetSection("Authentication").GetValue<string>("GoogleClientSecret");
            options.AccessDeniedPath = "/AccessDeniedPathInfo";
        })
        .AddFacebook(options =>
        {
            options.ClientId = config.GetSection("Authentication").GetValue<string>("FacebookClientId");
            options.ClientSecret = config.GetSection("Authentication").GetValue<string>("FacebookClientSecret");
            options.AccessDeniedPath = "/AccessDeniedPathInfo";
        })
        ;


builder.Services.AddControllersWithViews();




// Configure method

var app = builder.Build();

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (true || app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
