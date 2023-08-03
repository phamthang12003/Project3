using System.ComponentModel.DataAnnotations.Schema;

namespace E_PROJECT_MANAGER.Models
{
    public class LichPhongVan : Base
    {
        public string? tenLich { get; set; }
        public DateTime? NgayPhongVan { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public int? ViTriTuyenDungId { get; set; }
        public int? UngVienId { get; set; }



        [ForeignKey("UngVienId")]
        public virtual UngVien? GetUngVien { get; set; }


		//[ForeignKey("ViTriTuyenDungId")]
		//public virtual ViTriTuyenDung GetTriTuyenDung { get; set; }
	}
}
