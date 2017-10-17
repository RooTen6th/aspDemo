<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewDataPage.aspx.cs" Inherits="NewDataPage" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <br />
        <asp:Label ID="Label6" runat="server" Text="新增資料"></asp:Label>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Text="ProductID："></asp:Label>
        <asp:TextBox ID="txtProductID" runat="server" style="margin-bottom: 0px"></asp:TextBox>
        <br />
        <asp:Label ID="Label5" runat="server" Text="ProductName："></asp:Label>
        <asp:TextBox ID="txtProductName" runat="server" ></asp:TextBox>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text="Quantity："></asp:Label>
        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Text="Price："></asp:Label>
        <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="newbt" runat="server" OnClick="newbt_Click1" Text="新增" Width="80px" />
        <br />
        <asp:Label ID="txtMsg" runat="server" ForeColor="#CC0000" Text="txtMsg"></asp:Label>
        <br />
    </form>
</body>
</html>
