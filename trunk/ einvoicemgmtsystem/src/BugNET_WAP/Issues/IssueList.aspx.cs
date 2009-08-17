using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text;

namespace BugNET.Issues
{
	/// <summary>
	/// Summary description for Issue List.
	/// </summary>
    public partial class IssueList : BasePage 
	{
		#region Private Variables
            /// <summary>
            /// 
            /// </summary>
		    protected CheckBox IncludeComments;
            private const string ISSUELISTSTATE = "IssueListState";
		#endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!Page.IsPostBack)
            {                
                if (!User.Identity.IsAuthenticated)
                {   
                    dropView.Items.Remove(dropView.Items.FindByValue("Relevant"));
                    dropView.Items.Remove(dropView.Items.FindByValue("Assigned"));
                    dropView.Items.Remove(dropView.Items.FindByValue("Owned"));
                    dropView.Items.Remove(dropView.Items.FindByValue("Created"));
                    dropView.SelectedIndex = 1;
                }

                // Set Project ID from Query String
                if (Request.QueryString["pid"] != null)
                {
                    try
                    {
                        ProjectId = Int32.Parse(Request.QueryString["pid"]);
                    }
                    catch { }
                }

                //lblProjectName.Text = dropProjects.SelectedItem.Text;

                IssueListState state = (IssueListState)Session[ISSUELISTSTATE];
                if (state != null)
                {
                    dropView.SelectedValue = state.ViewIssues;
                    ProjectId = state.ProjectId;
                    ctlDisplayIssues.CurrentPageIndex = state.IssueListPageIndex;
                }
           
                BindIssues();
                  
            }
	
		}

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Page_PreRender(object sender, System.EventArgs e)
        {
           
            // Intention is to restore IssueList page state when if it is redirected back to.
            // Put all necessary data in IssueListState object and save it in the session.
            IssueListState state = (IssueListState)Session[ISSUELISTSTATE];
            if (state == null) state = new IssueListState();
            state.ViewIssues = dropView.SelectedValue;
            state.ProjectId = ProjectId;
            state.IssueListPageIndex = ctlDisplayIssues.CurrentPageIndex;
            Session[ISSUELISTSTATE] = state;
        }

        #region Querystring Properties
        /// <summary>
        /// Returns the component Id from the querystring
        /// </summary>
        public string IssueCategoryId
        {
            get
            {
                if (Request.Params["c"] == null)
                {
                    return string.Empty;
                }
                return Request.Params["c"];
            }
        }
        /// <summary>
        /// Returns the keywords from the querystring
        /// </summary>
        public string Key
        {
            get
            {
                if (Request.Params["key"] == null)
                {
                    return string.Empty;
                }
                return Request.Params["key"].Replace("+", " ");
            }
        }
        /// <summary>
        /// Returns the Milestone Id from the querystring
        /// </summary>
        public string IssueMilestoneId
        {
            get
            {
                if (Request.Params["m"] == null)
                {
                    return string.Empty;
                }
                return Request.Params["m"].ToString();
            }
        }

       
        /// <summary>
        /// Returns the priority Id from the querystring
        /// </summary>
        public string IssuePriorityId
        {
            get
            {
                if (Request.Params["p"] == null)
                {
                    return string.Empty;
                }
                return Request.Params["p"].ToString();
            }
        }
        /// <summary>
        /// Returns the Type Id from the querystring
        /// </summary>
        public string IssueTypeId
        {
            get
            {
                if (Request.Params["t"] == null)
                {
                    return string.Empty;
                }
                return Request.Params["t"].ToString();
            }
        }
        /// <summary>
        /// Returns the status Id from the querystring
        /// </summary>
        public string IssueStatusId
        {
            get { 
                if (Request.Params["s"] == null)
                {
                    return string.Empty;
                }
                return Request.Params["s"].ToString(); }
        }
        /// <summary>
        /// Returns the assigned to user Id from the querystring
        /// </summary>
        public string AssignedUserName
        {
            get
            {
                if (Request.Params["u"] == null)
                {
                    return string.Empty;
                }
                return Request.Params["u"].ToString();
            }
        }

        /// <summary>
        /// Gets the name of the reporter user.
        /// </summary>
        /// <value>The name of the reporter user.</value>
        public string ReporterUserName
        {
            get
            {
                if (Request.Params["ru"] == null)
                {
                    return string.Empty;
                }
                return Request.Params["ru"].ToString();
            }
        }
        /// <summary>
        /// Returns the hardware Id from the querystring
        /// </summary>
        public string IssueResolutionId
        {
            get
            {
                if (Request.Params["r"] == null)
                {
                    return string.Empty;
                }
                return Request.Params["r"].ToString();
            }
        }

        /// <summary>
        /// Gets the bug id.
        /// </summary>
        /// <value>The bug id.</value>
        public int IssueId
        {
            get { return Convert.ToInt32(Request.Params["bid"]); }
        }
        #endregion

        /// <summary>
        /// Views the selected index changed.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ViewSelectedIndexChanged(Object s, EventArgs e)
        {
            ctlDisplayIssues.CurrentPageIndex = 0;
            if(User.Identity.IsAuthenticated)
                ctlDisplayIssues.PageSize = WebProfile.Current.IssuesPageSize;
            BindIssues();
        }


