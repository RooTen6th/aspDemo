<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    

    
        <br />
        <asp:Label ID="Label4" runat="server" Text="帳號："></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Label ID="Label6" runat="server" Text="t or s or p"></asp:Label>
        <br />

    
    </div>
        <br />
        <asp:Label ID="Label5" runat="server" Text="密碼："></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Label ID="Label7" runat="server" Text="123"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="登入" />
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="留言:"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox3" runat="server" Height="150px" TextMode="MultiLine" Width="200px"></asp:TextBox>
        <br />
    </form>
</body>
</html>
