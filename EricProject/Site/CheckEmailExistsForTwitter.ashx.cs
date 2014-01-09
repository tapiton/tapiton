using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using BAL;
using DAL;
using BusinessObject;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using Encryption64;

namespace EricProject.Site
{
    /// <summary>
    /// Summary description for CheckEmailExistsForTwitter
    /// </summary>
    public class CheckEmailExistsForTwitter : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //Customer
            context.Response.ContentType = "text/plain";
            string customeremail = context.Request.QueryString["customeremail"].ToString();

            _plugin objPlugin = new _plugin();
            objPlugin.EmailID = customeremail;
            objPlugin.Password = "";
            objPlugin.IsFacebook = false;
            objPlugin.IsTwitter = false;
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            SqlDataReader drPlugin = sqlPlugin.CheckCustomerLoginPage(objPlugin);
            if (drPlugin.Read())
            {
                if (drPlugin["Customer_ID"].ToString() != "0")
                    context.Response.Write("1");
                else
                    if (HttpContext.Current.Session["oauth_token"] != null)
                    {
                        //Insert Into Customer master
                        try
                        {
                            if (drPlugin["Customer_Id"].ToString() != "0")
                            {
                                if (Convert.ToBoolean(drPlugin["Is_Twitter"].ToString()) == false)
                                {
                                    _plugin objCredits = new _plugin();
                                    objCredits.EmailID = customeremail;
                                    SqlDataReader drcredits = sqlPlugin.Is_credits(objCredits);
                                    if (drcredits.Read())
                                    {
                                        if (Convert.ToInt32(drcredits["Credits"].ToString()) > 0)
                                        {
                                            string EmailContent = "";
                                            string URL =context.Server.MapPath("~/EmailTemplate/CustomerThanks/CustomerActivation.htm");
                                            StreamReader SR = new StreamReader(URL);
                                            EmailContent = SR.ReadToEnd();
                                            SR.Close();
                                            EmailContent = EmailContent.Replace("{Link}", "Please <a href='"+ConfigurationManager.AppSettings["pageURL"]+"Site/Custometer/Activation?u=" + context.Server.UrlEncode(customeremail + "^" + 2) + "'>Click here</a> To Activate Your Account.<br/><br/>");
                                            EmailContent = GetEmailHeaderFooter(customeremail).Replace("{BODYCONTENT}", EmailContent);
                                            comman.SendMail(customeremail, "Activation Mail", EmailContent);
                                           //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Please check your email to activate');window.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Login" + "';", true);
                                            context.Response.Write("2");
                                        }
                                        else
                                        {
                                           
                                            objPlugin.Customer_ID = 0;
                                            objPlugin.FirstName = HttpContext.Current.Session["CustomerName"] + "";
                                            objPlugin.LastName = "";
                                            objPlugin.EmailID = customeremail;
                                            objPlugin.Password = "";
                                            objPlugin.IsActive = false;
                                            objPlugin.Address = "";
                                            objPlugin.City = "";
                                            objPlugin.State = "";
                                            objPlugin.Country_ID = "";
                                            objPlugin.Zip = 1;
                                            objPlugin.PhoneNumber = "";
                                            objPlugin.IsFacebook = false;
                                            objPlugin.IsTwitter = true;
                                            objPlugin.Status = "2";
                                            objPlugin.Facebook_Id = "";
                                            SqlDataReader dr = sqlPlugin.UpdateCustomer_Master(objPlugin);

                                            HttpContext.Current.Session["CustomerID"] = drPlugin["Customer_ID"].ToString();
                                            HttpContext.Current.Session["CustomerEmailId"] = drPlugin["Email_Id"].ToString();
                                            context.Response.Write("3");
                                           //context.Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Dashboard");
                                        }
                                    }
                                }
                                else
                                {
                                    
                                    objPlugin.Customer_ID = 0;
                                    objPlugin.FirstName = HttpContext.Current.Session["CustomerName"] + "";
                                    objPlugin.LastName = "";
                                    objPlugin.EmailID = customeremail;
                                    objPlugin.Password = "";
                                    objPlugin.IsActive = false;
                                    objPlugin.Address = "";
                                    objPlugin.City = "";
                                    objPlugin.State = "";
                                    objPlugin.Country_ID = "";
                                    objPlugin.Zip = 1;
                                    objPlugin.PhoneNumber = "";
                                    objPlugin.IsFacebook = false;
                                    objPlugin.IsTwitter = true;
                                    objPlugin.Status = "2";
                                    objPlugin.Facebook_Id = "";
                                    SqlDataReader dr = sqlPlugin.UpdateCustomer_Master(objPlugin);

                                    HttpContext.Current.Session["CustomerID"] = drPlugin["Customer_ID"].ToString();
                                    HttpContext.Current.Session["CustomerEmailId"] = drPlugin["Email_Id"].ToString();
                                    context.Response.Write("3");
                                    //context.Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Dashboard");
                                }
                            }
                            else
                            {
                                
                                objPlugin.Customer_ID = 0;
                                objPlugin.FirstName = HttpContext.Current.Session["CustomerName"] + "";
                                objPlugin.LastName = "";
                                objPlugin.EmailID = customeremail;
                                objPlugin.Password = "";
                                objPlugin.Address = "";
                                objPlugin.City = "";
                                objPlugin.State = "";
                                objPlugin.Country_ID = "";
                                objPlugin.Zip = 1;
                                objPlugin.PhoneNumber = "";
                                objPlugin.IsFacebook = false;
                                objPlugin.IsTwitter = true;
                                objPlugin.IsActive = false;
                                objPlugin.Twitter_Id = HttpContext.Current.Session["TwitterId"] + "";
                                SqlDataReader dr = sqlPlugin.InsertIntoCustomer_MasterTwitter(objPlugin);
                                while (dr.Read())
                                {
                                    HttpContext.Current.Session["CustomerID"] = dr["Customer_ID"].ToString();
                                    HttpContext.Current.Session["CustomerEmailId"] = dr["Email_Id"].ToString();
                                    context.Response.Write("3");
                                   //context.Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Dashboard");
                                }
                            }

                            DBAccess.InstanceCreation().disconnect();
                        }
                        catch(Exception ex) { }
                        //Insert Into Customer master
                    }
            }
        }
        public string GetEmailHeaderFooter(string Email)
        {
            //Header Footer Email Code
            StreamReader HeaderFooterSR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Standard/Header_Footer.htm"));
            string HeaderFooter = HeaderFooterSR.ReadToEnd();
            HeaderFooterSR.Close();
            string BasePath = ConfigurationManager.AppSettings["pageURL"].ToString();
            HeaderFooter = HeaderFooter.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
            HeaderFooter = HeaderFooter.Replace("{logoURL}", "<img src='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "images/newimages/logo.png' alt='" + ConfigurationManager.AppSettings["site_name"].ToString() + "' title='" + ConfigurationManager.AppSettings["site_name"].ToString() + "'/>");
            HeaderFooter = HeaderFooter.Replace("{UNSUBSCRIBEURL}", BasePath + "Site/EmailUnsubscription.aspx?e=" + HttpContext.Current.Server.UrlEncode(new EncryptDecrypt().Encrypt(Email, ConfigurationManager.AppSettings["PublicKey"].ToString())));
            return HeaderFooter;
            //Header Footer Email Code
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}