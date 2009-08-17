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
	public partial class ProjectSubversion : System.Web.UI.UserControl,IEditProjectControl
	{

        private int _ProjectId = -1;
        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{

		}

        /// <summary>
        /// Handles the Click event of the createRepoBttn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void createRepoBttn_Click(object sender, EventArgs e)
        {
            string name = repoName.Text.Trim();

            if(!SubversionIntegration.IsValidSubversionName(name)) {
                createErrorLbl.Text = "The repository name was not formated correctlly. Only alpha-numeric charaters, \"-\", and \"_\" are allowed.";
                return;
            }

            svnOut.Text = SubversionIntegration.CreateRepository(name);

            string rootUrl = HostSetting.GetHostSetting("RepoRootUrl");
            if(!rootUrl.EndsWith("/"))
                rootUrl += "/";

            svnUrl.Text = rootUrl + name;

        }

        /// <summary>
        /// Handles the Click event of the createTagBttn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void createTagBttn_Click(object sender, EventArgs e)
        {
            string name = tagName.Text.Trim();
           
            if (tagComment.Text.Trim().Length == 0)
                createTagErrorLabel.Text = "A comment is required to create a tag";
            else if(!SubversionIntegration.IsValidSubversionName(name))
                createTagErrorLabel.Text = "The tag name was not formated correctlly. Only alpha-numeric charaters, \"-\", and \"_\" are allowed.";
            else
                svnOut.Text = SubversionIntegration.CreateTag(this.ProjectId, name, tagComment.Text, tagUserName.Text.Trim(), tagPassword.Text.Trim());
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
                return _ProjectId;
            }

            set { _ProjectId = value; }
		}

        /// <summary>
        /// Inits this instance.
        /// </summary>
		public void Initialize()
		{
           Project projectToUpdate = Project.GetProjectById(ProjectId);
            svnUrl.Text = projectToUpdate.SvnRepositoryUrl;

           bool svnAdminEnabled = bool.Parse(HostSetting.GetHostSetting("EnableRepositoryCreation"));

           if (svnAdminEnabled)
           {
               createErrorLbl.Text = "";
               createRepoBttn.Enabled = true;
               repoName.Enabled = true;
           }
           else
           {
               createErrorLbl.Text = "Creation of repositories is disabled.";
               createRepoBttn.Enabled = false;
               repoName.Enabled = false;
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
                Project projectToUpdate = Project.GetProjectById(ProjectId);
                projectToUpdate.SvnRepositoryUrl = svnUrl.Text;

            
                if (projectToUpdate.Save()) 
				{
                    ProjectId = projectToUpdate.Id;
					return true;
				} 
				else
					lblError.Text = "Could not save project, please verify that the data is correct.";
			}
			return false;
		}
      

		#endregion
	}
}
