<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="ASP.NETWebForm.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        a.a2
        {
            color: gray;
            border:1px solid silver;border-bottom:1px solid #ddd;
            padding:10px;
            text-decoration:none;
            background-image:url(image/gray.png);

        }
        a.a2:hover
        {
            color:blue;
            border:1px solid #ddd;
            border-bottom:1px solid silver;
            border-right:1px solid silver;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ListBox ID="ListBox1" runat="server" Height="181px" Width="170px" SelectionMode="Multiple">
            <asp:ListItem>面包</asp:ListItem>
            <asp:ListItem>牛奶</asp:ListItem>
            <asp:ListItem>香蕉</asp:ListItem>
            <asp:ListItem>饼干</asp:ListItem>
            <asp:ListItem>鸡蛋</asp:ListItem>
            <asp:ListItem>面条</asp:ListItem>
            <asp:ListItem>火腿肠</asp:ListItem>
            <asp:ListItem>薯条</asp:ListItem>

        </asp:ListBox>

        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="&lt;&lt;" />
    <asp:Button ID="Button1" runat="server" Text="&gt;&gt;" OnClick="Button1_Click" />
        <asp:ListBox ID="ListBox2" runat="server" Height="176px" SelectionMode="Multiple" Width="177px"></asp:ListBox>

    </div>
        <p>

        <asp:TextBox ID="TextBox1" runat="server" Height="244px" OnTextChanged="TextBox1_TextChanged" TextMode="MultiLine" Width="163px"></asp:TextBox>

        </p>
        <p>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="代码生成2个链接，放入Panel" Width="543px" />
        </p>
        <p>
            <asp:Button ID="Button4" runat="server" Text="代码生成2个带CssClass属性的链接，放入Panel" OnClick="Button4_Click" />
        </p>
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
        <p>
            <asp:ImageMap ID="ImageMap1" runat="server" src="image/gray.png" usemap="#ImageMapimgMap#">

            </asp:ImageMap>
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="添加方形热区" />
        </p>
        <p>
        </p>
    </form>
</body>
</html>
