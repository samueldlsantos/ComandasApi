using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComandasAPI.Models
{
    public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public ICollection<OrderItemOption> Options { get; set; } = new List<OrderItemOption>();

    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}

}