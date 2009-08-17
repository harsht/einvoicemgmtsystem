<%@ Page Language="C#" MasterPageFile="~/Shared/FullWidth.master"  Title="Administration" AutoEventWireup="true" Inherits="BugNET.Administration.Admin" Codebehind="Admin.aspx.cs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <h1 class="page-title">Administration</h1>
    <div align="center" style="width:100%">
        <table width="350" style="text-align:left;">
            <tr>
                <td style="width: 3px">
                    <asp:Image ID="Image1" ImageUrl="~/Images/blocks.gif" runat="server" /> </td>
                <td style="width: 150px;">
                    <asp:HyperLink ID="lnkProjects" NavigateUrl="~/Administration/Projects/ProjectList.aspx" runat="server" Text="Projects"></asp:HyperLink>
                 </td>
                <td style="width: 3px">
                    <asp:Image ID="Image2" ImageUrl="~/Images/users.gif" runat="server" />
                </td>
                <td> 
                    <asp:HyperLink ID="lnkUserAccounts" NavigateUrl="~/Administration/Users/UserList.aspx" Text="User Accounts" runat="server"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td style="width: 3px">
                    <asp:Image ID="Image4" ImageUrl="~/Images/configuration.gif" runat="server" /></td>
                <td style="width: 150px;"><asp:HyperLink ID="lnkConfiguration" NavigateUrl="~/Administration/Host/Settings.aspx" runat="server" Text="Application Configuration"></asp:HyperLink></td>
                 <td style="width:3px">
                    <asp:Image ID="Image5" ImageUrl="~/Images/log_viewer.gif" runat="server" />
                </td>
                <td> 
                    <asp:HyperLink ID="lnkLogViewer" runat="server"  NavigateUrl="~/Administration/Host/LogViewer.aspx" Text="Log Viewer"></asp:HyperLink>
               </td>
            </tr>
        </table>
    </div>
    
</asp:Content>