        /// <summary>
        /// Issueses the rebind.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void IssuesRebind(Object s, EventArgs e)
        {
            BindIssues();
        }

        /// <summary>
        /// Binds the issues.
        /// </summary>
        protected void BindIssues()
        {
           List<Issue> colIssues = null;
     
            //only do this if the user came from the project summary page.
           if (Request.UrlReferrer != null && Request.UrlReferrer.ToString().Contains("ProjectSummary"))
           {
               dropView.SelectedValue = string.Empty;
               QueryClause q;
               bool isStatus = false;
               string BooleanOperator = "AND";
               List<QueryClause> queryClauses = new List<QueryClause>();
               if (!string.IsNullOrEmpty(IssueCategoryId))
               {
                   q = new QueryClause(BooleanOperator, "IssueCategoryId", "=", IssueCategoryId.ToString(), SqlDbType.Int, false);
                   queryClauses.Add(q);
               }
               if (!string.IsNullOrEmpty(IssueTypeId))
               {
                   q = new QueryClause(BooleanOperator, "IssueTypeId", "=", IssueTypeId.ToString(), SqlDbType.Int, false);
                   queryClauses.Add(q);
               }
               if (!string.IsNullOrEmpty(IssueMilestoneId))
               {
                   //if zero, do a null comparison.
                   if (IssueMilestoneId == "0")
                   {
                       q = new QueryClause(BooleanOperator, "IssueMilestoneId", "IS", null, SqlDbType.Int, false);
                   }
                   else
                   {
                       q = new QueryClause(BooleanOperator, "IssueMilestoneId", "=", IssueMilestoneId, SqlDbType.Int, false);
                   }                  
                   queryClauses.Add(q);
               }
               if (!string.IsNullOrEmpty(IssueResolutionId))
               {
                   q = new QueryClause(BooleanOperator, "IssueResolutionId", "=", IssueResolutionId.ToString(), SqlDbType.Int, false);
                   queryClauses.Add(q);
               }
               if (!string.IsNullOrEmpty(IssuePriorityId))
               {
                   q = new QueryClause(BooleanOperator, "IssuePriorityId", "=", IssuePriorityId.ToString(), SqlDbType.Int, false);
                   queryClauses.Add(q);
               }
               if (!string.IsNullOrEmpty(IssueStatusId))
               {
                   isStatus = true;
                   q = new QueryClause(BooleanOperator, "IssueStatusId", "=", IssueStatusId.ToString(), SqlDbType.Int, false);
                   queryClauses.Add(q);
               }
               if (!string.IsNullOrEmpty(AssignedUserName))
               {
                   q = new QueryClause(BooleanOperator, "IssueAssignedUserId", "=", AssignedUserName, SqlDbType.NVarChar, false);
                   queryClauses.Add(q);
               }

               //exclude all closed status's
               if (!isStatus)
               {
                   List<Status> status = Status.GetStatusByProjectId(ProjectId).FindAll(delegate(Status s) { return s.IsClosedState == true; });
                   foreach (Status st in status)
                   {
                       q = new QueryClause(BooleanOperator, "IssueStatusId", "<>", st.Id.ToString(), SqlDbType.Int, false);
                       queryClauses.Add(q);
                   }
               }
               //q = new QueryClause(BooleanOperator, "new", "=", "another one", SqlDbType.NVarChar, true);
               //queryClauses.Add(q);
               colIssues = Issue.PerformQuery(ProjectId, queryClauses);
               ctlDisplayIssues.RssUrl = string.Format("~/Rss.aspx?{0}&channel=7", Request.QueryString.ToString());
           }
           else
           {  
         
               switch (dropView.SelectedValue)
               {
                   case "Relevant":
                       colIssues = Issue.GetIssuesByRelevancy(ProjectId, User.Identity.Name);
                       ctlDisplayIssues.RssUrl = string.Format("~/Rss.aspx?pid={0}&channel=8", ProjectId);
                       break;
                   case "Assigned":
                       colIssues = Issue.GetIssuesByAssignedUserName(ProjectId, User.Identity.Name);
                       ctlDisplayIssues.RssUrl = string.Format("~/Rss.aspx?pid={0}&channel=9", ProjectId);
                       break;
                   case "Owned":
                       colIssues = Issue.GetIssuesByOwnerUserName(ProjectId, User.Identity.Name);
                       ctlDisplayIssues.RssUrl = string.Format("~/Rss.aspx?pid={0}&channel=10", ProjectId);
                       break;
                   case "Created":
                       colIssues = Issue.GetIssuesByCreatorUserName(ProjectId, User.Identity.Name);
                       ctlDisplayIssues.RssUrl = string.Format("~/Rss.aspx?pid={0}&channel=11", ProjectId);
                       break;
                   case "All":
                       colIssues = Issue.GetIssuesByProjectId(ProjectId);
                       ctlDisplayIssues.RssUrl = string.Format("~/Rss.aspx?pid={0}&channel=12", ProjectId);
                       break;
                   default:
                       colIssues = new List<Issue>();
                       break;
               }
           }
           
            ctlDisplayIssues.DataSource = colIssues;
            ctlDisplayIssues.DataBind();
        }

        /// <summary>
        /// Adds the issue.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void AddIssue(Object s, EventArgs e)
        {
            Response.Redirect("~/Issues/IssueDetail.aspx?pid=" + ProjectId);
        }
       
	}
}
