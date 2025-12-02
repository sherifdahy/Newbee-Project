using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Abstractions.Consts;
public static class Permissions
{
    public static string Type { get; } = "permissions";


    #region Branches Permissions
    public const string GetBranches = "branches:read";
    public const string CreateBranches = "branches:create";
    public const string UpdateBranches = "branches:update";
    public const string ToggleStatusBranches = "branches:toggleStatus";
    #endregion

    #region ActiveCode Permissions
    public const string GetActiveCodes = "activeCodes:read";
    public const string CreateActiveCodes = "activeCodes:create";
    public const string UpdateActiveCodes = "activeCodes:update";
    public const string ToggleStatusActiveCodes = "activeCodes:toggleStatus";
    #endregion

    #region Country Permissions
    public const string GetCounties = "counties:read";
    public const string CreateCounties = "counties:create";
    public const string UpdateCounties = "counties:update";
    public const string ToggleStatusCounties = "counties:toggleStatus";
    #endregion

    #region Governorate Permissions
    public const string GetGovernorates = "governorates:read";
    public const string CreateGovernorates = "governorates:create";
    public const string UpdateGovernorates = "governorates:update";
    public const string ToggleStatusGovernorates = "governorates:toggleStatus";
    #endregion

    #region City Permissions
    public const string GetCities = "cities:read";
    public const string CreateCities = "cities:create";
    public const string UpdateCities = "cities:update";
    public const string ToggleStatusCities = "cities:toggleStatus";
    #endregion

    #region Roles Permissions
    public const string GetPermissions = "permissions:read";
    public const string GetRoles = "roles:read";
    public const string CreateRoles = "roles:create";
    public const string UpdateRoles = "roles:update";
    public const string ToggleStatus = "roles:toggleStatus";
    #endregion

    #region Companies Permissions
    public const string GetCompanies = "companies:read";
    public const string CreateCompanies = "companies:create";
    public const string UpdateCompanies = "companies:update";
    public const string ToggleStatusCompanies = "companies:toggleStatus";
    #endregion

    #region Products Permissions
    public const string GetProducts = "products:read";
    public const string CreateProducts = "products:create";
    public const string UpdateProducts = "products:update";
    public const string ToggleStatusProducts = "products:toggleStatus";
    #endregion

    #region Categories Permissions
    public const string GetCategories = "categories:read";
    public const string CreateCategories = "categories:create";
    public const string UpdateCategories = "categories:update";
    public const string ToggleStatusCategories = "categories:toggleStatus";
    #endregion

    public static IList<string> GetAllPermissions() 
    { 
        return typeof(Permissions).GetFields().Select(x=>x.GetValue(x) as string).ToList()!;
    }
}
