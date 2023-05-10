using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.Entities
{
    [Index(nameof(AR), IsUnique = true)]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(60)]
        public string? Name { get; set; }

        [Required]
        [StringLength(12)]
        public string? AR { get; set; }

        [Required]
        public int CourseId { get; set; }

        public Course? Course { get; set; }

        public List<Test> Tests { get; set; } = new List<Test>();

    }
}
