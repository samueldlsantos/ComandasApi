using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComandasAPI.Models
{
    public class ProductOption
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;
        public ICollection<OptionValue> Values { get; set; } = new List<OptionValue>();

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }

}