<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NotificationSettings.ascx.cs" Inherits="BugNET.Administration.Host.UserControls.NotificationSettings" %>

<h4>Notification Settings</h4>
 <BN:Message ID="Message1" runat="server" visible="false"  /> 
 <table>
    <tr>
        <td>Admin Notification User:</td>
        <td>        
            <asp:DropDownList ID="AdminNotificationUser" runat="server" />
        </td>
    </tr>
    <tr>
        <td><asp:Label ID="label9" runat="server" AssociatedControlID="cblNotificationTypes" Text="Enabled Notification Types:" /></td>
    </tr>
    <tr>
        <td> <asp:CheckBoxList ID="cblNotificationTypes"  RepeatColumns="4"  RepeatDirection="Horizontal" runat="server"> </asp:CheckBoxList></td>
    </tr>
</table>