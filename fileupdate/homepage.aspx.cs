using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class homepage : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Response.Write("<h2>檔案上傳</h2>");

            SqlConnection con = new SqlConnection(
                WebConfigurationManager.ConnectionStrings["myDBConnection"].ConnectionString);

            SqlDataAdapter ad = new SqlDataAdapter(
                "SELECT FileName FROM FileUpdate", con);

            DataTable dt = new DataTable();

            ad.Fill(dt);

            gv.DataSource = dt;
            gv.DataBind();

            //dt.Columns.Add("檔案名稱");

            Session["dt"] = dt;

            gv.DataSource = dt;
            gv.DataBind();
        }
    }

    ///////把檔案下載到下載區
    //protected void DownLoads_Click(object sender, EventArgs e)
    //{
    //    //用戶端的物件
    //    System.Net.WebClient wc = new System.Net.WebClient();
    //    byte[] file = null;

    //    try
    //    {
    //        //用戶端下載檔案到byte陣列
    //        file = wc.DownloadData("http://140.138.155.180:9000/WebSite/upload/p2.jpg");
    //    }


    //    catch (Exception ex)
    //    {
    //        HttpContext.Current.Response.Write(
    //            "ASP.net禁止下載此敏感檔案(通常為：.cs、.vb、微軟資料庫mdb、mdf和config組態檔等)。<br/>檔案路徑：" 
    //            + "http://140.138.155.180:9000/WebSite/upload/" 
    //            + "<br/>錯誤訊息：" + ex.ToString());
    //        return;
    //    }

    //    HttpContext.Current.Response.Clear();
    //    string fileName = System.IO.Path.GetFileName(
    //        "http://140.138.155.180:9000/WebSite/upload/p2.jpg");
    //    //跳出視窗，讓用戶端選擇要儲存的地方                         //使用Server.UrlEncode()編碼中文字才不會下載時，檔名為亂碼
    //    HttpContext.Current.Response.AddHeader(
    //        "content-disposition", "attachment;filename=" 
    //        + HttpContext.Current.Server.UrlEncode(fileName));
    //    //設定MIME類型為二進位檔案
    //    HttpContext.Current.Response.ContentType = "application/octet-stream";

    //    try
    //    {
    //        //檔案有各式各樣，所以用BinaryWrite
    //        HttpContext.Current.Response.BinaryWrite(file);

    //    }
    //    catch (Exception ex)
    //    {
    //        HttpContext.Current.Response.Write(
    //            "檔案輸出有誤，您可以在瀏覽器的URL網址貼上以下路徑嘗試看看。<br/>檔案路徑：" 
    //            + "http://140.138.155.180:9000/WebSite/upload/" + "<br/>錯誤訊息：" + ex.ToString());
    //        return;
    //    }

    //    //這是專門寫文字的
    //    //HttpContext.Current.Response.Write();
    //    HttpContext.Current.Response.End();

    ///////////把檔案下載到指定路徑        
    //    //string MapPath = Server.MapPath("~/download/");

    //    //string remoteUri = "http://140.138.155.180:9000/WebSite/upload/";
        
    //    //string fileName = "p3.jpg", myStringWebResource = null;

    //    //// Create a new WebClient instance.
    //    //WebClient myWebClient = new WebClient();

    //    //// Concatenate the domain with the Web resource filename.
    //    //myStringWebResource = remoteUri + fileName;
        
    //    //// Download the Web resource and save it into the current filesystem folder.
    //    //myWebClient.DownloadFile(myStringWebResource, MapPath + fileName);
        
    //    //Response.Write(myStringWebResource); 
    //}
    
    
    protected void gv_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        LinkButton btn = (LinkButton)e.CommandSource;    //先抓到這個按鈕（我們設定了CommandName）
        GridViewRow myRow = (GridViewRow)btn.NamingContainer;

        //按下下載按鈕事件
        if (e.CommandName == "DownLoads")
        {
            DataTable dt = (DataTable)Session["dt"];

            byte[] file = null;

            string Filename = dt.Rows[myRow.DataItemIndex][0].ToString();

            //string url = "~/upload/";
            string url = Server.MapPath("~/upload/");
            Response.Write(url);

            //用戶端的物件
            System.Net.WebClient wc = new System.Net.WebClient();

            try
            {
                //用戶端下載檔案到byte陣列
                file = wc.DownloadData(url + dt.Rows[myRow.DataItemIndex][0].ToString().Trim());
            }

            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(
                    "ASP.net禁止下載此敏感檔案(通常為：.cs、.vb、微軟資料庫mdb、mdf和config組態檔等)。<br/>檔案路徑："
                    + url
                    + "<br/>錯誤訊息：" + ex.ToString());
                return;
            }

            HttpContext.Current.Response.Clear();

            string fileName = System.IO.Path.GetFileName(
                url + dt.Rows[myRow.DataItemIndex][0].ToString().Trim());
            //跳出視窗，讓用戶端選擇要儲存的地方                         //使用Server.UrlEncode()編碼中文字才不會下載時，檔名為亂碼
            HttpContext.Current.Response.AddHeader(
                "content-disposition", "attachment;filename="
                + HttpContext.Current.Server.UrlEncode(fileName));
            //設定MIME類型為二進位檔案
            HttpContext.Current.Response.ContentType = "application/octet-stream";

            try
            {
                //檔案有各式各樣，所以用BinaryWrite
                HttpContext.Current.Response.BinaryWrite(file);

            }
            catch (Exception ex)
            {

                HttpContext.Current.Response.Write(
                    "檔案輸出有誤，您可以在瀏覽器的URL網址貼上以下路徑嘗試看看。<br/>檔案路徑："
                    + url + "<br/>錯誤訊息：" + ex.ToString());
                return;

            }

            HttpContext.Current.Response.End();

        }
        //按下刪除按鈕事件
        else if (e.CommandName == "DeleteBtn")
        {
            //刪除路徑的資料
            SqlConnection con = new SqlConnection(
                WebConfigurationManager.ConnectionStrings["myDBConnection"].ConnectionString);

            SqlDataAdapter ad = new SqlDataAdapter(
                "SELECT FileName FROM FileUpdate", con);

            DataTable dt = (DataTable)Session["dt"];

            int count = dt.Rows.Count;

            string Filename = dt.Rows[myRow.DataItemIndex][0].ToString();

            string FilePath = @"D:/s1022673/WebSite/upload/" + Filename;

            if (File.Exists(FilePath))
            {

                try
                {

                    File.Delete(FilePath);

                }

                catch (System.IO.IOException ex)
                {

                    Response.Write(ex.Message);

                    return;

                }

            }
            else 
            {
                Response.Write("路徑中已無此筆資料");
            }
            //dt.Rows[e.RowIndex].Delete();//刪除datatable的資料列

            //刪除DB的資料
            SqlConnection con2 = new SqlConnection(
                WebConfigurationManager.ConnectionStrings["myDBConnection"].ConnectionString);

            SqlDataAdapter ad2 = new SqlDataAdapter(
                 "SELECT ID, FileName FROM FileUpdate", con2);
            
            //創造一個有索引值的table
            DataTable dt2 = new DataTable();

            SqlCommand myCmd = new SqlCommand("SELECT ID, FileName FROM FileUpdate", con2);

            SqlCommandBuilder myCmdBuilder = new SqlCommandBuilder(ad2);

            con2.Open();

            ad2.Fill(dt2);

            dt.Rows[myRow.DataItemIndex].Delete();
            dt2.Rows[myRow.DataItemIndex].Delete();
            
            ad2.Update(dt2);

            gv.DataSource = dt;
            gv.DataBind();
        }

        //用戶端的物件
       /* System.Net.WebClient wc = new System.Net.WebClient();
        byte[] file = null;

        try
        {
            //用戶端下載檔案到byte陣列
            file = wc.DownloadData("http://140.138.155.180:9000/WebSite/upload/p3.jpg");
        }


        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(
                "ASP.net禁止下載此敏感檔案(通常為：.cs、.vb、微軟資料庫mdb、mdf和config組態檔等)。<br/>檔案路徑："
                + "http://140.138.155.180:9000/WebSite/upload/"
                + "<br/>錯誤訊息：" + ex.ToString());
            return;
        }

        HttpContext.Current.Response.Clear();
        string fileName = System.IO.Path.GetFileName(
            "http://140.138.155.180:9000/WebSite/upload/p3.jpg");
        //跳出視窗，讓用戶端選擇要儲存的地方                         //使用Server.UrlEncode()編碼中文字才不會下載時，檔名為亂碼
        HttpContext.Current.Response.AddHeader(
            "content-disposition", "attachment;filename="
            + HttpContext.Current.Server.UrlEncode(fileName));
        //設定MIME類型為二進位檔案
        HttpContext.Current.Response.ContentType = "application/octet-stream";

        try
        {
            //檔案有各式各樣，所以用BinaryWrite
            HttpContext.Current.Response.BinaryWrite(file);

        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(
                "檔案輸出有誤，您可以在瀏覽器的URL網址貼上以下路徑嘗試看看。<br/>檔案路徑："
                + "http://140.138.155.180:9000/WebSite/upload/" + "<br/>錯誤訊息：" + ex.ToString());
            return;
        }

        //這是專門寫文字的
        //HttpContext.Current.Response.Write();
        HttpContext.Current.Response.End();
        // If multiple buttons are used in a GridView control, use the
        // CommandName property to determine which button was clicked.
        if (e.CommandName == "DownLoads")
        {
            LinkButton btn = (LinkButton)e.CommandSource;    //先抓到這個按鈕（我們設定了CommandName）
            GridViewRow myRow = (GridViewRow)btn.NamingContainer;
            Response.Write(myRow.DataItemIndex);
        }*/
    }


    protected void UpLoads_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(
                   WebConfigurationManager.ConnectionStrings["myDBConnection"].ConnectionString);

        SqlDataAdapter ad = new SqlDataAdapter(
                    "SELECT FileName FROM FileUpdate", con);

        SqlDataAdapter ad2 = new SqlDataAdapter(
                    "SELECT ID FROM FileUpdate", con);

        DataTable dt = (DataTable)Session["dt"];
        DataTable dt2 = new DataTable();

        ad2.Fill(dt2);

        SqlCommand cmd = null;

        if (FileUpload1.HasFile)
        {
            string saveResult = "";

            //fileupload裡檔案的名稱
            string fileName = FileUpload1.FileName;

            //上傳到的路徑
            string savePath = "D:/s1022673/WebSite/upload/";

            saveResult = savePath + fileName;

            //上傳檔案
            FileUpload1.SaveAs(saveResult);

            //新增一列
            dt.Rows.Add(FileUpload1.FileName);

            Session["dt"] = dt;
            Session["dt2"] = dt2;

            gv.DataSource = dt;
            gv.DataBind();



            Response.Write("<b>上傳成功</b>，檔名---- " + fileName+ "</br>");
            Response.Write("<br />路徑檔名---- " + savePath+ "</br>");

            try
            {
                int count;

                //當沒有資料時索引值為-1  後面新增時會加1
                if (dt2.Rows.Count == 0)
                {
                    count = -1;
                }
                //當有資料時索引值為前一筆的索引值 面新增時會加1
                else
                {
                    count = Convert.ToInt32(dt2.Rows[dt2.Rows.Count - 1]["ID"]);
                }

                con.Open();    //開啓資料庫連線

                //建立SqlCommand查詢命令
                cmd = new SqlCommand("Insert Into FileUpdate (ID, FileName, NextName) values (@ID, @FileName, @NextName) ", con);
                cmd.Parameters.Add("@ID", SqlDbType.NVarChar, 10).Value = count + 1;
                cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 10).Value = fileName;
                cmd.Parameters.Add("@NextName", SqlDbType.NVarChar, 10).Value = fileName;

                int rows = cmd.ExecuteNonQuery();

                Response.Write(string.Format("新增產品資料記錄{0}筆成功！", rows) + "</br>");

                Session["Index"] = Convert.ToInt32(dt2.Rows[dt2.Rows.Count - 1]["ID"]);

            }
            catch (Exception ex)
            {
                //顯示錯誤訊息
                Response.Write(ex.ToString() + "</br>");
            }
            finally
            {
                //釋放物件及連線資源
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
        }
        else
        {
            Response.Write("請先挑選檔案之後，再來上傳");
        }
    }
    ///舊刪除鍵
    //protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
        
    //    SqlConnection con = new SqlConnection(
    //              WebConfigurationManager.ConnectionStrings["myDBConnection"].ConnectionString);

    //    SqlDataAdapter ad = new SqlDataAdapter(
    //                "SELECT FileName FROM FileUpdate", con);

    //    DataTable dt = (DataTable)Session["dt"];

    //    int count = dt.Rows.Count;

    //    string Filename = dt.Rows[e.RowIndex][0].ToString();

    //    ////string FilePath = @"C:\" + Filename;
    //    string FilePath = @"D:/s1022673/WebSite/upload/" + Filename;

    //    if (File.Exists(FilePath))
    //    {

    //        try
    //        {

    //            File.Delete(FilePath);

    //        }

    //        catch (System.IO.IOException ex)
    //        {

    //            Response.Write(ex.Message);

    //            return;

    //        }

    //    }

    //    //dt.Rows[e.RowIndex].Delete();//刪除datatable的資料列

    //    SqlConnection con2 = new SqlConnection(
    //        WebConfigurationManager.ConnectionStrings["myDBConnection"].ConnectionString);

    //    SqlDataAdapter ad2 = new SqlDataAdapter(
    //         "SELECT ID, FileName FROM FileUpdate", con2);

    //    DataTable dt2 = new DataTable();

    //    SqlCommand myCmd = new SqlCommand("SELECT ID, FileName FROM FileUpdate", con2);

    //    SqlCommandBuilder myCmdBuilder = new SqlCommandBuilder(ad2);

    //    con2.Open();
    //    //DataSet ds = new DataSet();
    //    ad2.Fill(dt2);

    //    dt.Rows[e.RowIndex].Delete();
    //    dt2.Rows[e.RowIndex].Delete();
       
    //    ad2.Update(dt2);

    //    gv.DataSource = dt;
    //    gv.DataBind();
      
    //}
    //protected void gv_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Response.Write("123");
    //}
    //<Columns>
    //    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
    //</Columns>
}