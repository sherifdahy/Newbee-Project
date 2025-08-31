using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class LogDTO
    {
        public string actionLevel { get; set; }
        public string actionType { get; set; }
        public ActionsListDTO actionsList { get; set; }
        public TakenByDTO takenBy { get; set; }
        public string time { get; set; }
    }

}
