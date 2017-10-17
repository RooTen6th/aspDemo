using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Windows;

public partial class DeleteDataPage : System.Web.UI.Page
{
    int i;
    protected void Page_Load(object sender, EventArgs e)
    {

        //Response.Write(Session["Index"] + "<br/>");
        Response.Write("<H2>" + "刪除單筆資料頁面" + "</H2>");
        Response.Write(Session["ID"] + "<br/>");
        Response.Write(Session["Name"] + "<br/>");
        Response.Write(Session["Quantity"] + "<br/>");
        Response.Write(Session["Price"] + "<br/>");
        Session["check"] = "";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        i = Convert.ToInt32(Session["Index"]);

        SqlConnection con = new SqlConnection(
           WebConfigurationManager.ConnectionStrings["myDBConnectionString"].ConnectionString);

        SqlDataAdapter ad = new SqlDataAdapter(
             "SELECT ProductID, ProductName, Quantity, Price FROM myProducts", con);

        SqlCommand myCmd = new SqlCommand(
            "SELECT ProductID, ProductName, Quantity, Price FROM myProducts", con);

        SqlCommandBuilder myCmdBuilder = new SqlCommandBuilder(ad);

        con.Open();
        DataSet ds = new DataSet();
        ad.Fill(ds);
        DataRow deleteRow = ds.Tables[0].Rows[i];
        deleteRow.Delete();
        ad.Update(ds.Tables[0]);
        Response.Redirect("~/MainPage.aspx");
    }
}