using AutoMapper;

namespace Trinca.Soccer.API.Mapping
{
    public class MappingConfig
    {
        static IMapper mapper;

        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<Poster, PosterOutputDTO>();
                //cfg.CreateMap<PetPicture, PetPictureOutputDTO>();
            });

            mapper = config.CreateMapper();
        }

        public static IMapper Mapper()
        {
            return mapper;
        }
    }
}