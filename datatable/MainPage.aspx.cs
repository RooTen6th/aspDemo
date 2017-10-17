using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class MainPage : System.Web.UI.Page
{
    string str;
    int index;
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(
                   WebConfigurationManager.ConnectionStrings["myDBConnectionString"].ConnectionString);

        SqlDataAdapter ad = new SqlDataAdapter(
                    "SELECT ProductID, ProductName, Quantity, Price FROM myProducts", con);

        DataTable table = new DataTable();

        ad.Fill(table);

        GridView1.DataSource = table;
        GridView1.DataBind();
        
    }
    void FindInMultiPKey(DataTable table)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e) 
    { 
        if (e.CommandName == "updatebtn")//判斷是哪個按鈕事件 
        {

            LinkButton btn = (LinkButton)e.CommandSource;    //抓到這個按鈕

            GridViewRow myRow = (GridViewRow)btn.NamingContainer;

            //直接點選更新
            if (Convert.ToString(Session["IND"]) == "")
            {
                Session["Index"] = myRow.DataItemIndex;
                Session["ID"] = GridView1.Rows[myRow.DataItemIndex].Cells[1].Text;
                Session["Name"] = GridView1.Rows[myRow.DataItemIndex].Cells[2].Text;
                Session["Quantity"] = GridView1.Rows[myRow.DataItemIndex].Cells[3].Text;
                Session["Price"] = GridView1.Rows[myRow.DataItemIndex].Cells[4].Text;

                Response.Write("ID：" + Session["ID"] + "</br>");
                Response.Write("Name：" + Session["Name"] + "</br>");
                Response.Write("Quantity：" + Session["Quantity"] + "</br>");
                Response.Write("Price：" + Session["Price"]);

            }
            //查詢後點選更新
            else
            {
                //Session["Index"] = index;
                Session["ID"] = GridView1.Rows[Convert.ToInt16(Session["Index"])].Cells[1].Text;
                Session["Name"] = GridView1.Rows[Convert.ToInt16(Session["Index"])].Cells[2].Text;
                Session["Quantity"] = GridView1.Rows[Convert.ToInt16(Session["Index"])].Cells[3].Text;
                Session["Price"] = GridView1.Rows[Convert.ToInt16(Session["Index"])].Cells[4].Text;
                Session["IND"] = "";

                //Response.Write("ID：" + Session["ID"] + "</br>");
                //Response.Write("Name：" + Session["Name"] + "</br>");
                //Response.Write("Quantity：" + Session["Quantity"] + "</br>");
                //Response.Write("Price：" + Session["Price"]);
            }

            //Response.Write("存取成功");
            Response.Redirect("~/UpdateDataPage.aspx?");
        }

        if (e.CommandName == "deletebtn")
        {
            LinkButton btn = (LinkButton)e.CommandSource;    //先抓到這個按鈕（我們設定了CommandName）
            GridViewRow myRow = (GridViewRow)btn.NamingContainer;

            //直接點選刪除
            if (Convert.ToString(Session["IND"]) == "")
            {
                Session["Index"] = myRow.DataItemIndex;
                Session["ID"] = GridView1.Rows[myRow.DataItemIndex].Cells[1].Text;
                Session["Name"] = GridView1.Rows[myRow.DataItemIndex].Cells[2].Text;
                Session["Quantity"] = GridView1.Rows[myRow.DataItemIndex].Cells[3].Text;
                Session["Price"] = GridView1.Rows[myRow.DataItemIndex].Cells[4].Text;

                Response.Write("ID：" + Session["ID"] + "</br>");
                Response.Write("Name：" + Session["Name"] + "</br>");
                Response.Write("Quantity：" + Session["Quantity"] + "</br>");
                Response.Write("Price：" + Session["Price"]);
                Response.Write("直接點選刪除:");
            }
            //查詢後點選刪除
            else
            {
                //Session["Index"] = index;
                Session["ID"] = GridView1.Rows[Convert.ToInt16(Session["Index"])].Cells[1].Text;
                Session["Name"] = GridView1.Rows[Convert.ToInt16(Session["Index"])].Cells[2].Text;
                Session["Quantity"] = GridView1.Rows[Convert.ToInt16(Session["Index"])].Cells[3].Text;
                Session["Price"] = GridView1.Rows[Convert.ToInt16(Session["Index"])].Cells[4].Text;
                Session["IND"] = "";

                Response.Write("ID：" + Session["ID"] + "</br>");
                Response.Write("Name：" + Session["Name"] + "</br>");
                Response.Write("Quantity：" + Session["Quantity"] + "</br>");
                Response.Write("Price：" + Session["Price"]);
                Response.Write("查詢後刪除:" + "</br>");
                Response.Write("index:" + Convert.ToInt16(Session["Index"]));
            }
            Response.Redirect("~/DeleteDataPage.aspx?");
        }
    }

    protected void SelectBtn_Click(object sender, EventArgs e)
    {
        
        if (SelectTxt.Text != "")
        {
            Session["IND"] = Convert.ToInt16(SelectTxt.Text);
            //Response.Write(Session["IND"]);
        }
        else
        {
            Response.Write("TextBox為空");
        }
        
        if (Convert.ToInt16(Session["IND"]) < 10)
            str = "ProductID = '0" + Session["IND"] + "'";
        else
        {
            str = "ProductID = '" + Session["IND"] + "'";
        }


        SqlConnection con = new SqlConnection(
                   WebConfigurationManager.ConnectionStrings["myDBConnectionString"].ConnectionString);

        SqlDataAdapter ad = new SqlDataAdapter(
                   "SELECT ProductID, ProductName, Quantity, Price FROM myProducts", con);

        DataTable table = new DataTable();
        ad.Fill(table);
        DataRow[] row = table.Select(str);
        index = table.Rows.IndexOf(row[0]);
        //Response.Write("資料: " + index);
        Session["Index"] = index;


        DataSet ds = new DataSet();
        SqlCommand command = new SqlCommand("SELECT * FROM myProducts WHERE ProductID = @ProductID", con);
        command.Parameters.Add("@ProductID", SqlDbType.NChar, 5, "ProductID").Value = SelectTxt.Text;
        ad.SelectCommand = command;
        ad.Fill(ds);







        if (!string.IsNullOrEmpty(SelectTxt.Text))
        {
            //建立資料庫連線
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=myDB;User Id=sa;Password=0330");
            conn.Open();    //開啓資料庫連線

            //建立SqlCommand查詢命令
            SqlCommand cmd = new SqlCommand("Select * From myProducts Where ProductID=@paramPID", conn);
            //於SqlCommand中加入SqlParameter參數，並設定參數值
            cmd.Parameters.Add("@paramPID", SqlDbType.NVarChar, 10).Value = SelectTxt.Text;

            SqlDataReader dr = cmd.ExecuteReader(); //執行查詢

            //確定SqlDataReader是否含有回傳之資料記錄
            if (dr.HasRows)
            {
                //將資料記錄顯示在Details View控制項上
                GridView1.Visible = true;
                //txtMsg.Visible = false;
                GridView1.DataSource = dr;
                GridView1.DataBind();
            }
            else
            {
                //顯示查不到之訊息
                GridView1.Visible = false;
                //txtMsg.Visible = true;
                //txtMsg.Text = "查無該項產品之資料記錄！";
            }
            //釋放物件及連線資源
            dr.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
        else
        {
            //txtMsg.Visible = true;
            //txtMsg.Text = "產品名稱不得為空白！";
        }
        SelectTxt.Text = "";
    }

    protected void NewBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/NewDataPage.aspx?");
    }
}