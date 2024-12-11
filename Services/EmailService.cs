using System.Net;
using System.Net.Mail;

namespace BlogAspNet.Services;

public class EmailService
{
    public bool Send(
        string toName,
        string toEmail,
        string subject,
        string body,
        string fromName = "Equipe Alyson",
        string fromEmail = "alysonullirsch8@gmail.com")
    {
        var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port);
        
        smtpClient.Credentials = new NetworkCredential(Configuration.Smtp.Username, Configuration.Smtp.Password);
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.EnableSsl = true;
        
        var mail = new MailMessage();
        
        mail.From = new MailAddress(fromEmail, fromName);
        mail.To.Add(new MailAddress(toEmail, toName));
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        try
        {
            smtpClient.Send(mail);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

}