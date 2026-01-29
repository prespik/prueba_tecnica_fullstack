using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = null!;

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
