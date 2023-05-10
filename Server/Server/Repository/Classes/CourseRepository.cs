using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.Models.Entities;
using Server.Repository.Interfaces;

namespace Server.Repository.Classes
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Course>> GetCourseWithDiciplines(int id)
        {
            if (_context.Courses != null)
            {
                return await _context.Courses
                     .Where(c => c.CourseId == id)
                     .Include(c => c.Diciplines)
                     .ToListAsync();
            }
            return new List<Course>();
 
        }
    }
}
