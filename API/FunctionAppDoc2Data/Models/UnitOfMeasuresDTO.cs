using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Models;
public class UnitOfMeasuresDTO
{
    public int UnitOfMeasureId { get; set; }
    public string Name { get; set; }
    public bool? IsActive { get; set; }
}
