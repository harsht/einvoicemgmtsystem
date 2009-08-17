<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IssueTabs.ascx.cs" Inherits="BugNET.Issues.UserControls.IssueTabs" %>
<asp:DataList id="lstTabs" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal" 
    OnItemDataBound="lstTabs_ItemDataBound" OnItemCommand="lstTabs_ItemCommand" Runat="Server">
	<ItemStyle CssClass="adminTabInactive" />
	<SelectedItemStyle CssClass="adminTabActive" />
	<ItemTemplate>
        <asp:Image ID="TabIcon" CssClass="icon" runat="server" />&nbsp;<asp:LinkButton id="lnkTab" CausesValidation="false" Runat="Server" />
	</ItemTemplate>
	<SelectedItemTemplate>
		<asp:Image ID="TabIcon" CssClass="icon" runat="server" />&nbsp;<asp:LinkButton id="lnkTab" CausesValidation="false" Runat="Server" />
	</SelectedItemTemplate>
</asp:DataList>
<table class="contentTableShort">
	<tr>
		<td class="contentCell">
			<asp:PlaceHolder id="plhContent" Runat="Server" />
			<p>&nbsp;</p>
		</td>
	</tr>
</table>