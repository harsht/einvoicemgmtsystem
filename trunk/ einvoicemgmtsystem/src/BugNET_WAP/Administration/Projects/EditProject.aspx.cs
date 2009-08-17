using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BugNET.Administration.Projects.UserControls;
using BugNET.BusinessLogicLayer;
using BugNET.UserInterfaceLayer;
using System.Web.Services;

namespace BugNET.Administration.Projects
{
	/// <summary>
	/// Edit project administration page.
	/// </summary>
	public partial class EditProject :  BugNET.UserInterfaceLayer.BasePage 
	{
		private Control contentControl;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!ITUser.HasPermission(Convert.ToInt32(Request.Params["id"]), Globals.Permission.ADMIN_EDIT_PROJECT.ToString()))
                Response.Redirect("~/Errors/AccessDenied.aspx");

			if (!Page.IsPostBack)
			{
				//get project id
				if (Request.QueryString["id"] != null)
					ProjectId = Convert.ToInt32(Request.Params["id"]);

                if (Request.QueryString["tid"] != null)
                    TabId = Convert.ToInt32(Request.Params["tid"]);

				litProjectName.Text = Project.GetProjectById(ProjectId).Name;

                tvAdminMenu.Nodes.Add(new TreeNode("Details", "Details", "~/images/application_home.gif", String.Format("~/Administration/Projects/EditProject.aspx?id={0}&tid={1}", ProjectId.ToString(), "1"), string.Empty));
                tvAdminMenu.Nodes.Add(new TreeNode("Categories", "Categories", "~/images/plugin.gif", String.Format("~/Administration/Projects/EditProject.aspx?id={0}&tid={1}", ProjectId.ToString(), "2"), string.Empty));             
                tvAdminMenu.Nodes.Add(new TreeNode("Status", "Status", "~/images/greencircle.png", String.Format("~/Administration/Projects/EditProject.aspx?id={0}&tid={1}", ProjectId.ToString(), "8"), string.Empty));
                tvAdminMenu.Nodes.Add(new TreeNode("Priorities", "Priorities", "~/images/Critical.gif", String.Format("~/Administration/Projects/EditProject.aspx?id={0}&tid={1}", ProjectId.ToString(), "9"), string.Empty));
                tvAdminMenu.Nodes.Add(new TreeNode("Milestones","Milestones","~/images/package.gif",String.Format("~/Administration/Projects/EditProject.aspx?id={0}&tid={1}", ProjectId.ToString(), "3"),string.Empty));
                tvAdminMenu.Nodes.Add(new TreeNode("Issue Types", "IssueTypes", "~/images/bug.gif", String.Format("~/Administration/Projects/EditProject.aspx?id={0}&tid={1}", ProjectId.ToString(), "10"), string.Empty));
                tvAdminMenu.Nodes.Add(new TreeNode("Resolutions", "Resolutions", "~/images/accept.gif", String.Format("~/Administration/Projects/EditProject.aspx?id={0}&tid={1}", ProjectId.ToString(), "11"), string.Empty));
                tvAdminMenu.Nodes.Add(new TreeNode("Members", "Members", "~/images/user.gif", String.Format("~/Administration/Projects/EditProject.aspx?id={0}&tid={1}", ProjectId.ToString(), "5"), string.Empty));
                tvAdminMenu.Nodes.Add(new TreeNode("Security Roles", "Security Roles", "~/images/shield.gif", String.Format("~/Administration/Projects/EditProject.aspx?id={0}&tid={1}", ProjectId.ToString(), "4"), string.Empty));
                tvAdminMenu.Nodes.Add(new TreeNode("Notifications", "Notifications", "~/images/email_go.gif", String.Format("~/Administration/Projects/EditProject.aspx?id={0}&tid={1}", ProjectId.ToString(), "13"), string.Empty));
                tvAdminMenu.Nodes.Add(new TreeNode("Custom Fields", "Custom Fields", "~/images/textfield.gif", String.Format("~/Administration/Projects/EditProject.aspx?id={0}&tid={1}", ProjectId.ToString(), "7"), string.Empty));
                //tvAdminMenu.Nodes.Add(new TreeNode("Mailboxes", "Mailboxes", "~/images/email.gif", String.Format("~/Administration/Projects/EditProject.aspx?id={0}&tid={1}", ProjectId.ToString(), "6"), string.Empty));
                tvAdminMenu.Nodes.Add(new TreeNode("Subversion", "Subversion", "~/images/svnLogo_sm.jpg", String.Format("~/Administration/Projects/EditProject.aspx?id={0}&tid={1}", ProjectId.ToString(), "12"), string.Empty));
                
			}
            if (TabId != -1)
                LoadTab(TabId);      
		}

        /// <summary>
        /// Gets or sets the tab id.
        /// </summary>
        /// <value>The tab id.</value>
        int TabId 
		{
			get 
			{
				if (ViewState["TabId"] == null)
					return -1;
				else
					return (int)ViewState["TabId"];
			}

			set { ViewState["TabId"] = value; }
		}

        /// <summary>
        /// Loads the tab.
        /// </summary>
        /// <param name="selectedTab">The selected tab.</param>
		private void LoadTab(int selectedTab) 
		{
			string controlName = "ProjectDescription.ascx";

			switch (selectedTab) 
			{
				case 1:
					controlName = "ProjectDescription.ascx";
					break;
				case 2:
					controlName = "ProjectCategories.ascx";
					break;
				case 3:
					controlName = "ProjectMilestones.ascx";
					break;
				case 4:
					controlName = "ProjectRoles.ascx";
					break;
				case 5:
					controlName = "ProjectMembers.ascx";
					break;
				case 6:
					controlName = "ProjectMailbox.ascx";
					break;
                case 7:
                    controlName = "ProjectCustomFields.ascx";
                    break;
                case 8:
                    controlName = "ProjectStatus.ascx";
                    break;
                case 9:
                    controlName = "ProjectPriorities.ascx";
                    break;
                case 10:
                    controlName = "ProjectIssueTypes.ascx";
                    break;
                case 11:
                    controlName = "ProjectResolutions.ascx";
                    break;
                case 12:
                    controlName = "ProjectSubversion.ascx";
                    break;
                case 13:
                    controlName = "ProjectNotifications.ascx";
                    break;
				
			}
			contentControl = Page.LoadControl("~/Administration/Projects/UserControls/" + controlName);
			((IEditProjectControl)contentControl).ProjectId = ProjectId;
			plhContent.Controls.Clear();
			plhContent.Controls.Add( contentControl );
			contentControl.ID = "ctlContent";
            ((IEditProjectControl)contentControl).Initialize();
            plhContent.Visible = true;
		}

        /// <summary>
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        /// <param name="s">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
		protected void DeleteButton_Click(Object s, EventArgs e) 
		{
			Project.DeleteProject(ProjectId);
			Response.Redirect("~/Administration/Projects/ProjectList.aspx");
		}

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="s">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SaveButton_Click(object s, EventArgs e)
        {
            Control c = plhContent.FindControl("ctlContent");
            if (c != null)
            {
                if (((IEditProjectControl)c).Update())
                    Message1.ShowInfoMessage("Project has been updated successfully");

            }

        }
	}
}
