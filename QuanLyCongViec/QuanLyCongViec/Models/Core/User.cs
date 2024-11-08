using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace QuanLyCongViec.Models.Core
{
    [Table("Users")]
    public class User : IdentityUser<Guid>
    {
        [Required]
        public DateTime created_at { set; get; } = DateTime.UtcNow;

        public DateTime updated_at { set; get; } = DateTime.UtcNow;

        [Required]
        public bool is_active { set; get; } = false;

        public string? information { set; get; }

        public string? profile_picture { set; get; }

        public string? social_links { set; get; }
    }

}
