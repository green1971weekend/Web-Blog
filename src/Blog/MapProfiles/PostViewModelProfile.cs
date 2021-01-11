using AutoMapper;
using Blog.Domain.Entities;
using Blog.ViewModels;

namespace Blog.MapProfiles
{
    /// <summary>
    /// Defines a profile for automapper. Mapping from Post to PostViewModel and vice versa.
    /// </summary>
    public class PostViewModelProfile : Profile
    {
        public PostViewModelProfile()
        {
            CreateMap<Post, PostViewModel>()
                .ForMember(model => model.CurrentImage, opt => opt.MapFrom(post => post.Image))
                .ForMember(model => model.Image, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
