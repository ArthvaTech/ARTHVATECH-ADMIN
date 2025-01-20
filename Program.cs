using ARTHVATECH_ADMIN.Controllers;
using ARTHVATECH_ADMIN.DbContext;
using ARTHVATECH_ADMIN.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DapperContext and Repository
builder.Services.AddScoped<DapperContext>();

builder.Services.AddScoped<DapperContext>(serviceProvider => {
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    return new DapperContext(configuration);
});
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(); // Stores session in-memory, use a distributed cache in production

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout.
    options.Cookie.HttpOnly = true; // Secure cookie settings
    options.Cookie.IsEssential = true; // Mark the session cookie as essential for GDPR.
});

builder.Services.AddScoped<ILoginRepository, ILoginRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
