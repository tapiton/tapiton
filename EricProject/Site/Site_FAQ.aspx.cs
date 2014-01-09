using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;
using BAL;
using System.Configuration;

namespace EricProject.Site
{
    public partial class Site_FAQ : System.Web.UI.Page
    {
        public bool validmerchant = false;
        public bool validcustomer = false;
        public bool NoRecord = false;
        public bool DesignMerchant = false;
        public bool DesignCustomer = false;
        public string CustomerLoginUrl="";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Frequently Asked Questions";
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                    CustomerLoginUrl = Request.UrlReferrer.ToString();
                string str = Request.Url.PathAndQuery.ToLower();
                if (str != "/site/faq")
                {
                    CustomerLoginUrl = CustomerLoginUrl.Replace("http://localhost:1592/EricProject/", "");
                }
                else
                {
                    CustomerLoginUrl = CustomerLoginUrl.Replace(ConfigurationManager.AppSettings["pageURL"] , "");
                }
                ViewState["CustomerLoginUrl"] = CustomerLoginUrl;
            }
            lnkClear.Visible = false;
            lnkClear2.Visible = false;
            lnkClear3.Visible = false;
            labelheadertext.Visible = false;
            hyper.InnerHtml = "";
            hyperdetail.InnerHtml = "";
            customerfaq.InnerHtml = "";
            customerquestion.InnerHtml = "";
            if (Session["MerchantEmailId"] != null)
            {
                MerchantLogin();
                DivMerchant.Visible = true;
                DivCustomer.Visible = false;
            }
            else if (Session["CustomerEmailId"] != null || ViewState["CustomerLoginUrl"].ToString() == "Site/Customer/Login")
            {
                CustomerLogin();
                DivMerchant.Visible = false;
                DivCustomer.Visible = true;
            }
            else
            {
                DivMerchant.Visible = true;
                DivCustomer.Visible = true;
                BindData();
            }

        }

        protected void BindData()
        {
            labelheadertext.Visible = false;
            StringBuilder strbuilder = new StringBuilder();
            //Label lblCategory = new Label();

            _Site obj = new _Site();
            obj.CategoryId = 0;
            DAL.Site sqlobj = new DAL.Site();
            SqlDataReader DR = sqlobj.BindSiteFAQCategoryName(obj);
            int count = DR.FieldCount;
            //For Merchant.......................................................
            while (DR.Read())
            {
                strbuilder.Append("<h3><span>" + DR["Category_Name"].ToString() + "</span></h3>");
                string str = DR["Category_Name"].ToString();
                //string CategoryType = DR["Category_Type"].ToString();
                obj.Category_Name = str;
                //obj.Category_Type = CategoryType;
                SqlDataReader DR1 = sqlobj.BindSiteFAQ_QuesAns(obj);
                while (DR1.Read())
                {
                    string ques = DR1["Question"].ToString();
                    string ans = DR1["Answer"].ToString();

                    strbuilder.Append("<a href='#' class='expandable'>" + DR1["Question"].ToString() + "</a>");
                    strbuilder.Append("<div class='textImg'><span class='categoryitems'>" + DR1["Answer"].ToString() + "</span></div>");
                }
                strbuilder.Append("<br/>");
                hyper.InnerHtml = strbuilder.ToString();
                hyperdetail.InnerHtml = strbuilder.ToString();

            }

            //For Customer........................................................
            StringBuilder strbuildercustomer = new StringBuilder();
            SqlDataReader DRcustomer = sqlobj.BindSiteFAQCategoryNameCustomer(obj);
            while (DRcustomer.Read())
            {
                strbuildercustomer.Append("<h3><span>" + DRcustomer["Category_Name"].ToString() + "</span></h3>");
                string str = DRcustomer["Category_Name"].ToString();
                //string CategoryType = DR["Category_Type"].ToString();
                obj.Category_Name = str;
                //obj.Category_Type = CategoryType;
                SqlDataReader DR1customer = sqlobj.BindSiteFAQ_QuesAnsCustomer(obj);
                while (DR1customer.Read())
                {
                    string ques = DR1customer["Question"].ToString();
                    string ans = DR1customer["Answer"].ToString();

                    strbuildercustomer.Append("<a href='#' class='expandable'>" + DR1customer["Question"].ToString() + "</a>");
                    strbuildercustomer.Append("<div class='textImg'><span class='categoryitems'>" + DR1customer["Answer"].ToString() + "</span></div>");
                }
                strbuildercustomer.Append("<br/>");
                customerfaq.InnerHtml = strbuildercustomer.ToString();
                customerquestion.InnerHtml = strbuildercustomer.ToString();

            }
            DBAccess.InstanceCreation().disconnect();
        }

        protected void btnFAQSearch_Click(object sender, EventArgs e)
        {
            lnkClear.Visible = true;
            labelheadertext.Visible = false;
            if (txtFAQSearch.Text != "")
            {
                DivMerchant.Visible = false;
                DivCustomer.Visible = false;
                hyper.InnerHtml = "";
                hyperdetail.InnerHtml = "";
                customerfaq.InnerHtml = "";
                customerquestion.InnerHtml = "";
                StringBuilder strbuilder = new StringBuilder();
                StringBuilder strbuildercustomer = new StringBuilder();

                _Site obj = new _Site();
                if (Session["MerchantEmailId"] != null)
                {
                    obj.CategoryId = 2;
                }
                else if (Session["CustomerEmailId"] != null || ViewState["CustomerLoginUrl"].ToString() == "Site/Customer/Login")
                {
                    obj.CategoryId = 1;
                }
                else
                {
                    obj.CategoryId = 0;
                }
                DAL.Site sqlobj = new DAL.Site();
                obj.Question = txtFAQSearch.Text;
                SqlDataReader DR1 = sqlobj.BindSiteFAQ_QuesAnsSearch(obj);
                string CategoryName = string.Empty;
                while (DR1.Read())
                {
                    NoRecord = true;
                    string CategoryType = DR1["Category_Type"].ToString();
                    if (CategoryType == "2")
                    {
                        if (validmerchant == false)
                        {
                            DivMerchant.Visible = true;
                            validmerchant = true;
                        }

                        if (DR1["Category_Name"].ToString() != CategoryName)
                        {
                            if (DesignMerchant == true)
                            {
                                strbuilder.Append("<br/>");
                            }
                            DesignMerchant = true;
                            strbuilder.Append("<h3><span>" + DR1["Category_Name"].ToString() + "</span></h3>");
                            CategoryName = DR1["Category_Name"].ToString();
                        }
                        string ques = DR1["Question"].ToString();
                        string ans = DR1["Answer"].ToString();

                        strbuilder.Append("<a href='#' class='expandable'>" + DR1["Question"].ToString() + "</a>");
                        strbuilder.Append("<div class='textImg'><span class='categoryitems'>" + DR1["Answer"].ToString() + "</span></div>");
                    }
                    else if (CategoryType == "1")
                    {
                        if (validcustomer == false)
                        {
                            DivCustomer.Visible = true;
                            validcustomer = true;
                        }

                        if (DR1["Category_Name"].ToString() != CategoryName)
                        {
                            if (DesignCustomer == true)
                            {
                                strbuildercustomer.Append("<br/>");
                            }
                            DesignCustomer = true;
                            strbuildercustomer.Append("<h3><span>" + DR1["Category_Name"].ToString() + "</span></h3>");
                            CategoryName = DR1["Category_Name"].ToString();
                        }

                        string ques = DR1["Question"].ToString();
                        string ans = DR1["Answer"].ToString();

                        strbuildercustomer.Append("<a href='#' class='expandable'>" + DR1["Question"].ToString() + "</a>");
                        strbuildercustomer.Append("<div class='textImg'><span class='categoryitems'>" + DR1["Answer"].ToString() + "</span></div>");
                    }
                }
                strbuilder.Append("<br/>");
                hyper.InnerHtml = strbuilder.ToString();
                hyperdetail.InnerHtml = strbuilder.ToString();

                strbuildercustomer.Append("<br/>");
                customerfaq.InnerHtml = strbuildercustomer.ToString();
                customerquestion.InnerHtml = strbuildercustomer.ToString();

                DBAccess.InstanceCreation().disconnect();

                if (NoRecord == false)
                {
                    DivMerchant.Visible = false;
                    DivCustomer.Visible = false;
                    hyper.InnerHtml = "";
                    hyperdetail.InnerHtml = "";
                    customerfaq.InnerHtml = "";
                    customerquestion.InnerHtml = "";
                    labelheadertext.Visible = true;
                    labelheadertext.InnerHtml = "No result found for \"" + txtFAQSearch.Text + "\".";
                }
                if (DivCustomer.Visible && DivMerchant.Visible)
                {
                    lnkClear.Visible = true;
                    lnkClear2.Visible = false;
                    lnkClear3.Visible = false;
                    labelheader.Visible = false;
                }
                else if (DivMerchant.Visible == true)
                {
                    lnkClear.Visible = true;
                    lnkClear2.Visible = false;
                    lnkClear3.Visible = false;
                    labelheader.Visible = false;
                }
                else if (DivCustomer.Visible == true)
                {
                    lnkClear.Visible = false;
                    lnkClear2.Visible = true;
                    lnkClear3.Visible = false;
                    labelheader.Visible = false;
                }
                else
                {
                    lnkClear.Visible = false;
                    lnkClear2.Visible = false;
                    lnkClear3.Visible = true;
                    labelheader.Visible = true;
                }
            }
        }

        public void MerchantLogin()
        {
            labelheadertext.Visible = false;
            StringBuilder strbuilder = new StringBuilder();
            //Label lblCategory = new Label();

            _Site obj = new _Site();
            obj.CategoryId = 0;
            DAL.Site sqlobj = new DAL.Site();
            SqlDataReader DR = sqlobj.BindSiteFAQCategoryName(obj);

            //For Merchant.......................................................
            while (DR.Read())
            {
                strbuilder.Append("<h3><span>" + DR["Category_Name"].ToString() + "</span></h3>");
                string str = DR["Category_Name"].ToString();
                //string CategoryType = DR["Category_Type"].ToString();
                obj.Category_Name = str;
                //obj.Category_Type = CategoryType;
                SqlDataReader DR1 = sqlobj.BindSiteFAQ_QuesAns(obj);
                while (DR1.Read())
                {
                    string ques = DR1["Question"].ToString();
                    string ans = DR1["Answer"].ToString();

                    strbuilder.Append("<a href='#' class='expandable'>" + DR1["Question"].ToString() + "</a>");
                    strbuilder.Append("<div class='textImg'><span class='categoryitems'>" + DR1["Answer"].ToString() + "</span></div>");
                }
                strbuilder.Append("<br/>");
                hyper.InnerHtml = strbuilder.ToString();
                hyperdetail.InnerHtml = strbuilder.ToString();
                DBAccess.InstanceCreation().disconnect();
            }
        }

        public void CustomerLogin()
        {
            labelheadertext.Visible = false;
            _Site obj = new _Site();
            obj.CategoryId = 0;
            DAL.Site sqlobj = new DAL.Site();

            //For Customer........................................................
            StringBuilder strbuildercustomer = new StringBuilder();
            SqlDataReader DRcustomer = sqlobj.BindSiteFAQCategoryNameCustomer(obj);
            while (DRcustomer.Read())
            {
                strbuildercustomer.Append("<h3><span>" + DRcustomer["Category_Name"].ToString() + "</span></h3>");
                string str = DRcustomer["Category_Name"].ToString();
                //string CategoryType = DR["Category_Type"].ToString();
                obj.Category_Name = str;
                //obj.Category_Type = CategoryType;
                SqlDataReader DR1customer = sqlobj.BindSiteFAQ_QuesAnsCustomer(obj);
                while (DR1customer.Read())
                {
                    string ques = DR1customer["Question"].ToString();
                    string ans = DR1customer["Answer"].ToString();

                    strbuildercustomer.Append("<a href='#' class='expandable'>" + DR1customer["Question"].ToString() + "</a>");
                    strbuildercustomer.Append("<div class='textImg'><span class='categoryitems'>" + DR1customer["Answer"].ToString() + "</span></div>");
                }
                strbuildercustomer.Append("<br/>");
                customerfaq.InnerHtml = strbuildercustomer.ToString();
                customerquestion.InnerHtml = strbuildercustomer.ToString();

            }
            DBAccess.InstanceCreation().disconnect();
        }

        protected void lnkClear_Click(object sender, EventArgs e)
        {
            txtFAQSearch.Text = "";
            Page_Load(sender, e);
        }
    }
}