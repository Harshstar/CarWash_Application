using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using Backend.Models.Domain;
using Microsoft.Extensions.Options;

namespace Backend.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
 
        // 🔹 Inject EmailSettings properly
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value ?? throw new ArgumentNullException(nameof(emailSettings));
        }
 
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            email.To.Add(new MailboxAddress("", toEmail));
            email.Subject = subject;
 
            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            email.Body = bodyBuilder.ToMessageBody();
 
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
 
        public async Task SendOtpEmailAsync(string toEmail, string otp)
        {
            string subject = "Your OTP for Password Reset";
            string body = $"<h3>Your OTP for password reset is: <strong>{otp}</strong></h3><p>Use this OTP to reset your password.</p>";
 
            await SendEmailAsync(toEmail, subject, body);
        }
    }
}