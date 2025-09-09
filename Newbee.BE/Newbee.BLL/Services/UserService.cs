using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newbee.BLL.DTO.Users;
using Newbee.DAL.Data;

namespace Newbee.BLL.Services;
public class UserService(
    UserManager<ApplicationUser>userManager,
    IUnitOfWork unitOfWork,
    ApplicationDbContext context
    ) : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ApplicationDbContext _context = context;

  

    public async Task<Result<UserProfileResponse>> GetProfileAsync(int  userId)
    {
       var user = await _context.Customers.Where(u=> u.ApplicationUserId ==userId)
            .Include(u=>u.ApplicationUser)
            .Select(u => new UserProfileResponse(
                u.ApplicationUser.Email!,
                u.ApplicationUser.FirstName,
                u.ApplicationUser.LastName,
                u.ApplicationUser.PhoneNumber,
                u.FirstLine
            )).FirstOrDefaultAsync();
        if(user is null)
            return Result.Failure<UserProfileResponse>(CustomerErrors.NotFound);
        return Result.Success(user);
    }

    public async Task<Result> UpdateProfileAsync(int userId, UpdateProfileRequest request)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u=>u.Id==userId);
        if(user is null)
            return Result.Failure(CustomerErrors.NotFound);
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        var customer = await _unitOfWork.Customers.FindAsync(c => c.ApplicationUserId == userId);
        if (customer is null)
            return Result.Failure(CustomerErrors.NotFound);
        customer.FirstLine = request.FirstLine;
        await _unitOfWork.SaveAsync();
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        return Result.Success();

    }
    public async Task<Result> ChangePasswordAsync(int userId, ChangePasswordRequest request)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        return Result.Success();
    }
}
