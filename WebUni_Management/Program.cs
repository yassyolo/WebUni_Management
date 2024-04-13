using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddServices();

builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error/500");
    app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
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
app.MapControllerRoute(
	name: "ManageGrade",
	pattern: "PersonalInfo/ManageGrade/{subjectId}/{studentId}",
	defaults: new { controller = "PersonalInfo", action = "ManageGrade" }
);
app.MapControllerRoute(
    	name: "Chat",
        pattern: "PersonalInfo/Chat/{id?}",
        defaults: new { controller = "PersonalInfo", action = "Chat" }
);
app.MapControllerRoute(
        name: "Back",
        pattern: "Home/Back/{previousPage}",
        defaults: new { controller = "Home", action = "Back" }
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
