<%@ Page Title="站点配置" Language="C#" MasterPageFile="~/Masters/Account.Master" AutoEventWireup="true" validateRequest="false"
    CodeBehind="SiteConfig.aspx.cs" Inherits="TopSite.Account.SiteConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        网站基本信息
    </h2>
    <div class="accountInfo">
        <fieldset class="register">
            <legend>站点信息</legend>
            <p>
                <asp:Label ID="SiteNameLabel" runat="server" AssociatedControlID="SiteName">站点名称:</asp:Label>
                <asp:TextBox ID="SiteName" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="SiteNameRequired" runat="server" ControlToValidate="SiteName"
                    CssClass="failureNotification" ErrorMessage="必须填写“站点名称”。" ToolTip="站点名称" ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="KeyWords">关键词:</asp:Label>
                <asp:TextBox ID="KeyWords" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="KeyWords"
                    CssClass="failureNotification" ErrorMessage="必须填写“关键词”。" ToolTip="必须填写“关键词”。"
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="LabelTopKeywords" runat="server" AssociatedControlID="TopKeywords">淘宝客关键词:</asp:Label>
                <asp:TextBox ID="TopKeywords" runat="server" CssClass="textEntry"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="SummaryLabel" runat="server" AssociatedControlID="Summary">站点描述:</asp:Label>
                <asp:TextBox ID="Summary" runat="server" CssClass="textEntry" TextMode="MultiLine"
                    Rows="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="SummaryRequired" runat="server" ControlToValidate="Summary"
                    CssClass="failureNotification" ErrorMessage="必须填写“站点描述”。" ToolTip="必须填写“站点描述”。"
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
                        <p>
                <asp:Label ID="LabelCopyRight" runat="server" AssociatedControlID="CopyRight">版权信息:</asp:Label>
                <asp:TextBox ID="CopyRight" runat="server" CssClass="textEntry" TextMode="MultiLine"
                    Rows="10"></asp:TextBox>
            </p>
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="btnSaveSiteConfig" runat="server" CommandName="MoveNext" Text="保存设置"
                ValidationGroup="RegisterUserValidationGroup" OnClick="SaveSiteConfig_Click" />
        </p>
    </div>
</asp:Content>
