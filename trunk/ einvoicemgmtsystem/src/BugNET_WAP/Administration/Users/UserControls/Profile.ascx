<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Profile.ascx.cs" Inherits="BugNET.Administration.Users.UserControls.Profile" %>
<asp:Label ID="lblError" runat="server"  ForeColor="Red" />
<table class="form">
    <tr>
        <td colspan="4"><h4>Edit Profile - <asp:Label id="lblUserName" runat="server"/></h4></td> 
   </tr>
   <tr>
        <th><asp:Label ID="Label1" AssociatedControlID="FirstName" runat="server" Text="First Name:" /></th>
        <td> <asp:TextBox ID="FirstName" runat="server" /></td>    
   </tr>
    <tr>
        <th><asp:Label ID="Label3" AssociatedControlID="LastName" runat="server" Text="Last Name:" /></th>
        <td><asp:TextBox ID="LastName" runat="server" /></td>
    </tr>
     <tr>
        <th><asp:Label ID="Label5" AssociatedControlID="DisplayName" runat="server" Text="Display Name:" /></th>
        <td><asp:TextBox ID="DisplayName" runat="server" /></td>
    </tr>
   <!--  
       <tr>
            <th><strong>Contact Info</strong></th>
         </tr>
          <tr>
            <th><asp:Label ID="Label2" AssociatedControlID="Telephone" runat="server" Text="Telephone:" /></th>
            <td><asp:TextBox ID="Telephone" runat="server" /></td>
        </tr>
        <tr>
            <th><asp:Label ID="Label6" AssociatedControlID="Mobile" runat="server" Text="Mobile:" /></th>
            <td><asp:TextBox ID="Mobile" runat="server" /></td>
        </tr>
        <tr>
            <th><asp:Label ID="Label4" AssociatedControlID="Fax" runat="server" Text="Fax:" /></th>
            <td><asp:TextBox ID="Fax" runat="server" /></td>
        </tr> 
    -->
</table>
<p align="center" style="margin-top:10px;">
    <asp:ImageButton OnClick="cmdUpdate_Click" runat="server" id="save" CssClass="icon" ImageUrl="~/Images/disk.gif" />
    <asp:LinkButton ID="cmdUpdate" OnClick="cmdUpdate_Click" runat="server">Update</asp:LinkButton>
</p>