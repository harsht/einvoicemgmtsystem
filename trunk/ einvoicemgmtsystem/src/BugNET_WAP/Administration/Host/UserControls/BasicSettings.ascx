<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BasicSettings.ascx.cs" Inherits="BugNET.Administration.Host.UserControls.BasicSettings" %>
 <h4>Basic Settings</h4>
 <bn:Message ID="Message1" runat="server" visible="false"  /> 
 <table class="form">
     <tr>
        <th><asp:Label ID="label28" runat="server" AssociatedControlID="ApplicationTitle" Text="Title:" /></th> 
        <td><asp:TextBox ID="ApplicationTitle" runat="Server" MaxLength="500" ></asp:TextBox></td>
    </tr>
    <tr>
        <th style="vertical-align:top"><asp:Label ID="label1" runat="server" AssociatedControlID="WelcomeMessageHtmlEditor" Text="Welcome Message:" /></th> 
        <td><bn:HtmlEditor id="WelcomeMessageHtmlEditor" Height="200" runat="server" /></td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
        <th><asp:Label ID="label2" runat="server" AssociatedControlID="DefaultUrl"  Text="Default Url:" /></th>
        <td><asp:TextBox id="DefaultUrl" Runat="Server" /></td>
    </tr>
</table>