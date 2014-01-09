using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.XPath;
using System.IO;
using System.Xml;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
namespace EricProject.Site
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public string ss = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //StringBuilder string_builder = new StringBuilder();
            //using (StringWriter string_writer = new StringWriter(string_builder))
            //{
            //    using (XmlTextWriter xml_writer = new XmlTextWriter(string_writer))
            //    {     //build XML string
            //        xml_writer.Formatting = Formatting.Indented;
            //        xml_writer.WriteStartElement("Transaction");
            //        xml_writer.WriteElementString("ExactID", "AD8837-06");//Gateway ID
            //        xml_writer.WriteElementString("Password", "9clt490v");//Password
            //        xml_writer.WriteElementString("Transaction_Type", "00");
            //        xml_writer.WriteElementString("DollarAmount", "1.66");
            //        xml_writer.WriteElementString("Expiry_Date", "1214");
            //        xml_writer.WriteElementString("CardHoldersName", "Jitendra Agrawal");
            //        xml_writer.WriteElementString("Card_Number", "4111111111111111");
            //        xml_writer.WriteElementString("VerificationStr2", "0614");
            //        xml_writer.WriteEndElement();
            //        String xml_string = string_builder.ToString();

            //        //SHA1 hash on XML string
            //        ASCIIEncoding encoder = new ASCIIEncoding();
            //        byte[] xml_byte = encoder.GetBytes(xml_string);
            //        SHA1CryptoServiceProvider sha1_crypto = new SHA1CryptoServiceProvider();
            //        string hash = BitConverter.ToString(sha1_crypto.ComputeHash(xml_byte)).Replace("-", "");
            //        string hashed_content = hash.ToLower();

            //        //assign values to hashing and header variables
            //        string method = "POST\n";
            //        string type = "application/xml\n";//REST XML
            //        string time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            //        string url = "/transaction/v12";
            //        string keyID = "44024 ";//key ID
            //        string key = "Rw~98Gnzsai_YYNWUMS89hwpf5jtwxOV";//Hmac key
            //        string hash_data = method + type + hashed_content + "\n" + time + "\n" + url;
            //        //hmac sha1 hash with key + hash_data
            //        HMAC hmac_sha1 = new HMACSHA1(Encoding.UTF8.GetBytes(key)); //key
            //        byte[] hmac_data = hmac_sha1.ComputeHash(Encoding.UTF8.GetBytes(hash_data)); //data
            //        //base64 encode on hmac_data
            //        string base64_hash = Convert.ToBase64String(hmac_data);

            //        //begin HttpWebRequest // use https://api.globalgatewaye4.firstdata.com/transaction/v12 for production
            //        HttpWebRequest web_request = (HttpWebRequest)WebRequest.Create("https://api.demo.globalgatewaye4.firstdata.com/transaction/v12");
            //        web_request.Method = "POST";
            //        web_request.Accept = "application/xml";
            //        web_request.Headers.Add("x-gge4-date", time);
            //        web_request.Headers.Add("x-gge4-content-sha1", hashed_content);
            //        web_request.ContentLength = xml_string.Length;
            //        web_request.ContentType = "application/xml";
            //        web_request.Headers["Authorization"] = "GGE4_API " + keyID + ":" + base64_hash;

            //        // send request as stream
            //        StreamWriter xml = null;
            //        xml = new StreamWriter(web_request.GetRequestStream());
            //        xml.Write(xml_string);
            //        xml.Close();
            //        Response.Write(xml_string + "<br/>");
            //        Response.Write(web_request.GetRequestStream());
            //        //get response and read into string
            //        string response_string;
            //        try
            //        {
            //            Response.Write("2");
            //            HttpWebResponse web_response = (HttpWebResponse)web_request.GetResponse();
            //            Response.Write("2ss");
            //            using (StreamReader response_stream = new StreamReader(web_response.GetResponseStream()))
            //            {
            //                Response.Write("5");
            //                response_string = response_stream.ReadToEnd();
            //                Response.Write("6");
            //                response_stream.Close();
            //                Response.Write("7");
            //            }
            //            //load xml
            //            Response.Write("3");
            //            XmlDocument xmldoc = new XmlDocument();
            //            xmldoc.LoadXml(response_string);
            //            XmlNodeList nodelist = xmldoc.SelectNodes("TransactionResult");
            //            //bind XML source DataList control
            //            DataList1.DataSource = nodelist;
            //            DataList1.DataBind();
            //            Response.Write("4");
            //            //output raw XML for debugging
            //            request_label.Text = web_request.Headers.ToString() + System.Web.HttpUtility.HtmlEncode(xml_string);
            //            response_label.Text = web_response.Headers.ToString() + System.Web.HttpUtility.HtmlEncode(response_string);
            //        }
            //        catch (System.Exception ex)
            //        {
            //            error.Text = ex.ToString();
            //        }
            //    }
            //}
        }
        protected override void SavePageStateToPersistenceMedium(object viewState)
        {

        }
        protected override object LoadPageStateFromPersistenceMedium()
        {

            return null;

        }
    }
}