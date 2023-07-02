using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        //private readonly BookingDbContext _context;

        public EmployeeRepository(BookingDbContext context) : base(context) { }

        public Employee? GetEmployeeByEmail(string email)
        {
            /*return _context.Set<Employee>().Where(e => e.Email.Single(email));*/
            return _context.Employees.SingleOrDefault(e => e.Email == email);
        }

        public string? GetLastEmployeeNik()
        {
            return _context.Set<Employee>().ToList().Select(e => e.Nik).LastOrDefault();
        }

        public Employee? GetByEmailAndPhoneNumber(string data)
        {
            return _context.Set<Employee>().FirstOrDefault(e => e.PhoneNumber == data || e.Email == data);
        }
    }
}
