namespace Server.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; }
        IDiciplineRepository DiciplineRepository { get; }
        IStudentsRepository StudentsRepository { get; }
        IProfessorRepository ProfessorsRepository { get; }
        ITestRepository TestRepository { get;  }
        Task Commit();

        void Dispose();

    }
}
