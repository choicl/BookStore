using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOModels.Author
{
    //using for POST
    public class AuthorCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(250)]
        public string Bio { get; set; }
    }
}
