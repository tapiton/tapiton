using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;

namespace EricProject.Site
{
    public partial class WebFormAuto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //      public function __doRequest( string request, string location,  string action,  string version, string one_way ) {
        //  string hmackey = "Rw~98Gnzsai_YYNWUMS89hwpf5jtwxOV"; // <-- Insert your HMAC key here
        //  string keyid = "44024"; // <-- Insert the Key ID here
        //  string hashtime = DateTime.Now.ToString();
        //  string hashstr = "POST\ntext/xml; charset=utf-8\n" . sha1($request) . "\n" . $hashtime . "\n" . parse_url($location,PHP_URL_PATH);
        //  string authstr = base64_encode(hash_hmac("sha1",$hashstr,$hmackey,TRUE));
        //  if (version_compare(PHP_VERSION, '5.3.11') == -1) {
        //      ini_set("user_agent", "PHP-SOAP/" . PHP_VERSION . "\r\nAuthorization: GGE4_API " . $keyid . ":" . $authstr . "\r\nx-gge4-date: " . $hashtime . "\r\nx-gge4-content-sha1: " . sha1($request));
        //  } else {
        //      stream_context_set_option($context,array("http" => array("header" => "authorization: GGE4_API " . $keyid . ":" . $authstr . "\r\nx-gge4-date: " . $hashtime . "\r\nx-gge4-content-sha1: " . sha1($request))));
        //  }
        //  return parent::__doRequest($request, $location, $action, $version, $one_way);
        //}

        protected void Buttonprocced_Click(object sender, EventArgs e)
        {
            StringBuilder string_builder = new StringBuilder();
            using (StringWriter string_writer = new StringWriter(string_builder))
            {
                using (XmlTextWriter xml_writer = new XmlTextWriter(string_writer))
                {     //build XML string
                    xml_writer.Formatting = Formatting.Indented;               
                    xml_writer.WriteStartElement("Transaction");
                    xml_writer.WriteElementString("User_Name", "flexsin744");
                    xml_writer.WriteElementString("ExactID", "AD8837-06");//Gateway ID
                    xml_writer.WriteElementString("Password", "55bd5i57");//Password 9clt490v
                    xml_writer.WriteElementString("Transaction_Type", "00");
                    xml_writer.WriteElementString("DollarAmount", "1.23");
                    xml_writer.WriteElementString("Expiry_Date", "0414");
                    xml_writer.WriteElementString("CardHoldersName", "Jitendra Agrawal");
                    xml_writer.WriteElementString("Card_Number", "4111111111111111");
                    xml_writer.WriteElementString("VerificationStr2", "123");
                    xml_writer.WriteElementString("Authorization_Num", "");
                    xml_writer.WriteElementString("Transaction_Tag", "");
                    xml_writer.WriteElementString("CVD_Presence_Ind", "");
                    xml_writer.WriteElementString("Secure_AuthRequired", "");
                    xml_writer.WriteElementString("Currency", "USD");
                    xml_writer.WriteElementString("PartialRedemption", "");
                    xml_writer.WriteElementString("TransarmorToken", "");

                    xml_writer.WriteEndElement();
                    String xml_string = string_builder.ToString();

                    //SHA1 hash on XML string
                    ASCIIEncoding encoder = new ASCIIEncoding();
                    byte[] xml_byte = encoder.GetBytes(xml_string);
                    SHA1CryptoServiceProvider sha1_crypto = new SHA1CryptoServiceProvider();
                    string hash = BitConverter.ToString(sha1_crypto.ComputeHash(xml_byte)).Replace("-", "");
                    string hashed_content = hash.ToLower();

                    //assign values to hashing and header variables
                    string method = "POST\n";
                    string type = "application/xml\n";//REST XML
                    string time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
                    string url = "/transaction/v12";
                    string keyID = "44030";//key ID
                    string key = "hyF4ByLELRRovbW7No1Gb0UJYR4SCWXo";//Hmac key Rw~98Gnzsai_YYNWUMS89hwpf5jtwxOV
                    string hash_data = method + type + hashed_content + "\n" + time + "\n" + url;
                    //hmac sha1 hash with key + hash_data
                    HMAC hmac_sha1 = new HMACSHA1(Encoding.UTF8.GetBytes(key)); //key
                    byte[] hmac_data = hmac_sha1.ComputeHash(Encoding.UTF8.GetBytes(hash_data)); //data
                    //base64 encode on hmac_data
                    string base64_hash = Convert.ToBase64String(hmac_data);

                    //begin HttpWebRequest // use https://api.globalgatewaye4.firstdata.com/transaction/v12 for production
                    //HttpWebRequest web_request = (HttpWebRequest)WebRequest.Create("https://api.globalgatewaye4.firstdata.com/transaction/v12/wsdl");                                                        
                    HttpWebRequest web_request = (HttpWebRequest)WebRequest.Create("https://api.demo.globalgatewaye4.firstdata.com/transaction/v11/wsdl");
                    web_request.Method = "POST";
                    web_request.Accept = "application/xml";
                    web_request.Headers.Add("x-gge4-date", time);
                    web_request.Headers.Add("x-gge4-content-sha1", hashed_content);
                    web_request.ContentLength = xml_string.Length;
                    web_request.ContentType = "application/xml";
                    web_request.Headers["Authorization"] = "GGE4_API " + keyID + ":" + base64_hash;

                    // send request as stream
                    StreamWriter xml = null;
                    xml = new StreamWriter(web_request.GetRequestStream());
                    xml.Write(xml_string);
                    xml.Close();
                    //Response.Write(xml_string + "<br/>");
                    //  Response.Write(web_request.GetRequestStream());
                    //get response and read into string
                    string response_string;
                    try
                    {
                       
                        HttpWebResponse web_response = (HttpWebResponse)web_request.GetResponse();

                        using (StreamReader response_stream = new StreamReader(web_response.GetResponseStream()))
                        {
                            // Response.Write("5");
                            response_string = response_stream.ReadToEnd();
                            // Response.Write("6");
                            response_stream.Close();
                            // Response.Write("7");
                        }
                        //load xml
                        // Response.Write("3");
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(response_string);
                        XmlNodeList nodelist = xmldoc.SelectNodes("TransactionResult");
                        //bind XML source DataList control
                        DataList1.DataSource = nodelist;
                        DataList1.DataBind();
                        //  Response.Write("4");
                        //output raw XML for debugging

                       // request_label.Text = web_request.Headers.ToString() + System.Web.HttpUtility.HtmlEncode(xml_string);
                        response_label.Text = web_response.Headers.ToString() + System.Web.HttpUtility.HtmlEncode(response_string);
                    }
                    catch (System.Exception ex)
                    {
                        error.Text = ex.ToString();
                    }
                }
            }

        }
    }
}