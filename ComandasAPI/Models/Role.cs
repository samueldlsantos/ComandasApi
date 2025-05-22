using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComandasAPI.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<User> Users { get; set; } = new List<User>();

        public int? CreatedBy { get; set; }  // FK
        public User? CreatedByUser { get; set; }  // Navegación
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }  // FK opcional
        public User? UpdatedByUser { get; set; }  // Navegación
        public DateTime? UpdatedAt { get; set; }
    }

}