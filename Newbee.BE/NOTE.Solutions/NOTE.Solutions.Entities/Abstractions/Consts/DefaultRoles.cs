using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Abstractions.Consts;
public static class DefaultRoles
{
    public const string Admin = nameof(Admin);
    public const string AdminRoleConcurrencyStamp = "727F7C04-0A04-4012-9AA0-5BDF83C53788";
    public const int AdminRoleId = 1;

    public const string Member = nameof(Member);
    public const string MemberRoleConcurrencyStamp = "10208DE5-8AD0-41E3-BFED-CFD49C46BEDF";
    public const int MemberRoleId= 2;

    public const string Manager = nameof(Manager);
    public const string ManagerRoleConcurrencyStamp = "34CA1036-7202-46C1-8C0D-D278841CECD8";
    public const int ManagerRoleId = 3;

    public const string Employee = nameof(Employee);
    public const string EmployeeRoleConcurrencyStamp = "D5CD1328-D599-4608-B5B0-C00056B6E7D7";
    public const int EmployeeRoleId = 4;

    public const string Support = nameof(Support);
    public const string SupportRoleConcurrencyStamp = "B8EF8D04-CA96-49D2-AB38-A1645EA8E7BB";
    public const int SupportRoleId = 5;


}
