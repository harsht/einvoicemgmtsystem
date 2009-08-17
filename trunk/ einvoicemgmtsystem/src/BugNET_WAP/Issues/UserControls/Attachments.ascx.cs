using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BugNET.BusinessLogicLayer;
using BugNET.UserInterfaceLayer;
using System.IO;
using log4net;

namespace BugNET.Issues.UserControls
{
    public partial class Attachments : System.Web.UI.UserControl, IIssueTab
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Attachments));
        private int _IssueId = 0;
        private int _ProjectId = 0;
        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region IIssueTab Members

        /// <summary>
        /// Gets or sets the bug id.
        /// </summary>
        /// <value>The bug id.</value>
        public int IssueId
        {
            get { return _IssueId; }
            set { _IssueId = value; }
        }

        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
        public int ProjectId
        {
            get { return _ProjectId; }
            set { _ProjectId = value; }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            AttachmentsDataGrid.Columns[0].HeaderText =  GetLocalResourceObject("AttachmentsGrid.FileNameHeader.Text").ToString();
            AttachmentsDataGrid.Columns[1].HeaderText =  GetLocalResourceObject("AttachmentsGrid.SizeHeader.Text").ToString();
            AttachmentsDataGrid.Columns[2].HeaderText =  GetLocalResourceObject("AttachmentsGrid.Description.Text").ToString();
            BindAttachments();

            //check users role permission for adding an attachment
            if (!Page.User.Identity.IsAuthenticated || !ITUser.HasPermission(ProjectId, Globals.Permission.ADD_ATTACHMENT.ToString()))
                pnlAddAttachment.Visible = false;

            if (!Page.User.Identity.IsAuthenticated || !ITUser.HasPermission(ProjectId, Globals.Permission.DELETE_ATTACHMENT.ToString()))
                AttachmentsDataGrid.Columns[3].Visible = false;
        }

        #endregion

        /// <summary>
        /// Binds the attachments.
        /// </summary>
        private void BindAttachments()
        {
            List<IssueAttachment> attachments = IssueAttachment.GetIssueAttachmentsByIssueId(_IssueId);

            if (attachments.Count == 0)
            {
                lblAttachments.Text = GetLocalResourceObject("NoAttachments").ToString();
                lblAttachments.Visible = true;
            }
            else
            {
                lblAttachments.Visible = false;
                AttachmentsDataGrid.DataSource = attachments;
                AttachmentsDataGrid.DataBind();   
            }
        }


        /// <summary>
        /// Handles the ItemDataBound event of the AttachmentsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.DataGridItemEventArgs"/> instance containing the event data.</param>
        protected void AttachmentsDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                IssueAttachment currentAttachment = (IssueAttachment)e.Item.DataItem;
                ((HtmlAnchor)e.Item.FindControl("lnkAttachment")).InnerText = currentAttachment.FileName;
                ((HtmlAnchor)e.Item.FindControl("lnkAttachment")).HRef = "DownloadAttachment.axd?id=" + currentAttachment.Id.ToString();
                ImageButton lnkDeleteAttachment = (ImageButton)e.Item.FindControl("lnkDeleteAttachment");
                lnkDeleteAttachment.OnClientClick = string.Format("return confirm('{0}');", GetLocalResourceObject("DeleteAttachment").ToString());
                LinkButton cmdDeleteAttachment = (LinkButton)e.Item.FindControl("cmdDeleteAttachment");
                cmdDeleteAttachment.OnClientClick = string.Format("return confirm('{0}');", GetLocalResourceObject("DeleteAttachment").ToString());

                float size;
                string label;
                if (currentAttachment.Size > 1000)
                {
                    size = currentAttachment.Size / 1000f;
                    label = string.Format("{0} kb", size.ToString("##,##"));
                }
                else
                {
                    size = currentAttachment.Size;
                    label = string.Format("{0} b", size.ToString("##,##"));
                }
                ((Label)e.Item.FindControl("lblSize")).Text = label;
            }
        }

        /// <summary>
        /// Handles the ItemCommand event of the dtgAttachment control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs"/> instance containing the event data.</param>
        protected void AttachmentsDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    IssueAttachment.DeleteIssueAttachment(Convert.ToInt32(e.CommandArgument));
                    break;
            }
            BindAttachments();
        }

        /// <summary>
        /// Handles the Click event of the LinkButton1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            // Do something that needs to be done such as refresh a gridView
            // say you had a gridView control called gvMyGrid displaying all 
            // the files uploaded. Refresh the data by doing a databind here.
            // gvMyGrid.DataBind();
            BindAttachments();
        }

        /// <summary>
        /// Alloweds the file extensions.
        /// </summary>
        /// <value>The allowed file extensions.</value>
        /// <returns></returns>
        protected string AllowedFileExtensions
        {
             get { return HostSetting.GetHostSetting("AllowedFileExtensions"); }
            
        }


        /// <summary>
        /// Gets the file size limit.
        /// </summary>
        /// <value>The file size limit.</value>
        protected string FileSizeLimit
        {
            get { return HostSetting.GetHostSetting("FileSizeLimit"); }

        }
    }
}