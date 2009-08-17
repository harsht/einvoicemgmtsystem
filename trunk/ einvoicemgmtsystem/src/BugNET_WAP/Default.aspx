<%@ Page language="c#" Inherits="BugNET._Default" Title="Home" MasterPageFile="~/Shared/Default.master" Codebehind="Default.aspx.cs" meta:resourcekey="Page" %>
<%@ Import Namespace="BugNET.BusinessLogicLayer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
			<asp:repeater id="rptProject" runat="Server">
	            <ItemTemplate>
		            <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                        CollapseControlID="ProjectPanelHeader"
                        ExpandControlID="ProjectPanelHeader"					    
                        TargetControlID="ProjectPanelDetails"
				        CollapsedSize="0"	 						    
				        SkinID="CollapsiblePanelExtender"
				        ImageControlID="Image1" Enabled="True" />
				      <div class="project">
					      <asp:Panel ID="ProjectPanelHeader" runat="server">   
		                    <table style="width:100%;" border="0">
		                        <tr class="projectTitle">
		                            <td><h2><a href="Projects/ProjectSummary.aspx?pid=<%# ((Project)Container.DataItem).Id %>" /><%#((Project)Container.DataItem).Name%></a> <span>(<%#((Project)Container.DataItem).Code%>) </span></h2></td>		                      
		                            <td style="text-align:right;">Manager: <%#((Project)Container.DataItem).ManagerUserName%></td>
		                            <td style="width:15px;"><asp:Image ID="Image1" runat="server" /></td>
		                        </tr>
		                     </table>
		                 </asp:Panel>
		                 <asp:Panel ID="ProjectPanelDetails" runat="server">
		                     <table style="width:100%;border-top:2px solid #fff;" class="projectDetails">
		                        <tr>
		                            <td class="projectDescription"><%#((Project)Container.DataItem).Description%></td>
		                        </tr>
		                        <tr >
		                            <td style="padding:0.5em;background-color:#F7F7EC;">
		                                <ul class="options">
				                            <li><a href="Projects/ProjectSummary.aspx?pid=<%# ((Project)Container.DataItem).Id %>" >Project Summary</a> |</li>
				                            <li><a href="Projects/ChangeLog.aspx?pid=<%# ((Project)Container.DataItem).Id %>">Change Log</a> |</li>
				                            <li><a href="Projects/Roadmap.aspx?pid=<%# ((Project)Container.DataItem).Id %>">Road Map</a></li>
				                            <li id="ReportIssue" runat="server"> | 
                                                <a href="Issues/IssueDetail.aspx?pid=<%# ((Project)Container.DataItem).Id %>">New Issue</a></li>
			                            </ul>
		                            </td>
		                            <td style="text-align:right;padding:0.5em;background-color:#F7F7EC;">
	                                     <ul class="options">
				                            <li><a href='Issues/IssueList.aspx?pid=<%# ((Project)Container.DataItem).Id %>'>
                                                <asp:Label ID="lblAll" runat="server" Text="All" meta:resourcekey="lblAll" /></a> |</li>
				                            <li><a href="Issues/IssueList.aspx?pid=<%# ((Project)Container.DataItem).Id %>&amp;cr=1">Created recently</a> |</li>
				                            <li><a href="Issues/IssueList.aspx?pid=<%# ((Project)Container.DataItem).Id %>&amp;ur=1">Updated recently</a></li>
				                            <li id="AssignedUserFilter" runat="server"> | <asp:hyperlink id="AssignedToUser" 
                                                    runat="server" meta:resourcekey="AssignedToUser">Assigned to me</asp:hyperlink></li>
			                            </ul>
		                            </td>
		                        </tr>
		                    </table>
		                 </asp:Panel>
		            </div> 
	            </ItemTemplate>
            </asp:repeater>
            <asp:Label ID="lblMessage" Visible="False" runat="server" 
                meta:resourcekey="lblMessage"></asp:Label>
	</asp:Content>
	
	<asp:Content ID="Content2" ContentPlaceHolderID="Left" runat="server">
        <div class="sidebar" id="sidebar-right">
            <div class="rightpadding">
                <div class="block">
                    <h2 class="title" ><span ><asp:label id="lblApplicationTitle" runat="server" 
                            meta:resourcekey="lblApplicationTitle">BugNET Issue Tracker</asp:label></span></h2>
                    <div class="content">
                        <p>
                            <asp:label id="WelcomeMessage" runat="Server" ></asp:label>
                        </p>
                    </div>
                 </div>
           </div>
        </div>
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                <asp:Login ID="LoginControl" runat="server"  
                    CreateUserText="Register" 
                    CreateUserUrl="~/Register.aspx" 
                    DestinationPageUrl="~/Default.aspx" 
                    PasswordRecoveryText="Forgot your password?" 
                    PasswordRecoveryUrl="~/ForgotPassword.aspx" 
                    VisibleWhenLoggedIn="False" 
                    TitleText="Login"
                    Width="230px" meta:resourcekey="LoginControl">
                    <TitleTextStyle  CssClass="header" />
                    <TextBoxStyle Width="110px" />
                    <LabelStyle CssClass="label" HorizontalAlign="Left" />
                    <CheckBoxStyle CssClass="label" />
                    <HyperLinkStyle Font-Size="85%" />     
                    <LayoutTemplate>
                        <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" style="width:auto;">
                                        <tr>
                                            <td align="left" class="header" colspan="2">
                                                <h2 style="padding-left:0px;padding-bottom:10px;">Login</h2></td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="label">
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" 
                                                    Text="<%$ Resources:CommonTerms, Username %>">Username:</asp:Label></td>
                                            
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="UserName" runat="server" Width="110px" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                    ErrorMessage="<%$ Resources:CommonTerms, UsernameRequired.ErrorMessage %>" ToolTip="User Name is required." 
                                                    ValidationGroup="ctl00$Login1" meta:resourcekey="UserNameRequired">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="label">
                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" 
                                                    Text="<%$ Resources:CommonTerms, Password %>">Password:</asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="110px" 
                                                    ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                    ErrorMessage="Password is required." ToolTip="Password is required." 
                                                    ValidationGroup="ctl00$Login1" meta:resourcekey="PasswordRequired">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" colspan="2">
                                                <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." 
                                                    meta:resourcekey="RememberMe" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" style="color: red">
                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False" 
                                                    meta:resourcekey="FailureTextResource1"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">
                                                <asp:Button ID="LoginButton" CssClass="button" runat="server" 
                                                    CommandName="Login" Text="Log In" ValidationGroup="ctl00$Login1" 
                                                    meta:resourcekey="LoginButton" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:HyperLink ID="CreateUserLink" runat="server" Font-Size="85%" 
                                                    NavigateUrl="~/Register.aspx" meta:resourcekey="CreateUserLink" >Register</asp:HyperLink>
                                                <br />
                                                <asp:HyperLink ID="PasswordRecoveryLink" runat="server" Font-Size="85%" 
                                                    NavigateUrl="~/ForgotPassword.aspx" 
                                                    meta:resourcekey="PasswordRecoveryLink">Forgot your password?</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:Login>
            </AnonymousTemplate>
        </asp:LoginView>	
	</asp:Content>


