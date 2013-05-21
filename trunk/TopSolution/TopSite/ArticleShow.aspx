﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ArticleShowMaster.master"
    AutoEventWireup="true" CodeBehind="ArticleShow.aspx.cs" Inherits="TopSite.ArticleShow" %>

<%@ Register src="Controls/RelatedArticleList.ascx" tagname="RelatedArticleList" tagprefix="uc1" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderID="HeadContent">
    <meta name="keywords" content="<%=CurArticle.KeyWords%>" />
    <meta name="description" content="<%=CurArticle.Summary %>" />
    <link href="Styles/article.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content runat="server" ID="main" ContentPlaceHolderID="MainContent">
    <form id="form1" runat="server">
    <div class="article">
        <div class="article_title">
            <%=CurArticle.Title%></div>
        <div class="article_info">
            <span>来源：<%=CurArticle.OrignSource%></span><span>时间：<%=CurArticle.CreateDate%></span><span>点击量：<%=CurArticle.ClickNum%></span></div>
        <div class="article_sumary">
            <%=CurArticle.Summary%></div>
        <div class="article_content">
            <%=CurArticle.Content%></div>
        <div class="article_related">
            <uc1:RelatedArticleList ID="RelatedArticleList1" runat="server" />
        </div>
    </div>
    </form>
</asp:Content>