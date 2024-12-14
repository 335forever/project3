using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCongViec.Models.Core
{
    [Table("Order_Permission")]
    public class Order_Permission
    {
        [Required]
        public Guid OrderId { set; get; }
        public Order? Order { get; set; }

        [Required]
        public Guid UserId { set; get; }
        public User? User { get; set; }
    }
}
