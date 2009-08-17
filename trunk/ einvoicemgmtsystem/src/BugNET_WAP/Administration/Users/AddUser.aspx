<%@ Page Language="C#" MasterPageFile="~/Shared/FullWidth.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Title="Add User" Inherits="BugNET.Administration.Users.AddUser" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="server">
    <table class="form">
        <tr><td colspan="2"><bn:Message ID="Message1" runat="server" visible="false"  /> </td></tr>
        <tr><td colspan="2" style="padding-bottom:5px;">Please enter the details for the new user.</td></tr>
        <tr>
            <td><asp:Label ID="Label2" AssociatedControlID="UserName" runat="server" Text="User Name:" /></td> 
            <td>
                <asp:TextBox ID="UserName" runat="server" />
               <asp:RequiredFieldValidator ID="rfvUserName"
                    runat="server" ErrorMessage="*" Display="Dynamic" ControlToValidate="UserName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label1" AssociatedControlID="FirstName" runat="server" Text="First Name:" /></td>
            <td> <asp:TextBox ID="FirstName" runat="server" /> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" 
                ControlToValidate="FirstName" Display="Dynamic"></asp:RequiredFieldValidator></td>
            <td>
        </tr>
          <tr>
            <td><asp:Label ID="Label3" CssClass="col1" AssociatedControlID="LastName" runat="server" Text="Last Name:" /></td>
            <td><asp:TextBox ID="LastName" runat="server" /> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" 
                ControlToValidate="LastName" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <th><asp:Label ID="Label5" CssClass="col1" AssociatedControlID="DisplayName" runat="server" Text="Display Name:" /></th>
            <td><asp:TextBox ID="DisplayName" runat="server"></asp:TextBox> <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" 
                ControlToValidate="DisplayName" Display="Dynamic"></asp:RequiredFieldValidator></td> 
        </tr>
        <tr>
            <td><asp:Label ID="Label40" CssClass="col1" AssociatedControlID="Email" runat="server" Text="Email:" /></td>
            <td><asp:TextBox ID="Email" runat="server" /> <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" 
                ControlToValidate="Email" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator></td> 
        </tr>
          <tr id="SecretQuestionRow" runat="server" visible="false">
                <td width="2"></td>
                <td>
                    <asp:Label ID="Label4" runat="server" AssociatedControlID="SecretQuestion" Text="Secret Question: "/>
                </td>
                <td>
                    <asp:textbox runat="server" id="SecretQuestion" maxlength="128" tabindex="104" Columns="30" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="SecretQuestion" Display="Dynamic" EnableClientScript="true">required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="SecretAnswerRow" runat="server" visible="false">
                <td width="2"></td>
                <td>
                    <asp:Label ID="Label6" runat="server" AssociatedControlID="SecretAnswer" Text="Secret Answer: "/>
                </td>
                <td>
                    <asp:textbox runat="server" id="SecretAnswer" maxlength="128" tabindex="105" Columns="30" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="SecretAnswer" Display="Dynamic" EnableClientScript="true">required</asp:RequiredFieldValidator>
                </td>
            </tr> 
            <tr>
                <td style="padding:10px 0 3px 0px;"><strong>Password</strong></td>
            </tr>
            <tr>
                <td colspan="2" style="white-space:normal;padding-bottom:5px;">
                    Optionally enter a password for this user, or allow the system to generate a random password.
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="Label10" AssociatedControlID="chkRandomPassword" runat="server" Text="Random Password:" /></td>
                <td class="input-group"><asp:CheckBox ID="chkRandomPassword" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label42" AssociatedControlID="Password" runat="server" Text="Password:" /></td>
                <td><asp:TextBox ID="Password" TextMode="password"  runat="server" />
                <asp:RequiredFieldValidator ID="rvPassword" runat="server" ErrorMessage="*" EnableClientScript="true"  Enabled="false"
                    ControlToValidate="Password" Display="Dynamic"></asp:RequiredFieldValidator>
                </td> 
            </tr>
            <tr>
                <td><asp:Label ID="Label41" AssociatedControlID="ConfirmPassword" runat="server" Text="Confirm Password:" /></td>
                <td><asp:TextBox ID="ConfirmPassword" TextMode="password" runat="server" />
                <asp:RequiredFieldValidator ID="rvConfirmPassword" runat="server" ErrorMessage="*" EnableClientScript="true" Enabled="false"
                    ControlToValidate="ConfirmPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>          
            </tr>         
            <tr>
                <td width="2"></td>
                <td class="input-group">
                    <asp:checkbox runat="server" id="ActiveUser" text="Active User" TabIndex="106" Checked="True"/>
                </td>
            </tr>   
            <tr>
                <td colspan="2" style="white-space:normal;padding:10px 0 10px 15px;">
                    <asp:CompareValidator ID="cvPassword" EnableClientScript="false"  Enabled="false"  Display="dynamic" ControlToCompare="ConfirmPassword" ControlToValidate="Password" runat="server" ErrorMessage="The password specified is invalid. Please specify a valid password. Passwords must be at least 7 characters in length and contain at least 0 non-alphanumeric characters."></asp:CompareValidator></td>
                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
            </tr>
        </asp:Panel>
    </table>
<p>
   <asp:ImageButton runat="server"  ImageUrl="~/Images/disk.gif" CssClass="icon"  AlternateText="Add New User" OnClick="AddNewUser_Click" ID="ImageButton2" />
    <asp:LinkButton ID="LinkButton2" runat="server" Text="Add New User" OnClick="AddNewUser_Click" />
    &nbsp;
    <asp:ImageButton runat="server"  ImageUrl="~/Images/lt.gif" CssClass="icon" CausesValidation="false"  AlternateText="Back to user list" ID="ImageButton3" OnClick="cmdCancel_Click" />
    <asp:HyperLink Id="ReturnLink" runat="server" NavigateUrl="~/Administration/Users/UserList.aspx" Text="Back to user list"></asp:HyperLink>
</p> 
</asp:Content>
