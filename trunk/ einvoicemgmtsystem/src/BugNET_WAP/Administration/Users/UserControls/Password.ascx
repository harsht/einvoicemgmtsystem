<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Password..ascx.cs" Inherits="BugNET.Administration.Users.UserControls.Password" %>
<asp:Label ID="lblError" runat="server"  ForeColor="Red" />
<div class="row">
    <asp:Label ID="Label16" CssClass="FieldName"  Font-Bold="True" Width="180px" runat="server" Text="Password Last Changed:"></asp:Label>
    <asp:Label ID="PasswordLastChanged" runat="server" CssClass="FieldValue" Text=""></asp:Label>
</div>
<br />
<span style="font-weight:bold;margin-top:5px;margin-bottom:10px;">Change Password</span>
<br />
<br />
<asp:Label ID="lblMessage" ForeColor="Red" runat="server" Visible="false">Your password has been changed successfully.</asp:Label>
<br />
<table class="form" style="margin-left:100px;width:300px;">
    <tr>
        <th><asp:Label ID="Label11" CssClass="col1" AssociatedControlID="NewPassword" runat="server" Text="New Password:" /></th>
        <td class="field"><asp:TextBox ID="NewPassword" runat="server" TextMode="Password" /></td>
    </tr>
    <tr>
        <th><asp:Label ID="Label12" CssClass="col1" AssociatedControlID="ConfirmPassword" runat="server" Text="Confirm Password:" /></th>
        <td class="field"><asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" /><asp:CompareValidator ID="cvPasswords" runat="server" ControlToValidate="NewPassword" ControlToCompare="ConfirmPassword" Type="String" ErrorMessage="Passwords must match."></asp:CompareValidator></td>
    </tr>
    <tr>
        <td colspan="2">  <asp:LinkButton ID="cmdChangePassword"  OnClick="cmdChangePassword_Click" runat="server">Change Password</asp:LinkButton></td>
    </tr>
</table>

<br />
<br />
<span style="font-weight:bold;">Reset Password</span>
<p class="desc">
    <asp:Label ID="Label13" runat="server" Text="You can reset the password for this user. The password will be randomly generated."></asp:Label>
 </p>
<p align="center" style="margin-top:10px;">
    <asp:ImageButton runat="server" id="Image7" OnClick="cmdResetPassword_Click" CssClass="icon" ImageUrl="~/Images/key_go.gif" />
    <asp:LinkButton ID="cmdResetPassword" OnClick="cmdResetPassword_Click" runat="server" Text="Reset Password" />
</p>