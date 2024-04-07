using Microsoft.EntityFrameworkCore;
using PRN221_VotingSystem.Models;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.Pkcs;
using System.Text;

namespace PRN221_VotingSystem.DAL
{
    public class OTPDAO
    {
        private static OTPDAO instance = null;
        private static readonly object instanceLock = new object();
        private OTPDAO() { }
        public static OTPDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OTPDAO();
                    }
                    return instance;
                }
            }
        }

        public void UpdateOrInsertToOTP(Otp OTP)
        {
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    var existingOTP = data.Otps.FirstOrDefault(o => o.Email == OTP.Email);

                    if (existingOTP == null)
                    {
                        data.Otps.Add(OTP);
                    }
                    else
                    {
                        existingOTP.Otp1 = OTP.Otp1;
                        data.Entry(existingOTP).State = EntityState.Modified;
                    }
                    data.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public string GenerateOTP()
        {
            Random random = new();
            const string chars = "0123456789";
            StringBuilder stringBuilder = new();

            for (int i = 0; i < 10; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }
            return stringBuilder.ToString();
        }

        public void SendMail(string Email, string Otp, string message)
        {
            string fromMail = "ductv6868@gmail.com";
            string fromPassword = "juwzpabifcbcpvym";
            MailMessage mailMessage = new()
            {
                From = new MailAddress(fromMail),
                Subject = "VotingSystem"
            };
            mailMessage.To.Add(new MailAddress(Email));
            mailMessage.Body = "<html><body>" + message + Otp + " </body></html>";
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(mailMessage);
        }
    }
}
