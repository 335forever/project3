using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCongViec.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid order_id { set; get; } = Guid.NewGuid();

        [Required]
        public string description { set; get; }

        public DateTime estimated_at { set; get; }

        [Required]
        public float price { set; get; }

        public Guid saler_id {  set; get; }
        [ForeignKey("saler_id")]
        public User Saler { get; set; }

        public Guid designer_id { set; get; }
        [ForeignKey("designer_id")]
        public User Designer { get; set; }

        public Guid producer_id {  set; get; }
        [ForeignKey("producer_id")]
        public User Producer { get; set; }

        public Guid manager_id { set; get; }
        [ForeignKey("manager_id")]
        public User Manager { get; set; }

        [Required]
        public string status { set; get; }

        [Required]
        public DateTime created_at { set; get; }

        public DateTime updated_at { set; get; }
    }
}
