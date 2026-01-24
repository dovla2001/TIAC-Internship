using Domain.Entities;

namespace Application.Employee.CommonEmployees
{
    public interface IEmployeesRepository
    {
        public Task<Employees?> GetByIdEmployeesAsync(int id, CancellationToken cancellationToken);
        public Task<Employees> CreateEmployeesAsync(Employees employee, CancellationToken cancellationToken);
        public Task UpdateEmployeesAsync(Employees employees, CancellationToken cancellationToken);
        public Task DeleteEmployeesAsync(Employees employees, CancellationToken cancellationToken);

        public Task<Employees?> GetByEmailEmployeesAsync(string email, CancellationToken cancellationToken);
        public Task<Employees?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        public Task<List<Employees>> GetAllEmployeesAsync(CancellationToken cancellationToken);
        public Task<(List<Employees> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        public Task<List<Employees>> GetAdminsAsync(CancellationToken cancellationToken);
    }
}
