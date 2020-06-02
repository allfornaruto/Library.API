using AutoMapper;
using Library.API.Entities;
using Library.API.Models;

namespace Library.API.Helpers
{
    public class LibraryMappingProfile: Profile
    {
        public LibraryMappingProfile()
        {
            // 创建对象映射关系
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Age, config => config.MapFrom(src => src.BirthDate));
            CreateMap<Book, BookDto>();
            CreateMap<AuthorForCreationDto, Author>();
            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForUpdateDto, Book>();
        }
    }
}
