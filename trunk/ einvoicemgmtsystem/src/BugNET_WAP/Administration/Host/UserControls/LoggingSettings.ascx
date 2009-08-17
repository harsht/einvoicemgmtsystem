<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoggingSettings.ascx.cs" Inherits="BugNET.Administration.Host.UserControls.LoggingSettings" %>
<h4>Logging Settings</h4>
<BN:Message ID="Message1" runat="server" visible="false"  /> 
<table class="form">
    <tr>
        <th><asp:Label ID="label23" runat="server" AssociatedControlID="EmailErrors" Text="Email Error Messages:" /></th>
        <td class="input-group"><asp:checkbox cssClass="checkboxlist" id="EmailErrors" runat="server"></asp:checkbox></td>
    </tr>
     <tr>
        <th><asp:Label ID="label24" runat="server" AssociatedControlID="ErrorLoggingEmail" CssClass="col1b" Text="From Address:" /></th>
        <td><asp:TextBox id="ErrorLoggingEmail" Runat="Server" /></td>
    </tr>
</table>