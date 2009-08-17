<%@ Reference Page="~/reports/chart.aspx" %>
<%@ Page language="c#" Inherits="BugNET.Reports.ProjectStatus" Theme="" Codebehind="ProjectStatus.aspx.cs" %>

<!doctype html public "-//w3c//dtd html 4.0 transitional//en" >
<html>
  <head>
    <title>BugNET - Project Status / Summary Charts</title>
  </head>
  <body>
    <form id="Form1" method="post" runat="server">
			<div style="width:900px">
				<div class="row" style="padding-bottom:20px;">
					<asp:dropdownlist id="ChartList" runat="Server">
						<asp:listitem>Issues by Status</asp:listitem>
						<asp:listitem>Open Issues by Priority</asp:listitem>
						<asp:listitem>Open Issues by Version</asp:listitem>
					</asp:dropdownlist>
					<asp:button id="ViewChart" runat="server" Text="View" cssclass="button"></asp:button>
				</div>	
				<asp:image id="Chart" Width="500px" ImageUrl="Chart.aspx" runat="server"></asp:image>
			</div>			
     </form>	
  </body>
</html>
