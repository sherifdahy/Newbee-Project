using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Document.Requests;

public class DocumentHeaderRequest
{
    public DateTime DateTime { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
}
