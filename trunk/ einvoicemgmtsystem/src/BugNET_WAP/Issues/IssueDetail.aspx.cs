using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BugNET.BusinessLogicLayer;
using BugNET.UserInterfaceLayer;
using BugNET.UserControls;
using System.Threading;
using System.Collections.Generic;

namespace BugNET.Issues
{
	/// <summary>
	/// Issue Detail Page
	/// </summary>
	public partial class IssueDetail : BasePage  
	{

		#region Private Events
			/// <summary>
			/// Page Load Event
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			protected void Page_Load(object sender, System.EventArgs e)
			{     

				if (!Page.IsPostBack)
				{
                    lnkDelete.Attributes.Add("onclick", string.Format("return confirm('{0}');", GetLocalResourceObject("DeleteIssue").ToString()));
                    imgDelete.Attributes.Add("onclick", string.Format("return confirm('{0}');", GetLocalResourceObject("DeleteIssue").ToString()));

                    // Get Issue Id from Query String
                    if (Request.QueryString["id"] != null)
                        IssueId = Int32.Parse(Request.QueryString["id"]);

                    // Get Project Id from Query String
                    if (Request.QueryString["pid"] != null)
                        ProjectId = Int32.Parse(Request.QueryString["pid"]);

                    // If don't know project or issue then redirect back
                    if (ProjectId == 0 && IssueId == 0)
                        Response.Redirect("~/Default.aspx");

                    // Initialize for Adding or Editing
                    if (IssueId == 0)
                    {
                        BindOptions();
                        Page.Title = GetLocalResourceObject("PageTitleNewIssue").ToString();
                        //lblIssueNumber.Text = "N/A";
                    }
                    else
                    {
                        BindValues();
                    }
                   
				}

                //need to rebind these on every postback because of dynamic controls
                if(IssueId ==0)
                {
                    ctlCustomFields.DataSource = CustomField.GetCustomFieldsByProjectId(ProjectId);
                }
                else
                {
                    ctlCustomFields.DataSource = CustomField.GetCustomFieldsByIssueId(IssueId);
                }
                ctlCustomFields.DataBind();

                // The ExpandIssuePaths method is called to handle
                // the SiteMapResolve event.
                SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(this.ExpandIssuePaths);

				ctlIssueTabs.IssueId = IssueId;
				ctlIssueTabs.ProjectId  = ProjectId;
          
			}

            /// <summary>
            /// Handles the PreRender event of the Page control.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
            protected void Page_PreRender(object sender, EventArgs e)
            {
                Project p = Project.GetProjectById(ProjectId);

                ////check if the user can access this project
                //if (p.AccessType == Globals.AccessType.Private && !User.Identity.IsAuthenticated)
                //{
                //    Response.Redirect("~/Errors/AccessDenied.aspx");
                //}
                //else if (User.Identity.IsAuthenticated && p.AccessType == Globals.AccessType.Private && !Project.IsUserProjectMember(User.Identity.Name, ProjectId))
                //{
                //    Response.Redirect("~/Errors/AccessDenied.aspx");
                //}		
     
                if (IssueId != 0)
                {
                    lblIssueNumber.Text = string.Format("{0}-{1}", p.Code, IssueId);
                    ctlIssueTabs.Visible = true;
                    TimeLogged.Visible = true;
                    chkNotifyAssignedTo.Visible = false;
                    chkNotifyOwner.Visible = false;
                    //btnDelete.Enabled = true;
                    SetFieldSecurity();
                }
            }

            /// <summary>
            /// Handles the Unload event of the Page control.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
            protected void Page_Unload(object sender, System.EventArgs e)
            {
                //remove the event handler
                SiteMap.SiteMapResolve -=
                 new SiteMapResolveEventHandler(this.ExpandIssuePaths);
            }

