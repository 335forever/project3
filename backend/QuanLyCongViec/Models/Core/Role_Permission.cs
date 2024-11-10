using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCongViec.Models.Core
{
    [Table("Role_Permission")]
    public class Role_Permission
    {
        [Required]
        public Guid RoleId { set; get; }
        public Role? Role { get; set; }

        [Required]
        public Guid ResourceId { set; get; }
        public Resource? Resource { get; set; }


        [Required]
        public int Permission { set; get; } = 0;
    }
}
