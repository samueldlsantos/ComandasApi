using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComandasAPI.DTO
{
    public class OptionValueDTO
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int ProductOptionId { get; set; }
    }
}