<%@ Page language="c#"  validateRequest="false" Inherits="BugNET.Administration.Host.Settings" MasterPageFile="~/Shared/FullWidth.master" Title="Application Configuration" Codebehind="Settings.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Content">
    <h1 class="page-title">Application Configuration</h1>
    <div id="menu">
         <asp:TreeView ID="tvAdminMenu" OnSelectedNodeChanged="tvAdminMenu_SelectedNodeChanged" runat="server">
             <LeafNodeStyle HorizontalPadding="5px" />
         </asp:TreeView>
    </div>
    <div id="contentcolumn">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block" UpdateMode="Always">
            <ContentTemplate>
                 <asp:PlaceHolder id="plhSettingsControl" runat="Server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="tvAdminMenu" />
            </Triggers>
        </asp:UpdatePanel>
        <div style="margin-top:1em;clear:both;border-top:1px dotted #cccccc;padding-top:10px;">
            <asp:imagebutton runat="server" id="save" OnClick="cmdUpdate_Click" CssClass="icon"  ImageUrl="~/Images/disk.gif" />
            <asp:LinkButton ID="cmdUpdate" OnClick="cmdUpdate_Click" runat="server" Text="Update Settings" />
        </div>
   </div>
</asp:Content>
