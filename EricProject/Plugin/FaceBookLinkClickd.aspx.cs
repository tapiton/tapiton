using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EricProject.Plugin
{
    public partial class FaceBookLinkClickd : System.Web.UI.Page
    {
        string OfferID;
        protected void Page_Load(object sender, EventArgs e)
        {if (Page.RouteData.Values["OfferID"] != null)
            {
                OfferID = Page.RouteData.Values["OfferID"].ToString();           

                if (!IsPostBack)
                {

                    string source = Page.RouteData.Values["Source"].ToString();
                    string url = (Request.UrlReferrer == null) ? "" : Request.UrlReferrer.ToString();
                    string CurrentURl = Request.Url.ToString();
                    if (url.IndexOf("goo.gl") == 7)
                    {
                    }
                    else
                    {

                        if (url == "" && source == "F")
                        {
                        }
                        else
                        {
                            if (url == "" && source == "F" && !CurrentURl.Contains("socialreferral.onlineshoppingpool.com"))
                            {
                            }
                            else
                            {
                                Response.Redirect(ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/F/"+OfferID);
                            }
                        }
                    }
                }
                // Response.Write(resultReferrerURL);
                
            }
        }

        }
    }
