using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

namespace EricProject.Temp
{
    public partial class Products : System.Web.UI.Page
    {
        public DataTable DT = new DataTable();
        DataColumn MyColumn;
        protected void Page_Load(object sender, EventArgs e)
        {
            DT = new DataTable();
            MyColumn = new DataColumn("ProductID");
            DT.Columns.Add(MyColumn);
            MyColumn = new DataColumn("ProductName");
            DT.Columns.Add(MyColumn);
            MyColumn = new DataColumn("Price");
            DT.Columns.Add(MyColumn);
            MyColumn = new DataColumn("img");
            DT.Columns.Add(MyColumn);
           
        }

        protected void btnwatch_Click(object sender, EventArgs e)
        {
            DataRow MyRow = DT.NewRow();
            DataTable dtnew = Session["mySession"] as DataTable;
            if (dtnew != null)
            {
                for (int i = 0; i < dtnew.Rows.Count; i++)
                {
                    MyRow[0] = dtnew.Rows[0]["ProductID"].ToString();
                    MyRow[1] = dtnew.Rows[0]["ProductName"].ToString();
                    MyRow[2] = dtnew.Rows[0]["Price"].ToString();
                    MyRow[3] = dtnew.Rows[0]["img"].ToString();
                    DT.Rows.Add(MyRow);
                    DT.AcceptChanges();
                }
            }
            DataRow MyRow1 = DT.NewRow();
            MyRow1[0] = "2";
            MyRow1[1] = "Watch";
            MyRow1[2] = "26.00";
            MyRow1[3] = "purchase_img1.jpg";
            DT.Rows.Add(MyRow1);
            DT.AcceptChanges();
            Session["mySession"] = DT;
            
        }

        protected void btnmagnet_Click(object sender, EventArgs e)
        {
            DataRow MyRow = DT.NewRow();
            DataTable dtnew = Session["mySession"] as DataTable;
            if (dtnew != null)
            {
                for (int i = 0; i < dtnew.Rows.Count; i++)
                {
                    MyRow[0] = dtnew.Rows[0]["ProductID"].ToString();
                    MyRow[1] = dtnew.Rows[0]["ProductName"].ToString();
                    MyRow[2] = dtnew.Rows[0]["Price"].ToString();
                    MyRow[3] = dtnew.Rows[0]["img"].ToString();
                    DT.Rows.Add(MyRow);
                    DT.AcceptChanges();
                }
            }
            DataRow MyRow1 = DT.NewRow();
            MyRow1[0] = "2";
            MyRow1[1] = "Product";
            MyRow1[2] = "55.00";
            MyRow1[3] = "purchase_img2.jpg";
            DT.Rows.Add(MyRow1);
            DT.AcceptChanges();
            Session["mySession"] = DT;
           
        }

        protected void btnbag_Click(object sender, EventArgs e)
        {
            DataRow MyRow = DT.NewRow();
            DataTable dtnew = Session["mySession"] as DataTable;
            if (dtnew != null)
            {
                for (int i = 0; i < dtnew.Rows.Count; i++)
                {
                    MyRow[0] = dtnew.Rows[0]["ProductID"].ToString();
                    MyRow[1] = dtnew.Rows[0]["ProductName"].ToString();
                    MyRow[2] = dtnew.Rows[0]["Price"].ToString();
                    MyRow[3] = dtnew.Rows[0]["img"].ToString();
                    DT.Rows.Add(MyRow);
                    DT.AcceptChanges();
                }
            }
            DataRow MyRow1 = DT.NewRow();
            MyRow1[0] = "2";
            MyRow1[1] = "Product";
            MyRow1[2] = "55.00";
            MyRow1[3] = "purchase_img4.jpg";
            DT.Rows.Add(MyRow1);
            DT.AcceptChanges();
            Session["mySession"] = DT;
            
        }

        protected void BtnCheckOut_Click(object sender, EventArgs e)
        {
            DT.Rows.Clear();
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Temp/CheckOut.aspx");
           
        }
    }
}