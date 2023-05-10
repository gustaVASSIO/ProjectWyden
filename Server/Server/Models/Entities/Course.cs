using System.ComponentModel.DataAnnotations;

namespace Server.Models.Entities
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [StringLength(40)]
        public string? Name { get; set; }

        public ICollection<Student>? Students { get; set; }

        public ICollection<Dicipline>? Diciplines { get; set; }

    }
}
