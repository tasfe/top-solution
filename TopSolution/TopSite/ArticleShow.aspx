<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ArticleShowMaster.master"
    AutoEventWireup="true" CodeBehind="ArticleShow.aspx.cs" Inherits="TopSite.ArticleShow" %>

<%--<%@ OutputCache Duration="3600" VaryByParam="id" %>--%>
<%@ Register Src="Controls/RelatedArticleList.ascx" TagName="RelatedArticleList"
    TagPrefix="uc1" %>
<asp:Content runat="server" ID="head" ContentPlaceHolderID="HeadContent">
    <meta name="keywords" content="<%=CurArticle.KeyWords%>" />
    <meta name="description" content="<%=CurArticle.Summary %>" />
    <link href="/Styles/article.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content runat="server" ID="main" ContentPlaceHolderID="MainContent">
    <div class="article">
        <div class="article_title">
            <%=CurArticle.Title%></div>
        <div class="article_info">
            <span>来源：<a href="<%=CurArticle.OrignSourceUrl %>" rel="nofollow" target="_blank"><%=CurArticle.OrignSource%></a></span><span>时间：<%=CurArticle.CreateTime%></span><span>点击量：<%=CurArticle.ClickNum%></span></div>
        <div class="article_sumary">
            <%=CurArticle.Summary%></div>
        <div class="article_content">
            <%=CurArticle.Content%></div>
        <div>
            <!-- Baidu Button BEGIN -->
            <div id="bdshare" class="bdshare_t bds_tools_32 get-codes-bdshare">
                <a class="bds_tsina"></a><a class="bds_qzone"></a><a class="bds_tqq"></a><a class="bds_renren">
                </a><a class="bds_t163"></a><a class="bds_tsohu"></a><a class="bds_tqf"></a><a class="bds_sqq">
                </a><a class="bds_hi"></a><a class="bds_mogujie"></a><a class="bds_meilishuo"></a>
                <span class="bds_more"></span>
            </div>
            <script type="text/javascript" id="bdshare_js" data="type=tools&amp;uid=6816366"></script>
            <script type="text/javascript" id="bdshell_js"></script>
            <script type="text/javascript">
                document.getElementById("bdshell_js").src = "http://bdimg.share.baidu.com/static/js/shell_v2.js?cdnversion=" + Math.ceil(new Date() / 3600000)
            </script>
            <!-- Baidu Button END -->
            <div style="clear: both;">
            </div>
        </div>
        <div>
            <!--高速版，加载速度快，使用前需测试页面的兼容性-->
            <div id="SOHUCS">
            </div>
            <script type="text/javascript">
                (function () {
                    var appid = 'cyqV8ohh0',
    conf = 'prod_1b388c8b7c863fde3f559142fdc123b0';
                    var doc = document,
    s = doc.createElement('script'),
    h = doc.getElementsByTagName('head')[0] || doc.head || doc.documentElement;
                    s.type = 'text/javascript';
                    s.charset = 'utf-8';
                    s.src = 'http://assets.changyan.sohu.com/upload/changyan.js?conf=' + conf + '&appid=' + appid;
                    h.insertBefore(s, h.firstChild);
                    window.SCS_NO_IFRAME = true;
                })()
            </script>
        </div>
        <div class="article_related">
            <uc1:RelatedArticleList ID="RelatedArticleList1" runat="server" />
        </div>
    </div>
</asp:Content>
