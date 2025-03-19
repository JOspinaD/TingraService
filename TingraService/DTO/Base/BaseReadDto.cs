using System.ComponentModel.DataAnnotations;

namespace TingraService.DTO.Base
{
    public class BaseReadDto
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
