namespace Newbee.BLL.Errors;
public class CompanyErrors
{
    public static readonly Error NotFound 
        = new Error("Company.NotFound","Company is Not Exist.",StatusCodes.Status404NotFound);
    public static readonly Error InvalidId
        = new Error("Company.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
}
