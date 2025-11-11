using NOTE.Solutions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Document.Responses;

public class DocumentHeaderResponse 
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public string UUID { get; set; } = string.Empty;
    
}
