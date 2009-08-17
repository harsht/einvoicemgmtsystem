<%@ Control Language="c#" CodeBehind="ProjectPriorities.ascx.cs" AutoEventWireup="True" Inherits="BugNET.Administration.Projects.UserControls.ProjectPriorities" %>
<%@ Register TagPrefix="IT" TagName="PickImage" Src="~/UserControls/PickImage.ascx" %>
<h4>Priorities</h4>
	<table cellpadding="5" width="100%">
		<tr>
			<td>
				When you create an issue, you assign the issue a priority such as High, Normal, 
				or Low. Enter the list of priorities below. Each priority can be associated 
				with an image.
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />
				<asp:CustomValidator Text="You must add at least one priority" Runat="server" id="CustomValidator1" onservervalidate="ValidatePriority" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:DataGrid id="grdPriorities"
				    SkinID="DataGrid"
					width="100%" 
					Runat="Server"
					OnUpdateCommand="grdPriorities_Update" 
					OnEditCommand="grdPriorities_Edit" 
					OnCancelCommand="grdPriorities_Cancel"
					OnItemCommand="grdPriorities_ItemCommand">
					<Columns>
					    <asp:editcommandcolumn edittext="Edit"  canceltext="Cancel" updatetext="Update" ButtonType="PushButton" >
					        <ItemStyle CssClass="standardText" /> 				    
					    </asp:editcommandcolumn> 
						<asp:TemplateColumn HeaderText="Priority">
							<headerstyle horizontalalign="Left"></headerstyle>
							<itemstyle cssclass="gridFirstItem" width="70%"></itemstyle>
							<ItemTemplate>
								<asp:Label id="lblPriorityName" runat="Server" />
							</ItemTemplate>
							<EditItemTemplate>
				               <asp:textbox runat="server" ID="txtPriorityName" />
				            </EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Image">
							<headerstyle horizontalalign="Center"></headerstyle>
							<itemstyle horizontalalign="Center" width="10%"></itemstyle>
							<ItemTemplate>
								<asp:Image id="imgPriority" runat="Server" />
							</ItemTemplate>
							<EditItemTemplate>
					             <it:PickImage id="lstEditImages" ImageDirectory="/Priority" runat="Server" />
					        </EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Order">
					        <headerstyle horizontalalign="center"></headerstyle>
					        <itemstyle horizontalalign="center" width="10%"></itemstyle>
					        <ItemTemplate>     
                                <asp:ImageButton ID="MoveUp" ImageUrl="~/Images/up.gif" CommandName="up" CommandArgument="" runat="server" />
                                <asp:ImageButton ID="MoveDown" ImageUrl="~/Images/down.gif" CommandName="down" CommandArgument="" runat="server" />
					        </ItemTemplate>
				        </asp:TemplateColumn>
						<asp:TemplateColumn>
							<headerstyle horizontalalign="Right"></headerstyle>
							<itemstyle horizontalalign="Right" width="10%"></itemstyle>
							<ItemTemplate>
								<asp:Button id="btnDelete" CommandName="delete" Text="Delete" CssClass="standardText" runat="Server" />
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</td>
		</tr>
		<tr>
			<td>
				<asp:TextBox id="txtName" CssClass="standardText" MaxLength="50" runat="Server" />
				<asp:Button Text="Add Priority" CssClass="standardText" CausesValidation="false" runat="server"
					id="Button1" onclick="AddPriority" />
			</td>
		</tr>
		<tr>
			<td>
				<it:PickImage id="lstImages" ImageDirectory="/Priority" runat="Server" />
			</td>
		</tr>
	</table>
