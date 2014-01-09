using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using System.Collections;


public partial class Site_MerchantTotalPosts : System.Web.UI.Page
{
    public string TotalCustomer = "";
    _CampaignsDetails objCampaignsDetails = new _CampaignsDetails();
    DAL.Plugin sqlPlugin = new DAL.Plugin();
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            if (Session["MerchantID"] != null)
            {
                //Bind Total Customer By MerchantId
                try
                {
                    
                    if (Session["PagingTotalPost"] == null)
                    {
                        Bindsearch(1, 5);
                    }
                    else
                    {
                        Bindsearch(Convert.ToInt32(Session["PagingTotalPost"].ToString()), 5);
                    }
                    _CampaignsDetails objCampaignsDetails = new _CampaignsDetails();
                    objCampaignsDetails.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
                    SqlDataReader drPluginTotalCustomerByMerchantId =
                         sqlPlugin.BindTotalCustomerPostByMerchantId(objCampaignsDetails);
                    if (drPluginTotalCustomerByMerchantId.Read())
                    {
                        TotalCustomer = drPluginTotalCustomerByMerchantId["TotalRecord"].ToString();
                    }
                    DBAccess.InstanceCreation().disconnect();
                }
                catch
                {
                }
                //Bind Total Customer By MerchantId
            }
        }
    }

    protected void Bindsearch(int pagenumber, int pagesize)
    {
        _CampaignsDetails objCampaignsDetails1 = new _CampaignsDetails();
        objCampaignsDetails1.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
        objCampaignsDetails1.PageNumber = pagenumber;
        objCampaignsDetails1.PageSize = pagesize;
        SqlDataReader drPost = sqlPlugin.spSearchingCustomerPostPaging(objCampaignsDetails1);
        DataTable dtPost = new DataTable();
        dtPost.Load(drPost);
        RepeaterPost.DataSource = dtPost;
        RepeaterPost.DataBind();
    }

    protected void RepeaterPost_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal litPost = (Literal)e.Item.FindControl("litPost");
            if (((DataRowView)e.Item.DataItem).Row.ItemArray[7].ToString() == "1")
            {
                litPost.Text += "<div class=\"postLine\">";
                litPost.Text += "<div class=\"postimage\">";
                litPost.Text += "<a href=\"https://www.facebook.com/" + ((DataRowView)e.Item.DataItem).Row.ItemArray[2].ToString() + "\" target=\"_blank\">";
                litPost.Text += "<img src=\"https://graph.facebook.com/" + ((DataRowView)e.Item.DataItem).Row.ItemArray[2].ToString() + "/picture\" alt=\"" + ((DataRowView)e.Item.DataItem).Row.ItemArray[3].ToString() + "\" title=\"" + ((DataRowView)e.Item.DataItem).Row.ItemArray[3].ToString() + "\"/>";
                litPost.Text += "</a>";
                litPost.Text += "</div>";
                litPost.Text += "<div class=\"postTxt\">";
                litPost.Text += "<div class=\"hd\">";
                litPost.Text += "<a href=\"https://www.facebook.com/" + ((DataRowView)e.Item.DataItem).Row.ItemArray[2].ToString() + "\" target=\"_blank\">" + ((DataRowView)e.Item.DataItem).Row.ItemArray[3].ToString() + "</a>";
                litPost.Text += "</div>";
                litPost.Text += "<div class=\"grnlink\">Reach: " + ((DataRowView)e.Item.DataItem).Row.ItemArray[5].ToString() + " Friends</div>";
                litPost.Text += "<p>" + ((DataRowView)e.Item.DataItem).Row.ItemArray[1].ToString() + "</p>";
                litPost.Text += "<div class=\"time\">" + ((DataRowView)e.Item.DataItem).Row.ItemArray[4].ToString() + "</div>";
                litPost.Text += "</div>";
                litPost.Text += "<div class=\"clr\"></div>";
                litPost.Text += "</div>";
            }
            if (((DataRowView)e.Item.DataItem).Row.ItemArray[7].ToString() == "2")
            {
                litPost.Text += "<div class=\"postLine\">";
                litPost.Text += "<div class=\"postimage\">";
                litPost.Text += "<a href=\"https://twitter.com/" + ((DataRowView)e.Item.DataItem).Row.ItemArray[6].ToString() + "\" target=\"_blank\">";
                litPost.Text += "<img src=\"https://abs.twimg.com/sticky/default_profile_images/default_profile_6_normal.png\" alt=\"" + ((DataRowView)e.Item.DataItem).Row.ItemArray[3].ToString() + "\" title=\"" + ((DataRowView)e.Item.DataItem).Row.ItemArray[3].ToString() + "\"/>";
                litPost.Text += "</a>";
                litPost.Text += "</div>";
                litPost.Text += "<div class=\"postTxt\">";
                litPost.Text += "<div class=\"hd\">";
                litPost.Text += "<a href=\"https://twitter.com/" + ((DataRowView)e.Item.DataItem).Row.ItemArray[6].ToString() + "\" target=\"_blank\">" + ((DataRowView)e.Item.DataItem).Row.ItemArray[3].ToString() + "</a>";
                litPost.Text += "</div>";
                litPost.Text += "<div class=\"grnlink\">Reach: " + ((DataRowView)e.Item.DataItem).Row.ItemArray[8].ToString() + " Followers</div>";
                litPost.Text += "<p>" + ((DataRowView)e.Item.DataItem).Row.ItemArray[1].ToString() + "</p>";
                litPost.Text += "<div class=\"time\">" + ((DataRowView)e.Item.DataItem).Row.ItemArray[4].ToString() + "</div>";
                litPost.Text += "</div>";
                litPost.Text += "<div class=\"clr\"></div>";
                litPost.Text += "</div>";
            }
        }
    }
}
