using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Abstractions.Consts;
public static class DefaultUsers
{
    public static int AdminId = 1;
    public static string AdminEmail = "admin@newbee.com";
    public static string AdminPassword = "333Sherif%";
    public static string AdminSecurityStamp = "3F52186E-A158-4F4A-8822-ABEE912135EB";
    public static string AdminConcurrencyStamp = "A999212D-E3FE-410C-BEA5-888AD868482C";

    public static int ManagerId = 2;
    public static string ManagerEmail = "Manager@newbee.com";
    public static string ManagerPassword = "333Sherif%";
    public static string ManagerSecurityStamp = "9B7F1E11-13B3-4FF7-95CB-49F4B81486EE";
    public static string ManagerConcurrencyStamp = "A6A25E9C-4768-4F1C-87F6-2D5E1CE80252";

    public static int ClientId = 3;
    public static string ClientEmail = "client@newbee.com";
    public static string ClientPassword = "333Sherif%";
    public static string ClientSecurityStamp = "B770584C-684A-40C4-87E3-BC175BC23550";
    public static string ClientConcurrencyStamp = "F8F79F8F-BBAE-47BD-BF9D-BDFA8BE91136";
}
