<%@ Control Language="c#" Inherits="BugNET.Administration.Projects.UserControls.ProjectDescription" Codebehind="ProjectDescription.ascx.cs" %>
<%@ Register TagPrefix="it" TagName="PickSingleUser" Src="~/UserControls/PickSingleUser.ascx" %>
<div>
	<h4>Details</h4>
	<asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />
	<p class="desc"><asp:Label ID="Label9" runat="server" Text="Label">Enter the details for the project.</asp:Label></p>
	 <table class="form" border="0" summary="new issue form table">
        <tr>
            <th><asp:Label ID="Label1" CssClass="required" AssociatedControlID="txtName" runat="server" Text="Name:" /></th>
            <td><asp:TextBox id="txtName" Columns="30" runat="Server" /><asp:RequiredFieldValidator Text=" *" SetFocusOnError="True" ControlToValidate="txtName" Runat="server" id="RequiredFieldValidator1" /></td>
        </tr>
        <tr>
            <th valign="top"><asp:Label ID="Label2" CssClass="required" AssociatedControlID="txtDescription" runat="server" >Description:</asp:Label></th>
            <td>
                <asp:TextBox id="txtDescription" TextMode="MultiLine"  Width="100%" Height="100px" runat="Server" />
                <asp:RequiredFieldValidator Text=" *" SetFocusOnError="True" ControlToValidate="txtDescription" Runat="server" id="RequiredFieldValidator2" />
            </td>
        </tr>
         <tr>
            <th><asp:Label ID="Label3" CssClass="required" AssociatedControlID="ProjectCode" runat="server" Text="Project Code:" /></th>
            <td>
                <asp:TextBox id="ProjectCode" width="30" MaxLength="3" runat="Server" /><asp:RequiredFieldValidator Text=" *" ControlToValidate="ProjectCode" Runat="server" id="RequiredFieldValidator3" />
            </td>
        </tr>
         <tr>
           <th>
                <asp:Label CssClass="col1" ID="Label6" class="required" AssociatedControlID="ProjectManager" runat="server" Text="Manager:" />
            </th>
            <td>    
                <asp:dropdownlist id="ProjectManager" DataTextField="Username" DataValueField="Username" Runat="Server" />
                <asp:RequiredFieldValidator Text=" *" InitialValue="" ControlToValidate="ProjectManager" Runat="server" id="RequiredFieldValidator4" />
            </td> 
        </tr>
    </table> 
    <h4>Security</h4>
    <table class="form" border="0" summary="new issue form table">
        <tr>
            <th>
                <asp:Label ID="Label8" AssociatedControlID="rblAccessType" runat="server" Text="Access Type:"></asp:Label>
            </th>
            <td class="input-group">	        
                <asp:radiobuttonlist cssclass="checkboxlist" RepeatDirection="Horizontal" id="rblAccessType" runat="server">
	                <asp:listitem value="Public" />
	                <asp:listitem value="Private" />
                </asp:radiobuttonlist>
           </td>
         </tr>
    </table>
    <h4>Issue Attachments</h4>
    <table class="form" border="0" summary="new issue form table">
        <tr>
            <th><asp:Label ID="Label4" AssociatedControlID="AllowAttachments" runat="server" Text="Enable Attachments:"></asp:Label></th>
            <td class="input-group">
                <asp:checkbox cssclass="inputCheckBox"  id="AllowAttachments" AutoPostBack="true" OnCheckedChanged="AllowAttachmentsChanged" runat="server"/></td>
         </tr>    
         <tr id="AttachmentStorageTypeRow" runat="server" visible="false">
            <th><asp:Label ID="Label10" AssociatedControlID="AttachmentStorageType" runat="server" Text="Storage Type:"></asp:Label></th>
            <td class="input-group">
                <asp:RadioButtonList ID="AttachmentStorageType" RepeatDirection="Horizontal" OnSelectedIndexChanged="AttachmentStorageType_Changed" AutoPostBack="true" runat="server">
                    <asp:ListItem Text="Database (recommended)" Selected="True" Value="2" />
                    <asp:ListItem Text="File System" Value="1" />
                </asp:RadioButtonList>
            </td>
         </tr>
         <tr id="AttachmentUploadPathRow" runat="server" visible="false">
            <th> 
                <asp:Label CssClass="col1" ID="Label5" AssociatedControlID="txtUploadPath" runat="server" Text="Upload Path:" />
            </th>
            <td>~\Uploads\&nbsp;<asp:TextBox id="txtUploadPath" Width="300px" runat="Server" Text="" /></td></tr>
      </table>   
</div>