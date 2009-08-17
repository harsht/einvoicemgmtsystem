<%@ Control Language="c#" Inherits="BugNET.UserControls.DisplayIssues" Codebehind="DisplayIssues.ascx.cs" %>
<%@ Register TagPrefix="it" TagName="TextImage" Src="~/UserControls/TextImage.ascx" %>
<%@ Register TagPrefix="it" TagName="PickCategory" Src="~/UserControls/PickCategory.ascx" %>
<%@ Register TagPrefix="it" TagName="PickMilestone" Src="~/UserControls/PickMilestone.ascx" %>
<%@ Register TagPrefix="it" TagName="PickType" Src="~/UserControls/PickType.ascx" %>
<%@ Register TagPrefix="it" TagName="PickStatus" Src="~/UserControls/PickStatus.ascx" %>
<%@ Register TagPrefix="it" TagName="PickPriority" Src="~/UserControls/PickPriority.ascx" %>
<%@ Register TagPrefix="it" TagName="PickSingleUser" Src="~/UserControls/PickSingleUser.ascx" %>
<%@ Register TagPrefix="it" TagName="PickResolution" Src="~/UserControls/PickResolution.ascx" %>

<script type="text/javascript">
        function SelectAll(id)
        {
            // use $get instead of document.getElementById if AJAX client-side framework is currently loaded.
            var selectAllCheckBox = document.getElementById(id);

            // I personally don't like adding client ids this way because it means this JavaScript
            // has to be rendered on the page as opposed to being cached in an external file.
            // I find it better to register a client-side script block on the page load or in the case
            // of ASP.NET AJAX enabled pages in the pre render phase.

            // Get a reference of GridView control
            var grid = document.getElementById("<%= gvIssues.ClientID %>");

            // Existentce check of objects to see if we can proceed.
            if (selectAllCheckBox && grid)
            {
                var inputs = grid.getElementsByTagName("input")

                // inputs.length > 0 is not necessary, just use inputs.length as your conditional
                // 0 results in false, so the > 0 is redundant.
                if (inputs.length)
                {
                    //loop starts from 1. rows[0] points to the header.
                    for (i=1; i<inputs.length; i++)
                    {
                        //if childNode type is CheckBox
                        if (inputs[i].type =="checkbox")
                        {
                            inputs[i].checked = selectAllCheckBox.checked;
                        }
                    }
                }
            }
        }
        
        
        function toggle_visibility(id) {
           var e = document.getElementById(id);
           if(e.style.display == 'block')
              e.style.display = 'none';
           else
              e.style.display = 'block';
        }

    </script>
   

