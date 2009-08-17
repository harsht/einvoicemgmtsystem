<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectIssueTypes.ascx.cs" Inherits="BugNET.Administration.Projects.UserControls.ProjectIssueTypes" %>
<%@ Register TagPrefix="IT" TagName="PickImage" Src="~/UserControls/PickImage.ascx" %>

<h4>Issue Types</h4>
<table cellpadding="5" width="100%">
	    <tr>
	        <td>
	            <p>
				    When you create an issue, you assign the issue a type such as Bug or 
				    Task. Enter the list of status values below. Each type can be associated 
				    with an image.
				</p>
	        </td>
	    </tr>
		<tr>
			<td>
				<asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />
				<asp:CustomValidator Text="You must add at least one issue type" Runat="server" id="CustomValidator1" onservervalidate="ValidateIssueType" />
			</td>
		</tr>
		<tr>
			<td>
				
					<asp:DataGrid id="grdIssueTypes"
					    SkinID="DataGrid"
						width="100%" 
						Runat="Server"
						 OnUpdateCommand="grdIssueTypes_Update" 
						 OnEditCommand="grdIssueTypes_Edit" 
						 OnCancelCommand="grdIssueTypes_Cancel"
						 OnItemCommand="grdIssueTypes_ItemCommand">
						<Columns>
						    <asp:editcommandcolumn edittext="Edit"  canceltext="Cancel"
						    updatetext="Update" ButtonType="PushButton">
						        <ItemStyle CssClass="standardText" />
						    </asp:editcommandcolumn> 
							<asp:TemplateColumn HeaderText="Issue Type">
								<headerstyle horizontalalign="Left"></headerstyle>
								<itemstyle width="70%"></itemstyle>
								<ItemTemplate>
									<asp:Label id="lblIssueTypeName" runat="Server" />
								</ItemTemplate>
								<EditItemTemplate>
					               <asp:textbox runat="server" ID="txtIssueTypeName" />
					            </EditItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Image">
								<headerstyle horizontalalign="Center"></headerstyle>
								<itemstyle horizontalalign="Center" width="10%"></itemstyle>
								<ItemTemplate>
									<asp:Image id="imgIssueType" runat="Server" />
								</ItemTemplate>
								<EditItemTemplate>
					                <it:PickImage id="lstEditImages" ImageDirectory="/IssueType" runat="Server" />
					            </EditItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Order">
					            <headerstyle horizontalalign="center" ></headerstyle>
					            <itemstyle horizontalalign="center" width="10%"></itemstyle>
					            <ItemTemplate>     
                                    <asp:ImageButton ID="MoveUp" ImageUrl="~/Images/up.gif" CommandName="up" runat="server" />
                                    <asp:ImageButton ID="MoveDown" ImageUrl="~/Images/down.gif" CommandName="down" runat="server" />
					            </ItemTemplate>
				            </asp:TemplateColumn>
							<asp:TemplateColumn>
								<headerstyle horizontalalign="Right" ></headerstyle>
								<itemstyle horizontalalign="Right" width="10%"></itemstyle>
								<ItemTemplate>
									<asp:Button id="btnDelete" CommandName="delete" Text="Delete" CssClass="standardText" ToolTip="Delete IssueType"
										runat="Server" />
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
					<br>
					<asp:TextBox id="txtName" CssClass="standardText" Maxlength="50" runat="Server" />
					<asp:Button Text="Add IssueType" CssClass="standardText" CausesValidation="false" runat="server"
						id="Button1" onclick="AddIssueType" /></p>
			</td>
		</tr>
		<tr>
			<td>
				<it:PickImage id="lstImages" ImageDirectory="/IssueType" runat="Server" />
			</td>
		</tr>
	</table>
