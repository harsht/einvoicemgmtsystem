<%@ Page language="c#" Inherits="BugNET.Projects.ChangeLog" MasterPageFile="~/Shared/FullWidth.master" meta:resourcekey="Page" Title="Change Log" Codebehind="ChangeLog.aspx.cs" %>
<asp:Content ContentPlaceHolderID="Content" ID="content1" runat="server">
    <div class="centered-content">
	    <h1 class="page-title"> <asp:literal id="Literal1" runat="server"  meta:resourcekey="TitleLabel" /> - <asp:literal id="ltProject" runat="server" /> <span>(<asp:Literal ID="litProjectCode" runat="Server"></asp:Literal>)</span> </h1>
    </div>
    <asp:repeater runat="server" id="rptChangeLog" OnItemCreated="rptChangeLog_ItemCreated">
	    <HeaderTemplate>
		    <table class="grid">
			    <tr>
				    <td>
					    &nbsp;
				    </td>
				    <td runat="server" id="tdCategory" class="gridHeader"><asp:LinkButton ID="lnkCategory" Runat="server" Text="<%$ Resources:CommonTerms, Category %>" OnClick="SortCategoryClick" /></td> 
				    <td runat="server" id="tdIssueType" class="gridHeader"><asp:LinkButton ID="LinkButton1" Runat="server" OnClick="SortIssueTypeClick" Text="<%$ Resources:CommonTerms, Type %>" /></td> 
				    <td runat="server" id="tdId" class="gridHeader"><asp:LinkButton ID="LinkButton2" Runat="server" OnClick="SortIssueIdClick" Text="<%$ Resources:CommonTerms, Id %>" /></td> 
				    <td runat="server" id="tdTitle" class="gridHeader"><asp:LinkButton ID="LinkButton3" Runat="server" OnClick="SortTitleClick" Text="<%$ Resources:CommonTerms, Title %>" /></td>
				    <td runat="server" id="tdAssigned" class="gridHeader"><asp:LinkButton ID="LinkButton4" Runat="server" OnClick="SortAssignedUserClick" Text="<%$ Resources:CommonTerms, AssgnedTo %>" /></td>
				    <td runat="server" id="tdStatus" class="gridHeader"><asp:LinkButton ID="LinkButton6" Runat="server" OnClick="SortStatusClick" Text="<%$ Resources:CommonTerms, Status %>" /></td>  
			    </tr> 
	    </HeaderTemplate> 
	    <ItemTemplate>
	        <tr id="row" runat="server" class="changeLogGroupHeader">
	            <td colspan="8" style="width:80px;">
	                 <asp:Image id="img" runat="server" CssClass="icon" ImageUrl="~\images\package.gif" />&nbsp;&nbsp;<asp:label id="lblVersion" runat="Server"></asp:label>
	            </td>
	        </tr>
		    <tr>
			    <td style="width:100px;border:none;">&nbsp;</td>
			    <td><asp:label id="lblComponent" runat="Server"></asp:label></td>
			    <td><asp:label id="lblType" runat="Server"></asp:label></td>
			    <td><a href='BugDetail.aspx?bid=<%#DataBinder.Eval(Container.DataItem, "Id") %>'><%#DataBinder.Eval(Container.DataItem, "FullId") %></a></td>
			    <td><a href='BugDetail.aspx?bid=<%#DataBinder.Eval(Container.DataItem, "Id") %>'><asp:label id="lblSummary" runat="Server"></asp:label></a></td>
			    <td><asp:label id="lblAssignedTo" runat="Server"></asp:label></td>
			    <td><asp:label id="lblStatus" runat="Server"></asp:label></td>
		    </tr> 
	    </ItemTemplate> 
	    <FooterTemplate>
		    </table>
	    </FooterTemplate> 
    </asp:repeater> 
</asp:Content>