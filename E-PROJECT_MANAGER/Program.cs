using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.Models;
using E_PROJECT_MANAGER.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<CustomUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddTransient<IEmailSender, SendGird>
builder.Services.AddControllersWithViews();

// Add vong doi cho thanh phan repository
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUngVienRepository, UngVienRepository>();
builder.Services.AddScoped<IPhongBanRepository, PhongBanRepository>();
builder.Services.AddScoped<IQuanLyTrangThaiRepository, QuanLyTrangThaiRepository>();
builder.Services.AddScoped<IQuanLyLoaiRepository, QuanLyLoaiRepository>();
builder.Services.AddScoped<IHoSoRepository, HoSoRepository>();

builder.Services.AddScoped<ILichPhongVanRepository, LichPhongVanRepository>();
builder.Services.AddScoped<IViTriTuyenDungRepository, ViTriTuyenDungRepository>();


builder.Services.AddScoped<IViTriTuyenDungRepository, ViTriTuyenDungRepository>();
builder.Services.AddScoped<INhanVienPhuTrachTuyenDungRepository, NhanVienPhuTrachTuyenDungRespository>();

builder.Services.AddControllersWithViews();

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

//app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserPage}/{action=Index}");
app.MapRazorPages();


app.Run();
