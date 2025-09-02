namespace Newbee.BLL.DTO.Auth.Requests;

public record RegisterRequest
(
    string FirstName,
    string LastName,
    string Email,
    string SSN,
    string Password,
    string CompanyName,
    string CompanyAddress,
    string PhoneNumber,
    string TaxNumber
);
