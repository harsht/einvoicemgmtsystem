<%@ Control Language="c#" CodeBehind="ProjectStatus.ascx.cs" AutoEventWireup="True" Inherits="BugNET.Administration.Projects.UserControls.ProjectStatus" %>
<%@ Register TagPrefix="IT" TagName="PickImage" Src="~/UserControls/PickImage.ascx" %>
<h4>Status</h4>
	<table cellpadding="5" width="100%">
	    <tr>
	        <td>
	            <p>
				    When you create an issue, you assign the issue a status such as In Progress or 
				    Completed. Enter the list of status values below. Each status can be associated 
				    with an image.
				<p>
	        </td>
	    </tr>
		<tr>
			<td>
				<asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />
				<asp:CustomValidator Text="You must add at least one status" Runat="server" id="CustomValidator1" onservervalidate="ValidateStatus" />
			</td>
		</tr>
		<tr>
			<td>
				
					<asp:DataGrid id="grdStatus" 
					    SkinID="DataGrid"
						width="100%" 
						Runat="Server"
						OnUpdateCommand="grdStatus_Update" 
						OnEditCommand="grdStatus_Edit" 
						OnCancelCommand="grdStatus_Cancel"
					    OnItemCommand="grdStatus_ItemCommand">
						<Columns>
						    <asp:editcommandcolumn edittext="Edit"  canceltext="Cancel" updatetext="Update" ButtonType="PushButton" /> 
							<asp:TemplateColumn HeaderText="Status">
								<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
								<itemstyle width="50%"></itemstyle>
								<ItemTemplate>
									<asp:Label id="lblStatusName" runat="Server" />
								</ItemTemplate>
								<EditItemTemplate>
					               <asp:textbox runat="server" ID="txtStatusName" />
					            </EditItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Image">
								<headerstyle horizontalalign="Center"></headerstyle>
								<itemstyle horizontalalign="Center" width="10%"></itemstyle>
								<ItemTemplate>
									<asp:Image id="imgStatus" runat="Server" />
								</ItemTemplate>
								<EditItemTemplate>
					                <it:PickImage id="lstEditImages" ImageDirectory="/Status" runat="Server" />
					            </EditItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Is Closed State">
								<headerstyle horizontalalign="Center" ></headerstyle>
								<itemstyle horizontalalign="Center" width="20%"></itemstyle>
								<ItemTemplate>
									<asp:CheckBox ID="chkClosedState" runat="server" Enabled="false" />
								</ItemTemplate>
								<EditItemTemplate>
					               <asp:CheckBox ID="chkEditClosedState" runat="server"  />
					            </EditItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Order">
					            <headerstyle horizontalalign="center"></headerstyle>
					            <itemstyle horizontalalign="center" width="10%"></itemstyle>
					            <ItemTemplate>     
                                    <asp:ImageButton ID="MoveUp" ImageUrl="~/Images/up.gif" CommandName="up" runat="server" />
                                    <asp:ImageButton ID="MoveDown" ImageUrl="~/Images/down.gif" CommandName="down" runat="server" />
					            </ItemTemplate>
				            </asp:TemplateColumn>
							<asp:TemplateColumn>
								<headerstyle horizontalalign="Right"></headerstyle>
								<itemstyle horizontalalign="Right" width="10%"></itemstyle>
								<ItemTemplate>
									<asp:Button id="btnDelete" CommandName="delete" Text="Delete" CssClass="standardText" ToolTip="Delete Status"
										runat="Server" />
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
			</td>
		</tr>
		<tr>
			<td>
			    <table>
			        <tr>
			            <td colspan="2"><strong>Add New Status</strong></td>
			        </tr>
			        <tr>
			            <td>Name:</td>
			            <td><asp:TextBox id="txtName" CssClass="standardText" Maxlength="50" runat="Server" /></td>
			        </tr>
			        <tr>
			            <td>Is Closed State:</td>
			            <td><asp:CheckBox ID="chkClosedState" runat="server" /></td>
			        </tr>
			        <tr>
		                <td>Image:</td>
		                <td><it:PickImage id="lstImages" ImageDirectory="/Status" runat="Server" /></td>
			        </tr>
			        <tr>
		               <td></td>
		               <td>
		                <asp:Button Text="Add Status" CssClass="standardText" CausesValidation="false" runat="server"
					        id="Button1" onclick="AddStatus" />
		               </td>
			        </tr>
			    </table>
			</td>
		</tr>
	</table>
