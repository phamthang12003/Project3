using System.ComponentModel.DataAnnotations.Schema;

namespace E_PROJECT_MANAGER.Models
{
    public class LichPhongVan : Base
    {
        public DateTime? NgayPhongVan { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public int? ViTriTuyenDungId { get; set; }
        public int? UngVienId { get; set; }

<<<<<<< HEAD
		//[ForeignKey("UngVienId")]
		//public virtual UngVien GetUngVien { get; set; }
=======
        [ForeignKey("UngVienId")]
        public virtual UngVien? GetUngVien { get; set; }
>>>>>>> e5bd96790c82331a991f980fdf3a13048c1f4148

		//[ForeignKey("ViTriTuyenDungId")]
		//public virtual ViTriTuyenDung GetTriTuyenDung { get; set; }
	}
}
