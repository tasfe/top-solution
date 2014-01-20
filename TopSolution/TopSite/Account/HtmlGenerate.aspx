<%@ Page Title="生成Html页面" Language="C#" MasterPageFile="~/Masters/Account.Master"
    AutoEventWireup="true" CodeBehind="HtmlGenerate.aspx.cs" Inherits="TopSite.Account.HtmlGenerate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
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
    <script type="text/javascript">
        $(
        function () {
            generateHtml("btnCreateHomepage", "0");
            generateHtml("btnCreateAllArticlesPage", "1");
        }
        );

        // 显示为忙碌
        function showBusy() {
            $("#msg").html("<img src='/images/loading.gif'/>");
        }

        function generateHtml(btn, type) {
            var $btn = $("#" + btn);
            $btn.click(
            function () {
                showBusy();
                $.get("HtmlGenerater.ashx?type=" + type + "&t=" + new Date().toString(), null, function (data) {
                    $("#msg").html(data);
                }, "text");
            }
            );
        }
    </script>
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
    <div id="msg">
    </div>
</asp:Content>
