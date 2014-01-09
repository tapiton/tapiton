using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;


namespace EricProject.Site
{
    public partial class MerchantCustomizeColor : System.Web.UI.Page
    {
        //int Id;
        string imagepath = ConfigurationManager.AppSettings["pageURL"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CampaignId"] == null)
            {
                if (Request.QueryString["val"] != null)
                    Session["CampaignId"] = Request.QueryString["val"].ToString();
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Oops! Something went wrong. Please select campaign again.');", true);
                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/CampaignManagement");
                }
            }
            if (Session["EditCampaignId"] == null && Session["PreviousID"] == null)
            {
                // functon for values by default
                DefaultValues();
            }
            if (Session["CampaignId"] != null)
            {
                hiddenCampaignId.Value = Session["CampaignId"].ToString();

                _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
                objMerchantCampaign.Campaign_Id = Convert.ToInt32(Session["CampaignId"]);
                objMerchantCampaign.Status = 5;
                DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
                SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
                if (drMerchantCampaign.Read())
                {
                    if (drMerchantCampaign["BackGroundColor"].ToString() != "")
                        colorpickerField3.Value = drMerchantCampaign["BackGroundColor"].ToString();
                    else
                        colorpickerField3.Value = "D9DBE0";

                    if (drMerchantCampaign["ForeColor"].ToString() != "")
                        colorpickerField2.Value = drMerchantCampaign["ForeColor"].ToString();
                    else
                        colorpickerField2.Value = "FAFAFA";

                    if (drMerchantCampaign["BorderColor"].ToString() != "")
                        colorpickerField1.Value = drMerchantCampaign["BorderColor"].ToString();
                    else
                        colorpickerField1.Value = "2C7499";
                    if (drMerchantCampaign["Campaign_Image"].ToString() != "")
                        imgCoupon.ImageUrl = ConfigurationManager.AppSettings["pageURL"].ToString() + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();
                    else
                    {
                        imgCoupon.Width = 0;
                        imgCoupon.Height = 0;
                        tdimage.Width = "0%";
                        tdcoupon.Width = "100%";
                    }
                    ddlDisplayType.Value = drMerchantCampaign["Display_Type"].ToString();
                }
            }

            if (Request.QueryString["val"] != null)
            {
                _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
                objMerchantCampaign.Campaign_Id = Convert.ToInt32(Request.QueryString["val"]);
                objMerchantCampaign.Status = 5;
                DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
                SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
                if (drMerchantCampaign.Read())
                {
                    if (drMerchantCampaign["Campaign_Image"].ToString() != "")
                        imgCoupon.ImageUrl = ConfigurationManager.AppSettings["pageURL"].ToString() + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();
                    else
                    {
                        imgCoupon.Width = 0;
                        imgCoupon.Height = 0;
                        tdimage.Width = "0%";
                        tdcoupon.Width = "100%";
                    }
                }


                int count = 0;
                string CheckCampaignId = Request.QueryString["val"].ToString();
                _Merchant_Website_Campaign obj = new _Merchant_Website_Campaign();
                obj.Campaign_Id = Convert.ToInt32(Session["MerchantID"].ToString());
                DAL.Plugin sql = new DAL.Plugin();
                SqlDataReader dr = sql.SelectCampaignId(obj);
                while (dr.Read())
                {
                    if (dr["Campaign_ID"].ToString() == CheckCampaignId)
                    {
                        count++;
                    }

                }

                if (count > 0)
                {
                    hiddenCampaignId.Value = CheckCampaignId;
                    Session["CampaignId"] = CheckCampaignId;
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Invalid Request')</script>");
                    Response.End();
                }
            }
            setCouponValues();
            hiddenmerchant.Value = Session["MerchantID"].ToString();
        }
        public void DefaultValues()
        {
            DAL.Plugin sqlDefaultValues = new DAL.Plugin();
            _Merchant obj = new _Merchant();
            obj.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
            obj.status = 2;
            SqlDataReader drValues = sqlDefaultValues.DefaultVales(obj);
            while (drValues.Read())
            {
                if (drValues["Key_Details"].ToString() == "BorderColor")
                {
                    colorpickerField1.Value = drValues["Value"].ToString();

                }
                if (drValues["Key_Details"].ToString() == "ForeColor")
                {
                    colorpickerField2.Value = drValues["Value"].ToString();

                }
                if (drValues["Key_Details"].ToString() == "BackGroundColor")
                {
                    colorpickerField3.Value = drValues["Value"].ToString();
                }
            }
        }
        //protected void lnkprevious_Click(object sender, EventArgs e)
        //{
        //    if (Session["CampaignId"] != null)
        //    {
        //        Session["PreviousID"] = Session["CampaignId"].ToString();
        //        _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
        //        objMerchantCampaign.Campaign_Id = Convert.ToInt32(Session["CampaignId"]);
        //        objMerchantCampaign.Status = 5;
        //        DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
        //        SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
        //        //Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
        //        //Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
        //        if (drMerchantCampaign.Read())
        //        {
        //            Session["CampaignName"] = drMerchantCampaign["Campaign_Name"].ToString();
        //            Session["CustomerRebate"] = drMerchantCampaign["Customer_reward"].ToString();
        //            Session["CampaignId"] = Session["CampaignId"].ToString();
        //            Session["ReferrerReward"] = drMerchantCampaign["Referrer_reward"].ToString();
        //            Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
        //            Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
        //            Session["Expiration"] = drMerchantCampaign["Expiration"].ToString();
        //            Session["Insert"] = 0;
        //            string str = drMerchantCampaign["Campaign_Image"].ToString();
        //            if (str != "" && str != "~/images/userface.jpg" && File.Exists(Server.MapPath("~/images/MerchantImage/") + drMerchantCampaign["Campaign_Image"]))
        //            {
        //                Session["ImgName"] =  drMerchantCampaign["Campaign_Image"].ToString();
        //                Session["imagename"] = imagepath + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();

