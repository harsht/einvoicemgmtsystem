<%@ Page language="c#" Inherits="BugNET.Administration.Projects.EditProject" Title="Project Administration" MasterPageFile="~/Shared/TwoColumn.master" Codebehind="EditProject.aspx.cs" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="PageTitle">
    <h1 class="page-title">Project Administration - <asp:Literal id="litProjectName" runat="Server"/></h1>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="Left">
   <div style="margin-top:1em">
        <asp:TreeView ID="tvAdminMenu" runat="server"  LeafNodeStyle-HorizontalPadding="5px" LeafNodeStyle-VerticalPadding="2px" > 
        </asp:TreeView>
    </div>
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Content">
	<div style="width:650px">
	    <BN:Message ID="Message1" runat="server" Visible="False"  />
        <asp:PlaceHolder id="plhContent" Runat="Server" />     
        <div style="margin:2em 0 0 0;border-top:1px solid #ddd;padding-top:5px;">
            <asp:ImageButton runat="server" id="Image2" onclick="SaveButton_Click" CssClass="icon"  ImageUrl="~/Images/disk.gif" />
            <asp:linkbutton id="SaveButton" runat="server"  CssClass="button" onclick="SaveButton_Click"  Text="Save" />
            &nbsp;
            <asp:imageButton runat="server" OnClientClick="return confirm('Are you sure you want to delete this project?');"  onclick="DeleteButton_Click" id="Image1" CssClass="icon"  ImageUrl="~/Images/cross.gif" />
            <asp:linkbutton id="DeleteButton" runat="server"  CssClass="button" causesvalidation="False" OnClientClick="return confirm('Are you sure you want to delete this project?');" onclick="DeleteButton_Click" Text="Delete Project" />
        </div>
    </div>
</asp:Content>


		
	
