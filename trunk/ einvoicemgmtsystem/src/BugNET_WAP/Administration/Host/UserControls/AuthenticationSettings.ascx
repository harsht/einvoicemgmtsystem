<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AuthenticationSettings.ascx.cs" Inherits="BugNET.Administration.Host.UserControls.AuthenticationSettings" %>
 <h4>Authentication Settings</h4>
 <bn:Message ID="Message1" runat="server" visible="false"  /> 
 <table class="form">
    <tr>
        <th><asp:Label ID="label3" runat="server" AssociatedControlID="UserAccountSource" Text="User Account Source:" /></th>
        <td class="input-group"><asp:radiobuttonlist RepeatDirection="Horizontal" cssClass="checkboxlist" id="UserAccountSource" runat="server">
            <asp:listitem id="option1" runat="server" value="WindowsSAM" />
            <asp:listitem id="option2" runat="server" value="ActiveDirectory" />
            <asp:listitem id="option3" runat="server" value="None" />
        </asp:radiobuttonlist></td>
    </tr>
     <tr>
        <th><asp:Label ID="label25" runat="server" AssociatedControlID="ADPath"  Text="Domain / Path:" /></th>
        <td> <asp:TextBox id="ADPath"  Runat="Server" /></td>
    </tr>
     <tr>
        <th><asp:Label ID="label4" runat="server" AssociatedControlID="ADUserName"  Text="Username:" /></th>
        <td><asp:TextBox id="ADUserName"  Runat="Server" /></td>
    </tr>
     <tr>
        <th><asp:Label ID="label5" runat="server" AssociatedControlID="ADPassword" Text="Password:" /></th>
        <td><asp:TextBox TextMode="Password" id="ADPassword"  Runat="Server" /></td>
    </tr>
     <tr>
        <th><asp:Label ID="label6" runat="server" AssociatedControlID="ADConfirmPassword" Text="Confirm Password:" /></th>
        <td><asp:TextBox TextMode="Password" id="ADConfirmPassword"  Runat="Server" />
            <asp:CompareValidator id="compval" Display="dynamic" ControlToValidate="ADPassword"
                ControlToCompare="ADConfirmPassword" ForeColor="red" Type="String" EnableClientScript="true" 
                Text=" Passwords must match" runat="server" />	
        </td>
    </tr>
     <tr>
        <th><asp:Label ID="label7" runat="server" AssociatedControlID="DisableAnonymousAccess" Text="Disable Anonymous Access:" /></th>
        <td class="input-group"><asp:CheckBox  id="DisableAnonymousAccess"  Runat="Server" /></td>
    </tr>
     <tr>
        <th><asp:Label ID="label8" runat="server" AssociatedControlID="DisableUserRegistration" Text="Disable User Registration:" /></th>
        <td class="input-group"><asp:CheckBox id="DisableUserRegistration"  Runat="Server" /></td>
    </tr> 
</table>