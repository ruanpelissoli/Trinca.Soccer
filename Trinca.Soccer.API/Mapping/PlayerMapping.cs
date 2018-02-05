using AutoMapper;
using Trinca.Soccer.Dto.Player;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.API.Mapping
{
    public static class PlayerMapping
    {
        public static IMappingExpression<Player, PlayerOutputDto> PlayerOutputMapping(
            IMapperConfigurationExpression config)
        {
            return config.CreateMap<Player, PlayerOutputDto>()
                .ForMember(dest => dest.GuestName, src => src.MapFrom(item => item.IsGuest ? $"{item.Name} ({item.Employee.Name})" : item.Name));
        }

        public static IMappingExpression<PlayerInputDto, Player> PlayerInputMapping(
            IMapperConfigurationExpression config)
        {
            return config.CreateMap<PlayerInputDto, Player>()
                  .ForMember(dest => dest.Employee, src => src.Ignore());
        }
    }
}