using AutoMapper;
using Blog.Application.DTO;
using Blog.Domain.Entities;

namespace Blog.Application.MapProfiles
{
    /// <summary>
    /// Defines a profile for automapper. Mapping from Post to PostDto and vice versa.
    /// </summary>
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>()
                .ReverseMap();
        }
    }
}
