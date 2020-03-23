using DemoApplication.MVC.Models.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApplication.MVC.Models.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
    }

    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(StudentDbContext context) : base(context)
        {

        }
    }
}
