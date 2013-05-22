<%@ Page Title="备份数据库" Language="C#" MasterPageFile="~/Masters/Account.Master" AutoEventWireup="true"
    CodeBehind="DbBackUp.aspx.cs" Inherits="TopSite.Account.DbBackUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(
        function () {
            var $btn = $("#btnBackup");
            $btn.click(
            function () {
                showBusy();
                $.get("DbBackUp.aspx?action=doback&t=" + new Date().toString(), null, function (data) {
                    $("#msg").html(data);
                }, "text");
            }
            );
        }
        );

        function showBusy() {
            $("#msg").html("<img src='/images/loading.gif'/>");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="msg">
    </div>
    <div>
        <input id="btnBackup" type="button" value="备份数据库" /></div>
</asp:Content>
