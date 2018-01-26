using AutoMapper;
using Trinca.Soccer.Dto.Employee;
using Trinca.Soccer.Dto.Login;
using Trinca.Soccer.Dto.Match;
using Trinca.Soccer.Dto.Player;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.API.Mapping
{
    public class MappingConfig
    {
        private static IMapper _mapper;

        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                EmployeeMapping.EmployeeLoginMapping(cfg);
                EmployeeMapping.EmployeeOutputMapping(cfg);
                EmployeeMapping.EmployeeInputMapping(cfg);

                MatchMapping.MatchInputMapping(cfg);
                MatchMapping.MatchOutputMapping(cfg);
                MatchMapping.MatchListOutputMapping(cfg);

                PlayerMapping.PlayerInputMapping(cfg);
                PlayerMapping.PlayerOutputMapping(cfg);
            });

            _mapper = config.CreateMapper();
        }

        public static IMapper Mapper()
        {
            return _mapper;
        }
    }

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

    public static class MatchMapping
    {
        public static IMappingExpression<Match, MatchOutputDto> MatchOutputMapping(
            IMapperConfigurationExpression config)
        {
            return config.CreateMap<Match, MatchOutputDto>();
        }

        public static IMappingExpression<MatchInputDto, Match> MatchInputMapping(
            IMapperConfigurationExpression config)
        {
            return config.CreateMap<MatchInputDto, Match>();
        }

        public static IMappingExpression<Match, MatchListOutputDto> MatchListOutputMapping(
            IMapperConfigurationExpression config)
        {
            return config.CreateMap<Match, MatchListOutputDto>()
                .ForMember(dest => dest.Place, src => src.MapFrom(item => $"{item.Place} - {item.Date:dd/MM/yyyy hh:mm:ss}"))
                .ForMember(dest => dest.CreatedBy, src => src.MapFrom(item => item.Employee != null ? $"Created by: {item.Employee.Name}" : string.Empty))
                .ForMember(dest => dest.Score, src => src.MapFrom(item => item.IsFinished ? $"{item.BlueTeamScore}x{item.RedTeamScore}" : string.Empty));
        }
    }

    public static class PlayerMapping
    {
        public static IMappingExpression<Player, PlayerOutputDto> PlayerOutputMapping(
            IMapperConfigurationExpression config)
        {
            return config.CreateMap<Player, PlayerOutputDto>()
                .ForMember(dest => dest.Name, src => src.MapFrom(item => item.IsGuest ? $"{item.Name} ({item.Employee.Name})" : item.Name));
        }

        public static IMappingExpression<PlayerInputDto, Player> PlayerInputMapping(
            IMapperConfigurationExpression config)
        {
            return config.CreateMap<PlayerInputDto, Player>();
        }
    }
}