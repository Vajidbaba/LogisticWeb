using Microsoft.EntityFrameworkCore;
using Common.Data.Context;
using Common.Core.Services.Contracts;
using Common.Core.Services;
using Common.Data.Repositories.Contracts;
using Common.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using static Common.Core.Services.IAttendanceService;
using static Common.Core.Services.IOvertimeRecordService;
using static Common.Core.Services.ISalaryService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LogisticContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Sql"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequiredRole",
    policy => policy.RequireRole("Admin"));
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    option => { option.LoginPath = "/Access/Login"; });

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUsersService, UsresService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<IContextHelper, ContextHelper>();
builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IOvertimeRecordService, OvertimeRecordService>();
builder.Services.AddScoped<ISalaryService, SalaryService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=List}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Login}/{id?}");
});


app.Run();
