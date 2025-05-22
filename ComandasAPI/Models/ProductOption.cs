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

        public int CreatedBy { get; set; }  // FK
        public User CreatedByUser { get; set; }  // Navegación
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }  // FK opcional
        public User? UpdatedByUser { get; set; }  // Navegación
        public DateTime? UpdatedAt { get; set; }
    }

}