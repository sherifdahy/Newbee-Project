namespace Newbee.BLL.DTO.Auth.Requests;

public record RegisterCompanyRequest
(
    string Name,
    string FirstName,
    string LastName,
    string SSN,
    string Email,
    string Password,
    string PhoneNumber,
    string TaxRegistrationNumber
);
