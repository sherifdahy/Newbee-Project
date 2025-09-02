

namespace Newbee.BLL.DTO.Auth.Requests;

public record ResetPasswordRequest
(
    string Email,
    string Code,
    string newPassword
);
