<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Message.ascx.cs" Inherits="BugNET.UserControls.Message" %>
<asp:Panel ID="MessageContainer" Width="100%" runat="server">
    <asp:image ID="MessageIcon" runat="server" CssClass="icon" />
    <asp:Label ID="lblMessage" runat="server" />
    <asp:ImageButton ID="HideButton" style="float:right;" runat="server" ImageUrl="~/images/close.gif"  CssClass="icon" OnClick="HideButton_Click" />
</asp:Panel>