        //            }
        //            if (drMerchantCampaign["Customer_reward_type"].ToString() == "1")
        //            {
        //                Session["dollar"] = "$";
        //            }
        //            else
        //            {
        //                Session["dollar"] = "%";
        //            }

        //            Session["dollar2"] = drMerchantCampaign["Customer_reward_type"].ToString();
        //            Session["ReferrerRewardType"] = drMerchantCampaign["Referrer_reward_type"].ToString();

        //        }
        //    }
        //    else
        //    {
        //        Session["PreviousID"] = Request.QueryString["val"].ToString();
        //        _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
        //        objMerchantCampaign.Campaign_Id = Convert.ToInt32(Request.QueryString["val"].ToString());
        //        objMerchantCampaign.Status = 5;
        //        DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
        //        SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
        //        //Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
        //        //Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
        //        if (drMerchantCampaign.Read())
        //        {
        //            Session["CampaignName"] = drMerchantCampaign["Campaign_Name"].ToString();
        //            Session["CustomerRebate"] = drMerchantCampaign["Customer_reward"].ToString();
        //            Session["CampaignId"] = Request.QueryString["val"].ToString();
        //            Session["ReferrerReward"] = drMerchantCampaign["Referrer_reward"].ToString();
        //            Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
        //            Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
        //            Session["Expiration"] = drMerchantCampaign["Expiration"].ToString();
        //            Session["Insert"] = 0;
        //            string str = drMerchantCampaign["Campaign_Image"].ToString();
        //            if (str != "" && str != "~/images/userface.jpg" && File.Exists(Server.MapPath("~/images/MerchantImage/") + drMerchantCampaign["Campaign_Image"]))
        //            {
        //                Session["ImgName"] = drMerchantCampaign["Campaign_Image"].ToString();
        //                Session["imagename"] = imagepath + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();

        //            }
        //            if (drMerchantCampaign["Customer_reward_type"].ToString() == "1")
        //            {
        //                Session["dollar"] = "$";
        //            }
        //            else
        //            {
        //                Session["dollar"] = "%";
        //            }

        //            Session["dollar2"] = drMerchantCampaign["Customer_reward_type"].ToString();
        //            Session["ReferrerRewardType"] = drMerchantCampaign["Referrer_reward_type"].ToString();

        //        }
        //    }
        //    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Campaign/New");
        //}

