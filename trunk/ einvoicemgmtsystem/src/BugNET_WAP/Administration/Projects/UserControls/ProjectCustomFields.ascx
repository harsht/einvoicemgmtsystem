<%@ Control Language="C#" AutoEventWireup="true" Inherits="BugNET.Administration.Projects.UserControls.ProjectCustomFields" Codebehind="ProjectCustomFields.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %> 
 <div>
    <h4>Custom Fields</h4>
	<p>
	You can add one or more custom fields to a project. You can assign a data type 
	to each custom field. In addtion, each custom field can be marked as either 
	required or optional.
	</p>
	<p>
		<asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />
	</p> 
    <script type="text/javascript">
		
		function Toggle( commId, imageId )
			{
				
				var div = document.getElementById(commId);
				var img = document.getElementById(imageId);
				if (div.style.display == 'none')
				{	
				    div.style.display = 'inline';
					img.src = '../../images/minus.gif';
				}
				else
				{	
				    div.style.display = 'none';
					img.src = '../../images/plus.gif';
				}
			}
	</script>
        <asp:DataGrid id="grdCustomFields" SkinID="DataGrid"
            OnUpdateCommand="grdCustomFields_Update" 
            OnItemCreated="grdCustomFields_ItemCreated"
            AutoGenerateColumns="false" BorderStyle="None"
            OnEditCommand="grdCustomFields_Edit" 
            OnCancelCommand="grdCustomFields_Cancel"
            width="100%" Runat="Server">
        <Columns>   
            <asp:BOUNDCOLUMN visible="False" datafield="Id" headertext="Id" />
            <asp:TemplateColumn ItemStyle-Width="10">
                  <ItemTemplate>
                        <img alt="expand / collapse" id="image_" runat="server" width="9" src="~/images/minus.gif" height="9" />
                  </ItemTemplate>        
            </asp:TemplateColumn>        
            <asp:editcommandcolumn edittext="Edit"  canceltext="Cancel" updatetext="Update" ButtonType="PushButton" />         
	        <asp:TemplateColumn HeaderText="Field">
		        <headerstyle horizontalalign="Left"></headerstyle>
		        <ItemTemplate>
			        <asp:Label id="lblName" runat="Server" />
		        </ItemTemplate>
		         <EditItemTemplate>
	               <asp:textbox runat="server" ID="txtCustomFieldName" />
	            </EditItemTemplate>
	        </asp:TemplateColumn>
	        <asp:TemplateColumn HeaderText="Field Type">
		        <headerstyle horizontalalign="Center"></headerstyle>
		        <itemstyle horizontalalign="left" ></itemstyle>
		        <ItemTemplate>
			        <asp:Label id="lblFieldType" runat="Server"></asp:Label>
		        </ItemTemplate>
		        <EditItemTemplate>
		         <asp:DropDownList id="dropCustomFieldType" AutoPostBack="True" 
            OnSelectedIndexChanged="DropFieldType_SelectedIndexChanged" runat="Server">
		            <asp:ListItem Text="Text" Selected="True" Value="1" />
                    <asp:ListItem Text="Drop Down List" Value="2" />
                    <asp:ListItem Text="Date" Value="3" />
                    <asp:ListItem Text="Rich Text" Value="4" />
                    <asp:ListItem Text="Yes / No" Value="5" />
                    <asp:ListItem Text="User List" Value="6" />
		         </asp:DropDownList>
		      </EditItemTemplate>
	        </asp:TemplateColumn>
	        <asp:TemplateColumn HeaderText="Data Type">
		        <headerstyle horizontalalign="Center" ></headerstyle>
		        <itemstyle horizontalalign="left"></itemstyle>
		        <ItemTemplate>
			        <asp:Label id="lblDataType" runat="Server" />
		        </ItemTemplate>
		        <EditItemTemplate>
		            <asp:DropDownList id="dropEditDataType" runat="Server" />
		        </EditItemTemplate>
	        </asp:TemplateColumn>	
	        <asp:TemplateColumn HeaderText="Required">
		        <headerstyle horizontalalign="Center"></headerstyle>
		        <itemstyle horizontalalign="left"></itemstyle>
		        <ItemTemplate>
			        <asp:Label id="lblRequired" runat="Server" />
		        </ItemTemplate>
		        <EditItemTemplate>
		            <asp:CheckBox id="chkEditRequired" Runat="Server" />
		        </EditItemTemplate>
	        </asp:TemplateColumn>
	        
	        <asp:TemplateColumn>
		        <headerstyle horizontalalign="Right"></headerstyle>
		        <itemstyle horizontalalign="Right"></itemstyle>
		        <ItemTemplate>				     
		            <asp:ImageButton id="btnDelete" ImageUrl="~/images/cross.gif" 
					    AlternateText="Delete Custom Field" 
					    CausesValidation="false" CommandName="Delete" 
					    CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' 
					    runat="server"/>						  
		        </ItemTemplate>
	        </asp:TemplateColumn> 
	        <asp:TemplateColumn>
                  <ItemTemplate>
                  
                  	</td>
				</tr>
				<tr>
					<td colspan="6" style="padding-left:60px;">
						<table border="0" cellpadding="3" cellspacing="3" width="100%">
							<tr>
								<td>
                                    <div id="divSelectionValues" runat="server" style="display:inline;">
                             
                                     <div style="font-size:12px;padding:5px 0 5px 0;font-weight:bold;">Selection Values</div>
                                    <asp:DataGrid id="grdSelectionValues"                              
                                        runat="server"
                                        ShowFooter="true"
                                        AutoGenerateColumns="False"
                                        Visible="true"
                                        HeaderStyle-BackColor="#dddddd"
                                        HeaderStyle-Font-Bold="True"
                                        HeaderStyle-Font-Size="11px"
                                        HeaderStyle-ForeColor="#000000"
                                        ItemStyle-BackColor="#efefef"
                                        ItemStyle-Font-Size="11px"
                                        BorderColor="#cccccc"
                                        FooterStyle-BackColor="#F2F7FC"
                                        width="100%">
                                    <Columns>
                                          <asp:boundcolumn visible="False" datafield="Id" headertext="ID" />
                                          <asp:editcommandcolumn edittext="Edit"  canceltext="Cancel" updatetext="Update" ButtonType="PushButton" /> 
                                          <asp:TemplateColumn HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:label id="lblSelectionName" runat="server" text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "Name").ToString()) %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            	<asp:textbox id="txtEditSelectionName" Text='<%# DataBinder.Eval (Container.DataItem, "Name") %>'  runat="server" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
												<asp:textbox id="txtAddSelectionName" runat="server" />
                                            </FooterTemplate>
                                          </asp:TemplateColumn>
                                           <asp:TemplateColumn HeaderText="Value">
                                            <ItemTemplate>
                                                <asp:label id="lblSelectionValue" runat="server" text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "Value").ToString()) %>' />
                                            </ItemTemplate>
                                             <EditItemTemplate>
                                            	<asp:textbox id="txtEditSelectionValue" Text='<%# DataBinder.Eval (Container.DataItem, "Value") %>'  runat="server" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
												<asp:textbox id="txtAddSelectionValue" cssclass="textbox" runat="server" />
                                            </FooterTemplate>
                                          </asp:TemplateColumn>
                                          <asp:TemplateColumn HeaderText="Order">
					                        <headerstyle horizontalalign="center" cssclass="gridHeader"></headerstyle>
					                        <itemstyle horizontalalign="center" cssclass="gridLastItem" width="10%"></itemstyle>
					                        <ItemTemplate>     
                                                <asp:ImageButton ID="MoveUp" ImageUrl="~/Images/up.gif" CommandName="up" runat="server" />
                                                <asp:ImageButton ID="MoveDown" ImageUrl="~/Images/down.gif" CommandName="down" runat="server" />
					                        </ItemTemplate>
				                        </asp:TemplateColumn>
							            <asp:TemplateColumn>
								            <headerstyle horizontalalign="Right" cssclass="gridHeader"></headerstyle>
								            <itemstyle horizontalalign="Right" cssclass="gridLastItem" width="10%"></itemstyle>
								            <ItemTemplate>
									            <asp:Button id="btnDelete" CommandName="delete" Text="Delete" CssClass="standardText" ToolTip="Delete Custom Field Selection"
										            runat="Server" />
								            </ItemTemplate>
								            <FooterTemplate>
								                <asp:Button id="btnAddSelectionValue" CommandName="add" Text="Add" CssClass="standardText" ToolTip="Add new custom field selection"
										            runat="Server" />
								            </FooterTemplate>
							            </asp:TemplateColumn>
                                          <ASP:TEMPLATECOLUMN visible="false">
											<ITEMTEMPLATE>
												<ASP:LABEL id="lblCustomFieldId" runat="server" text='<%# DataBinder.Eval (Container.DataItem, "CustomFieldId") %>'>
												</ASP:LABEL>
											</ITEMTEMPLATE>
										</ASP:TEMPLATECOLUMN>
                                    </Columns>
                                    </asp:DataGrid>

                                </div>
                            </td>
						</tr>
					</table>
                  </ItemTemplate>
              </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>		                           
