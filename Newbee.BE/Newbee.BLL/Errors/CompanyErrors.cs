namespace Newbee.BLL.Errors;
public class CompanyErrors
{
    public static readonly Error NotFound 
        = new Error("Company.NotFound","Company is Not Exist.",StatusCodes.Status404NotFound);
    public static readonly Error InvalidId
        = new Error("Company.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
    public static readonly Error DuplicatedTRN =
            new("Company.DuplicatedCompany", "Tax Registration Number is Already Exist.", StatusCodes.Status409Conflict);
    public static readonly Error InvalidApiKey =
            new("Company.InvalidApiKey", "ApiKey is Invalid", StatusCodes.Status400BadRequest);


}
