<%@ Register TagPrefix="it" TagName="DisplayIssues" Src="~/UserControls/DisplayIssues.ascx" %>
<%@ Register TagPrefix="it" TagName="PickQueryField" Src="~/UserControls/PickQueryField.ascx" %>
<%@ Register TagPrefix="it" TagName="PickProject" Src="~/UserControls/PickProject.ascx" %>
<%@ Page Language="c#" CodeBehind="QueryDetail.aspx.cs" AutoEventWireup="True" MasterPageFile="~/Shared/FullWidth.master" Title="Query Detail" Inherits="BugNET.Queries.QueryDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
	<table class="contentTableShort">
		<tr>
			<td class="contentCell">
				<table class="queryList" CellPadding="4" CellSpacing="0">
					<tr>
						<td>
						    <asp:label ID="lblProject" meta:resourcekey="lblProject"  runat="server" />
						</td>
						<td>
						    <asp:Label ID="lblProjectName" runat="server" />
						</td>
					</tr>
				</table>
				<br/>
				<br/>
				<table class="queryList" cellpadding="4" cellspacing="0">
					<asp:PlaceHolder id="plhClauses" Runat="Server" />
				</table>
				<br/>
				<asp:Button id="btnAddClause" Text="Add Clause" meta:resourcekey="btnAddClause" CssClass="standardText" Runat="Server" onclick="btnAddClauseClick" />
				<asp:Button id="btnRemoveClause" Text="Remove Clause" CssClass="standardText" meta:resourcekey="btnRemoveClause" Runat="Server" onclick="btnRemoveClauseClick" />
				<asp:CheckBox ID="chkGlobalQuery" meta:resourcekey="chkGlobalQuery" runat="server" Text="All all users to run this query" />
				<hr/>
				<br />
				<br />
				<asp:TextBox id="txtQueryName" Runat="Server" />
				&nbsp;
				<asp:Button id="btnSaveQuery" Text="Save Query"  meta:resourcekey="btnSaveQuery" CssClass="standardText" Runat="Server" onclick="btnSaveQueryClick" />
				&nbsp;
				<asp:Button id="btnPerformQuery" Text="Perform Query" meta:resourcekey="btnPerformQuery" CssClass="standardText" Runat="Server" onclick="btnPerformQueryClick" />
				<asp:Label id="lblSaveError" ForeColor="Red" EnableViewState="false" Runat="Server" />
				<asp:Label id="lblResult" Runat="Server" />
			</td>
		</tr>
	</table>
	<br/>
	<br/>
	<table class="contentTableShort">
		<tr>
			<td class="contentCell">
				<h1>Results:</h1>
				<asp:Label id="lblError" ForeColor="red" EnableViewState="false" Runat="Server" />
				<it:DisplayIssues id="ctlDisplayIssues" Runat="Server" />
			</td>
		</tr>
	</table>
</asp:Content>
