using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Configuration;
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
                var fromAddress = new MailAddress("socialreferral.testing@gmail.com", "Flexsin");
                var toAddress = new MailAddress(ToEmail);
                const string fromPassword = "flexsin$$#7856";
                string subject = subjectsend;
                string body = messagesend;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["MerchantID"] != null)
                {
                     HttpContext.Current.Response.Redirect("http://socialreferral.onlineshoppingpool.com/Site/SMTP_ErrorHandling.aspx");
                }
                else if (HttpContext.Current.Session["CustomerEmailId"] != null)
                {
                     HttpContext.Current.Response.Redirect("http://socialreferral.onlineshoppingpool.com/Site/SMTP_errorHandling_Customer.aspx");
                }
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
