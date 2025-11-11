using Microsoft.AspNetCore.Identity;

namespace NOTE.Solutions.Entities.Entities.Identity;
public class ApplicationUser : IdentityUser<int>
{
    public string Name { get; set; } = string.Empty;
    public string IdentifierNumber { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();

}
