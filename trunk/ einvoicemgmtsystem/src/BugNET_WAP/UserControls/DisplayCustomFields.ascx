<%@ Control Language="C#" AutoEventWireup="true" Inherits="BugNET.UserControls.DisplayCustomFields" Codebehind="DisplayCustomFields.ascx.cs" %>
<div class="small-box" id="divider" runat="server" >
    <p class="small-box-right">
	    <span class="title">Additional Information</span>
    </p>
</div>
<div class="bug-content">
    <asp:DataList id="lstCustomFields"  RepeatColumns="2" 
    RepeatDirection="Horizontal" CellPadding="5" Runat="Server">
	   <ItemStyle Width="15%" />
	    <ItemTemplate>       
		    <asp:Label id="lblFieldName" CssClass="col1" Runat="Server" />
	        </td>
	        <td>
	            <asp:PlaceHolder id="PlaceHolder" runat="server">
	            </asp:PlaceHolder>
	    </ItemTemplate>
    </asp:DataList>
</div>
