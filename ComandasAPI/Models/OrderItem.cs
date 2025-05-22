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

    public int CreatedBy { get; set; }  // FK
    public User CreatedByUser { get; set; }  // Navegación
    public DateTime CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }  // FK opcional
    public User? UpdatedByUser { get; set; }  // Navegación
    public DateTime? UpdatedAt { get; set; }
}

}