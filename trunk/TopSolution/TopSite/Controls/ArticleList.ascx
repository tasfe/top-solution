<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleList.ascx.cs"
    Inherits="TopSite.Controls.ArticleList" %>
<div class="index_newslist">
    <div class="catalogue_title">
        <h3>
            <a target="_blank" href="<%="/ArticleList.aspx?id="+CatalogueId.ToString() %>">
                <%=CatalogueTitle %>
            </a>
        </h3>
    </div>
    <div class="articlelist">
        <asp:Repeater ID="RepeaterArticleList" runat="server">
            <HeaderTemplate>
                <ul class="S_arclist">
            </HeaderTemplate>
            <ItemTemplate>
                <li><a target="_blank" title="<%#Eval("Title")+" - "+ CatalogueTitle %>"
                    href="<%#"ArticleShow.aspx?id="+Eval("id") %>">
                    <%#Eval("Title") %></a></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>
