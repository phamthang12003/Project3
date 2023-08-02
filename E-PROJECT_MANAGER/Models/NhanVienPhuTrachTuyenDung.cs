using System.ComponentModel.DataAnnotations.Schema;

namespace E_PROJECT_MANAGER.Models
{
    public class NhanVienPhuTrachTuyenDung : Base
    {
        public int? ViTriTuyenDungId { get; set; }
        public string? NhanVienId { get; set; }

        [ForeignKey("ViTriTuyenDungId")]
        public virtual ViTriTuyenDung? GetViTriTuyenDung { get; set; }

    }
}
