<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Account.Master" AutoEventWireup="true"
    CodeBehind="ArticleEdit.aspx.cs" Inherits="TopSite.Account.ArticleEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../editor/ckeditor.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        网站基本信息
    </h2>
    <div class="accountInfo">
        <fieldset class="register">
            <legend>站点信息</legend>
            <p>
                <asp:Label ID="txtTitleLabel" runat="server" AssociatedControlID="txtTitle">文章标题:</asp:Label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="txtTitleRequired" runat="server" ControlToValidate="txtTitle"
                    CssClass="failureNotification" ErrorMessage="必须填写“文章标题”。" ToolTip="文章标题" ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="KeyWords">关键词:</asp:Label>
                <asp:TextBox ID="KeyWords" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="KeyWords"
                    CssClass="failureNotification" ErrorMessage="必须填写“关键词”。" ToolTip="必须填写“关键词”。"
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="SummaryLabel" runat="server" AssociatedControlID="Summary">文章摘要:</asp:Label>
                <asp:TextBox ID="Summary" runat="server" CssClass="textEntry" TextMode="MultiLine"
                    Rows="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="SummaryRequired" runat="server" ControlToValidate="Summary"
                    CssClass="failureNotification" ErrorMessage="文章摘要" ToolTip="必须填写“文章摘要”。" ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="ContentLabel" runat="server" AssociatedControlID="txtContent">文章内容:</asp:Label>
                <asp:TextBox ID="txtContent" runat="server" CssClass="ckeditor" TextMode="MultiLine"
                    Rows="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ContentRequired" runat="server" ControlToValidate="txtContent"
                    CssClass="failureNotification" ErrorMessage="必须填写“文章内容”。" ToolTip="文章内容" ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="btnSaveArticle" runat="server" CommandName="MoveNext" Text="保存文章"
                ValidationGroup="RegisterUserValidationGroup" 
                OnClick="btnSaveArticle_Click" />
        </p>
    </div>
</asp:Content>
