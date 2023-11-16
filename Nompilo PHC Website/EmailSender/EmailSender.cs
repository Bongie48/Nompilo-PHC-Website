using System.Net;
using System.Net.Mail;
namespace Nompilo_PHC_Website.EmailSender
{
    public class EmailSender: IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "enompileprimaryhealthcare@gmail.com";
            var password = "@Nompilo886";

            
            var Client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password),
             
        };

            return Client.SendMailAsync(
                new MailMessage(from: mail,
                                to: email,
                                subject,
                                message));
        }
    }
}
