<%@ Page Language="C#" AutoEventWireup="true" CodeFile="homepage.aspx.cs" Inherits="homepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="UpLoads" runat="server" OnClick="UpLoads_Click" Text="上傳" />
        <br />
        <asp:GridView ID="gv" OnRowCommand="gv_RowCommand" runat="server">
            <Columns>
                <asp:TemplateField> 
	                <ItemTemplate> 
	                   <asp:LinkButton ID="DownLoads" Text="下載" CommandName="DownLoads" CommandArgument='<%# Eval("FileName") %>' runat="server"></asp:LinkButton> 
	                   <asp:LinkButton ID="DeleteBtn" Text="刪除" CommandName="DeleteBtn" CommandArgument='<%# Eval("FileName") %>' runat="server"></asp:LinkButton> 
                    </ItemTemplate> 
	           </asp:TemplateField> 
            </Columns>
        </asp:GridView> 
    </form>
</body>
</html>
