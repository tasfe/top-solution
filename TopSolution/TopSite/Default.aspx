<%@ Page Title="主页" Language="C#" MasterPageFile="~/Masters/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TopSite._Default" %>

<%@ Register Src="Controls/ArticleList.ascx" TagName="ArticleList" TagPrefix="uc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">   
    <link href="../Styles/default.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
</asp:Content>
