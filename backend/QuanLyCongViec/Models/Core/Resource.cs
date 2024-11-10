using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCongViec.Models.Core
{
    [Table("Resources")]
    public class Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Name { set; get; } = string.Empty;

        public ICollection<Role_Permission>? Role_Permissions { set; get; }
    }
}
