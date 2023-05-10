using Server.Context;
using Server.Models.Entities;
using Server.Repository.Interfaces;

namespace Server.Repository.Classes
{
    public class DiciplineRepository : Repository<Dicipline>, IDiciplineRepository
    {
        public DiciplineRepository(AppDbContext context) : base(context)
        {
        }
    }
}
