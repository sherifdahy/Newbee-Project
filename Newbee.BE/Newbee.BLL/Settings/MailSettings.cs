using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Setting
{
    public class MailSettings
    {
        public string Email { get; set; } = string.Empty;
        public string AppPassword { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool SSL { get; set; }
        public bool IsBodyHtml { get; set; }

    }
}
