using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComandasAPI.DTO
{
    public class OrderItemOptionDTO
    {
        public int Id { get; set; }
        public int OrderItemId { get; set; }
        public int? OptionValueId { get; set; } 

        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Información adicional para respuesta, si se desea
        public OptionValueDTO? OptionValue { get; set; }
    }
}