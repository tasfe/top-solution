<%@ Page Title="生成Html页面" Language="C#" MasterPageFile="~/Masters/Account.Master"
    AutoEventWireup="true" CodeBehind="HtmlGenerate.aspx.cs" Inherits="TopSite.Account.HtmlGenerate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .block
        {
            margin-bottom: 5px;
        }
        .center
        {
            vertical-align: middle;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="block">
        <span class="center">站点首页：</span><input id="btnCreateHomepage" type="button" class="center"
            value="生成首页" /></div>
    <div class="block">
        <span class="center">栏目列表：</span><input id="btnCreateCataloguePage" type="button"
            class="center" value="生成栏目列表页" /></div>
    <div class="block">
        <span class="center">&nbsp;&nbsp;&nbsp;文章页：</span><input id="btnCreateAllArticlesPage"
            type="button" class="center" value="生成所有文章页" /></div>
</asp:Content>
