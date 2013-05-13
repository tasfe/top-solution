<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ArticleShowMaster.master" AutoEventWireup="true" CodeBehind="ArticleShow.aspx.cs" Inherits="TopSite.ArticleShow" %>
<asp:Content runat="server" ID="head" ContentPlaceHolderID="HeadContent">
    <meta name="keywords" content="<%=TopLogic.BasicCache.SiteConfig.KeyWords%>" />
    <meta name="description" content="<%=TopLogic.BasicCache.SiteConfig.Summary %>"/>
    <link href="Styles/articlelist.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content runat="server" ID="main" ContentPlaceHolderID="MainContent">
    
</asp:Content>