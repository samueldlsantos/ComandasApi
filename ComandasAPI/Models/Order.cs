using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComandasAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CreatedByUserId { get; set; }

        public User CreatedByUser { get; set; } = null!;
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }

}