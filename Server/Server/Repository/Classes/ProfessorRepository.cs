using Server.Context;
using Server.Models.Entities;
using Server.Repository.Interfaces;

namespace Server.Repository.Classes
{
    public class ProfessorRepository : Repository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(AppDbContext context) : base(context)
        {
        }
    }
}
