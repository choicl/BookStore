namespace BookStore.API.DTOModels.Book
{
    public class BookUpdateDTO : BaseDTO
    {
        public string? Title { get; set; }
        public int? Year { get; set; }
        public string? Isbn { get; set; }
        public string? Summary { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }
        public int? TotalPages { get; set; }
        public decimal? Rating { get; set; }
        public DateTime? PublishDate { get; set; }

    }
}
