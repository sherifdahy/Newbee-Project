using Newbee.Entities.Models.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.ProductUnit.Responses;
public class ProductUnitResponse
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Stock { get; set; }
    public decimal Price { get; set; }
    public decimal Rate { get; set; }
    public int UnitId { get; set; }
    public int ProductId { get; set; }
    public int ProductColorId { get; set; }
    public int ProductSizeId { get; set; }
}
