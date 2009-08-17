<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" MasterPageFile="~/Shared/TwoColumn.master"  Title="User Profile" Inherits="BugNET.UserProfile" meta:resourcekey="Page1" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="PageTitle">
    <h1 class="page-title">User Profile - <asp:Literal id="litUserName" runat="Server" 
            meta:resourcekey="litUserName1"/></h1>
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Content">
    <asp:MultiView ID="ProfileView" ActiveViewIndex="0"  runat="server">
         <asp:View ID="UserDetails" runat="server">
            <h4><asp:Label ID="Label6" runat="server" meta:resourcekey="TreeNode1"></asp:Label></h4>
            <br />
            <BN:Message ID="Message1" runat="server" Visible="False"  />
            <table class="form" style="width:500px" border="0" summary="update profile table">
                <tr>
                    <th><asp:Label ID="Label2" AssociatedControlID="UserName" runat="server" 
                            Text="<%$ Resources:CommonTerms, Username %>" /></th>
                    <td class="field disabled">
                        <asp:TextBox ID="UserName" ReadOnly="True" runat="server"  />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                            Text="<%$ Resources:CommonTerms, Required %>" ControlToValidate="UserName" /></td>
                </tr>   
                 <tr>
                    <th><asp:Label ID="Label1" AssociatedControlID="FirstName" runat="server" 
                            Text="<%$ Resources:CommonTerms, FirstName %>" /></th>
                    <td class="field"><asp:TextBox ID="FirstName" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                            Text="<%$ Resources:CommonTerms, Required %>" ControlToValidate="FirstName" /></td>
                </tr> 
                 <tr>
                    <th><asp:Label ID="Label3" AssociatedControlID="LastName" runat="server" 
                            Text="<%$ Resources:CommonTerms, LastName %>" /></th>
                    <td class="field"><asp:TextBox ID="LastName" runat="server"  />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                            Text="<%$ Resources:CommonTerms, Required %>" ControlToValidate="LastName"></asp:RequiredFieldValidator></td>
                </tr> 
                <tr>
                    <th><asp:Label ID="Label5" AssociatedControlID="FullName" runat="server" 
                            Text="<%$ Resources:CommonTerms, DisplayName %>" /></th>
                    <td class="field"><asp:TextBox ID="FullName" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            Display="Dynamic" Text="<%$ Resources:CommonTerms, Required %>"
                            ControlToValidate="FullName"></asp:RequiredFieldValidator></td>
                </tr>
                 <tr>
                    <th><asp:Label ID="Label4" AssociatedControlID="Email" 
                            runat="server" Text="<%$ Resources:CommonTerms, Email %>" meta:resourcekey="Label41" /></th>
                    <td class="field"><asp:TextBox ID="Email" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic"
                            Text="<%$ Resources:CommonTerms, Required %>" ControlToValidate="Email" /></td>
                </tr>    
                <tr>
                    <td colspan="2" style="padding:1em   0 1em 5px" class="input-group">
                        <asp:ImageButton runat="server" id="Image2" onclick="SaveButton_Click" 
                            CssClass="icon"  ImageUrl="~/Images/disk.gif" />
                        <asp:linkbutton id="SaveButton" runat="server"  CssClass="button" 
                            onclick="SaveButton_Click"  Text="<%$ Resources:CommonTerms, Save %>" />            
                        <asp:ImageButton runat="server" id="Image4" onclick="BackButton_Click" 
                            CssClass="icon"  ImageUrl="~/Images/lt.gif" />
                        <asp:linkbutton id="BackButton" runat="server"  CssClass="button" 
                            onclick="BackButton_Click"  Text="<%$ Resources:CommonTerms, Return %>" />
                    </td>
                </tr>  
            </table>                        
         </asp:View>
         
         <asp:View ID="ManagePassword" runat="server">  
            <h4><asp:Label ID="Label9" runat="server" meta:resourcekey="TreeNode2"></asp:Label></h4>               
            <BN:Message ID="Message2" runat="server" Visible="False"  />   
            <div style="font-weight:bold;margin-bottom:10px;"><asp:Label ID="Label12" runat="server" Text="<%$ Resources:CommonTerms, ChangePassword %>"></asp:Label></div>
            <table class="form" style="width:500px;margin-top:1em;" border="0" summary="update password table">
                <tr>
                    <th ><asp:Label ID="Label11" AssociatedControlID="NewPassword" runat="server" 
                            Text="New Password:" meta:resourcekey="Label111" /></th>
                    <td class="field">
                        <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" 
                            meta:resourcekey="NewPassword1" />
                        <asp:RequiredFieldValidator ID="rfvPassword" ValidationGroup="Password" 
                            runat="server"  ControlToValidate="NewPassword" SetFocusOnError="True" 
                            Text="<%$ Resources:CommonTerms, Required %>" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>  
                 <tr>
                    <th><asp:Label ID="lblConfirmPassword" AssociatedControlID="ConfirmPassword" 
                            runat="server" Text="<%$ Resources:CommonTerms, ConfirmPassword %>" /></th>
                    <td class="field">
                        <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"  />
                        <asp:CompareValidator ID="cvPasswords" runat="server" 
                            ControlToValidate="NewPassword" ControlToCompare="ConfirmPassword" 
                            ErrorMessage="<%$ Resources:CommonTerms, ConfirmPasswordErrorMessage %>"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding:1em  0 0 5px"><asp:LinkButton ID="cmdChangePassword" 
                            OnClick="cmdChangePassword_Click" ValidationGroup="Password" runat="server" 
                            Text="<%$ Resources:CommonTerms, ChangePassword %>" /></td>
                </tr>  
            </table>
           <ajaxToolkit:PasswordStrength ID="PS" runat="server"
                TargetControlID="NewPassword"
                DisplayPosition="RightSide"
                StrengthIndicatorType="Text"
                PreferredPasswordLength="10"
                PrefixText="Strength:"
                TextCssClass="TextIndicator_TextBox1"
                MinimumNumericCharacters="0"
                MinimumSymbolCharacters="0"
                RequiresUpperAndLowerCaseCharacters="false"
                TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent"
                CalculationWeightings="50;15;15;20" />
         </asp:View>
         <asp:View ID="Customize" runat="server">
            <h4><asp:Label ID="Label10" runat="server" meta:resourcekey="TreeNode3"></asp:Label></h4>
            <BN:Message ID="Message3" runat="server" Visible="False"  />
            <table class="form" style="width:500px;margin-top:2em;" border="0" summary="update customize table">
                 <tr>
                     <th colspan="2" style="font-weight:bold;margin-bottom:10px; height: 17px;">My Issues</th>
                 </tr>
                 <tr>
                     <th>
                         <asp:Label ID="lblItemPageSize" AssociatedControlID="MyIssuesItems" runat="server" 
                             Text="Item Page Size:" meta:resourcekey="lblItemPageSize" /></th>
                     <td>
                         <asp:DropDownList ID="MyIssuesItems" runat="server">
                             <asp:ListItem Text="5" Value="5" />
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="25" Value="25" />
                            <asp:ListItem Text="50" Value="50" />
                         </asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <th valign="top"><asp:Label ID="Label7" CssClass="col1" runat="server" 
                             Text="Show Section(s):" meta:resourcekey="Label71" /></th>
                     <td class="input-group" >
                         <asp:CheckBox ID="cbAssigned" runat="server" Text="Assigned to Me" 
                             meta:resourcekey="cbAssigned" /><br />
                         <asp:CheckBox ID="cbReported" runat="server" Text="Reported by Me" 
                             meta:resourcekey="cbReported" /><br />
                         <asp:CheckBox ID="cbMonitored" runat="server" Text="Monitored by Me" 
                             meta:resourcekey="cbMonitored" /><br />
                         <asp:CheckBox ID="cbInProgress" runat="server" Text="In Progress by Me" 
                             meta:resourcekey="cbInProgress" /><br />
                         <asp:CheckBox ID="cbClosed" runat="server" Text="Closed by Me" 
                             meta:resourcekey="cbClosed" /><br />
                         <asp:CheckBox ID="cbResolved" runat="server" Text="Resolved by Me" 
                             meta:resourcekey="cbResolved" /></td>
                 </tr>
                 <tr>
                     <th colspan="2" style="font-weight:bold;margin-bottom:10px; height: 17px;">
                         <br />
                         Issue List</th>
                 </tr>
                 <tr>
                     <th><asp:Label ID="Label8" AssociatedControlID="MyIssuesItems" runat="server" 
                             Text="Issue Page Size:" meta:resourcekey="lblItemPageSize" /></th>
                     <td>
                         <asp:DropDownList ID="IssueListItems" runat="server" >
                            <asp:ListItem Text="5" Value="5"  />
                            <asp:ListItem Text="10" Value="10"  />
                            <asp:ListItem Text="15" Value="15" />
                            <asp:ListItem Text="20" Value="20"  />
                            <asp:ListItem Text="25" Value="25" />
                         </asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <th colspan="2" style="font-weight:bold;margin-bottom:10px; height: 17px;">
                         <br />
                         Preferences</th>
                 </tr>
                 <tr>
                    <td>Preferred Locale: </td>
                    <td><asp:DropDownList ID="ddlPreferredLocale" runat="server" >
                        <asp:ListItem Text="English (United States)" Value="en-US" />
                    </asp:DropDownList></td>
                </tr>
                 <tr>
                     <td colspan="2" style="padding:1em   0 1em 5px" class="input-group">
                         <asp:ImageButton runat="server" id="ImageButton2" 
                             onclick="SaveCustomSettings_Click" CssClass="icon"  
                             ImageUrl="~/Images/disk.gif" />
                         <asp:LinkButton ID="SaveCustomSettings" OnClick="SaveCustomSettings_Click" 
                             runat="server" Text="<%$ Resources:CommonTerms, Save %>" />
                         <asp:ImageButton runat="server" id="ImageButton1" onclick="BackButton_Click" 
                             CssClass="icon"  ImageUrl="~/Images/lt.gif" 
                             meta:resourcekey="ImageButton11" />
                         <asp:linkbutton id="Linkbutton1" runat="server"  CssClass="button" 
                             onclick="BackButton_Click"  Text="<%$ Resources:CommonTerms, Return %>"   />
                     </td>
                 </tr>
                
             </table>
         </asp:View>
         
         <asp:View ID="Notifications" runat="server">
            <h4><asp:Label ID="Label13" runat="server" meta:resourcekey="TreeNode4" Text="Notifications"></asp:Label></h4>
            <BN:Message ID="Message4" runat="server" Visible="False"  />
            <table class="form" style="width:500px;margin-top:1em;" border="0" summary="update customize table">
                <tr>
                    <td colspan="3">Recieve notifictions for projects:<br /><br /></td>
                </tr>
                 <tr>
                    <td style="font-weight:bold">All Projects</td>
                    <td>&nbsp;</td>
                    <td style="font-weight:bold">Selected Projects</td>
                </tr>
                <tr>
                    <td style="height: 108px">
	                    <asp:ListBox id="lstAllProjects" SelectionMode="Multiple" Runat="Server" Width="150" Height="110px"  />
                    </td>
                    <td style="height: 108px">
	                    <asp:Button Text="->"  CssClass="button" style="FONT:9pt Courier" Runat="server" id="Button1" onclick="AddProjectNotification"/>
	                    <br />
	                    <asp:Button Text="<-"  CssClass="button" style="FONT:9pt Courier;clear:both;" Runat="server" id="Button2" onclick="RemoveProjectNotification" />
                    </td>
                    <td style="height: 108px">
	                    <asp:ListBox id="lstSelectedProjects" SelectionMode="Multiple"  Runat="Server" Width="150" Height="110px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3"><br />Receive notifications by:</td>
                </tr>
                 <tr>
                    <td colspan="3">
                        <asp:CheckBoxList ID="CheckBoxList1"  RepeatColumns="4" RepeatDirection="Horizontal" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding:1em   0 1em 5px" class="input-group">
                        <asp:ImageButton runat="server" id="ImageButton3" onclick="SaveNotificationsButton_Click" 
                            CssClass="icon"  ImageUrl="~/Images/disk.gif" />
                        <asp:linkbutton id="Linkbutton2" runat="server"  CssClass="button" 
                            onclick="SaveNotificationsButton_Click"  Text="<%$ Resources:CommonTerms, Save %>" />            
                        <asp:ImageButton runat="server" id="ImageButton4" onclick="BackButton_Click" 
                            CssClass="icon"  ImageUrl="~/Images/lt.gif" />
                        <asp:linkbutton id="Linkbutton3" runat="server"  CssClass="button" 
                            onclick="BackButton_Click"  Text="<%$ Resources:CommonTerms, Return %>" />
                    </td>
                </tr> 
            </table>
         </asp:View>
    </asp:MultiView>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="Left">
     <asp:TreeView ID="tvAdminMenu" runat="server"  
         OnSelectedNodeChanged="tvAdminMenu_SelectedNodeChanged" 
         LeafNodeStyle-HorizontalPadding="5px" meta:resourcekey="tvAdminMenu1">  
        <Nodes>
            <asp:TreeNode Text="User Details" Value="0" ImageUrl="~/Images/user_edit.gif" 
                meta:resourcekey="TreeNode1" />
            <asp:TreeNode Text="Manage Password" Value="1" ImageUrl="~/Images/key.gif" 
                meta:resourcekey="TreeNode2" />
            <asp:TreeNode ImageUrl="~/Images/application_edit.gif" Text="Customize" 
                Value="2" meta:resourcekey="TreeNode3"></asp:TreeNode>
            <asp:TreeNode ImageUrl="~/Images/email_go.gif" Text="Notifications" 
                Value="3" meta:resourcekey="TreeNode4"></asp:TreeNode>
        </Nodes>
         <LeafNodeStyle HorizontalPadding="5px" />
     </asp:TreeView>
</asp:Content>



