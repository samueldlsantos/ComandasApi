using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComandasAPI.Models
{
    public class OptionValue
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public int ProductOptionId { get; set; }

        public ProductOption ProductOption { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}