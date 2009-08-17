<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectNotifications.ascx.cs" Inherits="BugNET.Administration.Projects.UserControls.ProjectNotifications" %>
<h4>Notifications</h4>
<p>
    You can add project members that should receive notifications for the entire project.  This includes new comments and updated issues.
</p>
 <BN:Message ID="Message" runat="server" /> 
  <table>
    <tr>
        <td style="font-weight:bold">All Users</td>
        <td>&nbsp;</td>
        <td style="font-weight:bold">Selected Users</td>
    </tr>
    <tr>
        <td style="height: 108px">
	        <asp:ListBox id="lstAllUsers" SelectionMode="Multiple" Runat="Server" Width="150" Height="110px" />
        </td>
        <td style="height: 108px">
	        <asp:Button Text="->"  CssClass="button" style="FONT:9pt Courier" Runat="server" id="Button1" onclick="AddUser" />
	        <br />
	        <asp:Button Text="<-"  CssClass="button" style="FONT:9pt Courier;clear:both;" Runat="server" id="Button2" onclick="RemoveUser" />
        </td>
        <td style="height: 108px">
	        <asp:ListBox id="lstSelectedUsers" SelectionMode="Multiple"  Runat="Server" Width="150" Height="110px" />
        </td>
    </tr>
</table>