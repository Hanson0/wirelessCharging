<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ASP.NETWebForm.WebForm1" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Hello,World!</h1>
            <asp:Label ID="Label1" runat="server" Text="你家几只鸡："></asp:Label>
<%--            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px"></dx:ASPxTextBox>--%>
            <%--<asp:Button ID="Button1" runat="server" Text="Button" />--%>
            <asp:TextBox ID="TextBox1" runat="server" Height="17px" Width="177px"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        </div>
        <p>
            <asp:Label ID="Label2" runat="server" Text="我家几只鸡："></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server" Height="17px" Width="180px" AutoPostBack="True" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="Label3" runat="server"></asp:Label>
        </p>
        <asp:RadioButton ID="rbtn1" runat="server"  Text="猫" GroupName="animal"/>
        <asp:RadioButton ID="rbtn2" runat="server"  Text="狗" GroupName="animal"/>
        <asp:RadioButton ID="rbtn3" runat="server"  Text="猪" GroupName="animal"/>
        <asp:RadioButton ID="rbtn4" runat="server"  Text="羊" GroupName="animal"/>
        <p>
            <asp:Button ID="Button2" runat="server" Text="动物确定" OnClick="Button2_Click" />
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="成绩计算" />
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        </p>
        <p>
        <asp:RadioButton ID="rbtn5" runat="server"  Text="不及格" GroupName="grade"/>
        <asp:RadioButton ID="rbtn6" runat="server"  Text="及格" GroupName="grade"/>
        <asp:RadioButton ID="rbtn7" runat="server"  Text="良好" GroupName="grade"/>
        <asp:RadioButton ID="rbtn8" runat="server"  Text="优秀" GroupName="grade" />
        </p>
        <p>
            <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" Text="我是AutoPost"  AutoPostBack="true"/>
            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
            </asp:RadioButtonList>

        </p>
    </form>
</body>
</html>
