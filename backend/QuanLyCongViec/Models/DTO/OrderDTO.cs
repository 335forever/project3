using System.ComponentModel.DataAnnotations;

namespace QuanLyCongViec.Models.DTO
{
    public class CreateOrderDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime? EstimatedAt { get; set; }

        [Required]
        public float? Price { get; set; }
    }
}
