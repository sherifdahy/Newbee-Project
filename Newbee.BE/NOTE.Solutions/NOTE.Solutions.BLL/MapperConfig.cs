using NOTE.Solutions.BLL.Contracts.Employee.Requests;
using NOTE.Solutions.BLL.Contracts.Employee.Responses;
using NOTE.Solutions.BLL.Contracts.Manager.Responses;
using NOTE.Solutions.Entities.Entities.Employee;
using NOTE.Solutions.Entities.Entities.Manager;

namespace NOTE.Solutions.API.ApplicationConfiguration;

public class MapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Company, CompanyResponse>()
            .Map(dest => dest.ActiveCodes, src => src.ActiveCodeCompanies.Select(x => x.ActiveCode));
            
        config.NewConfig<Branch, BranchResponse>()
            .Map(dest => dest.CityId, src => src.City.Id).Map(dest => dest.GovernorateId, src => src.City.GovernorateId)
            .Map(dest => dest.CountryId, src => src.City.Governorate.CountryId)
            .Map(dest => dest.Employees,src=>src.BranchEmplyees.Select(x=>x.Employee));

        config.NewConfig<Employee,EmployeeResponse>()
            .Map(dest=> dest.Email,src=> src.ApplicationUser.Email)
            .Map(dest => dest.Name, src => src.ApplicationUser.Name)
            .Map(dest => dest.PhoneNumber, src => src.ApplicationUser.PhoneNumber)
            .Map(dest=>dest.IdentifierNumber ,src=>src.ApplicationUser.IdentifierNumber);

        config.NewConfig<Manager,ManagerResponse>()
            .Map(dest => dest.Email,src => src.ApplicationUser.Email)
            .Map(dest => dest.Name,src => src.ApplicationUser.Name)
            .Map(dest => dest.PhoneNumber,src=>src.ApplicationUser.PhoneNumber)
            .Map(dest => dest.IdentifierNumber, src => src.ApplicationUser.IdentifierNumber);

    }
}
