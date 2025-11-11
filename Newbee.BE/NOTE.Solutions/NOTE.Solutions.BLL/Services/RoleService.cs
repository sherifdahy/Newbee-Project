
using Microsoft.EntityFrameworkCore;
using NOTE.Solutions.Entities.Abstractions.Consts;
using System.Security.Claims;

namespace NOTE.Solutions.BLL.Services;
public class RoleService(IUnitOfWork unitOfWork,RoleManager<ApplicationRole> roleManager) : IRoleService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;


    public async Task<Result> UpdateAsync(
        int id, RoleRequest request,
        CancellationToken cancellationToken = default)
    {
        var roleIsExists = await _roleManager
            .Roles.AnyAsync(x=>x.Name == request.Name && x.Id != id);
        
        if(roleIsExists)
            return Result.Failure<RoleDetailResponse>(RoleErrors.Duplicated);

        var allowedPermissions = Permissions.GetAllPermissions();

        if(request.Permissions.Except(allowedPermissions).Any())
            return Result.Failure<RoleDetailResponse>(RoleErrors.InvalidPermissions);

        if (await _roleManager.FindByIdAsync(id.ToString()) is not { } role)
            return Result.Failure(RoleErrors.NotFound);

        role.Name = request.Name;

        var result = await _roleManager.UpdateAsync(role);

        if(result.Succeeded)
        {
            var currentPermissions = await _roleManager.GetClaimsAsync(role);

            var newPermissions = request
                .Permissions.Except(currentPermissions.Select(x => x.Value));

            foreach (var permission in newPermissions)
            {
                var claim = new Claim(Permissions.Type, permission);
                await _roleManager.AddClaimAsync(role, claim);
            }

            var removedPermissions = currentPermissions
                .Select(x=>x.Value).Except(request.Permissions);

            foreach(var permission in removedPermissions)
            {
                var claim = new Claim(Permissions.Type, permission);
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            return Result.Success();
        }
        var error = result.Errors.First();
        return Result.Failure(
            new Error(error.Code,
            error.Description, 
            StatusCodes.Status400BadRequest));
    }
    public async Task<Result<RoleDetailResponse>> CreateAsync(
        RoleRequest request, 
        CancellationToken cancellationToken = default)
    {
        var roleIsExists = await _roleManager.RoleExistsAsync(request.Name);
        
        if(roleIsExists)
            return Result.Failure<RoleDetailResponse>(RoleErrors.Duplicated);

        var allowedPermissions = Permissions.GetAllPermissions();

        if (request.Permissions.Except(allowedPermissions).Any())
            return Result.Failure<RoleDetailResponse>(RoleErrors.InvalidPermissions);

        var role = new ApplicationRole
        {
            Name = request.Name,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
        };

        var result = await _roleManager.CreateAsync(role);

        if(result.Succeeded)
        {
            foreach(var permission in request.Permissions)
            {
                var claim = new Claim(Permissions.Type, permission);
                await _roleManager.AddClaimAsync(role, claim);
            }

            var response = new RoleDetailResponse()
            {
                Id = role.Id,
                IsDeleted = role.IsDeleted,
                Name = role.Name,
                Permissions = request.Permissions,
            };

            return Result.Success(response);
        }

        var error = result.Errors.First();
        return Result.Failure<RoleDetailResponse>(
            new Error(error.Code,
            error.Description,
            StatusCodes.Status400BadRequest));
    }
    public async Task<Result<IEnumerable<RoleResponse>>> GetAllAsync(bool? includeDisabled = false,CancellationToken cancellationToken = default)
    {
        var roles = await _roleManager.Roles.Where(x=> !x.IsDefault && x.IsDeleted == includeDisabled).ProjectToType<RoleResponse>().ToListAsync(cancellationToken);
        return Result.Success<IEnumerable<RoleResponse>>(roles);
    }

    public async Task<Result<RoleDetailResponse>>
        GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await _roleManager.FindByIdAsync(id.ToString()) is not { } role)
            return Result.Failure<RoleDetailResponse>(RoleErrors.NotFound);

        var permissions = await _roleManager.GetClaimsAsync(role);

        var response = new RoleDetailResponse()
        {
            Id = role.Id,
            IsDeleted = role.IsDeleted,
            Name = role.Name!,
            Permissions = permissions.Select(x=>x.Value),
        };

        return Result.Success(response);
    }

    public async Task<Result> ToggleStatus(
        int id,
        CancellationToken cancellationToken = default)
    {
        if(await _roleManager.FindByIdAsync(id.ToString()) is not { } role)
            return Result.Failure<RoleDetailResponse>(RoleErrors.NotFound);

        role.IsDeleted = !role.IsDeleted;

        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();

        return Result.Failure(new
            Error(error.Code,
            error.Description,
            StatusCodes.Status400BadRequest));
    }
}
