<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/FullWidth.master" CodeBehind="CloneProject.aspx.cs" Inherits="BugNET.Administration.Projects.CloneProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
      <h1 class="page-title">Clone Project</h1>
        <p style="WIDTH:500px">
			When you clone a project, you make a copy of the project. For example, you make 
			a copy of all of the project's categories, milestones, and custom fields. 
			However, when you clone a project, issues are not copied to the new project.
		</p>
		<asp:Label id="lblError" ForeColor="red" Font-Bold="true" EnableViewState="false" Runat="Server" />
		<table cellpadding="5">
			<tr>
				<td align="right"><b>Existing Project Name:</b></td>
				<td><asp:DropDownList ID="ddlProjects" DataTextField="Name"  DataValueField="Id" runat="Server"></asp:DropDownList></td>
			</tr>
			<tr>
				<td align="right"><b>New Project Name:</b></td>
				<td>
					<asp:TextBox id="txtNewProjectName" Runat="Server" />
					<asp:RequiredFieldValidator ControlToValidate="txtNewProjectName" Text="(required)" Runat="server" id="RequiredFieldValidator1" />
				</td>
			</tr>
		</table>
		<p>
			<asp:Button id="btnClone" Text="Clone Project" Runat="Server" OnClick="btnClone_Click" />
			&nbsp;&nbsp;
			<asp:Button id="btnCancel" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click"
				Runat="Server" />
		</p>
</asp:Content>
