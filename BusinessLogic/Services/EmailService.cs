using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.IO;

namespace BusinessLogic.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com"; // SMTP-сервер Gmail
        private readonly int _smtpPort = 587; // Порт для Gmail
        private readonly string _smtpUser = "skrypka.danylo@chnu.edu.ua"; // Ваша email-адреса
        private readonly string _smtpPassword = "20052010asdfg"; // Ваш пароль або App Password

        public async Task<bool> SendTicketEmailAsync(string recipientEmail, string subject, string body, byte[] ticketBytes, string ticketFileName = "ticket.pdf")
        {
            try
            {
                using (var client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    // Налаштування підключення
                    client.Credentials = new NetworkCredential(_smtpUser, _smtpPassword);
                    client.EnableSsl = true; // Використовуємо SSL для безпеки

                    // Створення листа
                    using (var message = new MailMessage())
                    {
                        message.From = new MailAddress(_smtpUser, "КіноМанія"); // Відправник
                        message.To.Add(recipientEmail); // Отримувач
                        message.Subject = subject; // Тема листа
                        message.Body = body; // Текст листа
                        message.IsBodyHtml = true; // Дозволяємо HTML-форматування

                        // Додаємо квиток у вкладення
                        using (var memoryStream = new MemoryStream(ticketBytes))
                        {
                            var attachment = new Attachment(memoryStream, ticketFileName, "application/pdf");
                            message.Attachments.Add(attachment);

                            // Відправка листа
                            await client.SendMailAsync(message);
                        }
                    }
                }

                return true; // Лист успішно відправлено
            }
            catch (Exception ex)
            {
                // Логування помилки (можна додати логування в файл або консоль)
                Console.WriteLine($"Помилка при відправці листа: {ex.Message}");
                return false; // Лист не відправлено
            }
        }
    }
}