using Newbee.BLL.DTO.Users;
namespace Newbee.BLL.Interfaces;
public interface IUserService
{
    Task<Result<UserProfileResponse>> GetProfileAsync(int userId);
    Task<Result> UpdateProfileAsync(int userId, UpdateProfileRequest request);
    Task<Result> ChangePasswordAsync(int userId, ChangePasswordRequest request);
}
