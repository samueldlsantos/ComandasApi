using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComandasAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public int? CreatedBy { get; set; }
        public User? CreatedByUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }  // FK opcional
        public User? UpdatedByUser { get; set; }  // Navegación
        public DateTime? UpdatedAt { get; set; }
    }

}