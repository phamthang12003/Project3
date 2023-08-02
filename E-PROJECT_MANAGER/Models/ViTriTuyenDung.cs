using System.ComponentModel.DataAnnotations.Schema;

namespace E_PROJECT_MANAGER.Models
{
    public class ViTriTuyenDung : Base
    {
        public int? PhongBanID { get; set; }
        public string? TenViTriTuyenDung { get; set; } = "";
        public string? Title { get; set; } = "";
        public string?  Request { get; set; }
        public int? Number { get; set; }

        [ForeignKey("PhongBanID")]
        public virtual PhongBan? GetPhongBan { get; set; }
    }
}
