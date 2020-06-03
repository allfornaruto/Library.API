using AutoMapper;
using Library.API.Entities;
using Library.API.Models;
using System;

namespace Library.API.Helpers
{
    public class LibraryMappingProfile: Profile
    {
        public LibraryMappingProfile()
        {
            // 创建对象映射关系
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Age, config => config.MapFrom(src => DateTime.Now.Year - src.BirthDate.Year));
            CreateMap<AuthorForCreationDto, Author>();

            CreateMap<Book, BookDto>();
            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForUpdateDto, Book>();
        }
    }
}
