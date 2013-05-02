<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Account.Master" AutoEventWireup="true"
    CodeBehind="CatalogueList.aspx.cs" Inherits="TopSite.Account.CatalogueList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        文章分类
    </h2>
    <div>
        <asp:GridView ID="GridViewCatalogue" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="序号" />
                <asp:BoundField DataField="Title" HeaderText="标题" />
                <asp:BoundField DataField="Order" HeaderText="排序" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Eval("Id") %>'
                            OnClick="lbtnEdit_Click">编辑</asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="lbtnDel" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lbtnDel_Click"
                            OnClientClick="return confirm(&quot;确认要删除吗？&quot;);">删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="accountInfo">
        <fieldset class="register">
            <legend>分类信息</legend>
            <asp:HiddenField ID="txtId" runat="server" />
            <p>
                <asp:Label ID="TitleLabel" runat="server" AssociatedControlID="txtTitle">分类名称:</asp:Label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="TitleRequired" runat="server" ControlToValidate="SiteName"
                    CssClass="failureNotification" ErrorMessage="必须填写“分类名称”。" ToolTip="分类名称" ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="Label1" runat="server" AssociatedControlID="KeyWords">排序:</asp:Label>
                <asp:TextBox ID="Order" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="OrderRequired" runat="server" ControlToValidate="Order"
                    CssClass="failureNotification" ErrorMessage="必须填写“排序”。" ToolTip="必须填写“排序”。" ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                <asp:RangeValidator ID="OrderRangeValidator" runat="server" ControlToValidate="Order"
                    CssClass="failureNotification" ErrorMessage="*" ForeColor="Red" Type="Integer"></asp:RangeValidator>
            </p>
            <p>
                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="KeyWords">关键词:</asp:Label>
                <asp:TextBox ID="KeyWords" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="KeyWords"
                    CssClass="failureNotification" ErrorMessage="必须填写“关键词”。" ToolTip="必须填写“关键词”。"
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="SummaryLabel" runat="server" AssociatedControlID="Summary">分类描述:</asp:Label>
                <asp:TextBox ID="Summary" runat="server" CssClass="textEntry" TextMode="MultiLine"
                    Rows="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="SummaryRequired" runat="server" ControlToValidate="Summary"
                    CssClass="failureNotification" ErrorMessage="必须填写“站点描述”。" ToolTip="必须填写“站点描述”。"
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="btnSaveCatalogue" runat="server" CommandName="MoveNext" Text="保存分类"
                ValidationGroup="RegisterUserValidationGroup" OnClick="btnSaveCatalogue_Click" />
        </p>
    </div>
</asp:Content>
