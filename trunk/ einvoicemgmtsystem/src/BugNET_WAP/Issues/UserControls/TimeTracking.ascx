<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimeTracking.ascx.cs" Inherits="BugNET.Issues.UserControls.TimeTracking" %>
 <asp:label id="TimeEntryLabel" Font-Italic="true" runat="server"></asp:label>
<asp:datagrid id="TimeEntriesDataGrid" runat="server" Width="100%"
    OnItemCommand="TimeEntriesDataGrid_ItemCommand"
    OnItemDataBound="TimeEntriesDataGrid_ItemDataBound"  
    ShowFooter="True" SkinID="DataGrid">  
    <Columns>
        <asp:BoundColumn DataField="WorkDate" HeaderText="Date" DataFormatString="{0:d}">
            <HeaderStyle Width="80px"></HeaderStyle>
        </asp:BoundColumn>
        <asp:BoundColumn DataField="Duration" HeaderText="Hours" DataFormatString="{0:0.00}">
            <HeaderStyle Width="60px" ></HeaderStyle>
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundColumn>
        <asp:BoundColumn DataField="CreatorDisplayName" HeaderText="User">
            <HeaderStyle Width="140px"></HeaderStyle>
        </asp:BoundColumn>		
        <asp:BoundColumn DataField="CommentText" HeaderText="Comment">
        </asp:BoundColumn>
        <asp:TemplateColumn>
            <ItemStyle width="70px" />
            <ItemTemplate>
	            <asp:ImageButton AlternateText="Delete" id="RemoveEntry"  ImageUrl="~/images/cross.gif" CssClass="icon"
		            BorderWidth="0px" CommandName="Delete" CausesValidation="false" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' runat="server"/> 
		            <asp:linkbutton id="lnkDeleteTimeEntry" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' CausesValidation="false" Text="<%$ Resources:CommonTerms, Delete %>" runat="server" />
            </ItemTemplate>
        </asp:TemplateColumn>		
    </Columns>
    <PagerStyle HorizontalAlign="Center"></PagerStyle>
</asp:datagrid>

<asp:panel id="AddTimeEntryPanel" style="padding:15px 15px 15px 0px;" runat="server">
    <h5 class="bug-tab-title"><asp:Literal ID="Literal1" runat="server" meta:resourcekey="AddTimeEntry" /></h5>
    <table style="width:650px;table-layout:fixed;font-size:11px;">
        <tr>
            <th style="text-align:left;font-weight:normal;padding:2px 0 2px 5px;width:120px;"><asp:label runat="server" AssociatedControlID="TimeEntryDate" ID="Label3" meta:resourcekey="DateLabel" Text="Date:"></asp:label></th>
            <td>
                <asp:textbox id="TimeEntryDate"  Width="70" runat="server" />
                <asp:Image id="imgCalendar" runat="Server" CssClass="icon" ImageUrl="~/images/calendar.gif" />
                <asp:RequiredFieldValidator  SetFocusOnError="True" ID="RequiredFieldValidator5" ControlToValidate="TimeEntryDate" ValidationGroup="AddTimeEntry"  runat="server" ErrorMessage=" *"></asp:RequiredFieldValidator>
              
                <asp:CompareValidator id="compDateDataTypeValidator" meta:resourcekey="compDateDataTypeValidator"
                    ControlToValidate="TimeEntryDate" Operator="DataTypeCheck"
                    Type="Date" runat="server" Display="dynamic" ErrorMessage="You must enter a valid
                    date."></asp:CompareValidator>
                <asp:CompareValidator ID="cpTimeEntry" runat="server"  ValidationGroup="AddTimeEntry"   meta:resourcekey="cpTimeEntry"
                    ControlToValidate="TimeEntryDate" ErrorMessage="Date cannot be in the future."  
                    Display="dynamic" Type="Date"  Operator="LessThanEqual"></asp:CompareValidator>
            </td>
        </tr>
         <tr>
            <th style="text-align:left;font-weight:normal;padding:2px 0 2px 5px;width:120px;"><asp:label  runat="server"  meta:resourcekey="lblDuration"  AssociatedControlID="DurationTextBox" ID="lblDuration" Text="Duration:" /></th>
            <td><asp:textbox id="DurationTextBox" runat="server"  Width="70" style="text-align:right" MaxLength="5"></asp:textbox>&nbsp;hrs
            <asp:RequiredFieldValidator  SetFocusOnError="True" ID="RequiredFieldValidator4" ControlToValidate="DurationTextBox" ValidationGroup="AddTimeEntry"  runat="server" ErrorMessage=" *"></asp:RequiredFieldValidator>
            <asp:rangevalidator id="RangeValidator1" Display="Dynamic" runat="server" meta:resourcekey="RangeValidator1"  ErrorMessage="Duration is out of range." Type="Double"
                MaximumValue="24" MinimumValue="0.01" ControlToValidate="DurationTextBox"></asp:rangevalidator></td></tr>
        
        <tr>
            <td colspan="2" style="padding:2px 0 2px 5px;"><label for="FCKComment"><asp:Literal ID="Literal2" runat="server" meta:resourcekey="Comments" /> <span style="font-size:90%;color:#999999"><asp:Literal ID="CommentOptional" runat="server" meta:resourcekey="CommentOptional" /></span></label></td>
        </tr>
        <tr>
            <td colspan="2"><bn:HtmlEditor id="CommentHtmlEditor" Height="200" runat="server" /></td>
        </tr>
    </table>
    <div class="bug-tab-buttons">
         <asp:Imagebutton runat="server" id="AddTimeEntry" ValidationGroup="AddTimeEntry" OnClick="AddTimeEntry_Click" CssClass="icon" ImageUrl="~/images/add.gif" />
        <asp:LinkButton ID="cmdAddTimeEntry" CausesValidation="True" ValidationGroup="AddTimeEntry" OnClick="AddTimeEntry_Click" meta:resourcekey="cmdAddTimeEntry" runat="server"></asp:LinkButton>
    </div>
</asp:panel>