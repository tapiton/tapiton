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


public partial class Site_CustomerProfile : System.Web.UI.Page
{
    _plugin objCustomer = new _plugin();
    DAL.Plugin sqlPlugin = new DAL.Plugin();
    DAL.Admin sqlAdmin = new DAL.Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        SpanSuccess.Visible = false;
      
        this.PopulateCountry();
        if (!IsPostBack)
        {
            if (Session["CustomerID"] != null)
            {
                objCustomer.Customer_ID = Convert.ToInt32(Session["CustomerID"].ToString());
                SqlDataReader drPlugin = sqlPlugin.BindCustomerById(objCustomer);
                if (drPlugin.Read())
                {
                    txtFirstName.Value = drPlugin["First_Name"].ToString();
                    txtLastName.Value = drPlugin["Last_Name"].ToString();
                    lblEmail.Text = drPlugin["Email_Id"].ToString();
                    txtAddress.Value = drPlugin["Address"].ToString();
                    txtCity.Value = drPlugin["City"].ToString();
                    txtState.Value = drPlugin["State"].ToString();
                    ddlCountry.SelectedValue = drPlugin["Country_Id"].ToString();
                    if (drPlugin["zip"].ToString() == "1")
                        txtZip.Value = "";
                    else
                        txtZip.Value = drPlugin["zip"].ToString();
                    txtPhone.Value = drPlugin["Phone_Number"].ToString();
                    hdPassword.Value = drPlugin["Password"].ToString();
                }
                DBAccess.InstanceCreation().disconnect();
            }
        }
    }

    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
      
        objCustomer.Customer_ID = Convert.ToInt32(Session["CustomerID"].ToString());
        objCustomer.FirstName = txtFirstName.Value;
        objCustomer.LastName = txtLastName.Value;
        objCustomer.EmailID = lblEmail.Text;
        objCustomer.Address = txtAddress.Value;
        objCustomer.City = txtCity.Value;
        objCustomer.State = txtState.Value;
        objCustomer.Country_ID = ddlCountry.SelectedValue;
        if(txtZip.Value=="")
            objCustomer.Zip = Convert.ToInt32(1);
        else
        objCustomer.Zip =Convert.ToInt32(txtZip.Value);
        objCustomer.PhoneNumber = txtPhone.Value;
        if (txtConfirmPassword.Value != "")
        {
            objCustomer.Password = txtConfirmPassword.Value;
            hdPassword.Value = txtConfirmPassword.Value;
        }
        else
        {
            objCustomer.Password = hdPassword.Value;
        }
        int result = sqlPlugin.UpdateCustomerById(objCustomer);
        SpanSuccess.Visible = true;
    }

    public void PopulateCountry()
    {
        _Country objCountry = new _Country();
        objCountry.ID = 0;
        SqlDataReader drAdmin = sqlAdmin.BindCountry(objCountry);
        while (drAdmin.Read())
        {
            ddlCountry.Items.Add(new ListItem(drAdmin["Country_Name"].ToString(), drAdmin["Country_Id"].ToString()));

        }
        ddlCountry.Items.Insert(0, new ListItem("Choose Country", "0"));
        DBAccess.InstanceCreation().disconnect();
    }
}
