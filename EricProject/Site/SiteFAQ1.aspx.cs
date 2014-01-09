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
public partial class Site_SiteFAQ : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //BindData();
        //Admin obj = new Admin();
        //_Admin obj1 = new _Admin();
        //obj1.ID = 0;
        //SqlDataReader dr = obj.BindFAQCategoryGrid(obj1);
        
    }

    protected void BindData()
    {
        StringBuilder strbuilder = new StringBuilder();
        //Label lblCategory = new Label();

        _Site obj = new _Site();
        obj.ID = 0;
        DAL.Site sqlobj = new DAL.Site();
        SqlDataReader DR = sqlobj.BindSiteFAQCategoryName(obj);

        //EricProject.WebServices.Site objSite = new WebServices.Site();
        //objSite.BindSiteFAQLoad();

        while (DR.Read())
        {
            strbuilder.Append("<h3>" + DR["Category_Name"].ToString() + "</h3>");
            string str = DR["Category_Name"].ToString();
            obj.Category_Name = str;
            SqlDataReader DR1 = sqlobj.BindSiteFAQ_QuesAns(obj);

            while (DR1.Read())
            {
                string ques = DR1["Question"].ToString();
                string ans = DR1["Answer"].ToString();

                strbuilder.Append("<a href='#' class='expandable'>" + DR1["Question"].ToString() + " ?" + "</a>");
                strbuilder.Append("<div class='textImg'><span class='categoryitems'>" + DR1["Answer"].ToString() + "</span></div>");
            }
            strbuilder.Append("<br/>");
            
            
        }
    }
}