using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Services;
using WebUni_Management.Data;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddApplicationIdentity(builder.Configuration);

builder.Services.AddControllersWithViews();

builder.Services.AddServices();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Library}/{action=Details}/{id?}");

app.MapControllerRoute(
    name: "ApproveRequest",
    pattern: "Account/ApproveRequest/{username}",
    defaults: new { controller = "Account", action = "ApproveRequest" }
);
app.MapControllerRoute(
    name: "DiscardRequest",
    pattern: "Account/DiscardRequest/{username}",
    defaults: new { controller = "Account", action = "DiscardRequest" }
);
app.MapControllerRoute(
	name: "EditSubject",
	pattern: "PersonalInfo/EditSubject/{subjectId}/{studentId}",
	defaults: new { controller = "PersonalInfo", action = "EditSubject" }
);
app.MapControllerRoute(
    name: "ManageAttendance",
    pattern: "PersonalInfo/ManageAttendance/{subjectId}/{studentId}",
    defaults: new { controller = "PersonalInfo", action = "ManageAttendance" }
);

app.MapRazorPages();

using(var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = new string[] { "Admin", "Student" };
    
    foreach(var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);

        if(!roleExist)
        {
           await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

app.Run();
