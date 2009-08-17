<%@ Page Language="C#" MasterPageFile="~/Shared/FullWidth.master" AutoEventWireup="true" Inherits="BugNET.Error" Title="Error" Codebehind="Error.aspx.cs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <h4>Application Error - <%=Request.QueryString["aspxerrorpath"]%></h4>
    <p>An error has occured in processing your request.</p> 
    <p>Please <a href='<%=Page.ResolveUrl("~/Default.aspx")%>'>try again.</a></p>
</asp:Content>


