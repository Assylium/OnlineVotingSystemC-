using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace PRN221_VotingSystem.Pages
{
    public class InviteVoterModel : PageModel
    {
        public IActionResult OnPostSendEmails([FromBody] List<string> emails)
        {
            if (emails == null || emails.Count == 0)
            {
                return BadRequest("Email addresses are required.");
            }

            try
            {
                string fromMail = "ductv6868@gmail.com";
                string fromPassword = "juwzpabifcbcpvym";

                foreach (var email in emails)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(fromMail);
                    mailMessage.Subject = "Invitation to Vote";
                    mailMessage.To.Add(new MailAddress(email));
                    mailMessage.Body = "https://localhost:7158/HomePage"; // Nội dung email
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromMail, fromPassword),
                        EnableSsl = true,
                    };
                    smtpClient.Send(mailMessage);
                }

                return Content("Emails sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to send emails: " + ex.Message);
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                var file = Request.Form.Files["fileInput"]; // Lấy tệp từ form
                if (file == null || file.Length == 0)
                {
                    return BadRequest("File is required.");
                }

                List<string> emails = ReadEmailsFromExcel(file);

                if (emails.Count == 0)
                {
                    return BadRequest("No email addresses found in the file.");
                }

                SendEmails(emails);

                return Content("Emails sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to send emails: " + ex.Message);
            }
        }

        private List<string> ReadEmailsFromExcel(IFormFile file)
        {
            List<string> emails = new List<string>();

            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 1; row <= rowCount; row++)
                {
                    string email = worksheet.Cells[row, 1].Value?.ToString();
                    if (!string.IsNullOrEmpty(email))
                    {
                        emails.Add(email);
                    }
                }
            }

            return emails;
        }


        private void SendEmails(List<string> emails)
        {
            string fromMail = "ductv6868@gmail.com";
            string fromPassword = "juwzpabifcbcpvym";

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(fromMail, fromPassword);
                smtpClient.EnableSsl = true;

                foreach (var email in emails)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(fromMail);
                    mailMessage.Subject = "Invitation to Vote";
                    mailMessage.To.Add(new MailAddress(email));
                    mailMessage.Body = "https://localhost:7158/HomePage"; // Nội dung email
                    mailMessage.IsBodyHtml = true;

                    smtpClient.Send(mailMessage);
                }
            }
        }

    }
}
