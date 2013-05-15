<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RelatedArticleList.ascx.cs" Inherits="TopSite.Controls.RelatedArticleList" %>
    <div class="catalogue_title">
        <h3>
            相关文章
        </h3>
    </div>
    <div class="articlelist">
        <asp:Repeater ID="RepeaterArticleList" runat="server">
            <HeaderTemplate>
                <ul class="S_arclist">
            </HeaderTemplate>
            <ItemTemplate>
                <li><a target="_blank" title="<%#Eval("Title") %>" href="<%#"ArticleShow.aspx?id="+Eval("id") %>">
                    <%#Eval("Title") %></a></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>