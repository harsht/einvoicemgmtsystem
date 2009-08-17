<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectResolutions.ascx.cs" Inherits="BugNET.Administration.Projects.UserControls.ProjectResolutions" %>
<%@ Register TagPrefix="IT" TagName="PickImage" Src="~/UserControls/PickImage.ascx" %>
<div>
    <h4>Resolutions</h4>
    <asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />
    <asp:CustomValidator Text="You must add at least one resolution" Display="dynamic" Runat="server" id="ResolutionValidation" OnServerValidate="ResolutionValidation_Validate" />
    <p class="desc">When you complete an issue or task you can set how the issue was resolved such as fixed, duplicate or not valid.
     </p>
<asp:UpdatePanel ID="updatepanel1" runat="server">
 <ContentTemplate>
	<asp:DataGrid id="grdResolutions" SkinID="DataGrid"
	    OnUpdateCommand="grdResolutions_Update" 
	    OnEditCommand="grdResolutions_Edit" 
	    OnCancelCommand="grdResolutions_Cancel"
	    OnItemCommand="grdResolutions_ItemCommand" 
	    OnDeleteCommand="DeleteResolution" 
	    OnItemDataBound="grdResolutions_ItemDataBound"
	    width="100%" Runat="Server">
			<Columns>
			    <asp:editcommandcolumn edittext="Edit"  canceltext="Cancel" updatetext="Update" ButtonType="PushButton" ItemStyle-CssClass="standardText" /> 
				<asp:TemplateColumn HeaderText="Resolution">
					<headerstyle horizontalalign="Left"></headerstyle>
					<itemstyle width="70%"></itemstyle>
					<ItemTemplate>
						<asp:Label id="lblResolutionName" runat="Server" />
					</ItemTemplate>
					<EditItemTemplate>
					   <asp:textbox runat="server" ID="txtResolutionName" />
					</EditItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Image">
					<headerstyle horizontalalign="Center" ></headerstyle>
					<itemstyle horizontalalign="Center" width="10%"></itemstyle>
					<ItemTemplate>
						<asp:Image id="imgResolution" runat="Server" />
					</ItemTemplate>
					<EditItemTemplate>
					  <it:PickImage id="lstEditImages" ImageDirectory="/Resolution" runat="Server" />
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
					<itemstyle horizontalalign="Right"  width="10%"></itemstyle>
					<ItemTemplate>
						<asp:Button id="btnDelete" CommandName="delete" Text="Delete" CssClass="standardText" ToolTip="Delete Resolution"
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
                       <asp:Button Text="Add Resolution" CssClass="standardText"  OnClick="AddResolution" CausesValidation="false" runat="server"
			            id="btnAdd" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <it:PickImage id="lstImages" ImageDirectory="/Resolution" runat="Server" />
                    </td>
                </tr>
            </table>

      
        
</div>
