using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComandasAPI.Models
{
    public class OrderItemOption
{
    public int Id { get; set; }
    public int OrderItemId { get; set; }
    public int OptionValueId { get; set; }

    public OrderItem OrderItem { get; set; } = null!;
    public OptionValue OptionValue { get; set; } = null!;

    public int CreatedBy { get; set; }  // FK
    public User CreatedByUser { get; set; }  // Navegación
    public DateTime CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }  // FK opcional
    public User? UpdatedByUser { get; set; }  // Navegación
    public DateTime? UpdatedAt { get; set; }
}
}