        //protected void lnkPreviousmessage_Click(object sender, EventArgs e)
        //{
        //    if (Session["CampaignId"] != null)
        //    {
        //        Session["PreviousID"] = Session["CampaignId"].ToString();
        //        _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
        //        objMerchantCampaign.Campaign_Id = Convert.ToInt32(Session["CampaignId"]);
        //        objMerchantCampaign.Status = 5;
        //        DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
        //        SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
        //        //Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
        //        //Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
        //        if (drMerchantCampaign.Read())
        //        {
        //            Session["CampaignName"] = drMerchantCampaign["Campaign_Name"].ToString();
        //            Session["CustomerRebate"] = drMerchantCampaign["Customer_reward"].ToString();
        //            Session["CampaignId"] = Session["CampaignId"].ToString();
        //            Session["ReferrerReward"] = drMerchantCampaign["Referrer_reward"].ToString();
        //            Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
        //            Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
        //            Session["Expiration"] = drMerchantCampaign["Expiration"].ToString();
        //            Session["Insert"] = 0;
        //            string str = drMerchantCampaign["Campaign_Image"].ToString();
        //            if (str != "" && str != "~/images/userface.jpg" && File.Exists(Server.MapPath("~/images/MerchantImage/") + drMerchantCampaign["Campaign_Image"]))
        //            {
        //                Session["ImgName"] = drMerchantCampaign["Campaign_Image"].ToString();
        //                Session["imagename"] = imagepath + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();

        //            }
        //            if (drMerchantCampaign["Customer_reward_type"].ToString() == "1")
        //            {
        //                Session["dollar"] = "$";
        //            }
        //            else
        //            {
        //                Session["dollar"] = "%";
        //            }

        //            Session["dollar2"] = drMerchantCampaign["Customer_reward_type"].ToString();
        //            Session["ReferrerRewardType"] = drMerchantCampaign["Referrer_reward_type"].ToString();

        //        }
        //    }
        //    else
        //    {
        //        Session["PreviousID"] = Request.QueryString["val"].ToString();
        //        _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
        //        objMerchantCampaign.Campaign_Id = Convert.ToInt32(Request.QueryString["val"].ToString());
        //        objMerchantCampaign.Status = 5;
        //        DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
        //        SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
        //        //Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
        //        //Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
        //        if (drMerchantCampaign.Read())
        //        {
        //            Session["CampaignName"] = drMerchantCampaign["Campaign_Name"].ToString();
        //            Session["CustomerRebate"] = drMerchantCampaign["Customer_reward"].ToString();
        //            Session["CampaignId"] = Request.QueryString["val"].ToString();
        //            Session["ReferrerReward"] = drMerchantCampaign["Referrer_reward"].ToString();
        //            Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
        //            Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
        //            Session["Expiration"] = drMerchantCampaign["Expiration"].ToString();
        //            Session["Insert"] = 0;
        //            string str = drMerchantCampaign["Campaign_Image"].ToString();
        //            if (str != "" && str != "~/images/userface.jpg" && File.Exists(Server.MapPath("~/images/MerchantImage/") + drMerchantCampaign["Campaign_Image"]))
        //            {
        //                Session["ImgName"] = drMerchantCampaign["Campaign_Image"].ToString();
        //                Session["imagename"] = imagepath + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();

        //            }
        //            if (drMerchantCampaign["Customer_reward_type"].ToString() == "1")
        //            {
        //                Session["dollar"] = "$";
        //            }
        //            else
        //            {
        //                Session["dollar"] = "%";
        //            }

        //            Session["dollar2"] = drMerchantCampaign["Customer_reward_type"].ToString();
        //            Session["ReferrerRewardType"] = drMerchantCampaign["Referrer_reward_type"].ToString();

        //        }
        //    }
        //    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Campaign/Message");
        //}


