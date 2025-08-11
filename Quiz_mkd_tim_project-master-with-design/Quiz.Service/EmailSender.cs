//using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Quiz.Utility.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Utility
{
    public class EmailSender(IOptions<SmtpOptions> options) : IEmailSender
    {

        private readonly SmtpOptions _options = options.Value;
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            var message = new MimeMessage
            {
                Sender = new MailboxAddress(_options.SendersName, _options.SmtpUserName),
                Subject = subject,

            };

            message.From.Add(new MailboxAddress(_options.SendersName, _options.SmtpUserName));
            message.To.Add(new MailboxAddress(null, email));

            using var body = new TextPart(TextFormat.Html);
            body.Text = htmlMessage;
            message.Body = body;

            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var sockerOptions = SecureSocketOptions.Auto;

                    await smtp.ConnectAsync(_options.SmtpServer, _options.SmtpServerPort ?? 587, sockerOptions);

                    if (!string.IsNullOrEmpty(_options.SmtpServer))
                    {
                        var credentials = new NetworkCredential(_options.SmtpUserName, _options.SmtpPassword);
                        await smtp.AuthenticateAsync(credentials);
                    }
                    await smtp.SendAsync(message);
                    await smtp.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                // Handle SMTP command errors
                Console.WriteLine($"SMTP command error: {e.Message} ");
            }
            


            //logic for sending email
            //return Task.CompletedTask;
            //throw new NotImplementedException();
            //using var body = new TextPart(TextFormat.Html);
            //body.Text = htmlMessage;

            //using var message = new MimeMessage();
            //message.From.Add(new MailboxAddress(// configure this for my application 
            //        null,
            //        "noreply@example.com"
            //    ));

            //message.To.Add(new MailboxAddress(//sending message to user 
            //            null,
            //            email
            //       ));

            //message.Subject = subject;
            //message.Body = body;    

            //using var client = new SmtpClient();
            //await client.ConnectAsync(_options.Host,_options.Port);
            //await client.SendAsync(message);
            //await client.DisconnectAsync(true);

        }
    }
}