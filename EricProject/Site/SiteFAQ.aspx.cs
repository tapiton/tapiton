using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class FAQ : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection("Data Source=100.100.7.63; Initial Catalog=EricReferral;  User Id=sa; Password=flexsinsa");
    SqlCommand cmd;
    DataSet ds;
    SqlDataAdapter adpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
    }

    protected void BindData()
    {
        StringBuilder strbuilder = new StringBuilder();
        con.Open();
        cmd = new SqlCommand();
        cmd.Connection = con;
        //cmd.CommandType = CommandType.Text;
        //cmd.CommandText = "Select Top 5 FAQ_ID,Question,Answer from FAQ order by Updated_On desc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SiteFAQ";
        ds = new DataSet();
        adpt = new SqlDataAdapter();
        adpt.SelectCommand = cmd;
        adpt.Fill(ds);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            strbuilder.Append("<a href='#' class='expandable'>" + ds.Tables[0].Rows[i]["Question"].ToString() + " ?" + "</a>");
            strbuilder.Append("<span class='categoryitems'>" + ds.Tables[0].Rows[i]["Answer"].ToString() + "</span>");
        }
        //hyperdiv.InnerHtml = strbuilder.ToString();
        //hyperdetaildiv.InnerHtml = strbuilder.ToString();
    }
}