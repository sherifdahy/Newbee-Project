using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class HistoryItemDTO
    {
        public string title { get; set; }
        public string date { get; set; }
        public List<SubHistoryDTO> subs { get; set; }
    }
}
