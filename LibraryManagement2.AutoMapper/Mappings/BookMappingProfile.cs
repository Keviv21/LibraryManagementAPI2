using AutoMapper;
using LibraryManagement2.Data.Entities;
using LibraryManagement2.Shared.DTOs;


namespace LibraryManagement2.AutoMapper.Profiles
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
