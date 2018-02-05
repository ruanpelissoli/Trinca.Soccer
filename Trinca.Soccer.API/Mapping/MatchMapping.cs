using AutoMapper;
using Trinca.Soccer.Dto.Match;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.API.Mapping
{
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
                .ForMember(dest => dest.Score, src => src.MapFrom(item => item.IsFinished ? $"{item.YellowTeamScore}x{item.BlackTeamScore}" : string.Empty));
        }
    }
}