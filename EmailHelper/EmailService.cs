using System.Net.Mail;
using System.Net;

namespace ARTHVATECH_ADMIN.EmailHelper
{
    public class EmailService
    {
        private readonly string smtpHost = "in-v3.mailjet.com";
        private readonly int smtpPort = 587; // Use 465 for SSL
        private readonly string smtpUsername = "0aa87215cd7e7eff1b0b5f234f13a6a1"; // Replace with your Mailjet API key
        private readonly string smtpPassword = "584cfca94439acdf55e5bb4f6db88ec8"; // Replace with your Mailjet secret key

        public string SendEmail(string toEmail, string subject, string body, string html="")
        {
            try
            {
                var smtpClient = new SmtpClient(smtpHost)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true,  // Ensure SSL/TLS is enabled
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("info@arthvatech.com"),
                    Subject = subject,
                    Body = html,
                    IsBodyHtml = true,  // Set to true if you're sending HTML content
                };

                mailMessage.To.Add(toEmail);

                // Send email asynchronously
                smtpClient.Send(mailMessage);

                return ("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return ("Not Send");
            }
        }
    }
}
