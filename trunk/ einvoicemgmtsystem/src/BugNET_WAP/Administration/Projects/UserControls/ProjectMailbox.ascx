<%@ Register TagPrefix="it" TagName="PickType" Src="~/UserControls/PickType.ascx" %>
<%@ Register TagPrefix="it" TagName="PickSingleUser" Src="~/UserControls/PickSingleUser.ascx" %>
<%@ Control Language="c#" Inherits="BugNET.Administration.Projects.UserControls.Mailboxes" Codebehind="ProjectMailbox.ascx.cs" %>
<div class="myform">
    <h4>Mailboxes</h4>
    <p class="desc"><asp:label id="lblMailboxes" runat="server" visible="false" /></p>
    <asp:datagrid id="dtgMailboxes" runat="server" cssClass="gen-table" 
        CellPadding="3" GridLines="None" BorderWidth="0" autogeneratecolumns="False">
		<AlternatingItemStyle CssClass="gen-alt"></AlternatingItemStyle>
		<HeaderStyle CssClass="gen-header"></HeaderStyle>
		<Columns>
			<asp:BoundColumn DataField="Mailbox" HeaderText="Mailbox"></asp:BoundColumn>
			<asp:BoundColumn DataField="AssignToName" HeaderText="Assign To"></asp:BoundColumn>
			<asp:BoundColumn DataField="IssueTypeName" HeaderText="Issue Type"></asp:BoundColumn>
			<asp:ButtonColumn Text="Delete" CommandName="Delete"></asp:ButtonColumn>
		</Columns>
	</asp:datagrid>
	<h4>New Mailbox</h4>
	<table class="form" border="0" summary="new issue form table">
        <tr>
            <th><asp:Label ID="label1" runat="server" AssociatedControlID="txtMailbox" Text="Mailbox:" /></th>
            <td><asp:textbox id="txtMailbox" runat="server" enableviewstate="false"></asp:textbox> <asp:RequiredFieldValidator id="reqVal" Display="dynamic" ControlToValidate="txtMailBox" Text=" *"
	        Runat="Server" /></td>
        </tr>
          <tr>
            <th><asp:Label ID="label2" runat="server" Text="Assign To:" /></th>
            <td><it:picksingleuser id="IssueAssignedUser"   Runat="Server" Required="true" DisplayDefault="true"></it:picksingleuser></td>
        </tr>
          <tr>
            <th><asp:Label ID="label3" runat="server" Text="Type:" /></th>
            <td><it:picktype id="IssueAssignedType" DisplayLabel="True" Runat="Server" Required="true" DisplayDefault="true"></it:picktype></td>
        </tr>
     </table>   
<div >
    <asp:imagebutton runat="server" id="save" CssClass="icon" OnClick="btnAdd_Click"  ImageUrl="~/Images/email_add.gif" />
    <asp:linkbutton CausesValidation="true" runat="server" id="btnAdd" OnClick="btnAdd_Click" Text="Add Mailbox" />
</div>
</div>
