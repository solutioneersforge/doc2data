using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToData.Domain.DTO.Common
{
    public class CurrencyDTO
    {
        public int CurrenctId { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Symbol { get; set; }
        public bool IsActive { get; set; }
    }
}
