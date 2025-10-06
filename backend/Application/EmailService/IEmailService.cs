using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EmailService
{
    public interface IEmailService
    {
        public Task SendOrderConfirmationEmailAsync(Orders orders, string employeeEmail, CancellationToken cancellationToken);

        public Task SendAdminOrderNotificationAsync(Orders orders, string adminMail, CancellationToken cancellationToken);
    }
}
