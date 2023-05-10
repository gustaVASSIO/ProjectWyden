using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Server.Models.Entities
{
    [Index(nameof(Code), IsUnique = true)]
    public class Dicipline
    {
        [Key]
        public int DiciplineId { get; set; }

        [StringLength(10)]
        public string? Code { get; set; }

        [StringLength(60)]
        public string? Name { get; set; }

        public ICollection<Course>? Courses { get; set; }



    }
}
