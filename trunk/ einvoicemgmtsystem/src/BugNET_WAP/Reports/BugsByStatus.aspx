<%@ Page Language="C#" MasterPageFile="~/Shared/Default.master" AutoEventWireup="true" Inherits="Reports_BugReport" Title="Untitled Page" Codebehind="BugsByStatus.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Banner" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <rsweb:ReportViewer ShowPrintButton="True" DocumentMapCollapsed="true" DocumentMapWidth="0"
      ID="ReportViewer1" runat="server"   Font-Names="Verdana" Font-Size="8pt" Width="100%" >
        <LocalReport  ReportPath="Reports\BugsByStatus.rdlc" EnableExternalImages="True" EnableHyperlinks="True">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ReportDataSource" Name="Bug" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ReportDataSource" runat="server" SelectMethod="GetBugsByProjectId"
        TypeName="BugNET.BusinessLogicLayer.Bug">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="1" Name="projectId" QueryStringField="projectId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
</asp:Content>

