<%@ Page language="c#" Inherits="BugNET.Administration.Projects.ProjectList"  MasterPageFile="~/Shared/FullWidth.master" Title="Project List" Codebehind="ProjectList.aspx.cs" %>
<asp:Content ContentPlaceHolderID="content" runat="server" ID="Content1">
    <h1 class="page-title">Projects</h1>
    <div style="padding:0px 0 10px 0;">
        <span style="padding-right:5px;border-right:1px dotted #ccc">
            <a  href='<%= ResolveUrl("~/Administration/Projects/AddProject.aspx") %>'><img style="border:none;" src='<%= ResolveUrl("~/Images/application_add.gif") %>' class="icon" alt="Add Project" /></a>
            <asp:HyperLink ID="lnkNewProject" runat="server"  NavigateUrl="~/Administration/Projects/AddProject.aspx" Text="Create New Project"/>
        </span>
        &nbsp;
        <a href='<%= ResolveUrl("~/Administration/Projects/CloneProject.aspx") %>'><img style="border:none;" src='<%= ResolveUrl("~/Images/application_double.gif") %>' class="icon" alt="Add Project" /></a>
        <asp:HyperLink ID="lnkCloneProject" runat="server"  NavigateUrl="~/Administration/Projects/CloneProject.aspx" Text="Clone Project"/>
    </div>
	<asp:DataGrid id="dgProjects"  Width="100%" SkinID="DataGrid"  AllowSorting="true" runat="server">
		<columns>
			<asp:hyperlinkcolumn DataNavigateUrlField="Id" DataNavigateUrlFormatString="EditProject.aspx?id={0}&tid=1"
				DataTextField="Name"  HeaderText="Project" SortExpression="Name" />
			<asp:BoundColumn HeaderText="Description" DataField ="Description" SortExpression="Description" />
			<asp:BoundColumn HeaderText="Project Manager" DataField="ManagerUserName" SortExpression="Manager"/>
			<asp:templatecolumn HeaderText="Created" SortExpression="Created">
				<itemtemplate>
					<asp:label id="lblCreated" runat="server"></asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:BoundColumn HeaderText="Created By" DataField="CreatorUserName" SortExpression="Creator"/>
			<asp:templatecolumn HeaderText="Disabled" SortExpression="Disabled">
				<itemtemplate>
					<asp:label id="lblActive" runat="server"></asp:label>	
				</itemtemplate>
			</asp:templatecolumn>
		</columns>
		
	</asp:DataGrid>
</asp:Content>
