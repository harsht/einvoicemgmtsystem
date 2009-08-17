<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Roles.ascx.cs" Inherits="BugNET.Administration.Users.UserControls.Roles" %>
<asp:Label ID="lblError" runat="server"  ForeColor="Red" />
<div >
    <h4>Manage Roles For User - <asp:Label id="lblUserName" runat="server"/></h4>  
    Project: <bn:PickProject id="dropProjects" CssClass="standardText" DisplayDefault="true"  OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged" AutoPostBack="true" Runat="Server" />
    <asp:CheckBox ID="chkSuperUsers" OnCheckedChanged="chkSuperUsers_CheckChanged" AutoPostBack="true" Visible="false" Text="Super Users" runat="server" />
    <asp:CheckBoxList ID="RoleList" Width="500px" RepeatColumns="2" RepeatDirection="Horizontal" runat="server">
    </asp:CheckBoxList>
    <p align="center" style="margin-top:10px;">
        <asp:ImageButton OnClick="cmdUpdateRoles_Click" runat="server" id="save" CssClass="icon" ImageUrl="~/Images/disk.gif" />
        <asp:LinkButton ID="cmdUpdateRoles" runat="server" Text="Update" OnClick="cmdUpdateRoles_Click"></asp:LinkButton>
    </p>
</div>  