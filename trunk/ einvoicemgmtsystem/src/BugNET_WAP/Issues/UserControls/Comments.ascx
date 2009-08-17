<%@ Control Language="c#" Inherits="BugNET.Issues.UserControls.Comments" Codebehind="Comments.ascx.cs" %>
<%@ implements interface="BugNET.UserInterfaceLayer.IIssueTab" %>

<asp:Repeater ID="rptComments" OnItemCommand="rptComments_ItemCommand" OnItemDataBound="rptComments_ItemDataBound"  runat="server">
    <ItemTemplate>
		<div id="CommentArea" runat="server">
		    <a id='<%#DataBinder.Eval(Container.DataItem, "Id") %>'></a>
            <p class="CommentTitle" style="width:100%"> 
               <span style="float:right;clear:both;">
                 &nbsp; <asp:LinkButton ID="lnkDeleteComment" CausesValidation="false" runat="Server"  CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' Text="<%$ Resources:CommonTerms, Delete %>" />
                &nbsp; <asp:LinkButton ID="lnkEditComment" CausesValidation="false" runat="Server" CommandName="Edit" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' Text="<%$ Resources:CommonTerms, Edit %>" />
                </span>
                <asp:image ID="Image1" width="16" height="16" runat="server" CssClass="icon" meta:resourcekey="Image1"  AlternateText="User comment" ImageUrl="~/images/user_comment.gif"/>
                <asp:label ID="CreatorDisplayName" runat="server" CssClass="CommentAuthor" /> <small>(<asp:Label id="lblDateCreated" Runat="Server" Text='' />)</small>
                | &nbsp;<asp:HyperLink ID="hlPermalink" runat="server" meta:resourcekey="hlPermalink" Text="Permalink" />
            </p>

            <div class="CommentText">
                <div class="CommentText2">
                    <div class="CommentText3"><asp:Literal id="ltlComment" Runat="Server" /></div>
                </div>
            </div>
            <asp:panel id="pnlEditComment" style="padding:15px 15px 15px 0px;" runat="server">
	            <h5 class="bug-tab-title"><asp:label ID="lblEditComment" runat="server" meta:resourcekey="cmdEditComment">Edit Comment</asp:label></h5>
	            <bn:HtmlEditor id="EditCommentHtmlEditor" Height="200" runat="server" />
	        
	            <asp:HiddenField runat="server" id="commentNumber" value="" />
	            <div class="bug-tab-buttons">
		            <asp:Imagebutton runat="server" id="editComment" ValidationGroup="AddComment" OnClick="cmdEditComment_Click" CssClass="icon" ImageUrl="~/images/add.gif" />
		            <asp:LinkButton ID="cmdEditComment" CausesValidation="True" ValidationGroup="AddComment" OnClick="cmdEditComment_Click" meta:resourcekey="cmdEditComment" runat="server">Edit Comment</asp:LinkButton>&nbsp;
	            </div>   
            </asp:panel>                              
        </div>
    </ItemTemplate>
</asp:Repeater>    

   
<asp:label id="lblComments"  Font-Italic="true" runat="server"></asp:label>
<asp:panel id="pnlAddComment" style="padding:15px 15px 15px 0px;" runat="server">
    <h5 class="bug-tab-title"><asp:Literal ID="Literal1" runat="server" meta:resourcekey="LeaveComment" /></h5>    
    <bn:HtmlEditor id="CommentHtmlEditor" Height="200" runat="server" />
    <div class="bug-tab-buttons">
        <asp:Imagebutton runat="server" id="AddAttachment" ValidationGroup="AddComment" OnClick="cmdAddComment_Click" CssClass="icon" ImageUrl="~/images/add.gif" />
        <asp:LinkButton ID="cmdAddComment" CausesValidation="True" ValidationGroup="AddComment" OnClick="cmdAddComment_Click" meta:resourcekey="cmdAddComment" runat="server">Add Comment</asp:LinkButton>&nbsp;
    </div>
</asp:panel> 