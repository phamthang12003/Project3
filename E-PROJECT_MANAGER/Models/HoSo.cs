using System.ComponentModel.DataAnnotations.Schema;

namespace E_PROJECT_MANAGER.Models
{
    public class HoSo : Base
    {
        public int? UngVienId { get; set; }
        public string? LoaiHoSo { get; set; }
        public string? LinkHoSo { get; set; }

        [ForeignKey("UngVienId")]
        public virtual UngVien GetUngVien { get; set; }
    }
}
