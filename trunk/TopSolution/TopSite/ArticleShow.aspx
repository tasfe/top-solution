<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ArticleShowMaster.master" AutoEventWireup="true" CodeBehind="ArticleShow.aspx.cs" Inherits="TopSite.ArticleShow" %>
<asp:Content runat="server" ID="head" ContentPlaceHolderID="HeadContent">
    <meta name="keywords" content="<%=CurArticle.KeyWords%>" />
    <meta name="description" content="<%=CurArticle.Summary %>"/>
    <link href="Styles/article.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content runat="server" ID="main" ContentPlaceHolderID="MainContent">
    <div></div>
    <div></div>
    <div></div>
</asp:Content>