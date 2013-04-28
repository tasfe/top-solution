<%@ Page Title="主页" Language="C#" MasterPageFile="~/Masters/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TopSite._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Repeater ID="RepeaterTaoBaoKeItems" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%#Container.ItemIndex + 1 %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
