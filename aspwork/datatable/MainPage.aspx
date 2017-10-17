<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainPage.aspx.cs" Inherits="MainPage" %>


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
        <asp:TextBox ID="SelectTxt" runat="server"></asp:TextBox>
        <asp:Button ID="SelectBtn" runat="server" OnClick="SelectBtn_Click" Text="查詢" Width="80px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="NewBtn" runat="server" OnClick="NewBtn_Click" Text="新增" Width="80px" />
        <br />
        <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
            <Columns>
             <asp:TemplateField> 
	                <ItemTemplate> 
	                   <asp:LinkButton ID="LinkButton1" Text="更新" CommandName="updatebtn" CommandArgument='<%# Eval("ProductID") %>' runat="server"></asp:LinkButton> 
                       <asp:LinkButton ID="LinkButton2" Text="刪除" CommandName="deletebtn" CommandArgument='<%# Eval("ProductID") %>' runat="server"></asp:LinkButton> 
	                </ItemTemplate> 
	            </asp:TemplateField> 
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    </form>
</body>
</html>
