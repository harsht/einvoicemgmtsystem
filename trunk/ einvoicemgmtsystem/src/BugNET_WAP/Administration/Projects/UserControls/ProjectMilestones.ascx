<%@ Control Language="c#" Inherits="BugNET.Administration.Projects.UserControls.ProjectMilestones" Codebehind="ProjectMilestones.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="IT" TagName="PickImage" Src="~/UserControls/PickImage.ascx" %>
<div>
    <h4>Milestones</h4>
    <asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />
    <asp:CustomValidator Text="You must add at least one version" Display="dynamic" Runat="server" id="MilestoneValidation" OnServerValidate="MilestoneValidation_Validate" />
    <p class="desc">When creating an issue, you assign the issue a version or milestone such as First, 
        Second, or Third. Enter the list of Milestones below.  
     </p>
<asp:UpdatePanel ID="updatepanel1" runat="server">
 <ContentTemplate>
	<asp:DataGrid id="grdMilestones" SkinID="DataGrid" 
	    OnUpdateCommand="grdMilestones_Update" 
	    OnEditCommand="grdMilestones_Edit" 
	    OnCancelCommand="grdMilestones_Cancel" 
	    OnItemCommand="grdMilestones_ItemCommand" 
	    OnDeleteCommand="DeleteMilestone" 
	    OnItemDataBound="grdMilestones_ItemDataBound" width="100%" Runat="Server">
			<Columns>
			    <asp:editcommandcolumn edittext="Edit"  canceltext="Cancel" updatetext="Update" ButtonType="PushButton" HeaderStyle-CssClass="gridHeader" /> 
				<asp:TemplateColumn HeaderText="Milestone">
					<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
					<itemstyle cssclass="gridFirstItem" width="70%"></itemstyle>
					<ItemTemplate>
						<asp:Label id="lblMilestoneName" runat="Server" />
					</ItemTemplate>
					<EditItemTemplate>
					   <asp:textbox runat="server" ID="txtMilestoneName" />
					</EditItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Image">
					<headerstyle horizontalalign="Center"></headerstyle>
					<itemstyle horizontalalign="Center" width="10%"></itemstyle>
					<ItemTemplate>
						<asp:Image id="imgMilestone" runat="Server" />
					</ItemTemplate>
					<EditItemTemplate>
					  <it:PickImage id="lstEditImages" ImageDirectory="/Milestone" runat="Server" />
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
						<asp:Button id="btnDelete" CommandName="delete" Text="Delete" CssClass="standardText" ToolTip="Delete Milestone"
							runat="Server" />
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
 </ContentTemplate>
</asp:UpdatePanel>
    
            <table border="0" summary="new version form table">
                <tr>
                    <td>
                        <asp:TextBox id="txtName" Width="150" MaxLength="50"  Text='<%# Bind("name") %>' runat="Server" />              
                        <asp:RequiredFieldValidator Text="*"  Display="Dynamic"  ValidationGroup="Update" ControlToValidate="txtName" Runat="server" id="RequiredFieldValidator4" />
                    </td>
                    <td> 
                       <asp:Button Text="Add Milestone" CssClass="standardText"  OnClick="AddMilestone" CausesValidation="false" runat="server"
			            id="btnAdd" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <it:PickImage id="lstImages" ImageDirectory="/Milestone" runat="Server" />
                    </td>
                </tr>
            </table>

      
        
</div>
