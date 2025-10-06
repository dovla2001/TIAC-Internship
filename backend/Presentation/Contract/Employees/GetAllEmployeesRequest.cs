namespace Presentation.Contract.Employees
{
    public class GetAllEmployeesRequest
    {
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 2;
    }
}
