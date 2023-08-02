using System.ComponentModel.DataAnnotations;

namespace E_PROJECT_MANAGER.Models
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
        public int? LoaiId { get; set; } = 0;
        public int? TrangThaiId { get; set; } = 0;
        public bool? IsDelete { get; set; } = false;
        public DateTime? NgayXoa { get; set; }
    }
}
