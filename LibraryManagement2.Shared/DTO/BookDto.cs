using System.ComponentModel.DataAnnotations;

namespace LibraryManagement2.Shared.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [MaxLength(100)]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "ISBN is required")]
        [MaxLength(20)]
        public string ISBN { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Category { get; set; }

        public DateTime? PublishedDate { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
