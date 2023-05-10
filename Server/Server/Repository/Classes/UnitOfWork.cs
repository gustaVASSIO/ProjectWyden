using Server.Context;
using Server.Repository.Interfaces;

namespace Server.Repository.Classes
{
    public class UnitOfWork: IUnitOfWork
    {
        private CourseRepository _courseRepo;
        
        private DiciplineRepository _diciplineRepo;

        private StudentRepository _studentRepo;

        private ProfessorRepository _professorRepo;
        
        private TestRepository _testRepo;

        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICourseRepository CourseRepository
        {
            get
            {
                return _courseRepo = _courseRepo ?? new CourseRepository(_context);
            }
        }

        public IDiciplineRepository DiciplineRepository
        {
            get
            {
                return _diciplineRepo = _diciplineRepo ?? new DiciplineRepository(_context);
            }
        }

        public IStudentsRepository StudentsRepository
        {
            get
            {
                return _studentRepo = _studentRepo ?? new StudentRepository(_context);
            }
        }

        public IProfessorRepository ProfessorsRepository
        {
            get
            {
                return _professorRepo = _professorRepo ?? new Classes.ProfessorRepository(_context);
            }
        }

        public ITestRepository TestRepository
        {
            get
            {
                return _testRepo = _testRepo ?? new Classes.TestRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
