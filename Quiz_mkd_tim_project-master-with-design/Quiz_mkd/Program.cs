using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quiz.Domain.Identity;
using Quiz.Repository.Data;
using Quiz.Repository.Implementation;
using Quiz.Repository.Interface;
using Quiz.Utility;
using Quiz.Utility.Options;
using Quiz.Web.Data;
using Quiz.Web.Domain_Transfer.Implementation;
using Quiz.Web.Domain_Transfer.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "0";
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "0";
    })
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration["Authentication:Facebook:AppId"] ?? "0";
        options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"] ?? "0";

        options.Events = new OAuthEvents
        {
            OnRemoteFailure = context =>
            {
                context.Response.Redirect("/Identity/Account/Login");
                context.HandleResponse();
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddTransient<IRangListDetailsGeneral, RangListDetailsGeneral>();
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
builder.Services.AddHttpContextAccessor();



var app = builder.Build();

/* ✅ Seed Admin only in Development
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedAdminAsync(userManager, roleManager);
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    Quiz.Web.Data.DbInitializer.Seed(context);
} */

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();

    var smtpOptions = app.Services.GetRequiredService<IOptions<SmtpOptions>>().Value;
    Console.WriteLine("USERNAME: " + smtpOptions.SmtpUserName);
    Console.WriteLine("PASSWORD: " + smtpOptions.SmtpPassword);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");



app.Run();

/*
async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    string adminEmail = "admin@local.com";
    string adminPassword = "Admin123!";
    string adminRole = "Admin";

    if (!await roleManager.RoleExistsAsync(adminRole))
        await roleManager.CreateAsync(new IdentityRole(adminRole));

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            NameUser = "LocalAdmin",         // ✅ required field
            Surname = "Tester",              // ✅ required field
            PlaceOfOrigin = "Localhost",     // ✅ required field
            Points = 0                       // optional, init cleanly
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, adminRole);
            Console.WriteLine("✅ Local Admin created.");
        }
        else
        {
            Console.WriteLine("❌ Failed to create admin: " +
                string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
    else if (!await userManager.IsInRoleAsync(adminUser, adminRole))
    {
        await userManager.AddToRoleAsync(adminUser, adminRole);
        Console.WriteLine("✅ Existing user promoted to Admin.");
    }
} */
