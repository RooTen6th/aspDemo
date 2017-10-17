using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        Response.Write("<h2>Login</h2><BR/>");
    }


    protected void Button1_Click1(object sender, EventArgs e)
    {
        string id = TextBox1.Text;
        int i = (int)id[0];

        /*
        if (TextBox1.Text == "huan" && TextBox2.Text == "123")
        {
            Session["Login"] = "OK";
            Response.Redirect("~/MainPage.aspx");
        }
        else
        {
            //TextBox3.Text = "登入失敗";
            
        }
        */
        if (TextBox2.Text == "123")
        {
            switch (i)
            {
                case 116:
                    Session["Login"] = "OK";
                    Response.Redirect("~/TeacherPage.aspx");
                    break;
                case 115:
                    Session["Login"] = "OK";
                    Response.Redirect("~/StudentPage.aspx");
                    break;
                case 112:
                    Session["Login"] = "OK";
                    Response.Redirect("~/test.aspx");
                    break;
                default:
                    Session["Login"] = "OK";
                    Response.Redirect("~/OtherPage.aspx");
                    break;
            }
        }
    }
}