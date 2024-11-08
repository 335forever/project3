using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCongViec.Models.Core
{
    [Table("Roles")]
    public class Role : IdentityRole<Guid>
    {
        public ICollection<Role_Permission> Role_Permissions { set; get; }
    }
}
