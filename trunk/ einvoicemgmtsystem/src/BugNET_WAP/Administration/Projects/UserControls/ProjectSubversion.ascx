<%@ Control Language="c#" Inherits="BugNET.Administration.Projects.UserControls.ProjectSubversion" Codebehind="ProjectSubversion.ascx.cs" %>

<div>
	<h4>Subversion</h4>
	<asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />
	<p class="desc"><asp:Label ID="Label9" runat="server" Text="Enter the information about the subversion repository"></asp:Label></p>
	 <table class="form" border="0" summary="Subversion settings table">
        <tr>
            <th><asp:Label ID="Label1"  CssClass="col1" AssociatedControlID="svnUrl" runat="server" Text="Subversion Url:" /></th>
            <td><asp:TextBox id="svnUrl" Columns="30" runat="Server" /></td>
        </tr>
    </table> 
    
    <h4>Create a Tag</h4>
    <asp:Label id="createTagErrorLabel" ForeColor="red" EnableViewState="false" runat="Server" />
	<p class="desc"><asp:Label ID="Label3" runat="server" Text="Create a tag of the trunk. This assumes that the root contains both a trunk and a tags directory. Your username and password are used for the single command only and never stored."></asp:Label></p>
     <table class="form" border="0" style="width:100%" summary="Subversion tag creation table">
        <tr>
            <th><asp:Label ID="Label4" CssClass="col1" AssociatedControlID="tagName" runat="server" Text="Tag Name:" /></th>
            <td><asp:TextBox id="tagName" Columns="30" runat="Server" /></td>
        </tr>
         <tr>
            <th valign="top"><asp:Label ID="Label6" CssClass="col1" AssociatedControlID="tagComment" runat="server" Text="Comment:" /></th>
            <td><asp:TextBox id="tagComment" TextMode="MultiLine" Wrap="false"  Width="100%" Height="60px" runat="Server" /></td>
           
        </tr>
        <tr>
            <th><asp:Label ID="Label2" CssClass="col1" AssociatedControlID="tagUserName" runat="server" Text="Username:" /></th>
            <td><asp:TextBox id="tagUserName" Columns="30" runat="Server" /></td>
        </tr>
        <tr>
            <th><asp:Label ID="Label7" CssClass="col1" AssociatedControlID="tagPassword" runat="server" Text="Password:" /></th>
            <td><asp:TextBox id="tagPassword" Columns="30" runat="Server" TextMode="Password" /></td>
        </tr>
        <tr><td align="right" colspan="2">
        <asp:Button ID="createTagButton" runat="server" Text="Create Tag" OnClick="createTagBttn_Click" style="width:120px" /></td></tr>
    </table> 

    <h4>Create a New Subversion Repository</h4>
    <asp:Label id="createErrorLbl" ForeColor="red" EnableViewState="false" runat="Server" />
	<p class="desc"><asp:Label ID="Label5" runat="server" Text="After the creation is finished the projects subversion url will be updated to the url of the new repository."></asp:Label></p>
     <table class="form" border="0" style="width:100%" summary="Subversion repository creation table">
        <tr>
            <th><asp:Label ID="Label8" CssClass="col1" AssociatedControlID="repoName" runat="server" Text="Name:" /></th>
            <td><asp:TextBox id="repoName" Columns="30" runat="Server" /></td>
            </tr>
            <tr>
            <td align="right" colspan="2"> <asp:Button ID="createRepoBttn" runat="server" Text="Create Repository" OnClick="createRepoBttn_Click" style="width:120px" /></td></tr>
    </table> 
        
   <br /><br />
    <h4>Subversion Commands Output</h4>
    <asp:TextBox id="svnOut" TextMode="MultiLine" ReadOnly="true" Wrap="false" Enabled="false"  Width="100%" Height="160px" runat="Server" />

</div>