            /// <summary>
            /// Expands the issue paths.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The <see cref="System.Web.SiteMapResolveEventArgs"/> instance containing the event data.</param>
            /// <returns></returns>
            private SiteMapNode ExpandIssuePaths(Object sender, SiteMapResolveEventArgs e)
            {
                SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
                SiteMapNode tempNode = currentNode;
                
                // The current node, and its parents, can be modified to include
                // dynamic querystring information relevant to the currently
                // executing request.
                if (IssueId != 0)
                {
                    string title = (TitleTextBox.Text.Length >= 30) ? TitleTextBox.Text.Substring(0, 30) + " ..." : TitleTextBox.Text;
                    tempNode.Title = string.Format("{0}: {1}", lblIssueNumber.Text, title);
                    tempNode.Url = tempNode.Url + "?id=" + IssueId.ToString();
                }
                else
                    tempNode.Title = "New Issue";

                if ((null != (tempNode = tempNode.ParentNode)))
                {
                   tempNode.Url = string.Format("~/Issues/IssueList.aspx?pid={0}", ProjectId);
                }

                return currentNode;
            }

            /// <summary>
            /// Binds the values.
            /// </summary>
            private void BindValues()
            {
                Issue currentIssue = Issue.GetIssueById(IssueId);
                ProjectId = currentIssue.ProjectId;

                BindOptions();

                lblIssueNumber.Text = string.Concat("[", currentIssue.FullId, "]");
                Page.Title = string.Concat("[", currentIssue.FullId, "] ", currentIssue.Title);
                DropIssueType.SelectedValue = currentIssue.IssueTypeId;
                DropResolution.SelectedValue = currentIssue.ResolutionId;
                DropStatus.SelectedValue = currentIssue.StatusId;
                DropPriority.SelectedValue = currentIssue.PriorityId;
                DropOwned.SelectedValue = currentIssue.OwnerUserName;
               // lblDescription.Text = currentIssue.Description;
                DescriptionHtmlEditor.Text = currentIssue.Description;
                lblLastUpdateUser.Text = currentIssue.LastUpdateUserName;
                lblReporter.Text = currentIssue.CreatorDisplayName;
                TitleTextBox.Text = currentIssue.Title;
                lblDateCreated.Text = currentIssue.DateCreated.ToString("g");
                lblLastModified.Text = currentIssue.LastUpdate.ToString("g");
                lblIssueNumber.Text = currentIssue.FullId;
                DropCategory.SelectedValue = currentIssue.CategoryId;
                DropMilestone.SelectedValue = currentIssue.MilestoneId;
                DropAssignedTo.SelectedValue = currentIssue.AssignedUserName;
                lblLoggedTime.Text = currentIssue.TimeLogged.ToString();
                txtEstimation.Text = currentIssue.Estimation == 0 ? string.Empty : currentIssue.Estimation.ToString();
                DueDate.Text = currentIssue.DueDate == DateTime.MinValue ? String.Empty : currentIssue.DueDate.ToShortDateString();
                chkPrivate.Checked = currentIssue.Visibility == 0 ? false : true;
                ProgressSlider.Text = currentIssue.Progress.ToString();
            }

            /// <summary>
            /// Binds the options.
            /// </summary>
            private void BindOptions()
            {
                List<ITUser> users = ITUser.GetUsersByProjectId(ProjectId);
				//Get Type
                DropIssueType.DataSource = IssueType.GetIssueTypesByProjectId(ProjectId);
				DropIssueType.DataBind();
					
				//Get Priority
                DropPriority.DataSource = Priority.GetPrioritiesByProjectId(ProjectId);
                DropPriority.DataBind();

				//Get Resolutions
                DropResolution.DataSource = Resolution.GetResolutionsByProjectId(ProjectId);
                DropResolution.DataBind();

                //Get categories
                CategoryTree categories	= new CategoryTree();
                DropCategory.DataSource = categories.GetCategoryTreeByProjectId(ProjectId);
                DropCategory.DataBind();

                //Get milestones
                DropMilestone.DataSource = Milestone.GetMilestoneByProjectId(ProjectId);
                DropMilestone.DataBind();

				//Get Users
                DropAssignedTo.DataSource = users;
                DropAssignedTo.DataBind();

                DropOwned.DataSource = users;
                DropOwned.DataBind();

                DropStatus.DataSource = Status.GetStatusByProjectId(ProjectId);
                DropStatus.DataBind();

                lblDateCreated.Text = DateTime.Now.ToString("f");
                lblReporter.Text = User.Identity.Name;
                lblLastModified.Text = DateTime.Now.ToString("f");
                lblLastUpdateUser.Text = User.Identity.Name;
            }

