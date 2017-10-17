using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

public partial class exceledit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //FileUpLoad的檔名
        //string FileName = FileUpload1.FileName;
        //string path = "D:/s1022673/EXCEL/xlsxUpdate/" + FileName;
        //上傳檔案儲存位置
        string path = @"D:\s1022673\EXCEL\xlsxUpdate\studentdata.xlsx";
        string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 8.0;";

        FileUpload1.PostedFile.SaveAs(path);

        //建立OleDbConnection
        OleDbConnection myConn = new OleDbConnection(connStr);
        //Excel下方的sheet1
        OleDbCommand myCommand = new OleDbCommand("SELECT * FROM [Sheet1$]", myConn);
        //建立OleDbDataAdapter
        OleDbDataAdapter myDA = new OleDbDataAdapter(myCommand);

        DataTable dt = new DataTable();
        dt.Columns.Add(" ", typeof(string));
        //加入或重新整理DataTable中的資料列
        myDA.Fill(dt);
        
        //新增排行的欄位
        dt.Columns.Add("排行", typeof(Int64));


        DataRow Row = dt.NewRow();
        //Row總數
        int count = dt.Rows.Count;
        int total = 0;
        int[] array= new int[count];
        int[] id = new int[count];
        for (int n = 0; n < count; n++)
        {
            total += Convert.ToInt32(dt.Rows[n]["價格"]);
            array[n] = Convert.ToInt32(dt.Rows[n]["價格"]);
            id[n] = n;
        }
        
        //新增Row
        dt.Rows.Add(Row);
        //在最後一條Row計算總和
        dt.Rows[count][0] = "總計";
        dt.Rows[count]["價格"] = total;

        //Bubble Sort
        int i, j, temp, temp2;
        for (i = array.Length - 1; i >= 0; i--)
        {
            for (j = 0; j < i; j++)
            {
                if (array[j] < array[i])
                {
                    temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;

                    //把價格跟他原本所在的Row記錄起來
                    temp2 = id[i];
                    id[i] = id[j];
                    id[j] = temp2;
                }
            }
        }
        //依據上面所記錄的給他排行
        for (int n = 0; n < count; n++)
        {
            dt.Rows[id[n]]["排行"] = n + 1;
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
}