<asp:panel ID="BulkEditPanel" runat="server" style="height:auto;background-color:#F1F2EC;padding-top:5px;padding-left:7px;padding-right:7px;width:99%;border-bottom:1px solid #F1F2EC;">
    <div style="height:25px;background-color:#F1F2EC;width:100%;">
        <div style="float:left;background-color:#F1F2EC;padding-bottom:5px;width:auto;">
            For Selected:&nbsp;
            
            <span id="EditIssueProperties">
                <a href="#" onclick="toggle_visibility('SetProperties')">
                    <asp:Label ID="EditPropertiesLabel" meta:resourcekey="EditPropertiesLabel" Text="Test" runat="server"></asp:Label>
                </a>
            </span>
        </div>
        <div style="float:right;background-color:#F1F2EC;padding-bottom:5px;width:auto;" >
            <p id="AddRemoveColumns">
                <asp:LinkButton Text="Add / remove columns" Runat="server" id="SelectColumnsLinkButton" 
                    OnClick="SelectColumnsClick" meta:resourcekey="SelectColumnsLinkButton" />
            </p>
            <p id="ExportExcel">           
                <asp:LinkButton ID="ExportExcelButton" runat="server" OnClick="ExportExcelButton_Click">Export</asp:LinkButton>
            </p>
            <p id="Rss">
                <asp:HyperLink ID="lnkRSS" runat="server">RSS</asp:HyperLink>
            </p>
        </div> 
    </div>  
    <div id="SetProperties" style="clear:both;display:none;width:100%;background-color:#FFFAF6;margin-bottom:10px;">
        <div style="margin:0px 10px 10px 10px;padding:10px 0 5px 0;border-bottom:1px dotted #cccccc;">
            <asp:Label ID="Label10" runat="server" meta:resourcekey="SetPropertiesLabel" ></asp:Label></div>
        <div style="margin-left:10px;margin-right:10px;padding-bottom:10px;">
            <table width="90%" style="color:#55555F;">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:CommonTerms, Category %>"></asp:Label>:</td>
                    <td>
                        <it:PickCategory ID="dropCategory" DisplayDefault="true" Required="false" runat="server" />
                    </td>
                    <td><asp:Label ID="Label2" runat="server" Text="<%$ Resources:CommonTerms, Owner %>"></asp:Label>:</td>
                    <td>
                        <it:PickSingleUser ID="dropOwner"  DisplayDefault="true" Required="false" runat="server" />
                    </td>
                    <td><asp:Label ID="Label3" runat="server" Text="<%$ Resources:CommonTerms, Milestone %>"></asp:Label>:</td>
                    <td>
                        <it:PickMilestone ID="dropMilestone"  DisplayDefault="true" Required="false" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label4" runat="server" Text="<%$ Resources:CommonTerms, Type %>"></asp:Label>:</td>
                    <td><it:PickType ID="dropType"  DisplayDefault="true" Required="false" runat="server" /></td>
                    <td><asp:Label ID="Label5" runat="server" Text="<%$ Resources:CommonTerms, Resolution %>"/>:</td>
                    <td><it:PickResolution id="dropResolution" DisplayDefault="true" Required="false" runat="server"/></td>
                    <td><asp:Label ID="Label6" runat="server" Text="<%$ Resources:CommonTerms, Priority %>"/>:</td>
                    <td>                    
                        <it:PickPriority ID="dropPriority"  DisplayDefault="true" Required="false" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label7" runat="server" Text="<%$ Resources:CommonTerms, Status %>"/>:</td>
                    <td><it:PickStatus ID="dropStatus"  DisplayDefault="true" Required="false" runat="server" /></td> 
                    <td><asp:Label ID="Label8" runat="server" Text="<%$ Resources:CommonTerms, Assigned %>"/>:</td>
                    <td><it:PickSingleUser ID="dropAssigned"  DisplayDefault="true" Required="false" runat="server" /></td> 
                    <td><asp:Label ID="Label9" runat="server" Text="<%$ Resources:CommonTerms, DueDate %>"/>:</td>
                    <td>
                        <asp:textbox id="DueDate" Width="80" runat="server"></asp:textbox>
                        <asp:Image id="imgCalendar" runat="Server" ImageUrl="~/images/calendar.gif" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                            TargetControlID="DueDate" 
                            PopupButtonID="imgCalendar" />
                    </td> 
                </tr>
            </table>
            <br />
            <asp:Button ID="SaveIssues" runat="server" OnClick="SaveIssues_Click" Text="<%$ Resources:CommonTerms, Save %>" />
            <input type="button" id="CancelEditProperties" onclick="toggle_visibility('SetProperties')" runat="server" value="<%$ Resources:CommonTerms, Cancel%>" />
        </div>
    </div>        
    <asp:Panel id="pnlSelectColumns" Visible="False" style="width:100%;background-color:#FFFAF6;margin-bottom:10px;display:block;" runat="Server">
	  <div style="margin:0px 10px 10px 10px;padding:10px 0 5px 0;border-bottom:1px dotted #cccccc;">
          <asp:Literal ID="Literal3" runat="server" meta:resourcekey="SelectColumnsLiteral"></asp:Literal></div>
	  <div style="margin-left:10px;margin-right:10px;padding-bottom:10px;" >
	    <asp:CheckBoxList id="lstIssueColumns" Runat="server" 
            RepeatDirection="Horizontal">
		    <asp:ListItem Text="<%$ Resources:CommonTerms, Category %>" Value="4" />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, Creator %>" Value="5" />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, Owner %>" Value="6" />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, Assigned %>" Value="7" />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, Type %>" Value="8" />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, Milestone %>" Value="9" />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, Status %>" Value="10" />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, Priority %>" Value="11" />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, Resolution %>" Value="12" />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, DueDate %>" Value="13"  />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, Estimation %>" Value="14"  />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, Progress %>" Value="15"  />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, TimeLogged %>" Value="16"  />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, Created %>" Value="17" />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, LastUpdate %>" Value="18" />
		    <asp:ListItem Text="<%$ Resources:CommonTerms, LastUpdateUser %>" Value="19"  />
		    
	    </asp:CheckBoxList>
	    <br />
	    <asp:Button id="SaveButton" Runat="server" Text="<%$ Resources:CommonTerms, Save %>" CssClass="standardText" 
            OnClick="SaveClick"></asp:Button>&nbsp;&nbsp;
	    <asp:Button id="CancelButton" Runat="server" CssClass="standardText" 
            OnClick="CancelClick" Text="<%$ Resources:CommonTerms, Cancel %>"></asp:Button>
		</div>
    </asp:Panel>
