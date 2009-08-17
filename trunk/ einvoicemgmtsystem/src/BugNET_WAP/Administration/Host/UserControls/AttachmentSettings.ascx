<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentSettings.ascx.cs" Inherits="BugNET.Administration.Host.UserControls.AttachmentSettings" %>
<h4>Attachment Settings</h4>
<bn:Message ID="Message1" runat="server" visible="false"  /> 
<table class="form">
     <tr>
        <th><asp:Label ID="label25" runat="server" AssociatedControlID="AllowedFileExtentions"  Text="Allowed File Extensions:" /></th>
        <td> <asp:TextBox id="AllowedFileExtentions"  Runat="Server" /> (seperated by semi colon)</td>
    </tr>
    <tr>
        <th><asp:Label ID="label1" runat="server" AssociatedControlID="FileSizeLimit"  Text="File Size Limit:" /></th>
        <td> <asp:TextBox id="FileSizeLimit"  Runat="Server" /> (bytes)</td>
    </tr>
</table>