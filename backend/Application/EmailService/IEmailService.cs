using Domain.Entities;

namespace Application.EmailService
{
    public interface IEmailService
    {
        public Task SendOrderConfirmationEmailAsync(Orders orders, string employeeEmail, CancellationToken cancellationToken);

        public Task SendAdminOrderNotificationAsync(Orders orders, string adminMail, CancellationToken cancellationToken);
    }
}
