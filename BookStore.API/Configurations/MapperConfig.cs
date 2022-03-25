using AutoMapper;
using BookStore.API.Data;
using BookStore.API.DTO_Models.Author;
using BookStore.API.DTO_Models.Book;

namespace BookStore.API.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();
            CreateMap<AuthorReadOnlyDTO, Author>().ReverseMap();
            CreateMap<Book, BookReadOnlyDTO>()
                .ForMember(q => q.AuthorName, 
                d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();

            CreateMap<Book, BookDetailsDTO>()
                .ForMember(q => q.AuthorName,
                d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();
        }
    }
}
