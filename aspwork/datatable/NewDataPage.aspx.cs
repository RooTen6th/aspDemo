using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class NewDataPage : System.Web.UI.Page
{
    string info = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write();
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(
                   WebConfigurationManager.ConnectionStrings["myDBConnectionString"].ConnectionString);

        SqlDataAdapter ad = new SqlDataAdapter(
                    "SELECT ProductID, ProductName, Quantity, Price FROM myProducts", con);
        ad.Fill(dt);
        //TextBox1.Text = Convert.ToString(dt.Rows.Count + 1);
        int count = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["ProductID"]);
        Response.Write("最後一筆ID資料: " + count);

        //if (count < 10)
        //    txtProductID.Text = "0" + Convert.ToString(count);
        //else
        //    txtProductID.Text = Convert.ToString(count);
        //    //Response.Write(dt.Rows.Count);//共幾筆資料
    }


    protected void newbt_Click1(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(
                   WebConfigurationManager.ConnectionStrings["myDBConnectionString"].ConnectionString);

        SqlDataAdapter ad = new SqlDataAdapter(
                    "SELECT ProductID, ProductName, Quantity, Price FROM myProducts", con);
        ad.Fill(dt);

        if (checkInputValid())
        {
            //建立資料庫連線
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=myDB;User Id=sa;Password=0330");
            SqlCommand cmd = null;
            try
            {
                conn.Open();    //開啓資料庫連線

                //建立SqlCommand查詢命令
                cmd = new SqlCommand("Insert Into myProducts (ProductID, ProductName, Quantity, Price) values (@paramPID, @paramPN, @paramQuantity, @paramPrice) ", conn);
                cmd.Parameters.Add("@paramPID", SqlDbType.NVarChar, 10).Value = txtProductID.Text;
                cmd.Parameters.Add("@paramPN", SqlDbType.NVarChar, 50).Value = txtProductName.Text;
                cmd.Parameters.Add("@paramQuantity", SqlDbType.Int, 4).Value = txtQuantity.Text;
                cmd.Parameters.Add("@paramPrice", SqlDbType.SmallMoney, 4).Value = txtPrice.Text;
                int rows = cmd.ExecuteNonQuery();
                //txtMsg.Text = string.Format("新增產品資料記錄{0}筆成功！", rows);
            }
            catch (Exception ex)
            {
                txtMsg.Text = ex.ToString();  //顯示錯誤訊息
            }
            finally
            {
                //釋放物件及連線資源
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }
        else
        {
            txtMsg.Text = info;
        }
        Response.Redirect("~/MainPage.aspx");
    }

    protected bool checkInputValid()
    {
        bool ValidStatus = true;
        //檢查產品編號是否有輸入
        //if (string.IsNullOrEmpty(txtProductID.Text))
        //{
        //    ValidStatus = false;
        //    info = "*產品編號不得為空白！<br/>";
        //}

        //檢查產品名稱是否有輸入
        if (string.IsNullOrEmpty(txtProductName.Text))
        {
            ValidStatus = false;
            info += "*產品名稱不得為空白！<br/>";
        }
        //檢查產品數量是否有輸入，並且為數字
        int i;
        if (!int.TryParse(txtQuantity.Text, out i))
        {
            ValidStatus = false;
            info += "*產品數量不得為空白，且必須為數字！<br/>";
        }
        //檢查產品價格是否有輸入，並且為數字
        if (!int.TryParse(txtPrice.Text, out i))
        {
            ValidStatus = false;
            info += "*產品價格不得為空白，且必須為數字！<br/>";
        }
        return ValidStatus;
    }
}