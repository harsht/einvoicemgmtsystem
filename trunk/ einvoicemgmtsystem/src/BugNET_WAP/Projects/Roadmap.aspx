<%@ Page Language="C#" MasterPageFile="~/Shared/FullWidth.master"  Title="Road Map" AutoEventWireup="true" meta:resourcekey="Page" Inherits="BugNET.Projects.Roadmap" Codebehind="RoadMap.aspx.cs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <div class="centered-content">
	    <h1 class="page-title"><asp:literal id="Literal1" runat="server"  meta:resourcekey="TitleLabel" /> - <asp:literal id="ltProject" runat="server" /> <span>(<asp:Literal ID="litProjectCode" runat="Server"></asp:Literal>)</span> </h1>
    </div>
    <asp:repeater runat="server" id="rptRoadMap" OnItemDataBound="rptRoadMap_ItemDataBound" OnItemCreated="rptRoadMap_ItemCreated" >
		    <HeaderTemplate>
			    <table class="grid">
				    <tr>
					    <td colspan="2">
						    &nbsp;
					    </td>
					    <td runat="server" id="tdCategory" class="gridHeader"><asp:LinkButton ID="lnkCategory" Runat="server" Text="<%$ Resources:CommonTerms, Category %>" OnClick="SortCategoryClick" /> </td> 
					    <td runat="server" id="tdIssueType" class="gridHeader"><asp:LinkButton ID="LinkButton1" Runat="server" Text="<%$ Resources:CommonTerms, Type %>" OnClick="SortIssueTypeClick" /></td> 
					    <td runat="server" id="tdId" class="gridHeader"><asp:LinkButton ID="LinkButton2" Runat="server" Text="<%$ Resources:CommonTerms, Id %>"  OnClick="SortIssueIdClick" /></td> 
					    <td runat="server" id="tdTitle" class="gridHeader"><asp:LinkButton ID="LinkButton3" Runat="server" OnClick="SortTitleClick" Text="<%$ Resources:CommonTerms, Title %>"/></td>
					    <td runat="server" id="tdAssigned" class="gridHeader"><asp:LinkButton ID="LinkButton4" Runat="server" OnClick="SortAssignedUserClick" Text="<%$ Resources:CommonTerms, AssgnedTo %>"/></td>
					    <td runat="server" id="tdEstimation" class="gridHeader"><asp:LinkButton ID="LinkButton7" Runat="server" OnClick="SortEstimationClick" Text="<%$ Resources:CommonTerms, Estimation %>" /></td>
					    <td runat="server" id="tdProgress" class="gridHeader"><asp:LinkButton ID="LinkButton5" Runat="server" OnClick="SortProgressClick" Text="<%$ Resources:CommonTerms, Progress %>">Progress</asp:LinkButton></td>
					    <td runat="server" id="tdStatus" class="gridHeader"><asp:LinkButton ID="LinkButton6" Runat="server" OnClick="SortStatusClick" Text="<%$ Resources:CommonTerms, Status %>"/></td>  
				    </tr> 
		    </HeaderTemplate> 
		    <ItemTemplate>
		         <tr id="HeaderRow" runat="server" class="roadMapGroupHeader">
		            <td style="width:10px;"><asp:Image ID="img" runat="server" CssClass="icon" ImageUrl="~\images\package.gif" /></td> 
		            <td class="roadMapSummary">
		                <asp:label id="lblVersion" runat="Server"></asp:label>
		            </td>
		            <td>&nbsp;</td>
		            <td>&nbsp;</td>
		            <td>&nbsp;</td>
		            <td>&nbsp;</td>
		            <td>&nbsp;</td>
		            <td colspan="3" style="font-size:90%;text-align:right;"><asp:label id="lblProgress" runat="Server"></asp:label></td>
		         </tr>
			    <tr>
				    <td style="border:none">&nbsp;</td>
				    <td style="border:none">&nbsp;</td>
				    <td><asp:label id="lblComponent" runat="Server"></asp:label></td>
				    <td><asp:label id="lblType" runat="Server"></asp:label></td>
				    <td><a href='../Issues/IssueDetail.aspx?id=<%#DataBinder.Eval(Container.DataItem, "Id") %>'><%#DataBinder.Eval(Container.DataItem, "FullId") %></a></td>
				    <td><a href='../Issues/IssueDetail.aspx?id=<%#DataBinder.Eval(Container.DataItem, "Id") %>'><asp:label id="lblSummary" runat="Server"></asp:label></a></td>
				    <td><asp:label id="lblAssignedTo" runat="Server"></asp:label></td>
				    <td><asp:label id="EstimationLabel" runat="Server"></asp:label></td>
				    <td><asp:label id="ProgressLabel" runat="Server"></asp:label></td>
				    <td><asp:label id="lblStatus" runat="Server"></asp:label></td>
			    </tr>  
		    </ItemTemplate> 
		    <FooterTemplate>
			    </table>
		    </FooterTemplate> 
    </asp:repeater>
</asp:Content>


