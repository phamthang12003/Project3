using E_PROJECT_MANAGER.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_PROJECT_MANAGER.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HoSo> HoSos { get; set; }
        public DbSet<LichPhongVan> LichPhongVans { get; set; }
        public DbSet<NhanVienPhuTrachTuyenDung> NhanVienPhuTrachTuyenDungs { get; set; }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<QuanLyLoai> QuanLyLoais { get; set; }
        public DbSet<UngVien> UngViens { get; set; }
        public DbSet<ViTriTuyenDung> ViTriTuyenDungs { get; set; }
        public DbSet<QuanLyTrangThai> QuanLyTrangThais { get; set; }
    }
}