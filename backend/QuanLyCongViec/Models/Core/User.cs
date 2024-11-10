using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace QuanLyCongViec.Models.Core
{
    [Table("Users")]
    public class User : IdentityUser<Guid>
    {
        [Required]
        public DateTime CreatedAt { set; get; } = DateTime.UtcNow;

        public DateTime UpdatedAt { set; get; } = DateTime.UtcNow;

        [Required]
        public bool IsActived { set; get; } = false;

        public string? Information { set; get; }

        public string? ProfilePictureLink { set; get; }

        public string? SocialLinks { set; get; }
    }

}
