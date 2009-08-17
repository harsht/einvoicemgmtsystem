<%@ Page Language="C#" MasterPageFile="~/Shared/FullWidth.Master" AutoEventWireup="true" Title="Project Calendar" meta:resourcekey="Page" CodeBehind="ProjectCalendar.aspx.cs" Inherits="BugNET.Projects.ProjectCalendar"  %>
<%@ Register TagPrefix="it" TagName="PickProject" Src="~/UserControls/PickProject.ascx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="../js/niftycube.js"></script>
    <script type="text/javascript">
        window.onload=function(){
            Nifty("div.calIssue","small transparent all");
            Nifty("div.calIssuePastDue","small transparent all");
        }
    </script> 
    <div style="margin: 0 auto;width:90%;">
        <div class="centered-content">
            <h1><asp:Label id="lblProjectName" Runat="Server" /></h1>
        </div>
        <table style="width:100%;border:0;border-collapse:collapse;">
            <tr>
                <td>
                    <asp:Label ID="ViewLabel" runat="server" Text="View:" meta:resourcekey="ViewLabel"></asp:Label>
                    <asp:DropDownList id="dropView" CssClass="standardText" AutoPostBack="True" 
                            runat="Server" OnSelectedIndexChanged="ViewSelectedIndexChanged">
	                    <asp:ListItem Text="Issue Due Dates" Value="IssueDueDates" meta:resourcekey="ListItem3" />
                    </asp:DropDownList>
                    <br />
                    <br />
                </td>
                <td style="text-align:right;">
                    <asp:Label ID="Label1" runat="server" Text="View:" meta:resourcekey="CalendarViewLabel"></asp:Label>
                     <asp:DropDownList id="dropCalendarView" CssClass="standardText" AutoPostBack="True" 
                            runat="Server" OnSelectedIndexChanged="CalendarViewSelectedIndexChanged">
	                    <asp:ListItem Text="Month" Value="Month" meta:resourcekey="ListItem1" />
	                    <asp:ListItem Text="Week" Value="Week" meta:resourcekey="ListItem2" />
                    </asp:DropDownList>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" style="background-color:#F0F0F0;height:25px;padding-left:5px;">
                    <asp:LinkButton ID="btnPrevious" runat="server" OnClick="btnPrevious_Click" meta:resourcekey="btnPrevious" Text="< Previous" ToolTip="Previous" />
                </td>
                <td align="right" style="background-color:#F0F0F0;height:25px;padding-left:5px;">      
                    <asp:LinkButton ID="btnNext" runat="server" OnClick="btnNext_Click" Text="Next >" meta:resourcekey="btnNext"  ToolTip="Next" />
                </td>
            </tr>
            <tr> 
                <td colspan="2">
                    <asp:Calendar ID="prjCalendar" Width="100%" SkinID="Calendar" OnPreRender="prjCalendar_PreRender" OnDayRender="prjCalendar_DayRender" runat="server" />                 
                </td>
            </tr>
        </table>
       
    </div>
</asp:Content>
