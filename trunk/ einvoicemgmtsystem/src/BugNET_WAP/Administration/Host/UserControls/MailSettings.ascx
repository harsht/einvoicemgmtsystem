<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MailSettings.ascx.cs" Inherits="BugNET.Administration.Host.UserControls.MailSettings" %>
 <h4>Mail / SMTP Server Settings</h4>
 <BN:Message ID="Message1" runat="server" visible="false"  /> 
 <table class="form">
    <tr>
        <th><asp:Label ID="label9" runat="server" AssociatedControlID="SMTPServer" Text="Server:" /></th>
        <td><asp:TextBox id="SMTPServer" Runat="Server" /></td>
    </tr>
    <tr>
        <th><asp:Label ID="label22" runat="server" AssociatedControlID="HostEmail" Text="Host Email:" /></th>
        <td><asp:TextBox id="HostEmail" Runat="Server" /></td>
    </tr>
    <tr>
        <th><asp:Label ID="label10" runat="server" AssociatedControlID="SMTPEnableAuthentication" Text="Enable Authentication:" /></th>
        <td class="input-group"> <asp:checkbox id="SMTPEnableAuthentication" runat="server"></asp:checkbox></td>
    </tr>
    <tr>
        <th><asp:Label ID="label11" runat="server" AssociatedControlID="SMTPUsername" Text="Username:" /></th>
        <td><asp:TextBox id="SMTPUsername" Runat="Server" /></td>
    </tr>
    <tr>
        <th><asp:Label ID="label12" runat="server" AssociatedControlID="SMTPPassword" Text="Password:" /></th>
        <td><asp:TextBox id="SMTPPassword" TextMode="Password"  Runat="Server" /></td>
    </tr>
    <tr>
        <th><asp:Label ID="label26" runat="server" AssociatedControlID="SMTPPort" CssClass="col1b" Text="Port:" /></th>
        <td><asp:TextBox id="SMTPPort" Width="100px" Runat="Server" /></td>
    </tr>
    <tr>
        <th><asp:Label ID="label27" runat="server" AssociatedControlID="SMTPUseSSL" CssClass="col1b" Text="SSL:" /></th>
        <td class="input-group"><asp:CheckBox ID="SMTPUseSSL" runat="server" /></td>
    </tr>
</table>