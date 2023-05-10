using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Server.Models.Entities
{
    [Index(nameof(AR), IsUnique = true)]
    public class Professor
    {
        [Key]
        public int ProfessorId { get; set; }

        [StringLength(60)]
        public string? Name { get; set; }

        [StringLength(12)]
        public string? AR { get; set; }

        public int DiciplineId { get; set; }

        [JsonIgnore]
        public Dicipline? Dicipline { get; set; }

        [JsonIgnore]
        public List<Test> Tests { get; set; } = new List<Test>();
    }
}
