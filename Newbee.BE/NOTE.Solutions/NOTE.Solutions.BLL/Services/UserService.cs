using NOTE.Solutions.BLL.Contracts.User.Responses;

namespace NOTE.Solutions.BLL.Services;
public class UserService(IUnitOfWork unitOfWork,RoleManager<ApplicationRole> roleManager,UserManager<ApplicationUser> userManager) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<IEnumerable<UserResponse>>> GetAllAsync (CancellationToken cancellationToken = default (CancellationToken))
    {
        var users = _userManager.Users.ToList();
        var result = new List<UserResponse>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            result.Add(new UserResponse()
            {
                Id = user.Id,
                Email = user.Email!,
                IsDisabled = user.IsDisabled,
                Roles = roles,
            });
        }

        return Result.Success<IEnumerable<UserResponse>>(result);
    }
}