</asp:Panel>
<asp:GridView ID="gvIssues" SkinID="GridView" AllowPaging="True"
    AllowSorting="True"
    DataKeyNames="Id"
    PagerStyle-HorizontalAlign="right"
    OnRowCommand="gvIssues_RowCommand"
    OnRowCreated="gvIssues_RowCreated"
    OnRowDataBound="gvIssues_RowDataBound"
    OnSorting="gvIssues_Sorting"
    OnPageIndexChanging="gvIssues_PageIndexChanging"
    PageSize="10" runat="server">
    <Columns>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="center"   />
		    <HeaderStyle HorizontalAlign="center" />
	        <HeaderTemplate>
                <asp:CheckBox ID="cbSelectAll" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="cbSelectAll" runat="server" />
            </ItemTemplate>	 
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle HorizontalAlign="center"  />
		    <HeaderStyle HorizontalAlign="center"/>
		    <ItemTemplate>
		        <asp:image runat="server" id="imgPrivate" meta:resourcekey="imgPrivate" AlternateText="Private" ImageUrl="~/images/lock.gif" ></asp:image>
		    </ItemTemplate>		 
        </asp:TemplateField>
        <asp:BoundField DataField="FullId" HeaderText="<%$ Resources:CommonTerms, Id %>" 
            SortExpression="Id" ItemStyle-Wrap="false" >
            <ItemStyle Wrap="False"></ItemStyle>
        </asp:BoundField>
        <asp:HyperLinkField HeaderStyle-HorizontalAlign="Left" HeaderText="<%$ Resources:CommonTerms, Title %>" 
            SortExpression="Title" 
            DataNavigateUrlFormatString="~/Issues/IssueDetail.aspx?id={0}" 
            DataNavigateUrlFields="Id" DataTextField="Title">
            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        </asp:HyperLinkField>
        <asp:templatefield SortExpression="Category" HeaderText="<%$ Resources:CommonTerms, Category %>" Visible="false">           
            <itemstyle horizontalalign="Left" cssclass="gridItem"></itemstyle>
            <itemtemplate>
                &nbsp;<%# DataBinder.Eval(Container.DataItem, "CategoryName")%>
            </itemtemplate>
        </asp:templatefield>
        <asp:templatefield SortExpression="Creator" HeaderText="<%$ Resources:CommonTerms, Creator %>"  Visible="false">
            <itemstyle horizontalalign="Left" cssclass="gridItem"></itemstyle>
            <itemtemplate>
                &nbsp;<%# DataBinder.Eval(Container.DataItem, "CreatorDisplayName")%>
            </itemtemplate>
        </asp:templatefield>
        <asp:templatefield SortExpression="Owner" HeaderText="<%$ Resources:CommonTerms, Owner %>" Visible="false" >
            <itemstyle horizontalalign="Left"></itemstyle>
            <itemtemplate>
                &nbsp;<%# DataBinder.Eval(Container.DataItem, "OwnerDisplayName")%>
            </itemtemplate>
        </asp:templatefield>
        <asp:templatefield SortExpression="Assigned" HeaderText="<%$ Resources:CommonTerms, Assigned %>" Visible="false">
            <itemstyle horizontalalign="Left"></itemstyle>
            <itemtemplate>
                &nbsp;<%# DataBinder.Eval(Container.DataItem, "AssignedDisplayName" )%>
            </itemtemplate>
        </asp:templatefield>
        <asp:templatefield SortExpression="IssueType" HeaderText="<%$ Resources:CommonTerms, Type %>" Visible="false">
            <ItemStyle HorizontalAlign="center" />
		    <HeaderStyle HorizontalAlign="center" />
            <itemtemplate>
                &nbsp;<it:TextImage id="ctlType" ImageDirectory="/IssueType" 
                    Text='<%# DataBinder.Eval(Container.DataItem, "IssueTypeName" )%>' 
                    ImageUrl='<%# DataBinder.Eval(Container.DataItem, "IssueTypeImageUrl" )%>' Runat="server" />
            </itemtemplate>
        </asp:templatefield> 
        <asp:templatefield SortExpression="Milestone" HeaderText="<%$ Resources:CommonTerms, Milestone %>" Visible="false">
            <ItemStyle HorizontalAlign="center" />
		    <HeaderStyle HorizontalAlign="center" />
            <itemtemplate>
                &nbsp;<it:TextImage id="ctlMilestone" ImageDirectory="/Milestone" 
                    Text='<%# DataBinder.Eval(Container.DataItem, "MilestoneName" )%>' 
                    ImageUrl='<%# DataBinder.Eval(Container.DataItem, "MilestoneImageUrl" )%>'  
                    Runat="server" />
            </itemtemplate>
        </asp:templatefield>
        <asp:templatefield SortExpression="Status" HeaderText="<%$ Resources:CommonTerms, Status %>" Visible="false">
            <ItemStyle HorizontalAlign="center" />
		    <HeaderStyle HorizontalAlign="center" />
            <itemtemplate>
                &nbsp;<it:TextImage id="ctlStatus" ImageDirectory="/Status" 
                    Text='<%# DataBinder.Eval(Container.DataItem, "StatusName" )%>' 
                    ImageUrl='<%# DataBinder.Eval(Container.DataItem, "StatusImageUrl" )%>' Runat="server" />
            </itemtemplate>
        </asp:templatefield>     
        <asp:templatefield SortExpression="Priority" HeaderText="<%$ Resources:CommonTerms, Priority %>" Visible="false">
            <ItemStyle HorizontalAlign="center" />
		    <HeaderStyle HorizontalAlign="center" />
            <itemtemplate>
                &nbsp;<it:TextImage id="ctlPriority" ImageDirectory="/Priority" 
                    Text='<%# DataBinder.Eval(Container.DataItem, "PriorityName" )%>' 
                    ImageUrl='<%# DataBinder.Eval(Container.DataItem, "PriorityImageUrl" )%>' 
                    Runat="server" />
            </itemtemplate>
        </asp:templatefield>        
         <asp:templatefield SortExpression="Resolution" HeaderText="<%$ Resources:CommonTerms, Resolution %>" Visible="false">
            <ItemStyle HorizontalAlign="center" />
		    <HeaderStyle HorizontalAlign="center" />
            <itemtemplate>
                &nbsp;<it:TextImage id="ctlResolution" ImageDirectory="/Resolution"  
                    Text='<%# DataBinder.Eval(Container.DataItem, "ResolutionName" )%>'
                    ImageUrl='<%# DataBinder.Eval(Container.DataItem, "ResolutionImageUrl" )%>'
                    Runat="server" />
            </itemtemplate>
        </asp:templatefield>   
        <asp:TemplateField HeaderText="<%$ Resources:CommonTerms, DueDate%>" SortExpression="DueDate" Visible="false">
            <ItemTemplate>
                <%#(DateTime)Eval("DueDate") == DateTime.MinValue ? "none" : Eval("DueDate", "{0:d}") %>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="<%$ Resources:CommonTerms, EstimationHours %>" SortExpression="Estimation" Visible="false">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Estimation")%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:templatefield SortExpression="Progress" HeaderText="<%$ Resources:CommonTerms, Progress %>" Visible="false">
		   <ItemStyle HorizontalAlign="center" />
		    <HeaderStyle HorizontalAlign="center" />
            <itemtemplate>
                <div style="border:1px solid #ccc;width:100px;height:7px;margin:5px;text-align:left;">
                    <div id="ProgressBar" runat='server' style="text-align:left;background-color:#C4EFA1;height:7px;" >&nbsp;</div>
                </div>
            </itemtemplate>
        </asp:templatefield>
          <asp:TemplateField HeaderText="<%$ Resources:CommonTerms, TimeLoggedHours %>" SortExpression="TimeLogged" Visible="false">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TimeLogged")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:templatefield SortExpression="Created" HeaderText="<%$ Resources:CommonTerms, Created %>">
            <itemtemplate>
                &nbsp;<%# DataBinder.Eval(Container.DataItem, "DateCreated", "{0:d}")%>
            </itemtemplate>
        </asp:templatefield>
       <asp:templatefield SortExpression="LastUpdate" HeaderText="<%$ Resources:CommonTerms, LastUpdate %>">
            <itemtemplate>
                &nbsp;<%# DataBinder.Eval(Container.DataItem, "LastUpdate", "{0:d}")%>
            </itemtemplate>
        </asp:templatefield>
        <asp:templatefield SortExpression="LastUpdateUser" HeaderText="<%$ Resources:CommonTerms, LastUpdateUser %>"> 
            <itemtemplate>
                &nbsp;<%# DataBinder.Eval(Container.DataItem, "LastUpdateDisplayName")%>
            </itemtemplate>
        </asp:templatefield>
        
    </Columns>
     <PagerStyle HorizontalAlign="Right" VerticalAlign="Bottom" /> 
    <PagerTemplate>
        <asp:ImageButton ID="btnFirst" runat="server"
            ImageUrl='<%#gvIssues.PagerSettings.FirstPageImageUrl%>'   CommandArgument="First" ImageAlign="AbsBottom" CommandName="Page"/>
        <asp:ImageButton ID="btnPrevious" runat="server"
            ImageUrl='<%#gvIssues.PagerSettings.PreviousPageImageUrl%>'  CommandArgument="Prev"  ImageAlign="AbsBottom" CommandName="Page" />
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:CommonTerms, Page %>" />
        <asp:DropDownList ID="ddlPages" runat="server" OnSelectedIndexChanged="ddlPages_SelectedIndexChanged"
            AutoPostBack="True">
        </asp:DropDownList> <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:CommonTerms, Of %>" /> <asp:Label ID="lblPageCount"
            runat="server"></asp:Label>
        <asp:ImageButton ID="btnNext" runat="server"
            ImageUrl='<%#gvIssues.PagerSettings.NextPageImageUrl%>'  CommandArgument="Next" CommandName="Page"  ImageAlign="AbsBottom" />
        <asp:ImageButton ID="btnLast" runat="server"
            ImageUrl='<%#gvIssues.PagerSettings.LastPageImageUrl%>'  CommandArgument="Last" CommandName="Page"  ImageAlign="AbsBottom" />
    </PagerTemplate>
</asp:GridView>
<div style="width:100%;padding-top:10px">
    <asp:label id="lblResults" ForeColor="Red" Font-Italic="True" runat="server" Text="<%$ Resources:CommonTerms, NoIssueResults %>" />
</div>
<table id="tblOptions" width="100%" visible="false" runat="server">
	<tr>
		<td>
			
		</td>
		<td align="right">
			<asp:Label id="lblIssues" Runat="Server" meta:resourcekey="lblIssues">(Sort the results by clicking the column headings)</asp:Label>
		</td>
	</tr>
</table>
