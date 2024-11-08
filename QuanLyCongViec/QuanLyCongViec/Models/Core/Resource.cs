using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCongViec.Models.Core
{
    [Table("Resources")]
    public class Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid resource_id { set; get; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string resource_name { set; get; }

        public ICollection<Role_Permission> Role_Permissions { set; get; }
    }
}
