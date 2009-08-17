<%@ Control Language="c#" Inherits="BugNET.Administration.Projects.UserControls.ProjectMembers" Codebehind="ProjectMembers.ascx.cs" %>
	<div>
		<div>
			<h4>Project Members</h4>
		</div>
		<p class="desc">Add users to the project by selecting the user then clicking the right arrow button.</p>
		<div>
		<asp:UpdatePanel ID="UpdatePanel1" RenderMode="inline" runat="Server">
	        <ContentTemplate> 		
			    <table>
				    <tr>
				        <td style="font-weight:bold">All Users</td>
				        <td>&nbsp;</td>
				        <td style="font-weight:bold">Selected Users</td>
			        </tr>
			        <tr>
				        <td style="height: 108px">
					        <asp:ListBox id="lstAllUsers" SelectionMode="Multiple" Runat="Server" Width="150" Height="110px" />
				        </td>
				        <td style="height: 108px">
					        <asp:Button Text="->"  CssClass="button" style="FONT:9pt Courier" Runat="server" id="Button1" onclick="AddUser" />
					        <br />
					        <asp:Button Text="<-"  CssClass="button" style="FONT:9pt Courier;clear:both;" Runat="server" id="Button2" onclick="RemoveUser" />
				        </td>
				        <td style="height: 108px">
					        <asp:ListBox id="lstSelectedUsers" SelectionMode="Multiple"  Runat="Server" Width="150" Height="110px" />
				        </td>
			        </tr>
		        </table>
		    </ContentTemplate>
		</asp:UpdatePanel>
		</div>
		<div>
			<h4>Assign User to Roles</h4>
		</div>
		<p class="desc">	
		    Assign users to a role by selecting the user in the list and clicking the right arrow to add a role. 
			This role assignment is applicable for this project only.
		</p>
		<div>
		    <asp:UpdatePanel ID="UpdatePanel2" RenderMode="inline" runat="Server">
	            <ContentTemplate> 	
		            <table>
		    	        <tr>
				            <td style="font-weight:bold;width: 147px;height:40px;">
					            User Name:
				            </td>
				            <td colspan="2">                   
				                <asp:dropdownlist AutoPostBack="True" id="ddlProjectMembers" DataTextField="DisplayName" DataValueField="UserName" Runat="Server" Width="177px"  />
				                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" PromptPosition="bottom" runat="server"
                                    TargetControlID="ddlProjectMembers" PromptCssClass="ListSearchExtenderPrompt" />
				            </td>
			            </tr>
			            <tr>
				            <td style="font-weight:bold; width: 147px;">All Roles</td>
				            <td>&nbsp;</td>
				            <td style="font-weight:bold">Assigned Roles</td>
			            </tr>
			            <tr>
				            <td style="width: 147px; height: 113px">
					            <asp:ListBox id="lstAllRoles" Runat="Server" Width="150" Height="110px" />
				            </td>
				            <td style="height: 113px">
					            <asp:Button Text="->"  CssClass="button" style="FONT:9pt Courier;" Runat="server" id="Button3" />
					            <br />
					            <asp:Button Text="<-"  CssClass="button" style="FONT:9pt Courier;clear:both;" Runat="server" id="Button4" />
				            </td>
				            <td style="height: 113px">
					            <asp:ListBox id="lstSelectedRoles" Runat="Server" Width="150" Height="110px" />
				            </td>
			            </tr>
		            </table>
		        </ContentTemplate>
		    </asp:UpdatePanel>
	    </div>
    </div>


