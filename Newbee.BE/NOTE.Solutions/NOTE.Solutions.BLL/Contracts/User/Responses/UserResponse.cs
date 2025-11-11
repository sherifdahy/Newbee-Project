using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.User.Responses;
public class UserResponse
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }
    public IEnumerable<string> Roles { get; set; } = [];
}
