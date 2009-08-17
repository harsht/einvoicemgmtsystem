<%@ Page language="c#" validateRequest="false" Inherits="BugNET.Issues.IssueDetail" MasterPageFile="~/Shared/Bug.master" Title="New Issue" Codebehind="IssueDetail.aspx.cs" meta:resourcekey="Page" AutoEventWireup="True" %>
<%@ Register TagPrefix="it" TagName="DisplayCustomFields" Src="~/UserControls/DisplayCustomFields.ascx" %>
<%@ Register TagPrefix="it" TagName="PickCategory" Src="~/UserControls/PickCategory.ascx" %>
<%@ Register TagPrefix="it" TagName="PickMilestone" Src="~/UserControls/PickMilestone.ascx" %>
<%@ Register TagPrefix="it" TagName="PickType" Src="~/UserControls/PickType.ascx" %>
<%@ Register TagPrefix="it" TagName="PickStatus" Src="~/UserControls/PickStatus.ascx" %>
<%@ Register TagPrefix="it" TagName="PickPriority" Src="~/UserControls/PickPriority.ascx" %>
<%@ Register TagPrefix="it" TagName="PickSingleUser" Src="~/UserControls/PickSingleUser.ascx" %>
<%@ Register TagPrefix="it" TagName="PickResolution" Src="~/UserControls/PickResolution.ascx" %>
<%@ Register TagPrefix="it" TagName="IssueTabs" Src="~/Issues/UserControls/IssueTabs.ascx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" runat="Server">
    <asp:Label id="lblError" ForeColor="red" EnableViewState="false" Runat="Server" />
	<div class="bug-title-box">
	    <asp:panel ID="pnlBugNavigation" Style="height:30px;display:block;" runat="server">
	        <div style="float:left;">
                <asp:Label ID="Label5" meta:resourcekey="IssueLabel" runat="server" Text="Issue:"/>
	            <span class="bug-id"><asp:label id="lblIssueNumber" Font-Bold="true" runat="server"></asp:label></span>
	        </div>
	        <div style="float:right;padding-right:10px;">
	            <span style="padding-right:5px;">
	                <asp:imagebutton ID="imgSave" OnClick="lnkSave_Click" runat="server" CssClass="icon" ImageUrl="~\images\disk.gif" />
                    <asp:LinkButton ID="lnkSave" OnClick="lnkSave_Click" runat="server" Text="<%$ Resources:CommonTerms, Save %>" />
                </span>
                <span style="padding-right:5px;padding-left:10px;border-left:1px dotted #ccc">
   	                <asp:imagebutton ID="imgDone" OnClick="lnkDone_Click" runat="server" CssClass="icon" ImageUrl="~\images\disk.gif" />
                    <asp:LinkButton ID="lnkDone" OnClick="lnkDone_Click" runat="server" meta:resourcekey="SaveReturn"  Text="Save &amp; Return" />           
                </span>
                <span style="padding-right:5px;padding-left:10px;border-left:1px dotted #ccc">
                    <asp:imagebutton ID="Imagebutton1" OnClick="CancelButtonClick" CausesValidation="false" runat="server" CssClass="icon" ImageUrl="~\images\lt.gif" />
                    <asp:LinkButton ID="LinkButton1" OnClick="CancelButtonClick" CausesValidation="false" runat="server" Text="<%$ Resources:CommonTerms, Cancel %>" />
               </span>
                <span style="padding-left:10px;padding-right:5px;border-left:1px dotted #ccc" runat="server" id="DeleteButton" visible="False">
                    <asp:imagebutton ID="imgDelete" OnClick="lnkDelete_Click"  runat="server" CssClass="icon" ImageUrl="~\images\cross.gif" />
                    <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" CausesValidation="false"  runat="server" Text="<%$ Resources:CommonTerms, Delete %>" />
                </span>  
	        </div>
	    </asp:panel>
		<div class="bug-title">  
		    <asp:Label ID="TitleLabel" runat="server" AssociatedControlID="TitleTextBox"  meta:resourcekey="TitleLabel" ></asp:Label>
		    <asp:textbox id="TitleTextBox" Width="80%" runat="server" />
		   <asp:RequiredFieldValidator ControlToValidate="TitleTextBox" Text="<%$ Resources:CommonTerms, Required %>" runat="server" id="TitleRequired" />   
		</div>
		<div>
			<span class="small-bold">
                <asp:Label ID="DateCreatedLabel" runat="server" meta:resourcekey="DateCreatedLabel"></asp:Label>
             </span>
			<asp:Label cssClass="small" id="lblDateCreated" runat="server" />
			<span class="small-bold">
                <asp:Label ID="ByLabel" runat="server"  meta:resourcekey="ByLabel" Text="By:" />
            </span>
			<asp:Label cssClass="small" id="lblReporter" ForeColor="maroon" runat="server" />
			&nbsp;&nbsp;&nbsp;
			<span class="small-bold">
			    <asp:Label ID="LastModifiedLabel" runat="server" meta:resourcekey="LastUpdateLabel" ></asp:Label>
			</span>
			<asp:Label cssClass="small" id="lblLastModified" runat="server" />
			<span class="small-bold"><asp:Label ID="Label2" runat="server"  meta:resourcekey="ByLabel" Text="By:" /></span>
			<asp:Label cssClass="small" id="lblLastUpdateUser" ForeColor="maroon" runat="server" />	
		</div>
	</div>		
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">	
    <div class="small-box" >
        <p class="small-box-right">
            <span class="title">
                <asp:Label ID="DescriptionLabel" runat="server" Text="<%$ Resources:CommonTerms, Description %>"></asp:Label>
            </span>
        </p>
    </div>
    <div class="bug-content">
        <bn:HtmlEditor id="DescriptionHtmlEditor" Height="250" runat="server" />
    </div>
    <div style="margin-top:25px;"> 
       <it:DisplayCustomFields ID="ctlCustomFields" runat="server" />
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="Server">
   <div class="small-box" >
        <p class="small-box-right">
            <span class="title">
                <asp:Label ID="DetailLabel" runat="server" Text="Details" meta:resourcekey="DetailsLabel"></asp:Label></span>
        </p>
    </div>
    <asp:panel ID="pnlEditDetails" CssClass="bug-content" runat="server">
       
        <table class="form" style="padding:10px;width:100%;">
            <tr>
                <th><asp:Label ID="CategoryLabel" AssociatedControlID="DropCategory"  meta:resourcekey="CategoryLabel" Text="Category:" runat="server" /></th>
                <td colspan="2">
                   <it:PickCategory id="DropCategory" DisplayDefault="true" Required="false" Runat="Server" />
                </td>
            </tr>
            <tr>
                <th><asp:Label ID="IssueTypeLabel" AssociatedControlID="DropIssueType" runat="server"  meta:resourcekey="IssueTypeLabel" /></th>
                <td colspan="2">
                   <it:PickType id="DropIssueType" DisplayDefault="True" Required="True" Runat="Server" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="MilestoneLabel" AssociatedControlID="DropMilestone" meta:resourcekey="MilestoneLabel" runat="server"  />
                </th>
                <td colspan="2"><it:PickMilestone id="DropMilestone" DisplayDefault="True" Runat="Server" /></td>
            </tr>          
            <tr>
                <th><asp:Label ID="StatusLabel" runat="server" AssociatedControlID="DropStatus"  meta:resourcekey="StatusLabel"/></th>
                <td colspan="2">  
                    <it:PickStatus id="DropStatus" runat="Server" Required="True" DisplayDefault="true"/>
	            </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="PriorityLabel" runat="server" AssociatedControlID="DropPriority" meta:resourcekey="PriorityLabel" Text="Priority:" /></th>
                <td colspan="2"><it:PickPriority id="DropPriority" DisplayDefault="true" Required="True" Runat="Server" /></td>
            </tr>
             <tr>
                <th> <asp:Label ID="ResolutionLabel" runat="server" AssociatedControlID="DropResolution" meta:resourcekey="ResolutionLabel" Text="Resolution:"/></th>
                <td colspan="2"><it:PickResolution id="DropResolution"  DisplayDefault="True" Runat="Server" /></td>
            </tr>
            <tr>
                <th> <asp:Label ID="OwnerLabel" runat="server" AssociatedControlID="DropOwned" meta:resourcekey="OwnedByLabel" Text="Owned By:" /></th>
                <td colspan="2">
                    <it:PickSingleUser id="DropOwned" DisplayDefault="True" Required="True" Runat="Server" />
                    <asp:checkbox id="chkNotifyOwner" runat="server" Width="50px" Text="Notify" Checked="True"></asp:checkbox>
                </td>
            </tr>
            <tr>
                <th> <asp:Label ID="AssignedToLabel" runat="server" AssociatedControlID="DropAssignedTo" meta:resourcekey="AssignedToLabel" Text="Assigned To:" /></th>
                <td colspan="2">
                    <it:PickSingleUser id="DropAssignedTo" DisplayUnassigned="False" DisplayDefault="True" Required="false" Runat="Server" />
                    <asp:checkbox id="chkNotifyAssignedTo"  Width="50px" runat="server" Text="Notify" Checked="True"></asp:checkbox>
                </td>
            </tr>
            <tr>
                 <th><asp:Label ID="Label3" runat="server" AssociatedControlID="DropOwned" meta:resourcekey="ProgressLabel" Text="Percent Complete:" /></th>
                 <td style="width:140px;">
                    <asp:TextBox ID="ProgressSlider" runat="server" Text="0" />                 
                </td>
                <td style="width:50px;text-align:left;">
                    <asp:Label ID="ProgressSlider_BoundControl" runat="server" />% 
                </td>
                <ajaxToolkit:SliderExtender ID="SliderExtender2"  runat="server"
                    TargetControlID="ProgressSlider"
                    BoundControlID="ProgressSlider_BoundControl"
                    Orientation="Horizontal" TooltipText="{0}% Complete"
                    EnableHandleAnimation="true" />
                
            </tr>
             <tr>
                <th> <asp:Label ID="PrivateLabel"  AssociatedControlID="chkPrivate" runat="server" meta:resourcekey="PrivateLabel" Text="Private:" /></th>
                <td class="input-group" colspan="2"><asp:CheckBox ID="chkPrivate" runat="server" /></td>
            </tr>	
        </table>  
    </asp:panel>
    <div class="small-box" >
        <p class="small-box-right">
            <span class="title">
                <asp:Label ID="TimeLabel" runat="server" meta:resourcekey="TimeLabel" Text="Time"></asp:Label></span>
        </p>
    </div>
    <div class="bug-content">
        <table style="padding:10px;width:100%;" class="form">
            <tr>
                <th>
                    <asp:label runat="server" AssociatedControlID="DueDate" ID="DueDateLabel" meta:resourcekey="DueDateLabel" Text="Due Date:" />
                </th>
                <td> 
                    <asp:textbox id="DueDate" Width="80" runat="server"></asp:textbox>
                    <asp:Image id="imgCalendar" runat="Server" ImageUrl="~/images/calendar.gif" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                            TargetControlID="DueDate" 
                            PopupButtonID="imgCalendar" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="EstimationLabel" runat="server" Text="Estimation:" meta:resourcekey="EstimationLabel" AssociatedControlID="txtEstimation"/></th>
                <td><asp:Textbox ID="txtEstimation" style="text-align:right;" Width="80px" runat="server" />&nbsp;<small><asp:Label
                        ID="HoursLabel" meta:resourcekey="HoursLabel" runat="server" Text="hrs"></asp:Label></small></td>
            </tr>
           <tr runat="Server" id="TimeLogged" visible="false">
                <th>
                    <asp:Label ID="LoggedLabel" runat="server" meta:resourcekey="LoggedLabel" Text="Logged:" />    
                </th>
                <td>
                    <asp:Label ID="lblLoggedTime" runat="server" style="text-align:right;" Width="85px"  />&nbsp;
                    <small><asp:Label ID="Label1" meta:resourcekey="HoursLabel" runat="server" Text="hrs" /></small>
                </td>
            </tr>
        </table>       
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="IssueTabs" runat="Server">
    <br />
    <it:IssueTabs id="ctlIssueTabs" runat="server" Visible="False"></it:IssueTabs> 
</asp:Content>
