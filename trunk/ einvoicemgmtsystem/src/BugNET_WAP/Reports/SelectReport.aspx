<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectReport.aspx.cs" MasterPageFile="~/Shared/FullWidth.master" Inherits="BugNET.Reports.SelectReport"  Title="Reports" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<div class="centered-content">
    <h1 class="page-title">Reports</h1>
</div>
<div class="row">
  <asp:Label ID="Label1" AssociatedControlID="Reports" cssclass="col1" runat="Server">Report:</asp:Label>
  <span class="col2b">
    <asp:DropDownList ID="Reports" runat="server">
        <asp:ListItem Text="Issues By Status" Value="1" />
        <asp:ListItem Text="Project Status / Summary Charts" Value="2" />
        <asp:ListItem Text="Project Time Tracking" Value="3"/>
        <asp:ListItem Text="Project User Time Tracking" Value="4"/>
    </asp:DropDownList>
    <asp:ImageButton ID="btnSelectReport" runat="server" OnClick="SelectReport_Click" ImageUrl="~/images/page_white_go.gif"/>
   </span>
</div>   
<asp:panel id="SelectUserPanel" CssClass="row" Visible="False" runat="Server">
	<asp:Label ID="Label2" AssociatedControlID="SelectUser" cssclass="col1" runat="Server">Select User:</asp:Label>
	<span class="col2">
	    <asp:dropdownlist id="SelectUser" runat="Server"></asp:dropdownlist>
	</span>
	<br />
	<br />
	<asp:button cssclass="button" id="ViewReport" OnClick="ViewReport_Click" runat="Server" Text="View Report"></asp:button>
</asp:panel>
  <br />
  <br /> 
     <rsweb:ReportViewer 
        ShowPrintButton="True"
        ID="ReportViewer1" 
        runat="server"   
        SizeToReportContent="true" 
        Font-Names="Verdana" 
        Font-Size="8pt"
        ShowDocumentMapButton="false"
        ShowZoomControl="false">
      </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="BugsByStatusDataSource" OnSelecting="BugsByStatusDataSource_Selecting" runat="server" SelectMethod="GetBugsByProjectId"
        TypeName="BugNET.BusinessLogicLayer.Bug">
        <SelectParameters>
            <asp:QueryStringParameter Name="projectId" QueryStringField="projectId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
</asp:Content>