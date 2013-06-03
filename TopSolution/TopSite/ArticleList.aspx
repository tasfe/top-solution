<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ArticleListMaster.master"
    AutoEventWireup="true" CodeBehind="ArticleList.aspx.cs" Inherits="TopSite.ArticleList" %>

<%@ OutputCache Duration="3600" VaryByParam="id;page" %>
<%@ Register Src="Controls/ArticleList.ascx" TagName="ArticleList" TagPrefix="uc1" %>
<asp:Content runat="server" ID="head" ContentPlaceHolderID="HeadContent">
    <meta name="keywords" content="<%=curCatalogue.KeyWords%>" />
    <meta name="description" content="<%=curCatalogue.Summary %>"/>
    <link href="Styles/articlelist.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content runat="server" ID="main" ContentPlaceHolderID="MainContent">
    <uc1:ArticleList ID="ArticleList1" runat="server" />
    <div class="pager">
        <span><a href="/ArticleList.aspx?id=<%=CataId.ToString() %>&page=1">第一页</a></span>
        <%
            PageCount = this.ArticleList1.PageCount;
            int pageMin = PageIndex - 5 > 1 ? PageIndex - 5 : 1;
            int pageMax = PageIndex + 5 < PageCount ? PageIndex + 5 : PageCount;
            if (pageMin > 1)
            {
                int tempMin = (pageMin - 5) < 1 ? 1 : (pageMin - 5);
        %>
        <span><a href="/ArticleList.aspx?id=<%=CataId.ToString() %>&page=<%=tempMin.ToString() %>"
            title="前5页">……</a> </span>
        <%}
            for (int i = pageMin; i <= pageMax; i++)
            {              
        %>
        <span><a href="/ArticleList.aspx?id=<%=CataId.ToString() %>&page=<%=i.ToString() %>">
            <%=i.ToString() %></a></span>
        <%
            } %>
        <%if (pageMax < PageCount)
          {
              int tempMax = (pageMax + 5) > PageCount ? PageCount : (pageMax + 5);
        %>
        <span><a href="/ArticleList.aspx?id=<%=CataId.ToString() %>&page=<%=tempMax.ToString() %>"
            title="后5页">……</a> </span>
        <%} %>
        <span><a href="/ArticleList.aspx?id=<%=CataId.ToString() %>&page=<%=PageCount.ToString() %>">
            最后一页</a></span>
    </div>
</asp:Content>
