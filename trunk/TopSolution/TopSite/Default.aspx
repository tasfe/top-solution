<%@ Page Title="主页" Language="C#" MasterPageFile="~/Masters/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TopSite._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/Common.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="main_products">
        <asp:Repeater ID="RepeaterTaoBaoKeItems" runat="server">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Container.ItemIndex + 1 %>
                    </td>
                    <td>
                        <a href="<%#Eval("ClickUrl") %>">
                            <image src="<%#Eval("PicUrl") %>" class="productImg" alt="<%#Eval("Title")%>"></image>
                        </a>
                    </td>
                    <td>
                        <a href="<%#Eval("ClickUrl") %>">
                            <%#Eval("Title")%></a>
                    </td>
                    <td>
                        <%# Eval("Volume").ToString()%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="index_newslist">
        <asp:Repeater ID="RepeaterArticleList" runat="server">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <a href="<%#"ArticleShow.aspx?id"+Eval("id") %>">
                            <%#Eval("Title") %>
                        </a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
