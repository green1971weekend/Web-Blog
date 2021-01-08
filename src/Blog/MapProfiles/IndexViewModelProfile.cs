using Blog.Application.DTO;
using Blog.ViewModels;
using AutoMapper;

namespace Blog.MapProfiles
{
    /// <summary>
    /// Defines a profile for automapper. Mapping from PostCollectionDto to PostCollectionViewModel and vice versa.
    /// </summary>
    public class IndexViewModelProfile : Profile
    {
        public IndexViewModelProfile()
        {
            CreateMap<PostCollectionViewModel, PostCollectionDto>()
                .ReverseMap();
        }
    }
}
