using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComandasAPI.DTO
{
    public class ProductOptionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public List<OptionValueDTO>? Values { get; set; }
    }
}