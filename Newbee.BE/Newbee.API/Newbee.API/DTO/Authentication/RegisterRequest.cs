namespace Newbee.API.DTO.Authentication;

public record RegisterRequest
(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string CompanyName,
    string CompanyAddress,
    string PhoneNumber,
    string TaxNumber
);
