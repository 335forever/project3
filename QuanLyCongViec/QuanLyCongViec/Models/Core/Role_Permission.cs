using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCongViec.Models.Core
{
    [Table("Role_Permission")]
    public class Role_Permission
    {
        [Required]
        public Guid role_id { set; get; }
        public Role Role { get; set; }

        [Required]
        public Guid resource_id { set; get; }
        public Resource Resource { get; set; }


        [Required]
        public int permission { set; get; }
    }
}