            /// <summary>
            /// Saves the bug.
            /// </summary>
            /// <returns></returns>
            private bool SaveIssue()
            {
                decimal estimation;
                decimal.TryParse(txtEstimation.Text.Trim(), out estimation);
                DateTime dueDate = DueDate.Text.Length > 0 ? DateTime.Parse(DueDate.Text) : DateTime.MinValue;                

                bool NewIssue = (IssueId <= 0);

                Issue newIssue = new Issue(IssueId, ProjectId, string.Empty, string.Empty, TitleTextBox.Text, DescriptionHtmlEditor.Text.Trim(), 
                    DropCategory.SelectedValue, DropCategory.SelectedText, DropPriority.SelectedValue, DropPriority.SelectedText, 
                    string.Empty, DropStatus.SelectedValue, DropStatus.SelectedText, string.Empty, DropIssueType.SelectedValue, 
                    DropIssueType.SelectedText, string.Empty, DropResolution.SelectedValue, DropResolution.SelectedText, string.Empty,
                    DropAssignedTo.SelectedText, DropAssignedTo.SelectedValue, Guid.Empty,Security.GetDisplayName(), 
                    Security.GetUserName(), Guid.Empty, DropOwned.SelectedText, DropOwned.SelectedValue, Guid.Empty, dueDate, 
                    DropMilestone.SelectedValue, DropMilestone.SelectedText, string.Empty,chkPrivate.Checked == true ? 1 : 0,
                    0, estimation, DateTime.MinValue, DateTime.MinValue, Security.GetUserName(), Security.GetDisplayName(),
                    Convert.ToInt32(ProgressSlider.Text), false);

                if (!newIssue.Save())
                {
                    lblError.Text = "Could not save issue";
                    return false;
                }

                IssueId = newIssue.Id;

                if (!CustomField.SaveCustomFieldValues(IssueId, ctlCustomFields.Values))
                {
                    lblError.Text = "Could not save issue custom fields";
                    return false;
                }

                //if new issue check if notify owner and assigned is checked.
                if (NewIssue)
                {
                    if (chkNotifyOwner.Checked)
                    {
                        System.Web.Security.MembershipUser oUser = ITUser.GetUser(newIssue.OwnerUserName);
                        if (oUser != null)
                        {
                            IssueNotification notify = new IssueNotification(IssueId, oUser.UserName);
                            notify.Save();
                        }
                    }
                    if (chkNotifyAssignedTo.Checked && !string.IsNullOrEmpty(newIssue.AssignedUserName))
                    {
                        System.Web.Security.MembershipUser oUser = ITUser.GetUser(newIssue.AssignedUserName);
                        if (oUser != null)
                        {
                            IssueNotification notify = new IssueNotification(IssueId, oUser.UserName);
                            notify.Save();
                        }
                    }
                    IssueNotification.SendIssueAddNotifications(IssueId);
                }

                return true;
            }
            /// <summary>
            /// Handles the Click event of the lnkUpdate control.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
            protected void lnkSave_Click(object sender, EventArgs e)
            {
                if (Page.IsValid)
                {
                    SaveIssue();
                }
            }
     
            /// <summary>
            /// Handles the Click event of the lnkDone control.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
            protected void lnkDone_Click(object sender, EventArgs e)
            {
                if (Page.IsValid && SaveIssue())
                    Response.Redirect("~/Issues/IssueList.aspx?pid=" + ProjectId.ToString());
            }

            /// <summary>
            /// Handles the Click event of the lnkDelete control.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
            protected void lnkDelete_Click(object sender, EventArgs e)
            {
                Issue.DeleteIssue(IssueId);
                Response.Redirect(string.Format("~/Issues/IssueList.aspx?pid={0}", ProjectId.ToString()));
            }

