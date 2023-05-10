using Newtonsoft.Json;
using Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.Entities
{
    public class Test
    {
        [Key]
        public int TestId { get; set; }

        [Required]
        public DateTime Date{ get; set; }

        [Required]
        public TestTypes Type { get; set; }

        public float? Result { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int DiciplineId { get; set; }

        public int ProfessorId { get; set; }

        [JsonIgnore]
        public Student? Student { get; set; }

        [JsonIgnore]
        public Dicipline? Dicipline { get; set; }

        [JsonIgnore]
        public Professor? Professor { get; set; }


    }
}