</div>
 <br />
 <br />                     			
<h4>New Custom Field</h4>
<table class="form" border="0" summary="new issue form table">
    <tr>
        <th><asp:Label ID="label1" runat="server" AssociatedControlID="txtName" Text="Field Name:" /></th>
        <td><asp:TextBox id="txtName" MaxLength="50" runat="Server" /></td>
    </tr>
    <tr>
        <th valign="top"><asp:Label ID="label2" runat="server" AssociatedControlID="rblCustomFieldType" Text="Field Type:" CssClass="col1b" /></th>
        <td class="input-group"><asp:RadioButtonList AutoPostBack="True" 
            OnSelectedIndexChanged="rblCustomFieldType_SelectedIndexChanged"
            ID="rblCustomFieldType" runat="server">
            <asp:ListItem Text="Text" Selected="True" Value="1" />
            <asp:ListItem Text="Drop Down List" Value="2" />
            <asp:ListItem Text="Date" Value="3" />
            <asp:ListItem Text="Rich Text" Value="4" />
            <asp:ListItem Text="Yes / No" Value="5" />
            <asp:ListItem Text="User List" Value="6" />
            </asp:RadioButtonList>
       </td>
    </tr>
    <tr>
        <th><asp:Label ID="label3" runat="server" AssociatedControlID="dropDataType" Text="Data Validation Type:" /></th>
        <td>  <asp:DropDownList id="dropDataType" runat="Server" /></td>
    </tr>
    <tr>
        <th><asp:Label ID="label4" runat="server" AssociatedControlID="chkRequired" Text="Required:" /></th>
        <td class="input-group"><asp:CheckBox id="chkRequired" Runat="Server" /></td>
    </tr>
</table>
<br />
<asp:imagebutton runat="server" id="Image2" CssClass="icon" OnClick="lnkAddCustomField_Click" ImageUrl="~/Images/textfield_add.gif" />
<asp:LinkButton ID="lnkAddCustomField" OnClick="lnkAddCustomField_Click" CausesValidation="false"  runat="server" Text="Add New Custom Field" />



