using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCongViec.Models.Core
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; } = Guid.NewGuid();

        [Required]
        public string Title { set; get; } = string.Empty;

        [Required]
        public string Description { set; get; } = string.Empty;

        [Required]
        public DateTime? EstimatedAt { set; get; }

        [Required]
        public float? Price { set; get; }

        public Guid? SalerId { set; get; }
        [ForeignKey("SalerId")]
        public User? Saler { get; set; } 

        public Guid? DesignerId { set; get; }
        [ForeignKey("DesignerId")]
        public User? Designer { get; set; }

        public Guid? ProducerId { set; get; }
        [ForeignKey("ProducerId")]
        public User? Producer { get; set; }

        [Required]
        public string Status { set; get; } = "NEW";

        [Required]
        public DateTime CreatedAt { set; get; } = DateTime.UtcNow;

        public DateTime UpdatedAt { set; get; } = DateTime.UtcNow;
    }
}
