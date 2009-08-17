<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubversionSettings.ascx.cs" Inherits="BugNET.Administration.Host.UserControls.SubversionSettings" %>
<h4>Subversion Settings</h4>
<BN:Message ID="Message1" runat="server" visible="false"  /> 
<table class="form">
    <tr>
        <th><asp:Label ID="label29" runat="server" AssociatedControlID="EnableRepoCreation" 
                Text="Enable Administration:" /></th>
        <td class="input-group"><asp:checkbox cssClass="checkboxlist" 
                id="EnableRepoCreation" runat="server" ></asp:checkbox></td>
    </tr>
     <tr>
        <th><asp:Label ID="label32" runat="server" AssociatedControlID="RepoRootUrl" Text="Server Root Url:" /></th>
        <td><asp:TextBox id="RepoRootUrl" Runat="Server" /></td>
    </tr>
     <tr>
        <th><asp:Label ID="label30" runat="server" AssociatedControlID="RepoRootPath" Text="Root Folder:" /></th>
        <td><asp:TextBox id="RepoRootPath" Runat="Server" /></td>
    </tr>
    <tr>
        <th><asp:Label ID="label34" runat="server" AssociatedControlID="SvnHookPath"  Text="Hooks Executable File:" /></th>
        <td><asp:TextBox id="SvnHookPath" Runat="Server" /></td>
    </tr>
     
    <tr>
        <th><asp:Label ID="label31" runat="server" AssociatedControlID="RepoBackupPath" Text="Backup Folder:" /></th>
        <td><asp:TextBox id="RepoBackupPath" Runat="Server" /></td>
    </tr>
     <tr>
        <th><asp:Label ID="label33" runat="server" AssociatedControlID="SvnAdminEmailAddress" Text="Administrator Email:" /></th>
        <td><asp:TextBox id="SvnAdminEmailAddress" Runat="Server" /></td>
    </tr>
</table>