<%@ Page Title="编辑文章" Language="C#" MasterPageFile="~/Masters/Account.Master" AutoEventWireup="true" validateRequest="false"
    CodeBehind="ArticleEdit.aspx.cs" Inherits="TopSite.Account.ArticleEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        p
        {
            float: left;
            margin-right: 10px;
        }
    </style>
    <script src="../editor/ckeditor.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--    <h2>
        文章编辑
    </h2>--%>
    <div>
        <fieldset class="register">
            <legend>文章编辑</legend>
            <p>
                <asp:Label ID="txtTitleLabel" runat="server" AssociatedControlID="txtTitle">文章标题:</asp:Label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="txtTitleRequired" runat="server" ControlToValidate="txtTitle"
                    CssClass="failureNotification" ErrorMessage="必须填写“文章标题”。" ToolTip="文章标题" ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="CatalogueLabel" runat="server" AssociatedControlID="DropDownListCatalogue">栏目:</asp:Label>
                <asp:DropDownList ID="DropDownListCatalogue" runat="server" DataTextField="Title" DataValueField="Id"
                    CssClass="textEntry">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="CatalogueRequired" runat="server" ControlToValidate="DropDownListCatalogue"
                    CssClass="failureNotification" ErrorMessage="必须填写“栏目”。" ToolTip="必须填写“栏目”。" ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <div class="clear">
            </div>
            <p>
                <asp:Label ID="OrignSourceLabel" runat="server" AssociatedControlID="OrignSource">文章来源:</asp:Label>
                <asp:TextBox ID="OrignSource" runat="server" CssClass="textEntry"></asp:TextBox>
                <span class="failureNotification" style="visibility: hidden">*</span>
            </p>
            <p>
                <asp:Label ID="KeyWordsLabel" runat="server" AssociatedControlID="KeyWords">关键词:</asp:Label>
                <asp:TextBox ID="KeyWords" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="KeyWordsRequired" runat="server" ControlToValidate="KeyWords"
                    CssClass="failureNotification" ErrorMessage="必须填写“关键词”。" ToolTip="必须填写“关键词”。"
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <div class="clear">
            </div>
            <p>
                <asp:Label ID="SummaryLabel" runat="server" AssociatedControlID="Summary">文章摘要:</asp:Label>
                <asp:TextBox ID="Summary" runat="server" CssClass="textEntry" TextMode="MultiLine"
                    Rows="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="SummaryRequired" runat="server" ControlToValidate="Summary"
                    CssClass="failureNotification" ErrorMessage="文章摘要" ToolTip="必须填写“文章摘要”。" ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <div class="clear">
            </div>
            <p style="float: none;">
                <asp:Label ID="ContentLabel" runat="server" AssociatedControlID="txtContent">文章内容:</asp:Label>
                <asp:TextBox ID="txtContent" runat="server" CssClass="ckeditor" TextMode="MultiLine"
                    Rows="10"></asp:TextBox>                
            </p>
            <div class="clear">
            </div>
        </fieldset>
        <p class="submitButton" style="float: none">
            <asp:Button ID="btnSaveArticle" runat="server" CommandName="MoveNext" Text="保存文章"
                ValidationGroup="RegisterUserValidationGroup" OnClick="btnSaveArticle_Click" />
        </p>
    </div>
</asp:Content>
