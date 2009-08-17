<%@ Page language="c#" Inherits="BugNET.Reports.ExportExcel" responseEncoding="gb2312" Codebehind="ExportExcel.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server" id="Header" >
		<title>ExportExcel</title>
	</head>
	<body>
			<form id="Form1" method="post" runat="server">
				<asp:datagrid visible="false" id="dgBugs" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 48px" runat="server"
					Width="920px" Font-Size="20px" CellPadding="1" ItemStyle-BackColor="#ffffff" CssClass="attachment"
					AlternatingItemStyle-BackColor="#eeeeee" BorderWidth="1px" AutoGenerateColumns="False" BackColor="White"
					Font-Names="SimSun" HorizontalAlign="Center" BorderColor="Black" BorderStyle="None" CellSpacing="1">
					<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
					<AlternatingItemStyle BackColor="#F7F7F7"></AlternatingItemStyle>
					<ItemStyle Font-Size="Small" Font-Names="SimSun" HorizontalAlign="Right" ForeColor="#4A3C8C"
						VerticalAlign="Middle" BackColor="#E7E7FF"></ItemStyle>
					<HeaderStyle Font-Size="30px" Font-Names="YouYuan" Font-Bold="True" Wrap="False" HorizontalAlign="Center"
						ForeColor="#F7F7F7" VerticalAlign="Middle" BackColor="#4A3C8C"></HeaderStyle>
					<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
					<Columns>
						<asp:BoundColumn DataField="ComponentName" SortExpression="ComponentName" HeaderText="Component">
							<HeaderStyle Font-Bold="True"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="TypeName" SortExpression="TypeName" HeaderText="Type">
							<HeaderStyle Font-Bold="True"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="IssueId" SortExpression="Id" HeaderText="Id">
							<HeaderStyle Font-Bold="True"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="Summary" HeaderText="Issue">
							<HeaderStyle Font-Bold="True"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="StatusName" SortExpression="StatusName" HeaderText="Status">
							<HeaderStyle Font-Bold="True"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="PriorityName" SortExpression="Priority" HeaderText="Priority">
							<HeaderStyle Font-Bold="True"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="AssignedUserDisplayName" SortExpression="Assigned" HeaderText="Assigned To">
							<HeaderStyle Font-Bold="True"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="ReporterDisplayName" SortExpression="Reporter" HeaderText="Reporter">
							<HeaderStyle Font-Bold="True"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="LastUpdate" SortExpression="LastUpdate" HeaderText="Last Update" DataFormatString="{0:MM/dd/yyyy}">
							<HeaderStyle Font-Bold="True"></HeaderStyle>
						</asp:BoundColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
				</asp:datagrid>
				<asp:DataGrid Visible="false" id="dgTimeTrack" runat="server" Width="100%" Font-Size="12px" AutoGenerateColumns="False"
					BorderWidth="0px" AlternatingItemStyle-BackColor="#eeeeee" CssClass="attachment" ItemStyle-BackColor="#ffffff"
					CellPadding="3">
					<AlternatingItemStyle BackColor="#EEEEEE"></AlternatingItemStyle>
					<ItemStyle BackColor="White"></ItemStyle>
					<Columns>
						<asp:BoundColumn DataField="WorkDate" SortExpression="WorkDate" HeaderText="Date" DataFormatString="{0:d}">
							<HeaderStyle Font-Bold="True"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="BugID" SortExpression="Id" HeaderText="Id">
							<HeaderStyle Font-Bold="True" Width="20px"></HeaderStyle>
						</asp:BoundColumn>
						<asp:HyperLinkColumn Target="_top" DataNavigateUrlField="IssueId" DataNavigateUrlFormatString="~/Bugs/BugDetail.aspx?bid={0}"
							DataTextField="Summary" SortExpression="Summary" HeaderText="Issue">
							<HeaderStyle Font-Bold="True" Width="250px"></HeaderStyle>
						</asp:HyperLinkColumn>
						<asp:BoundColumn DataField="ReporterDisplayName" SortExpression="Reporter" HeaderText="Reporter">
							<HeaderStyle Font-Bold="True"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="Duration" SortExpression="Duration" HeaderText="Duration">
							<HeaderStyle Font-Bold="True"></HeaderStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid>
				<asp:DataGrid id="dgFullBugs" visible="false" AutoGenerateColumns=False
	                border="0"
	                Width="100%"
	                CssClass="grid"
	                CellPadding="3"
	                PagerStyle-Mode="NumericPages" 
	                PagerStyle-HorizontalAlign="right"
	                PageSize="20"
	                runat="server">
	                <AlternatingItemStyle BackColor="#DDDDDD">
	                </AlternatingItemStyle>

	                <HeaderStyle Font-Bold="True">
	                </HeaderStyle>

                <Columns>
	                <asp:BoundColumn DataField="Id" SortExpression="Status" HeaderText="Id"></asp:BoundColumn>
	                <asp:BoundColumn DataField="TypeName" SortExpression="Status" HeaderText="Type"></asp:BoundColumn>
	                <asp:BoundColumn DataField="PriorityName" SortExpression="Status" HeaderText="Priority"></asp:BoundColumn>
	                <asp:BoundColumn DataField="Summary" SortExpression="Status" HeaderText="Summary"></asp:BoundColumn>
	                <asp:BoundColumn DataField="StatusName" SortExpression="Status" HeaderText="Status"></asp:BoundColumn>
	                <asp:BoundColumn DataField="AssignedDisplayName" SortExpression="Assigned" HeaderText="Assigned To"></asp:BoundColumn>
	                <asp:BoundColumn DataField="ReporterDisplayName" SortExpression="Reporter" HeaderText="Reporter"></asp:BoundColumn>
	                <asp:BoundColumn DataField="LastUpdate" SortExpression="LastUpdate" HeaderText="Last Update" DataFormatString="{0:g}"></asp:BoundColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" Mode="NumericPages">
                </PagerStyle>
                </asp:DataGrid>
		</form>
	</body>
</html>
