<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RelatedIssues.ascx.cs" Inherits="BugNET.Issues.UserControls.RelatedIssues" %>
<p style="margin-bottom:1em">
	<asp:label ID="lblDescription" meta:resourcekey="lblDescription"  runat="server" Text="List all issues on which this issue is related. Enter the ID of the issue in the 
	text box below and click the Add Related Issue button."></asp:label>	
	
</p>
<BN:Message ID="RelatedIssuesMessage" runat="server" />
<asp:Label ID="RelatedIssuesLabel"  Font-Italic="true" runat="server"></asp:Label> 
<asp:Datagrid  runat="server" ID="RelatedIssuesDataGrid"  SkinID="DataGrid" EnableViewState="true" OnItemDataBound="grdIssueItemDataBound" OnItemCommand="RelatedIssuesDataGrid_ItemCommand">
    <columns>
		<asp:templatecolumn SortExpression="IssueId" HeaderText="<%$ Resources:CommonTerms, Id %>">
			<itemtemplate>
				&nbsp;
				<asp:Label id="IssueIdLabel" Runat="Server" />
			</itemtemplate>
		</asp:templatecolumn>
		<asp:hyperlinkcolumn DataNavigateUrlField="IssueId" DataNavigateUrlFormatString="~/Issues/IssueDetail.aspx?id={0}"
			DataTextField="Title" SortExpression="IssueTitle" HeaderText="<%$ Resources:CommonTerms, IssueHeader %>">
		</asp:hyperlinkcolumn>
		<asp:templatecolumn>
			<itemtemplate>
				<asp:Button id="btnDelete" Text="<%$ Resources:CommonTerms, Delete %>" CausesValidation="false" CssClass="standardText" Runat="Server" />
			</itemtemplate>
		</asp:templatecolumn>
	</columns>
</asp:Datagrid>	

<asp:panel id="AddRelatedIssuePanel" runat="server" style="padding:15px 15px 15px 0px;">
    <h5 class="bug-tab-title"><asp:label ID="lblAddRelatedIssue" runat="Server" meta:resourcekey="lblAddRelatedIssue"></asp:label></h5>
    <asp:label id="Label2" runat="server" Visible="False" ForeColor="Red"></asp:label>
    <table class="form" style="width:600px;font-size:11px;">
        <tr>
            <th><asp:Label ID="lblRelatedIssue" AssociatedControlID="txtRelatedIssue" runat="server"  Text="<%$ Resources:CommonTerms, IssueId %>" /></th>
            <td><asp:textbox id="txtRelatedIssue" Width="70" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtRelatedIssue" SetFocusOnError="True" ValidationGroup="AddRelatedIssue"  runat="server" ErrorMessage=" *"></asp:RequiredFieldValidator>
            <asp:CompareValidator ControlToValidate="txtRelatedIssue" Operator="DataTypeCheck" Type="Integer" Text="(integer)"
		        Runat="server" id="CompareValidator1" />
            </td>
        </tr>
    </table>       
    <div class="bug-tab-buttons">
        <asp:Imagebutton runat="server" id="AddRelatedIssue" ValidationGroup="AddRelatedIssue" OnClick="cmdAddRelatedIssue_Click" CssClass="icon" ImageUrl="~/images/add.gif" />
        <asp:LinkButton ID="cmdAddRelatedIssue" CausesValidation="True" ValidationGroup="AddRelatedIssue" OnClick="cmdAddRelatedIssue_Click" runat="server" meta:resourcekey="lblAddRelatedIssue" />
    </div>
</asp:panel>