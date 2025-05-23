using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComandasAPI.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        // Para crear/modificar pedidos con items
        public List<OrderItemDTO> Items { get; set; } = new();

        // Solo útil en respuestas, lo asigna el backend
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}