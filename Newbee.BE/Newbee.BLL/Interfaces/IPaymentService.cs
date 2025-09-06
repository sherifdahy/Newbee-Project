using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Interfaces;

public interface IPaymobService
{
    Task<(Order Order, string RedirectUrl)> ProcessPaymentAsync(int orderId, string paymentMethod, decimal amount);
    Task<Order> UpdateOrderSuccess(string specialReference);
    Task<Order> UpdateOrderFailed(string specialReference);
    string ComputeHmacSHA512(string data, string secret);
}