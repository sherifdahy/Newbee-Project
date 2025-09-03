using Newbee.BLL.DTO.Auth.Responses;
using Newbee.BLL.DTO.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Interfaces
{
    public interface ICustomerAuthServices
    {
        Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> ResendConfirmationEmailAsync(MailRequest request, CancellationToken cancellationToken = default);
    }
}
