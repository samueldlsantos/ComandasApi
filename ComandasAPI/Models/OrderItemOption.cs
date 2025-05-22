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

    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}
}