        private void setCouponValues()
        {
            _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
            objMerchantCampaign.Campaign_Id = Convert.ToInt32(Session["CampaignId"]);
            objMerchantCampaign.Status = 5;
            DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
            SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
            //Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
            //Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
            if (drMerchantCampaign.Read())
            {

                decimal Type_of_Reward_C = Convert.ToDecimal(drMerchantCampaign["Customer_reward"].ToString());
                decimal Type_of_Reward_R = Convert.ToDecimal(drMerchantCampaign["Referrer_reward"].ToString());
                decimal savingReferrerprice;
                string CustomerReward;
                string Referrer;

                if (drMerchantCampaign["Customer_reward_type"].ToString() == "1")
                {
                    CustomerReward = "$" + Type_of_Reward_C;
                }
                else
                {
                    CustomerReward = Type_of_Reward_C + "%";
                }
                if (drMerchantCampaign["Referrer_reward_type"].ToString() == "1")
                {
                    Referrer = "$" + Type_of_Reward_R;
                    literalReferrerReward.Text = "$" + Type_of_Reward_R;
                    SavingRefer1Friend.Text = Type_of_Reward_R.ToString();
                    SavingRefer3Friend.Text = (Type_of_Reward_R * 3).ToString();
                    SavingRefer5Friend.Text = (Type_of_Reward_R * 5).ToString();
                    if (((Convert.ToDecimal(250) - Convert.ToDecimal(Type_of_Reward_R)).ToString()).Contains("-"))
                        finalRefer1friend.Text = "-$" + (Convert.ToDecimal(250) - Convert.ToDecimal(Type_of_Reward_R)).ToString().Replace('-', ' ').TrimStart();
                    else
                        finalRefer1friend.Text = "$" + (Convert.ToDecimal(250) - Convert.ToDecimal(Type_of_Reward_R)).ToString().Replace('-', ' ').TrimStart();
                    if (((Convert.ToDecimal(250) - Convert.ToDecimal(Type_of_Reward_R * 3)).ToString()).Contains("-"))
                        finalRefer3friend.Text = "-$" + (Convert.ToDecimal(250) - Convert.ToDecimal(Type_of_Reward_R * 3)).ToString().Replace('-', ' ').TrimStart();
                    else
                        finalRefer3friend.Text = "$" + (Convert.ToDecimal(250) - Convert.ToDecimal(Type_of_Reward_R * 3)).ToString().Replace('-', ' ').TrimStart();
                    if (((Convert.ToDecimal(250) - Convert.ToDecimal(Type_of_Reward_R * 5)).ToString()).Contains("-"))
                        finalRefer5friend.Text = "-$" + (Convert.ToDecimal(250) - Convert.ToDecimal(Type_of_Reward_R * 5)).ToString().Replace('-', ' ').TrimStart();
                    else
                        finalRefer5friend.Text = "$" + (Convert.ToDecimal(250) - Convert.ToDecimal(Type_of_Reward_R * 5)).ToString().Replace('-', ' ').TrimStart();
                }
                else
                {
                    literalReferrerReward.Text = Type_of_Reward_R + "%";
                    Referrer = Type_of_Reward_R + "%";
                    savingReferrerprice = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal((Type_of_Reward_R / 100) * Convert.ToDecimal(250))));
                    SavingRefer1Friend.Text = savingReferrerprice.ToString();
                    SavingRefer3Friend.Text = (savingReferrerprice * 3).ToString();
                    SavingRefer5Friend.Text = (savingReferrerprice * 5).ToString();
                    if (((Convert.ToDecimal(250) - Convert.ToDecimal(savingReferrerprice)).ToString()).Contains("-"))
                        finalRefer1friend.Text = "-$" + string.Format("{0:0.00}", (Convert.ToDecimal(250) - Convert.ToDecimal(savingReferrerprice)).ToString()).Replace('-', ' ').TrimStart();
                    else
                        finalRefer1friend.Text = "$" + string.Format("{0:0.00}", (Convert.ToDecimal(250) - Convert.ToDecimal(savingReferrerprice)).ToString()).Replace('-', ' ').TrimStart();
                    if (((Convert.ToDecimal(250) - Convert.ToDecimal(savingReferrerprice * 3)).ToString()).Contains("-"))
                        finalRefer3friend.Text = "-$" + string.Format("{0:0.00}", (Convert.ToDecimal(250) - Convert.ToDecimal(savingReferrerprice * 3)).ToString()).Replace('-', ' ').TrimStart();
                    else
                        finalRefer3friend.Text = "$" + string.Format("{0:0.00}", (Convert.ToDecimal(250) - Convert.ToDecimal(savingReferrerprice * 3)).ToString()).Replace('-', ' ').TrimStart();
                    if (((Convert.ToDecimal(250) - Convert.ToDecimal(savingReferrerprice * 5)).ToString()).Contains("-"))
                        finalRefer5friend.Text = "-$" + string.Format("{0:0.00}", (Convert.ToDecimal(250) - Convert.ToDecimal(savingReferrerprice * 5)).ToString()).Replace('-', ' ').TrimStart();
                    else
                        finalRefer5friend.Text = "$" + string.Format("{0:0.00}", (Convert.ToDecimal(250) - Convert.ToDecimal(savingReferrerprice * 5)).ToString()).Replace('-', ' ').TrimStart();

                }
                if (Convert.ToDecimal(Type_of_Reward_R).ToString() == "0.00")
                    literalCustomerRebate.Text = "Invite your friends and they’ll get " + CustomerReward.ToString() + " off.";
                else if (Convert.ToDecimal(Type_of_Reward_C).ToString() == "0.00")
                    literalCustomerRebate.Text = "Invite your friends and get " + Referrer.ToString() + " back when they make a purchase.";
                else
                    literalCustomerRebate.Text = "Invite your friends and they’ll get  " + CustomerReward.ToString() + "  off.If they buy anything, you’ll Get " + Referrer.ToString() + " back";


            }
        }
        protected void btnprevious_Click(object sender, EventArgs e)
        {
            if (Session["CampaignId"] != null)
            {
                Session["PreviousID"] = Session["CampaignId"].ToString();
                _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
                objMerchantCampaign.Campaign_Id = Convert.ToInt32(Session["CampaignId"]);
                objMerchantCampaign.Status = 5;
                DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
                SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
                //Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
                //Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                if (drMerchantCampaign.Read())
                {
                    Session["CampaignName"] = drMerchantCampaign["Campaign_Name"].ToString();
                    Session["Campaign_title"] = drMerchantCampaign["Item_Name"].ToString();
                    Session["ProductURl"] = drMerchantCampaign["ProductURl"].ToString();
                    Session["CustomerRebate"] = drMerchantCampaign["Customer_reward"].ToString();
                    Session["CampaignId"] = Session["CampaignId"].ToString();
                    Session["ReferrerReward"] = drMerchantCampaign["Referrer_reward"].ToString();
                    Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
                    Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                    Session["Expiration"] = drMerchantCampaign["Expiration"].ToString();
                    Session["Insert"] = 0;
                    string str = drMerchantCampaign["Campaign_Image"].ToString();
                    if (str != "" && str != "~/images/userface.jpg" && File.Exists(Server.MapPath("~/images/MerchantImage/") + drMerchantCampaign["Campaign_Image"]))
                    {
                        Session["ImgName"] = drMerchantCampaign["Campaign_Image"].ToString();
                        Session["imagename"] = imagepath + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();

                    }
                    if (drMerchantCampaign["Customer_reward_type"].ToString() == "1")
                    {
                        Session["dollar"] = "$";
                    }
                    else
                    {
                        Session["dollar"] = "%";
                    }

                    Session["dollar2"] = drMerchantCampaign["Customer_reward_type"].ToString();
                    Session["ReferrerRewardType"] = drMerchantCampaign["Referrer_reward_type"].ToString();

                }
            }
            else
            {
                Session["PreviousID"] = Request.QueryString["val"].ToString();
                _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
                objMerchantCampaign.Campaign_Id = Convert.ToInt32(Request.QueryString["val"].ToString());
                objMerchantCampaign.Status = 5;
                DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
                SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
                //Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
                //Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                if (drMerchantCampaign.Read())
                {
                    Session["CampaignName"] = drMerchantCampaign["Campaign_Name"].ToString();
                    Session["Campaign_title"] = drMerchantCampaign["Item_name"].ToString();
                    Session["ProductURl"] = drMerchantCampaign["ProductURl"].ToString();
                    Session["CustomerRebate"] = drMerchantCampaign["Customer_reward"].ToString();
                    Session["CampaignId"] = Request.QueryString["val"].ToString();
                    Session["ReferrerReward"] = drMerchantCampaign["Referrer_reward"].ToString();
                    Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
                    Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                    Session["Expiration"] = drMerchantCampaign["Expiration"].ToString();
                    Session["Insert"] = 0;
                    string str = drMerchantCampaign["Campaign_Image"].ToString();
                    if (str != "" && str != "~/images/userface.jpg" && File.Exists(Server.MapPath("~/images/MerchantImage/") + drMerchantCampaign["Campaign_Image"]))
                    {
                        Session["ImgName"] = drMerchantCampaign["Campaign_Image"].ToString();
                        Session["imagename"] = imagepath + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();

                    }
                    if (drMerchantCampaign["Customer_reward_type"].ToString() == "1")
                    {
                        Session["dollar"] = "$";
                    }
                    else
                    {
                        Session["dollar"] = "%";
                    }

                    Session["dollar2"] = drMerchantCampaign["Customer_reward_type"].ToString();
                    Session["ReferrerRewardType"] = drMerchantCampaign["Referrer_reward_type"].ToString();

                }
            }
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Campaign/Message");
        }

        protected void lnkCampaignMessage_Click(object sender, EventArgs e)
        {
            if (Session["CampaignId"] != null)
            {
                Session["PreviousID"] = Session["CampaignId"].ToString();
                _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
                objMerchantCampaign.Campaign_Id = Convert.ToInt32(Session["CampaignId"]);
                objMerchantCampaign.Status = 5;
                DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
                SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
                //Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
                //Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                if (drMerchantCampaign.Read())
                {
                    Session["CampaignName"] = drMerchantCampaign["Campaign_Name"].ToString();
                    Session["Campaign_title"] = drMerchantCampaign["Item_name"].ToString();
                    Session["ProductURl"] = drMerchantCampaign["ProductURl"].ToString();
                    Session["CustomerRebate"] = drMerchantCampaign["Customer_reward"].ToString();
                    Session["CampaignId"] = Session["CampaignId"].ToString();
                    Session["ReferrerReward"] = drMerchantCampaign["Referrer_reward"].ToString();
                    Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
                    Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                    Session["Expiration"] = drMerchantCampaign["Expiration"].ToString();
                    Session["Insert"] = 0;
                    string str = drMerchantCampaign["Campaign_Image"].ToString();
                    if (str != "" && str != "~/images/userface.jpg" && File.Exists(Server.MapPath("~/images/MerchantImage/") + drMerchantCampaign["Campaign_Image"]))
                    {
                        Session["ImgName"] = drMerchantCampaign["Campaign_Image"].ToString();
                        Session["imagename"] = imagepath + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();

                    }
                    if (drMerchantCampaign["Customer_reward_type"].ToString() == "1")
                    {
                        Session["dollar"] = "$";
                    }
                    else
                    {
                        Session["dollar"] = "%";
                    }

                    Session["dollar2"] = drMerchantCampaign["Customer_reward_type"].ToString();
                    Session["ReferrerRewardType"] = drMerchantCampaign["Referrer_reward_type"].ToString();

                }
            }
            else
            {
                Session["PreviousID"] = Request.QueryString["val"].ToString();
                _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
                objMerchantCampaign.Campaign_Id = Convert.ToInt32(Request.QueryString["val"].ToString());
                objMerchantCampaign.Status = 5;
                DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
                SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
                //Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
                //Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                if (drMerchantCampaign.Read())
                {
                    Session["CampaignName"] = drMerchantCampaign["Campaign_Name"].ToString();
                    Session["Campaign_title"] = drMerchantCampaign["Item_name"].ToString();
                    Session["ProductURl"] = drMerchantCampaign["ProductURl"].ToString();
                    Session["CustomerRebate"] = drMerchantCampaign["Customer_reward"].ToString();
                    Session["CampaignId"] = Request.QueryString["val"].ToString();
                    Session["ReferrerReward"] = drMerchantCampaign["Referrer_reward"].ToString();
                    Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
                    Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                    Session["Expiration"] = drMerchantCampaign["Expiration"].ToString();
                    Session["Insert"] = 0;
                    string str = drMerchantCampaign["Campaign_Image"].ToString();
                    if (str != "" && str != "~/images/userface.jpg" && File.Exists(Server.MapPath("~/images/MerchantImage/") + drMerchantCampaign["Campaign_Image"]))
                    {
                        Session["ImgName"] = drMerchantCampaign["Campaign_Image"].ToString();
                        Session["imagename"] = imagepath + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();

                    }
                    if (drMerchantCampaign["Customer_reward_type"].ToString() == "1")
                    {
                        Session["dollar"] = "$";
                    }
                    else
                    {
                        Session["dollar"] = "%";
                    }

                    Session["dollar2"] = drMerchantCampaign["Customer_reward_type"].ToString();
                    Session["ReferrerRewardType"] = drMerchantCampaign["Referrer_reward_type"].ToString();

                }
            }
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Campaign/Message");
        }

        protected void lnkCampaignDetails_Click(object sender, EventArgs e)
        {
            if (Session["CampaignId"] != null)
            {
                Session["PreviousID"] = Session["CampaignId"].ToString();
                _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
                objMerchantCampaign.Campaign_Id = Convert.ToInt32(Session["CampaignId"]);
                objMerchantCampaign.Status = 5;
                DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
                SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
                //Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
                //Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                if (drMerchantCampaign.Read())
                {
                    Session["CampaignName"] = drMerchantCampaign["Campaign_Name"].ToString();
                    Session["Campaign_title"] = drMerchantCampaign["item_name"].ToString();
                    Session["ProductURl"] = drMerchantCampaign["ProductURl"].ToString();
                    Session["CustomerRebate"] = drMerchantCampaign["Customer_reward"].ToString();
                    Session["CampaignId"] = Session["CampaignId"].ToString();
                    Session["ReferrerReward"] = drMerchantCampaign["Referrer_reward"].ToString();
                    Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
                    Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                    Session["Expiration"] = drMerchantCampaign["Expiration"].ToString();
                    Session["Insert"] = 0;
                    string str = drMerchantCampaign["Campaign_Image"].ToString();
                    if (str != "" && str != "~/images/userface.jpg" && File.Exists(Server.MapPath("~/images/MerchantImage/") + drMerchantCampaign["Campaign_Image"]))
                    {
                        Session["ImgName"] = drMerchantCampaign["Campaign_Image"].ToString();
                        Session["imagename"] = imagepath + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();

                    }
                    if (drMerchantCampaign["Customer_reward_type"].ToString() == "1")
                    {
                        Session["dollar"] = "$";
                    }
                    else
                    {
                        Session["dollar"] = "%";
                    }

                    Session["dollar2"] = drMerchantCampaign["Customer_reward_type"].ToString();
                    Session["ReferrerRewardType"] = drMerchantCampaign["Referrer_reward_type"].ToString();

                }
            }
            else
            {
                Session["PreviousID"] = Request.QueryString["val"].ToString();
                _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
                objMerchantCampaign.Campaign_Id = Convert.ToInt32(Request.QueryString["val"].ToString());
                objMerchantCampaign.Status = 5;
                DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
                SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
                //Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
                //Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                if (drMerchantCampaign.Read())
                {
                    Session["CampaignName"] = drMerchantCampaign["Campaign_Name"].ToString();
                    Session["Campaign_title"] = drMerchantCampaign["Item_name"].ToString();
                    Session["ProductURl"] = drMerchantCampaign["ProductURl"].ToString();
                    Session["CustomerRebate"] = drMerchantCampaign["Customer_reward"].ToString();
                    Session["CampaignId"] = Request.QueryString["val"].ToString();
                    Session["ReferrerReward"] = drMerchantCampaign["Referrer_reward"].ToString();
                    Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
                    Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                    Session["Expiration"] = drMerchantCampaign["Expiration"].ToString();
                    Session["Insert"] = 0;
                    string str = drMerchantCampaign["Campaign_Image"].ToString();
                    if (str != "" && str != "~/images/userface.jpg" && File.Exists(Server.MapPath("~/images/MerchantImage/") + drMerchantCampaign["Campaign_Image"]))
                    {
                        Session["ImgName"] = drMerchantCampaign["Campaign_Image"].ToString();
                        Session["imagename"] = imagepath + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();

                    }
                    if (drMerchantCampaign["Customer_reward_type"].ToString() == "1")
                    {
                        Session["dollar"] = "$";
                    }
                    else
                    {
                        Session["dollar"] = "%";
                    }

                    Session["dollar2"] = drMerchantCampaign["Customer_reward_type"].ToString();
                    Session["ReferrerRewardType"] = drMerchantCampaign["Referrer_reward_type"].ToString();

                }
            }
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Campaign/New");
        }

        //protected void btnCustomizeFinish_Click(object sender, EventArgs e)
        //{
        //_MerchantCampaigns obj = new _MerchantCampaigns();
        //obj.Campaign_Id = Id;
        //obj.BorderColor = colorpickerField1.Value;
        //obj.ForeColor = colorpickerField2.Value;
        //obj.BackGroundColor = colorpickerField3.Value;
        //obj.ExpirySavingRefer1Friend_date = Convert.ToDateTime("01/01/1990");
        //obj.Start_date = Convert.ToDateTime("01/01/1990");
        //obj.UpdatedOn = Convert.ToDateTime("01/01/1990");
        //obj.Status = 3;

        //DAL.Plugin sqlPlugin = new DAL.Plugin();
        //int result = sqlPlugin.InsertIntoMerchant_Campaigns(obj);
        //}
    }
}