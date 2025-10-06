using Application.EmailService;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOrderConfirmationEmailAsync(Domain.Entities.Orders orders, string employeeEmail, CancellationToken cancellationToken)
        {
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "..\\Presentation\\Templates\\OrderConfirmation.html");
            string htmlBody = await File.ReadAllTextAsync(templatePath, cancellationToken);

            htmlBody = htmlBody.Replace("{{UserName}}", orders.Employees.FirstName);
            htmlBody = htmlBody.Replace("{{OrderId}}", orders.OrdersId.ToString());
            htmlBody = htmlBody.Replace("{{TotalPrice}}", orders.TotalPrice.ToString());

            var itemsHtml = new StringBuilder();
            foreach (var item in orders.OrderItems)
            {
                var productName = item.ProductVariants.Product.Name;

                var variantsDescription = string.Join(", ", item.ProductVariants.VariantValues
                    .Select(vv => $"{vv.AttributeValue.Attribute.Name}: {vv.AttributeValue.Value}"));

                var fullDescription = $"{productName} ({variantsDescription})";

                itemsHtml.Append($"<tr><td>{fullDescription}</td><td>{item.Quantity}</td><td>{item.Price:0.00} RSD</td></tr>");
            }

            htmlBody = htmlBody.Replace("{{OrderItems}}", itemsHtml.ToString());

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:Address"]));
            email.To.Add(MailboxAddress.Parse(employeeEmail));
            email.Subject = $"Order confirmation #{orders.OrdersId}";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlBody };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["EmailSettings:Host"], int.Parse(_config["EmailSettings:Port"]!), MailKit.Security.SecureSocketOptions.StartTls, cancellationToken);
            await smtp.AuthenticateAsync(_config["EmailSettings:Address"], _config["EmailSettings:Password"], cancellationToken);
            await smtp.SendAsync(email, cancellationToken);
            await smtp.DisconnectAsync(true, cancellationToken);
        }

        public async Task SendAdminOrderNotificationAsync(Domain.Entities.Orders orders, string adminMail, CancellationToken cancellationToken)
        {
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "..\\Presentation\\Templates\\AdminOrderNotification.html");
            string htmlBody = await File.ReadAllTextAsync(templatePath, cancellationToken);

            htmlBody = htmlBody.Replace("{{OrderId}}", orders.OrdersId.ToString());
            htmlBody = htmlBody.Replace("{{EmployeeName}}", $"{orders.Employees.FirstName} {orders.Employees.LastName}");
            htmlBody = htmlBody.Replace("{{EmployeeEmail}}", orders.Employees.Email);

            string formatedDate = orders.OrderDate.ToString("dd.MM.yyyy HH:mm:ss");
            htmlBody = htmlBody.Replace("{{DateTime}}", formatedDate);
            htmlBody = htmlBody.Replace("{{TotalPrice}}", orders.TotalPrice.ToString("0.00"));

            var itemsHtml = new StringBuilder();
            foreach (var item in orders.OrderItems)
            {
                var productName = item.ProductVariants.Product.Name;
                var variantsDescription = string.Join(", ", item.ProductVariants.VariantValues
                    .Select(vv => $"{vv.AttributeValue.Attribute.Name}: {vv.AttributeValue.Value}"));
                var fullDescription = $"{productName} ({variantsDescription})";
                itemsHtml.Append($"<tr><td>{fullDescription}</td><td>{item.Quantity}</td><td>{item.Price:0.00} RSD</td></tr>");
            }

            htmlBody = htmlBody.Replace("{{OrderItems}}", itemsHtml.ToString());

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:Address"]));
            email.To.Add(MailboxAddress.Parse(adminMail));
            email.Subject = $"New orders #{orders.OrdersId}";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlBody };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["EmailSettings:Host"], int.Parse(_config["EmailSettings:Port"]!), MailKit.Security.SecureSocketOptions.StartTls, cancellationToken);
            await smtp.AuthenticateAsync(_config["EmailSettings:Address"], _config["EmailSettings:Password"], cancellationToken);
            await smtp.SendAsync(email, cancellationToken);
            await smtp.DisconnectAsync(true, cancellationToken);
        }
    }
}
