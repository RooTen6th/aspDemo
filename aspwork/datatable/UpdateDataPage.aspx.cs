using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class UpdateDataPage : System.Web.UI.Page
{
    
    int i;//哪一列的資料
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write("Name: " + Session["Name"].ToString() + "<br/>");
        //Response.Write("Quantity: " + Session["Quantity"].ToString() + "<br/>");
        //Response.Write("Price: " + Session["Price"].ToString() + "<br/>");

        //Response.Write(Session["Index"]);
        ////清空Session防止資料發生錯誤
        if (Convert.ToString(Session["Name"]) != "")
        {
            TextBox1.Text = Convert.ToString(Session["Name"]);
        //    Session["Name"] = "";
        }
        if (Convert.ToString(Session["Quantity"]) != "")
        {
            TextBox2.Text = Convert.ToString(Session["Quantity"]);
        //    Session["Quantity"] = "";
        }
        if (Convert.ToString(Session["Price"]) != "")
        {
            TextBox3.Text = Convert.ToString(Session["Price"]);
        //    Session["Price"] = "";
        }
        Session["check"] = "";
    }
    protected void UpdateData()
    {
        i = Convert.ToInt32(Session["Index"]);
        Response.Write("i=" + i);
        SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["myDBConnectionString"].ConnectionString);

        //建立DataAdapter  溝通者 中間人  
        SqlDataAdapter da = new SqlDataAdapter(
            "SELECT ProductID, ProductName, Quantity, Price FROM myProducts", con);


        //取得或設定 TRANSACT-SQL 陳述式或預存程序用來更新資料來源中的記錄。
        da.UpdateCommand = new SqlCommand(
           "UPDATE myProducts SET ProductName = @ProductName, " + "Quantity = @Quantity, " +
           "Price = @Price WHERE ProductID = @ProductID", con);

        //設定存放空間
        da.UpdateCommand.Parameters.Add(
           "@ProductName", SqlDbType.NVarChar, 15, "ProductName");
        da.UpdateCommand.Parameters.Add(
            "@Quantity", SqlDbType.Int, 15, "Quantity");
        da.UpdateCommand.Parameters.Add(
            "@Price", SqlDbType.SmallMoney, 15, "Price");

        SqlParameter parameter = da.UpdateCommand.Parameters.Add(
          "@ProductID", SqlDbType.Int);

        parameter.SourceColumn = "ProductID";
        parameter.SourceVersion = DataRowVersion.Original;

        DataTable dt = new DataTable();
        da.Fill(dt);

        DataRow Row = dt.Rows[i];//第幾列
        Row["ProductName"] = TextBox1.Text;
        Row["Quantity"] = Convert.ToInt32(TextBox2.Text);
        Row["Price"] = Convert.ToDouble(TextBox3.Text);

        da.Update(dt);

        //foreach (datarow row in categorytable.rows)
        //{
        //    {
        //        response.write(row[0] + " ");//productid 欄
        //        response.write(row[1] + " ");//productname 欄
        //        response.write(row[2] + " ");//quantity 欄
        //        response.write(row[3] + "</br>");//price 欄
        //    }
        //}
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        UpdateData();
        Response.Redirect("~/MainPage.aspx");
    }
}