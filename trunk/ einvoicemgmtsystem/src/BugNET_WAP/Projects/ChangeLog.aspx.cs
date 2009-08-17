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
using System.Collections.Generic;

namespace BugNET.Projects
{

    /// <summary>
    /// Summary description for ChangeLog.
    /// </summary>
	public partial class ChangeLog : BugNET.UserInterfaceLayer.BasePage 
	{
		protected string VersionTitle;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
                // Set Project ID from Query String
                if (Request.QueryString["pid"] != null)
                {
                    try
                    {
                        ProjectId = Int32.Parse(Request.QueryString["pid"]);
                        //dropProjects.SelectedValue = Int32.Parse(Request.QueryString["pid"]);
                    }
                    catch { }
                }


                Project p = Project.GetProjectById(ProjectId);
                ltProject.Text = p.Name;
                litProjectCode.Text = p.Code;

                BindChangeLog();
			}

            // The ExpandIssuePaths method is called to handle
            // the SiteMapResolve event.
            SiteMap.SiteMapResolve +=
              new SiteMapResolveEventHandler(this.ExpandProjectPaths);

			
		}

          /// <summary>
        /// Projects the selected index changed.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ProjectSelectedIndexChanged(Object s, EventArgs e)
        {
            BindChangeLog();
        }

        /// <summary>
        /// Binds the project summary.
        /// </summary>
        private void BindChangeLog()
        {
            List<Issue> changeLogIssues = Project.GetChangeLog(ProjectId);
            changeLogIssues.Sort(delegate(Issue l1, Issue l2)
            {

                int r = l1.MilestoneId.CompareTo(l2.MilestoneId);
                switch (SortField)
                {
                    case "Category":
                        if (r == 0 && l1.CategoryName != null)
                            r = l1.CategoryName.CompareTo(l2.CategoryName) * (SortAscending ? -1 : 1);
                        break;
                    case "IssueType":
                        if (r == 0 && l1.IssueTypeName != null)
                            r = l1.IssueTypeName.CompareTo(l2.IssueTypeName) * (SortAscending ? -1 : 1);
                        break;
                    case "Status":
                        if (r == 0 && l1.StatusName != null)
                            r = l1.StatusName.CompareTo(l2.StatusName) * (SortAscending ? -1 : 1);
                        break;
                    case "Assigned":
                        if (r == 0 && l1.AssignedUserName != null)
                            r = l1.AssignedUserName.CompareTo(l2.AssignedUserName) * (SortAscending ? -1 : 1);
                        break;
                    case "Progress":
                        if (r == 0)
                            r = l1.Progress.CompareTo(l2.Progress) * (SortAscending ? -1 : 1);
                        break;
                    case "Title":
                        if (r == 0 && l1.Title != null)
                            r = l1.Title.CompareTo(l2.Title) * (SortAscending ? -1 : 1);
                        break;
                    case "Id":
                        if (r == 0)
                            r = l1.Id.CompareTo(l2.Id) * (SortAscending ? -1 : 1);
                        break;
                }
                return r;

            });
            rptChangeLog.DataSource = changeLogIssues;
            rptChangeLog.DataBind();
        }

        /// <summary>
        /// Handles the ItemCreated event of the rptChangeLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void rptChangeLog_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                foreach (Control c in e.Item.Controls)
                {
                    if (c.GetType() == typeof(HtmlTableCell) && c.ID == "td" + SortField)
                    {

                        System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                        // setting the dynamically URL of the image
                        img.ImageUrl = "~/images/" + (SortAscending ? "bullet_arrow_up" : "bullet_arrow_down") + ".gif";
                        img.CssClass = "icon";
                        c.Controls.Add(img);
                    }
                }

            }
        }

        /// <summary>
        /// Sorts the category click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SortCategoryClick(object sender, EventArgs e)
        {
            SortField = "Category";
            BindChangeLog();
        }


        /// <summary>
        /// Sorts the issue id click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SortIssueIdClick(object sender, EventArgs e)
        {

            SortField = "Id";
            BindChangeLog();
        }

        /// <summary>
        /// Sorts the status click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SortStatusClick(object sender, EventArgs e)
        {
            SortField = "Status";
            BindChangeLog();
        }

        /// <summary>
        /// Sorts the assigned user click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SortAssignedUserClick(object sender, EventArgs e)
        {
            SortField = "Assigned";
            BindChangeLog();
        }

        /// <summary>
        /// Sorts the title click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SortTitleClick(object sender, EventArgs e)
        {
            SortField = "Title";
            BindChangeLog();
        }

        /// <summary>
        /// Sorts the issue type click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SortIssueTypeClick(object sender, EventArgs e)
        {
            SortField = "IssueType";
            BindChangeLog();
        }

        /// <summary>
        /// Gets or sets the sort field.
        /// </summary>
        /// <value>The sort field.</value>
        string SortField
        {
            get
            {
                object o = ViewState["SortField"];
                if (o == null)
                {
                    return String.Empty;
                }
                return (string)o;
            }

            set
            {
                if (value == SortField)
                {
                    // same as current sort file, toggle sort direction
                    SortAscending = !SortAscending;
                }
                ViewState["SortField"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [sort ascending].
        /// </summary>
        /// <value><c>true</c> if [sort ascending]; otherwise, <c>false</c>.</value>
        bool SortAscending
        {
            get
            {
                object o = ViewState["SortAscending"];
                if (o == null)
                {
                    return true;
                }
                return (bool)o;
            }

            set
            {
                ViewState["SortAscending"] = value;
            }
        }

        /// <summary>
        /// Expands the project paths.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.SiteMapResolveEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        private SiteMapNode ExpandProjectPaths(Object sender, SiteMapResolveEventArgs e)
        {
            SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
            SiteMapNode tempNode = currentNode;

            // The current node, and its parents, can be modified to include
            // dynamic querystring information relevant to the currently
            // executing request.
            if (ProjectId != 0)
            {
                tempNode.Url = tempNode.Url + "?pid=" + ProjectId.ToString();
            }

            if ((null != (tempNode = tempNode.ParentNode)) &&
                (ProjectId != 0))
            {
                tempNode.Url = tempNode.Url + "?pid=" + ProjectId.ToString();
            }

            return currentNode;
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
             new SiteMapResolveEventHandler(this.ExpandProjectPaths);
        }

		#region Web Form Designer generated code
        /// <summary>
        /// Overrides the default OnInit to provide a security check for pages
        /// </summary>
        /// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.rptChangeLog.ItemDataBound +=new RepeaterItemEventHandler(rptChangeLog_ItemDataBound);
		}
		#endregion

        /// <summary>
        /// Handles the ItemDataBound event of the rptChangeLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
		private void rptChangeLog_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Issue issue = (Issue)e.Item.DataItem;
				if(e.Item.ItemIndex == 0)
				{
					//first control set title
                    VersionTitle = issue.MilestoneName;
					((Label)e.Item.FindControl("lblVersion")).Text =  VersionTitle;
                    
				}

                if (VersionTitle != issue.MilestoneName)
                {
                    VersionTitle = issue.MilestoneName;
                    ((Label)e.Item.FindControl("lblVersion")).Text = VersionTitle;
                }
                else if(e.Item.ItemIndex != 0)
                {
                    e.Item.FindControl("row").Visible = false;
                }

                ((Label)e.Item.FindControl("lblSummary")).Text = issue.Title;
                ((Label)e.Item.FindControl("lblComponent")).Text = issue.CategoryName;
				((Label)e.Item.FindControl("lblStatus")).Text = issue.StatusName;
				((Label)e.Item.FindControl("lblType")).Text =issue.IssueTypeName;
				((Label)e.Item.FindControl("lblAssignedTo")).Text = issue.AssignedDisplayName;		
			}
		}
	}
}
