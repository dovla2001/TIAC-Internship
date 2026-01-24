namespace Application.Employee.Services
{
    public interface IJwtTokenGenerator
    {
        public Task<string> GenerateToken(string email, CancellationToken cancellationToken);
    }
}
