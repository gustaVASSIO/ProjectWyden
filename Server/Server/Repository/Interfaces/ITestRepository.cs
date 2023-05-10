using Server.Models.Entities;

namespace Server.Repository.Interfaces
{
    public interface ITestRepository: IRepository<Test>
    {
        Task<IEnumerable<Test>> GetTestEmptyResult();
    }
}
