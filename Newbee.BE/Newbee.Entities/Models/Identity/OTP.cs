namespace Newbee.Entities;

public class OTP
{

    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime ExpiryTime { get; set; }

    public int ApplicationUserId { get; set; }

    public virtual ApplicationUser? User { get; set; }


}
