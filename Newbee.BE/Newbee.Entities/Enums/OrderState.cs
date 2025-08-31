using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities;
public enum OrderState : byte
{
    Pending = 1,        // الطلب لسه جديد ومستني تأكيد
    Processing = 2,     // الطلب بيتجهز (تحضير المنتجات/التغليف)
    ReadyToShip = 3,    // الطلب جاهز للشحن
    Shipping = 4,       // الطلب خرج للشحن
    Delivered = 5,      // الطلب اتسلم للعميل
    Canceled = 6,       // الطلب اتلغى
    Returned = 7        // العميل رجع الطلب
}