            /// <summary>
            /// Cancels the button click.
            /// </summary>
            /// <param name="s">The s.</param>
            /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
            protected void CancelButtonClick(Object s, EventArgs e)
            {
                Response.Redirect(string.Format("~/Issues/IssueList.aspx?pid={0}", ProjectId.ToString()));
            }
		#endregion
		
		#region Private Methods
			/// <summary>
			/// Sets security according to permissions
			/// </summary>
			private void SetFieldSecurity()
			{		
				//check permission objects
				if(User.Identity.IsAuthenticated)
				{
                    //enable editing of description if user has permission or in admin role
                    //if (ITUser.IsInRole(ProjectId, Globals.DefaultRoles[0]) || ITUser.HasPermission(ProjectId, Globals.Permission.EDIT_ISSUE_DESCRIPTION.ToString()))
                    //    FCKDescription.Visible = true;
                    //else
                    //    lblDescription.Visible = true;

                    //if (ITUser.IsInRole(ProjectId, Globals.DefaultRoles[0]) || ITUser.HasPermission(ProjectId, Globals.Permission.EDIT_ISSUE_SUMMARY.ToString()))
                    //  Title.Visible = true;
                    //else
                    //    lblTitle.Visible = true;

                    ////close issue permission check
                    //if(!ITUser.HasPermission(ProjectId,Globals.Permission.CLOSE_ISSUE.ToString()))
                    //{
                    //    cvCloseIssue.Enabled = true;
                    //    Resolution.Enabled=false;
                    //}

					//assign issue permission check
                    //if(!ITUser.HasPermission(ProjectId,Globals.Permission.ASSIGN_ISSUE.ToString()))
                    //    ReAssign.Enabled=false;

					//edit issue permission check
					if(!ITUser.HasPermission(ProjectId,Globals.Permission.EDIT_ISSUE.ToString()))
						LockFields();

                    if (ITUser.HasPermission(ProjectId, Globals.Permission.DELETE_ISSUE.ToString()))
                        DeleteButton.Visible=true;
            
					//if status is closed, check if user is allowed to reopen issue
                    //if(editIssue.StatusId.CompareTo((int)Globals.StatusType.Closed) == 0)
                    //{
                    //    LockFields();
                    //    pnlClosedMessage.Visible = true;

                    //    if(ITUser.HasPermission(ProjectId,Globals.Permission.REOPEN_ISSUE.ToString()))
                    //        lnkReopen.Visible=true;
                    //}
				}
				else
				{
					LockFields();
				}
			}

			/// <summary>
			/// Makes all editable fields on the form disabled
			/// </summary>
			private void LockFields()
			{
                //lnkDone.Visible = false;
                //imgDone.Visible = false;
                //lnkSave.Visible = false;
                //imgSave.Visible = false;
                //imgDelete.Visible = false;
                //lnkDelete.Visible = false;
                //Comps.Enabled = false;
                //dropStatus.Enabled=false;
                //Version.Enabled = false;
                //Type.Enabled = false;
                //Priority.Enabled = false;
                //Resolution.Enabled = false;
                //ReAssign.Enabled=false;
                //FixedInVersion.Enabled = false;
                //DueDate.Enabled = false;
                //chkPrivate.Enabled = false;
                //DueDate.Enabled = false;
                //imgCalendar.Visible = false;
                //ctlCustomFields.IsLocked = true;
                //txtEstimation.Enabled = false;
			}
		#endregion

		#region Properties

            /// <summary>
            /// Gets or sets the issue id.
            /// </summary>
            /// <value>The issue id.</value>
			int IssueId 
			{
				get 
				{
					if (ViewState["IssueId"] == null)
						return 0;
					else
						return (int)ViewState["IssueId"];
				}
				set { ViewState["IssueId"] = value; }
			}

            /// <summary>
            /// Gets or sets the project id.
            /// </summary>
            /// <value>The project id.</value>
            public override int ProjectId
            {
                get
                {
                    if (ViewState["ProjectId"] == null)
                        return 0;
                    else
                        return (int)ViewState["ProjectId"];
                }
                set { ViewState["ProjectId"] = value; }
            }
			
		#endregion																																								
	}
}
