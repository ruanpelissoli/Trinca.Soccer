using AutoMapper;

namespace Trinca.Soccer.Services.Mapping
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
}