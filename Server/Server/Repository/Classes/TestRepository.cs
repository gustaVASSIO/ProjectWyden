using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.Models.Entities;
using Server.Repository.Interfaces;

namespace Server.Repository.Classes
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Test>> GetTestEmptyResult()
        {
            return await _context.Tests.Where(t => t.Result == null).ToListAsync();
        }
    }
}
