using Server.Context;
using Server.Models.Entities;
using Server.Repository.Interfaces;

namespace Server.Repository.Classes
{
    public class StudentRepository : Repository<Student>, IStudentsRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
