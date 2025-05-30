using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComandasAPI.DTO
{
    public class OptionValueDTO
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public int ProductOptionId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}