<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Membership.ascx.cs" Inherits="BugNET.Administration.Users.UserControls.Membership" %>
<asp:Label ID="lblError" runat="server"  ForeColor="Red" />
<table class="form">
    <tr>
        <td colspan="4"><h4>Edit User - <asp:Label id="lblUserName" runat="server"/></h4></td> 
   </tr>
    <tr>
        <th><asp:Label ID="Label2" AssociatedControlID="UserName" runat="server" Text="User Name:" /></th> 
        <td><asp:TextBox ID="UserName" runat="server" /></td>
         <th><asp:Label ID="Label6" runat="server" Text="Created Date:" /></th>
        <td><asp:Label ID="CreatedDate" runat="server" Text="" /></td>
    </tr>
    <tr>
        <th><asp:Label ID="Label1" AssociatedControlID="FirstName" runat="server" Text="First Name:" /></th>
        <td> <asp:TextBox ID="FirstName" runat="server" /></td>
         <th><asp:Label ID="Label7" runat="server" Text="Last Login Date:" /></th>
        <td><asp:Label ID="LastLoginDate" runat="server" CssClass="col4" Text="" /></td>
    </tr>
      <tr>
        <th><asp:Label ID="Label3" CssClass="col1" AssociatedControlID="LastName" runat="server" Text="Last Name:" /></th>
        <td><asp:TextBox ID="LastName" runat="server" /></td>
         <th><asp:Label ID="Label8" CssClass="col3 label" runat="server" Text="Last Activity Date:" /></th>
        <td><asp:Label ID="LastActivityDate" runat="server" CssClass="col4" Text="" /></td>
    </tr>
    <tr>
        <th><asp:Label ID="Label5" CssClass="col1" AssociatedControlID="DisplayName" runat="server" Text="Display Name:" /></th>
        <td><asp:TextBox ID="DisplayName" runat="server" /></td>
        <th><asp:Label ID="Label9" AssociatedControlID="LockedOut" CssClass="col3 label" runat="server" Text="Locked Out:" /></th>
        <td class="input-group"><asp:CheckBox ID="LockedOut" Enabled="false" runat="server" /></td>
    </tr>
    <tr>
        <th><asp:Label ID="Label4" CssClass="col1" AssociatedControlID="Email" runat="server" Text="Email:" /></th>
        <td><asp:TextBox ID="Email" runat="server" /></td>
        <th><asp:Label ID="Label10" CssClass="col3" AssociatedControlID="Authorized" runat="server" Text="Authorized:" /></th>
        <td class="input-group"><asp:CheckBox ID="Authorized" Enabled="false" runat="server" /></td>
    </tr>
    <tr>
        <th></th>
        <td></td>
        <th><asp:Label ID="Label12" CssClass="col3" AssociatedControlID="Online" runat="server" Text="User Is Online:" /></th>
        <td class="input-group"><asp:CheckBox ID="Online" Enabled="false" runat="server" /></td>
    </tr>
</table>
<p align="center" style="margin-top:10px;">
    <asp:ImageButton OnClick="cmdUpdate_Click" runat="server" id="save" CssClass="icon" AlternateText="Update" ImageUrl="~/Images/disk.gif" />
    <asp:LinkButton ID="cmdUpdate" OnClick="cmdUpdate_Click" runat="server">Update</asp:LinkButton>
    &nbsp;&nbsp;
    <asp:ImageButton runat="server" id="ibAuthorize" OnClick="AuthorizeUser_Click" CssClass="icon" causesvalidation="False" ImageUrl="~/Images/shield.gif" />
	<asp:linkbutton id="cmdAuthorize" runat="server" OnClick="AuthorizeUser_Click" Text="Authorize User" causesvalidation="False" />
    &nbsp;&nbsp;
    <asp:ImageButton runat="server" id="ibUnAuthorize"  OnClick="UnAuthorizeUser_Click" CssClass="icon" ImageUrl="~/Images/shield.gif" />
	<asp:linkbutton id="cmdUnAuthorize" causesvalidation="False" OnClick="UnAuthorizeUser_Click" runat="server" Text="UnAuthorize User"></asp:linkbutton>	 
	&nbsp;&nbsp;
	<asp:ImageButton runat="server" id="ibUnLock"  OnClick="UnLockUser_Click" causesvalidation="False" CssClass="icon" ImageUrl="~/Images/shield.gif" />
	<asp:linkbutton id="cmdUnLock" runat="server" OnClick="UnLockUser_Click" causesvalidation="False" Text="Unlock User" />
</p>
