<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Attachments.ascx.cs" Inherits="BugNET.Issues.UserControls.Attachments" %>

<BN:Message ID="AttachmentsMessage" runat="server" /> 
<asp:label id="lblAttachments" visible="False" Font-Italic="True" 
    runat="server" meta:resourcekey="lblAttachments1"></asp:label>
<asp:datagrid id="AttachmentsDataGrid" Width="100%" runat="server" 
    SkinID="DataGrid"
    OnItemDataBound="AttachmentsDataGrid_ItemDataBound"
    OnItemCommand="AttachmentsDataGrid_ItemCommand"
    meta:resourcekey="AttachmentsDataGrid">
    <columns>
        <asp:templatecolumn headertext="Name"  meta:resourcekey="NameColumn">
            <ItemStyle Width="150px" />
            <itemtemplate >
                <a target="_blank" id="lnkAttachment" runat="server"></a>
            </itemtemplate>
        </asp:templatecolumn>
        <asp:TemplateColumn HeaderText="Size"  meta:resourcekey="SizeColumn">
            <ItemStyle Width="70px" />
            <ItemTemplate>
                <asp:Label ID="lblSize" runat="server" meta:resourcekey="lblSize" />
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:boundcolumn headertext="Description" datafield="Description" meta:resourcekey="DescriptionColumn" />
        <asp:TemplateColumn>
            <ItemStyle Width="70px" />
            <ItemTemplate>
                <asp:ImageButton AlternateText="<%$ Resources:CommonTerms, Delete %>" CssClass="icon" 
                    id="lnkDeleteAttachment" 
                    ImageUrl="~/images/cross.gif" BorderWidth="0px" CommandName="Delete" 
                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' 
                    runat="server" /> 
                <asp:LinkButton ID="cmdDeleteAttachment" 
                    CommandName="Delete" runat="server" 
                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' 
                    Text="<%$ Resources:CommonTerms, Delete %>">Delete</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>	
    </columns>
</asp:datagrid>
<asp:panel id="pnlAddAttachment" style="padding:15px 15px 15px 0px;" 
    runat="server" meta:resourcekey="pnlAddAttachment1">	
   <h5 class="bug-tab-title"><asp:label ID="lblAddAttachment" runat="server" meta:resourcekey="lblAddAttachment" Text="Add Attachment"></asp:label></h5> 
    <div>
        <asp:LinkButton ID="LinkButton1" CausesValidation="false"  runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
    </div>
    
    <script type="text/javascript" src="../js/swfupload.js"></script>
	<script type="text/javascript" src="../js/handlers.js"></script>
	<script type="text/javascript" src="../js/swfupload.queue.js"></script>
    <script type="text/javascript" src="../js/fileprogress.js"></script>
	<script type="text/javascript">
	    var swfu;
	    window.onload = function() {
	        swfu = new SWFUpload({
	            // Backend Settings
	            upload_url: "UploadAttachment.axd", // Relative to the SWF file
	            post_params: {
	                "ASPSESSID": "<%=Session.SessionID %>",
	                "AUTHID": "<%=HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value %>",
	                "id": "<%=IssueId %>"
	            },
	            //use the query string instead of post
	            use_query_string: true,
	            
	            // File Upload Settings
	            //file_size_limit: "2048", // 2MB
	            //file_types: "*.*",
	            file_size_limit: "<%=FileSizeLimit %>",	           
	            file_types: "<%=AllowedFileExtensions %>",
	            file_types_description: "All Files",
	            file_upload_limit: "0",    // Zero means unlimited

	            // Event Handler Settings - these functions as defined in Handlers.js
	            //  The handlers are not part of SWFUpload but are part of my website and control how
	            //  my website reacts to the SWFUpload events.
	            file_dialog_start_handler: fileDialogStart,
	            file_queued_handler: fileQueued,
	            file_queue_error_handler: fileQueueError,
	            file_dialog_complete_handler: fileDialogComplete,
	            upload_start_handler: uploadStart,
	            upload_progress_handler: uploadProgress,
	            upload_error_handler: uploadError,
	            upload_success_handler: uploadSuccess,
	            upload_complete_handler: uploadComplete,

	            // Button Settings  
	            button_image_url: "../images/XPButtonNoText_160x22.png", // Relative to the SWF file
	            button_placeholder_id: "spanButtonPlaceholder",
	            button_width: 160,
	            button_height: 22,
	            button_text: '<span class="button"><%=GetLocalResourceObject("SelectFiles") %></span>',
	            button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 12pt; } .buttonSmall { font-size: 10pt; }',
	            button_text_top_padding: 1,
	            button_text_left_padding: 5,

	            // Flash Settings
	            flash_url: "../swfupload.swf", // Relative to this file

	            // UI Settings
	            ui_container_id: "swfu_container",
	            //degraded_container_id: "degraded_container",

	            custom_settings: {
	                progressTarget: "divFileProgressContainer",
	                cancelButtonId: "btnCancel"	     
	            },
	            //custom_settings: {
	            //    upload_target: "divFileProgressContainer"	     
	            //},
	            // Debug Settings
	            debug: false
	        });

            
	        function uploadComplete() 
	        {
	          __doPostBack('ctl00$IssueTabs$ctlIssueTabs$ctlContent$LinkButton1', '')
	        }
	    }
	</script>
	 <div id="swfu_container" style="margin:0px 10px;">	   
	    <div id="divFileProgressContainer"></div>	    
	    <div style="padding-left: 5px;">
			<span id="spanButtonPlaceholder"></span>
			<input id="btnCancel" type="button" value="<%=GetLocalResourceObject("CancelUploads") %>" onclick="cancelQueue(swfu);" disabled="disabled" style="margin-left: 2px; height: 23px; font-size: 8pt;" />
			<br />
		</div>
	</div>
</asp:panel>