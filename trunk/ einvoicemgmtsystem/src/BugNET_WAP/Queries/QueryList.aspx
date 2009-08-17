<%@ Page Language="c#" CodeBehind="QueryList.aspx.cs" AutoEventWireup="True" MasterPageFile="~/Shared/FullWidth.master" Title="Query List" Inherits="BugNET.Queries.QueryList" %>
<%@ Register TagPrefix="it" TagName="PickProject" Src="~/UserControls/PickProject.ascx" %>
<%@ Register TagPrefix="it" TagName="PickQuery" Src="~/UserControls/PickQuery.ascx" %>
<%@ Register TagPrefix="it" TagName="DisplayIssues" Src="~/UserControls/DisplayIssues.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
	<table class="contentTableShort">
		<tr>
			<td class="contentCell">
				<table width="100%" cellspacing="10">
					<tr>
						<td align="right">
							<asp:Button Text="Create New Query" meta:resourcekey="btnAddQuery" Class="standardText" Runat="server" id="btnAddQuery" onclick="AddQuery" />
							<asp:Button Text="Delete Query" meta:resourcekey="btnDeleteQuery" Class="standardText" Runat="server" id="btnDeleteQuery" onclick="DeleteQuery" />
						</td>
					</tr>
				</table>
				<table  class="queryList" CellPadding="4" CellSpacing="0">
					<tr>
						<td>
							Query:
						</td>
						<td>
							<it:PickQuery id="dropQueries" CssClass="standardText" DisplayDefault="true" Runat="Server" />
						</td>
						<td>
							<asp:Button id="btnPerformQuery" Text="Perform Query" meta:resourcekey="btnPerformQuery" CssClass="standardText" Runat="Server" onclick="btnPerformQueryClick" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<br>
	<br>
	<table class="contentTableShort">
		<tr>
			<td class="contentCell">
				<h1><asp:Literal ID="Literal1" runat="server" meta:resourcekey="Results" /></h1>
				<asp:Label id="lblError" ForeColor="red" EnableViewState="false" Runat="Server" />
				<it:DisplayIssues id="ctlDisplayIssues" Runat="Server" />
			</td>
		</tr>
	</table>
</asp:content>
