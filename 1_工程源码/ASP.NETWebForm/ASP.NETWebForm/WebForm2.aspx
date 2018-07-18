<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="ASP.NETWebForm.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .style0 {
            border: 1px solid red;
        }

            .style0 li {
                font-size: 16px;
                padding: 5px;
                border-bottom: 1px dotted gray;
            }

        .style1 li {
            display: inline-block;
            width: 100px;
            padding: 5px;
        }

        .style2 li {
            padding: 15px;
            list-style-type: decimal-leading-zero;
        }

        .style3 li {
            padding: 45px 0px 0px 45px;
            color: gray;
            cursor: pointer;
        }

            .style3 li:hover {
                color: red;
            }

        .style4 {
            margin: 0px;
            padding: 0px;
            border: 1px solid #ccc;
            border-top: 1px solid #ccc;
            /*background-image:url(image/back_navbar.png);*/
        }

            .style4 li {
                display: inline-block;
                padding: 15px 30px 15px 30px;
                border-right: 1px solid #ddd;
            }

            .style4 a {
                color: #666;
                text-decoration: none;
            }

                .style4 a:hover {
                    color: red;
                }

        /*都会执行的部分*/
        ul {
            list-style-type: none;
            margin: 0;
            padding: 0;
            overflow: hidden;
            background-color: #333;
        }

        li {
            float: left;
        }

            li a {
                display: block;
                color: white;
                text-align: center;
                padding: 14px 16px;
                text-decoration: none;
            }

                /*鼠标移动到选项上修改背景颜色 */
                li a:hover {
                    background-color: #111;
                }
                /*ul {
    position: fixed;
    bottom: 0;
    width: 100%;
}*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div <%--style="border : 1px solid green"--%>>
            <div id="chkslist" runat="server">
                <asp:CheckBox ID="CheckBox2" runat="server" Text="喝茶" />
                <asp:CheckBox ID="CheckBox3" runat="server" Text="下棋" />
                <asp:CheckBox ID="CheckBox4" runat="server" Text="打球" />
                <asp:CheckBox ID="CheckBox5" runat="server" Text="打牌" />
                <br />
                <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <div>
            <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" Text="我是AutoPost" AutoPostBack="true" />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </div>
<%--        CssClass="style4"--%>
        <asp:BulletedList ID="BulletedList1" runat="server" 
            DataSourceID="AccessDataSource1" DataTextField="网站名称" DataValueField="网址"
            DisplayMode="HyperLink" >
            <asp:ListItem>项目一</asp:ListItem>
            <asp:ListItem>项目二</asp:ListItem>
            <asp:ListItem>项目三</asp:ListItem>
        </asp:BulletedList>
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/Data/Iink.accdb" SelectCommand="SELECT [网站名称], [网址] FROM [URL]"></asp:AccessDataSource>
        <p>
            <asp:RadioButton ID="RadioButton1" runat="server" Text="横向" AutoPostBack="true" GroupName="Arrange" OnCheckedChanged="RadioButton1_CheckedChanged" />
            <asp:RadioButton ID="RadioButton2" runat="server" Text="纵向" AutoPostBack="true" GroupName="Arrange" OnCheckedChanged="RadioButton2_CheckedChanged" />
            <asp:RadioButton ID="RadioButton3" runat="server" Text="超链接纵向" AutoPostBack="true" GroupName="Arrange" OnCheckedChanged="RadioButton3_CheckedChanged" />
                            
        </p>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="AccessDataSource2" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" DataTextField="网站名称" DataValueField="网址">
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" Text="选中第三项" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="清除所有选项" OnClick="Button3_Click" />
        <asp:Button ID="Button4" runat="server" Text="转向" OnClick="Button4_Click" />
        <asp:Button ID="Button5" runat="server" Text="1项改为京东" OnClick="Button5_Click" />
        <asp:Button ID="Button6" runat="server" Text="添加新项" OnClick="Button6_Click" />
        <asp:AccessDataSource ID="AccessDataSource2" runat="server" DataFile="~/Data/Iink.accdb" SelectCommand="SELECT [网站名称], [网址] FROM [URL]"></asp:AccessDataSource>
    </form>
</body>
</html>
