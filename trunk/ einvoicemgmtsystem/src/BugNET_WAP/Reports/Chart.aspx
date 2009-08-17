<%@ Page language="c#" Inherits="BugNET.Reports.Chart" Theme="" Codebehind="Chart.aspx.cs" %>
<%@ Register TagPrefix="zgw" Namespace="ZedGraph" Assembly="ZedGraph" %>
	
<form id="Form1" method="post" runat="server">
	<zgw:ZedGraphWeb id="ZedGraphWeb1" width="500" Height="375" runat="server"></zgw:ZedGraphWeb>
</form>

