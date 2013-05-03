<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Account.Master" AutoEventWireup="true"
    CodeBehind="ArticleList.aspx.cs" Inherits="TopSite.Account.ArticleList" %>

<%@ Import Namespace="TopArticleEntity.Enum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridViewArticleList" runat="server" AutoGenerateColumns="False"
        AllowPaging="True" OnPageIndexChanging="GridViewArticleList_PageIndexChanging"
        PageSize="20">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Title" HeaderText="标题" />
            <asp:BoundField DataField="ClickNum" HeaderText="点击" />
            <asp:BoundField DataField="CreateDate" HeaderText="发布时间" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:HyperLink ID="hlinkEdit" runat="server" NavigateUrl='<%# "ArticleEdit.aspx?action="+EditStateEnum.Edit.ToString()+"&id="+Eval("Id").ToString() %>'>编辑</asp:HyperLink>
                    &nbsp;
                    <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="return confirm('确认要删除吗？');"
                        OnClick="lbtnDel_Click">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
