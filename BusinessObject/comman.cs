using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
namespace BusinessObject
{
    public class comman
    {
        public static  string getData(object Value, string defaultValue)
        {
            if (Value != null && Value.ToString() != "")
                return Value.ToString();
            else
                return defaultValue;
        }
        public static int getData(object Value, int defaultValue)
        {
            if (Value != null && Value.ToString() != "")
                return Convert.ToInt32(Value);
            else
                return defaultValue;
        }
        public static decimal getData(object Value, decimal defaultValue)
        {
            if (Value != null && Value.ToString() != "")
                return Convert.ToDecimal(Value);
            else
                return defaultValue;
        }
        public class DropdownClass
        {
            public DropdownClass(int ValueP, string TextP)
            {
                Value = ValueP;
                Text = TextP;
            }
            public int Value { get; set; }
            public string Text { get; set; }
        }
        public static void SendMail(string ToEmail,  string subjectsend, string messagesend)
        {
            try
            {
                var fromAddress = new MailAddress("noreply@tapiton.com", "Tap It On");
                var toAddress = new MailAddress(ToEmail);
                const string UserName = "AKIAIJ6GPJKNYKXVBJLQ";
                const string Password = "ApYjCYLyRR9LBFERugGZx/PCI25g8Z7GKmi30TZX2Saz";
                string subject = subjectsend;
                string body = messagesend;

                var smtp = new SmtpClient
                {
                    Host = "email-smtp.us-east-1.amazonaws.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(UserName, Password)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    message.Bcc.Add("sentmail@tapiton.com");
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["MerchantID"] != null)
                {
                    HttpContext.Current.Response.Redirect(WebConfigurationManager.AppSettings["pageURL"].ToString() + "Site/SMTP_ErrorHandling.aspx");
                }
                else if (HttpContext.Current.Session["CustomerEmailId"] != null)
                {
                    HttpContext.Current.Response.Redirect(WebConfigurationManager.AppSettings["pageURL"].ToString() + "Site/SMTP_errorHandling_Customer.aspx");
                }
            }
        }
        public static void SendMailfrom(string sToEmail, string sFromEmail, string emailsubject, string emailcontent, string AttachmentPathExcel, string AttachmentPathPDF)
        {
            string sHeader, sMessage;

            sHeader = emailsubject;
            sMessage = emailcontent;

            MailMessage mail = new MailMessage();

            //  mail.From = new MailAddress( sFromEmail , sFromEmail);
            mail.From = new MailAddress(sFromEmail, sFromEmail);         
            // mail.From = new MailAddress(sFromEmail);
            mail.To.Add(new MailAddress(sToEmail, sToEmail));
            //if (ccEmail)
            //{
            //    mail.CC.Add(ToEmailSettings);
            //}
            //set the content
            mail.Subject = sHeader;
            mail.Body = sMessage;
            mail.IsBodyHtml = true;
            if (AttachmentPathExcel != "")
            {
                Attachment myAttachment = new Attachment(AttachmentPathExcel);
                mail.Attachments.Add(myAttachment);
            }
            if (AttachmentPathPDF != "")
            {
                Attachment myAttachment = new Attachment(AttachmentPathPDF);
                mail.Attachments.Add(myAttachment);
            }
            //send the message
            // string susername, spass;
            //susername = "socialreferral.testing@gmail.com";
            // susername = FromEmailSettings;
            // spass = "flexsin$$#7856";
            //System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(susername, spass);

            const string UserName = "AKIAIJ6GPJKNYKXVBJLQ";
            const string Password = "ApYjCYLyRR9LBFERugGZx/PCI25g8Z7GKmi30TZX2Saz";
            var smtp = new SmtpClient
            {
                Host = "email-smtp.us-east-1.amazonaws.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(UserName, Password)
                //Credentials = new NetworkCredential(FromEmailSettings, "flexsin$$#789")
            };
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
            }
            
        }
        public static string FormatCredits(object credits)
        {
            try
            {
                Decimal dec = Convert.ToDecimal(credits);
                if (dec > 0)
                    return String.Format("{0:C}", dec).Replace(".00","").Replace("$","");
                else
                    return "0";
            }
            catch
            {
                return "0";
            }
        }
        public static string Analyticscompare(object Value, string defaultValue,string sign)
        {
            if (Value.ToString().Replace('-',' ') != "0" && Value.ToString() !="0.00")
                if(sign=="%")
                    return  Value.ToString()+sign;
                else

                return sign+Value.ToString();
            else
                return defaultValue;
        }
   
    }
}
