namespace BugNET.Administration.Projects.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Collections;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BugNET.BusinessLogicLayer;
	using BugNET.UserInterfaceLayer;
	using BugNET.UserControls;


	/// <summary>
	///		Summary description for ProjectDescription.
	/// </summary>
	public partial class ProjectDescription : System.Web.UI.UserControl,IEditProjectControl
	{

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		#region IEditProjectControl Members

        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
		public int ProjectId
		{
            get
            {
                return ((BasePage)Page).ProjectId;
            }

            set { ((BasePage)Page).ProjectId = value; }
		}

        /// <summary>
        /// Inits this instance.
        /// </summary>
		public void Initialize()
		{
           
			ProjectManager.DataSource = ITUser.GetAllUsers();
			ProjectManager.DataBind();
            ProjectManager.Items.Insert(0, new ListItem("-- Select a User --", ""));

			if (ProjectId != -1) 
			{
				Project projectToUpdate = Project.GetProjectById(ProjectId);

				if (projectToUpdate != null) 
				{
					txtName.Text = projectToUpdate.Name;
					txtDescription.Text = projectToUpdate.Description;
					txtUploadPath.Text = projectToUpdate.UploadPath;
					ProjectCode.Text = projectToUpdate.Code;
					rblAccessType.SelectedValue = projectToUpdate.AccessType.ToString();
                    ProjectManager.SelectedValue = projectToUpdate.ManagerUserName;
                    AllowAttachments.Checked = projectToUpdate.AllowAttachments;
                    AttachmentStorageTypeRow.Visible = AllowAttachments.Checked;
                    if (AttachmentStorageType.Visible)
                    {
                        AttachmentStorageType.SelectedValue = Convert.ToInt32(projectToUpdate.AttachmentStorageType).ToString();
                        AttachmentUploadPathRow.Visible = AllowAttachments.Checked && AttachmentStorageType.SelectedValue == "1";
                    }
				}
			}
			else
			{
                rblAccessType.SelectedIndex = 0;
			}
		}

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <returns></returns>
		public bool Update()
		{
			if (Page.IsValid) 
			{
				Globals.ProjectAccessType at = (rblAccessType.SelectedValue == "Public") ? Globals.ProjectAccessType.Public : Globals.ProjectAccessType.Private;
                IssueAttachmentStorageType attachmentStorageType = (AttachmentStorageType.SelectedValue == "2") ? IssueAttachmentStorageType.Database : IssueAttachmentStorageType.FileSystem;

                Project newProject = new Project(ProjectId, txtName.Text.Trim(), ProjectCode.Text.Trim(), txtDescription.Text.Trim(), ProjectManager.SelectedValue, Page.User.Identity.Name, txtUploadPath.Text.Trim(), at,false, AllowAttachments.Checked, attachmentStorageType,string.Empty);
				if (newProject.Save()) 
				{
					ProjectId = newProject.Id;
					return true;
				} 
				else
					lblError.Text = "Could not save project, please verify that the project name and code are unique values.";
			}
			return false;
		}
      

		#endregion

        /// <summary>
        /// Allows the attachments changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void AllowAttachmentsChanged(object sender, EventArgs e)
        {
            if (!AllowAttachments.Checked)
                txtUploadPath.Text = string.Empty;
            AttachmentStorageTypeRow.Visible = AllowAttachments.Checked;
            AttachmentUploadPathRow.Visible = AllowAttachments.Checked && AttachmentStorageType.SelectedValue == "1";        
        }

        /// <summary>
        /// Handles the Changed event of the AttachmentStorageType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void AttachmentStorageType_Changed(object sender, EventArgs e)
        {
            if (AttachmentStorageType.SelectedValue != "1")
                txtUploadPath.Text = string.Empty;
            AttachmentUploadPathRow.Visible = AllowAttachments.Checked && AttachmentStorageType.SelectedValue == "1"; 
         
        }
	}
}
