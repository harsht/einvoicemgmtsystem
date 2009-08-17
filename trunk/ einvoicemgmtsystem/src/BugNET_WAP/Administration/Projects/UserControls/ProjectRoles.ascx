<%@ Control Language="c#" Inherits="BugNET.Administration.Projects.UserControls.ProjectRoles" Codebehind="ProjectRoles.ascx.cs" %>

<asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />

	<asp:panel id="Roles" Visible="True" CssClass="myform" runat="server">
	    <h4>Manage Security Roles</h4>
	    <p class="desc">
	        Each project can have its own roles to group like users and permissions.
	        By default when a new project is created several roles are predefined for you.  
	        If you need to create a custom role 
	        click the add new role button then assign permissions to the role.
	    </p> 
	    <asp:ImageButton runat="server" OnClick="AddRole_Click" ImageUrl="~/Images/shield_add.gif" CssClass="icon"  AlternateText="Add Role" ID="add" />
        <asp:LinkButton ID="cmdAddRole" OnClick="AddRole_Click" runat="server" Text="Add New Role" />
        <br />
        <br />
	    <asp:GridView OnRowCommand="gvRoles_RowCommand" GridLines="None" BorderStyle="None" BorderWidth="0px" ID="gvRoles" runat="server"  AutoGenerateColumns="False" DataSourceID="SecurityRoles">
        <Columns>
            <asp:TemplateField>
                <ItemStyle Width="20px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdEditRole" runat="server" CommandName="Edit"  CommandArgument='<%# Eval("Id") %>' ImageUrl="~\images\pencil.gif"
                      ImageAlign="Top" />
                </ItemTemplate>
           </asp:TemplateField>
           <asp:BoundField DataField="Name" ItemStyle-Width="150px" HeaderText="Name"  />
           <asp:BoundField DataField="Description" ItemStyle-Width="200px"  HeaderText="Description"  />
           <asp:CheckBoxField DataField="AutoAssign" HeaderText="Auto Assignment" ItemStyle-HorizontalAlign="center" />
   </Columns>
</asp:GridView>

<asp:ObjectDataSource ID="SecurityRoles" runat="server" SelectMethod="GetRolesByProject"
    TypeName="BugNET.BusinessLogicLayer.Role">
</asp:ObjectDataSource>

 </asp:panel>

<asp:panel id="AddRole" CssClass="myform" Visible="False" runat="server">  
	<h4> <asp:Label ID="RoleNameTitle" runat="server">Manage Security Role - </asp:Label></h4>
	<p>
        To create a new role enter the name, description and check the required permssions then click save.
    </p>
    
    <asp:Label ID="Label1" ForeColor="Red" runat="server"></asp:Label>
    <table class="form" border="0" summary="new issue form table">
        <tr>
            <th><asp:Label ID="Label2" CssClass="col1"  AssociatedControlID="txtRoleName" runat="server" Text="Role Name:"></asp:Label></th>
            <td><asp:TextBox ID="txtRoleName" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvRoleName" runat="server" ControlToValidate="txtRoleName"
                    ErrorMessage="(required)" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th valign="top"><asp:Label ID="Label4" AssociatedControlID="txtDescription" Text="Description:"  CssClass="col1" runat="server"></asp:Label></th>
            <td> <asp:TextBox ID="txtDescription" TextMode="multiLine" Height="100px" Width="300px" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th><asp:Label ID="Label5" AssociatedControlID="chkAutoAssign" Text="Auto Assignment?"  CssClass="col1" runat="server"></asp:Label></th>
            <td class="input-group"><asp:checkbox id="chkAutoAssign" runat="server" /></td>
        </tr>
    </table>
    <br />
    <br />
     <h5><asp:Label ID="Label3" Font-Bold="true" runat="server">Permissions</asp:Label></h5>
     <asp:Panel ID="Panel1" runat="server">
        <table>
			<tr>
				<td colspan="5" style="padding-bottom:5px;border-bottom:1px solid #000;">Add</td>
			</tr>
			<tr>
				<td>
					<asp:checkbox id="chkAddIssue" Text="Issue" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkAddComment" Text="Comments" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkAddAttachment" Text="Attachments" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkAddRelated" Text="Related Issue" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkAddTimeEntry" Text="Time Entry" runat="server"></asp:checkbox>
				</td>
			</tr>
			<tr><td><asp:checkbox  id="chkAddQuery" Text="Add Query" runat="server"></asp:checkbox></td></tr>
			<tr><td><br/></td></tr>
			<tr>
				<td colspan="5" style="padding-bottom:5px;border-bottom:1px solid #000;">Edit</td>
			</tr>
			<tr>
				<td>
					<asp:checkbox  id="chkEditIssue" Text="Issue" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkEditComment" Text="Comments" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkEditOwnComment" Text="Own Comments" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkEditIssueDescription" Text="Issue Description" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkEditIssueSummary" Text="Issue Title" runat="server"></asp:checkbox>
				</td>
			</tr>
			<tr><td><br/></td></tr>
			<tr>
				<td colspan="5" style="padding-bottom:5px;border-bottom:1px solid #000;">Delete</td>
			</tr>
			<tr>
				<td>
					<asp:checkbox  id="chkDeleteIssue" Text="Issue" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkDeleteComment" Text="Comments" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkDeleteAttachment" Text="Attachments" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkDeleteRelated" Text="Related" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkDeleteTimeEntry" Text="Time Entry" runat="server"></asp:checkbox>
				</td>
			</tr>
			<tr><td><asp:checkbox  id="chkDeleteQuery" Text="Delete Query" runat="server"></asp:checkbox></td></tr>
			<tr><td><br/></td></tr>
			<tr>
				<td colspan="5" style="padding-bottom:5px;border-bottom:1px solid #000;">Other</td>
			</tr>
			<tr>
				<td>
					<asp:checkbox  id="chkCloseIssue" Text="Close Issue" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkAssignIssue" Text="Assign Issue" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkSubscribeIssue" Text="Subscribe to Issue" runat="server"></asp:checkbox>
				</td>
				<td>
					<asp:checkbox  id="chkReOpenIssue" Text="Re-Open Issue" runat="server"></asp:checkbox>
				</td>									
			</tr>
		</table>
    </asp:Panel>
    <br />
    <br />
    <div align="center">
        <asp:ImageButton runat="server" id="ImageButton1" OnClick="cmdAddUpdateRole_Click" CssClass="icon"  ImageUrl="~/Images/disk.gif" />
        <asp:LinkButton ID="cmdAddUpdateRole" OnClick="cmdAddUpdateRole_Click" runat="server" CausesValidation="True" Text="Add Role" />
        <asp:ImageButton runat="server" id="Image1" OnClick="cmdCancel_Click" CssClass="icon"  ImageUrl="~/Images/lt.gif" />
        <asp:LinkButton ID="cmdCancel" OnClick="cmdCancel_Click" runat="server" CausesValidation="False" Text="Cancel" />
        <asp:imagebutton runat="server" OnClientClick="return confirm('Are you sure you want to delete this role?');" OnClick="cmdDelete_Click" id="cancel" CssClass="icon" ImageUrl="~/Images/shield_delete.gif" />
        <asp:LinkButton ID="cmdDelete" OnClientClick="return confirm('Are you sure you want to delete this role?');" OnClick="cmdDelete_Click" runat="server" CausesValidation="False" Text="Delete Role" />
    </div>
</asp:panel>
	
