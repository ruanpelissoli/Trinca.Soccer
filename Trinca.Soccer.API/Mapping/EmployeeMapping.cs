using AutoMapper;
using Trinca.Soccer.Dto.Employee;
using Trinca.Soccer.Dto.Login;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.API.Mapping
{
    public static class EmployeeMapping
    {
        public static IMappingExpression<Employee, LoginOutputDto> EmployeeLoginMapping(
            IMapperConfigurationExpression config)
        {
            return config.CreateMap<Employee, LoginOutputDto>();
        }

        public static IMappingExpression<Employee, EmployeeOutputDto> EmployeeOutputMapping(
            IMapperConfigurationExpression config)
        {
            return config.CreateMap<Employee, EmployeeOutputDto>();
        }

        public static IMappingExpression<EmployeeInputDto, Employee> EmployeeInputMapping(
            IMapperConfigurationExpression config)
        {
            return config.CreateMap<EmployeeInputDto, Employee>();
        }
    }
}