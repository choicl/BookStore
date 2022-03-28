using AutoMapper;
using BookStore.API.Data;
using BookStore.API.DTOModels.Author;
using BookStore.API.DTOModels.Book;
using BookStore.API.DTOModels.User;

namespace BookStore.API.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();
            CreateMap<AuthorReadOnlyDTO, Author>().ReverseMap();

            CreateMap<BookCreateDTO, Book>().ReverseMap();
            CreateMap<BookUpdateDTO, Book>().ReverseMap();
            CreateMap<Book, BookReadOnlyDTO>()
                .ForMember(q => q.AuthorName, 
                d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();

            CreateMap<Book, BookDetailsDTO>()
                .ForMember(q => q.AuthorName,
                d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();

            CreateMap<APIUser, UserDTO>().ReverseMap();
        }
    }
}
