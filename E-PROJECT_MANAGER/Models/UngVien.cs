using System.ComponentModel.DataAnnotations.Schema;

namespace E_PROJECT_MANAGER.Models
{
    public class UngVien : Base
    {
        public string? GioiTinh { get; set; } = "";
        public int? Tuoi { get; set; }
		public string? Email { get; set; } = "";
		public string? phoneNumber { get; set; } = "";
		public string? TenUngVien { get; set; } = "";
        public string? DiaChi { get; set; } = ""; 
        public string? ViTriUngTuyen { get; set; } = "";
        public string? KinhNghiemLamViec { get; set; } = "";
        public int vttdId { get; set; }

        [ForeignKey("vttdId")]
        public virtual ViTriTuyenDung? GetViTriTuyenDung { get; set; }

    }
}
