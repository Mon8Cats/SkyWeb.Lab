using Microsoft.AspNetCore.Authorization;
using WebUnderTheHood.AuthorizationRequirements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication("MyCookieAuth") // Cookie Authentication Handler is injected
    .AddCookie("MyCookieAuth", options => 
    {
        options.Cookie.Name = "MyCookieAuth";
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromSeconds(120);
    });

builder.Services.AddAuthorization(options => 
{
    options.AddPolicy("MustBelongToHRDepartment", 
        policy => policy.RequireClaim("Department", "HR"));

    options.AddPolicy("AdminOnly", 
        policy => policy.RequireClaim("Admin"));

    options.AddPolicy("HRManagerOnly",
        policy => policy
                    .RequireClaim("Department", "HR")
                    .RequireClaim("Manager")
                    .Requirements.Add(new HRManagerProbationRequirement(0))
                    );
});

builder.Services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementHandler>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapRazorPages();

app.Run();
