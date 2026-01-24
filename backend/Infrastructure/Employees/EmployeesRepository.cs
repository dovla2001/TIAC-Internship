using Application.Employee.CommonEmployees;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Employees
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly TiacWebShopDbContext _dbcontext;

        public EmployeesRepository(TiacWebShopDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Domain.Entities.Employees> CreateEmployeesAsync(Domain.Entities.Employees employee, CancellationToken cancellationToken = default)
        {
            var newEmployees = await _dbcontext.Employees.AddAsync(employee, cancellationToken);
            await _dbcontext.SaveChangesAsync(cancellationToken);
            return newEmployees.Entity;
        }

        public async Task<Domain.Entities.Employees?> GetByIdEmployeesAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbcontext.Employees.FirstOrDefaultAsync(e => e.EmployeesId == id, cancellationToken);
        }

        public async Task DeleteEmployeesAsync(Domain.Entities.Employees employees, CancellationToken cancellationToken)
        {
            _dbcontext.Employees.Remove(employees);
            await _dbcontext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateEmployeesAsync(Domain.Entities.Employees employees, CancellationToken cancellationToken)
        {
            _dbcontext.Employees.Update(employees);
            await _dbcontext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Domain.Entities.Employees?> GetByEmailEmployeesAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbcontext.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Email.ToUpper() == email.ToUpper(), cancellationToken);
        }

        public async Task<Domain.Entities.Employees?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            return await _dbcontext.Employees.FirstOrDefaultAsync(e => e.RefreshToken == refreshToken && e.RefreshTokenExpiryDate > DateTime.UtcNow, cancellationToken);
        }

        public async Task<List<Domain.Entities.Employees>> GetAllEmployeesAsync(CancellationToken cancellationToken)
        {
            return await _dbcontext.Employees.ToListAsync(cancellationToken);
        }

        public async Task<(List<Domain.Entities.Employees> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var totalCount = await _dbcontext.Employees.CountAsync(cancellationToken);

            var items = await _dbcontext.Employees
                .AsNoTracking()
                .OrderBy(e => e.LastName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        public async Task<List<Domain.Entities.Employees>> GetAdminsAsync(CancellationToken cancellationToken)
        {
            return await _dbcontext.Employees
                .Where(e => e.IsAdmin == true)
                .ToListAsync(cancellationToken);
        }
    }
}
