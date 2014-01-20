<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TopSite._Default" %>

<%--<%@ OutputCache Duration="3600" VaryByParam="none" %>--%>
<%@ Register Src="Controls/ArticleList.ascx" TagName="ArticleList" TagPrefix="uc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <meta name="keywords" content="<%=TopLogic.BasicCache.SiteConfig.KeyWords%>" />
    <meta name="description" content="<%=TopLogic.BasicCache.SiteConfig.Summary %>" />
    <link href="/Styles/default.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
</asp:Content>
