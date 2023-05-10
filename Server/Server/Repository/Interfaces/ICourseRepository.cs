using Server.Models.Entities;

namespace Server.Repository.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetCourseWithDiciplines(int id);
    }
}
