using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Application.Services;
using System.Net.Mail;
using System.Net;
using System;
using Volo.Abp;

namespace MMFinancial.Transactions
{
    public static class AppEmailSender
    {
        public static async Task SendEmailAsync(string title,string text, string userEmail)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("mmfinancialapp@hotmail.com");
                message.To.Add(new MailAddress(userEmail));
                message.Subject = title;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = text;
                smtp.Port = 587;
                smtp.Host = "smtp.live.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("mmfinancialapp@hotmail.com", "financial123");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }catch(Exception){ }
            }
        }
    }
