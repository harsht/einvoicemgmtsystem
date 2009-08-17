using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BugNET.UserInterfaceLayer;
using BugNET.BusinessLogicLayer;
using System.Drawing;
using System.Collections.Generic;

namespace BugNET.Projects
{
    /// <summary>
    /// Project Road Map
    /// </summary>
    public partial class Roadmap : BasePage
    {
        /// <summary>
        /// The current version title.
        /// </summary>
        protected string VersionTitle;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Set Project ID from Query String
                if (Request.QueryString["pid"] != null)
                {
                    try
                    {
                        ProjectId = Int32.Parse(Request.QueryString["pid"]);
                    }
                    catch { }
                }

                Project p = Project.GetProjectById(ProjectId);
                ltProject.Text = p.Name;
                litProjectCode.Text = p.Code;

                BindRoadmap();             
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
            BindRoadmap();
        }

        /// <summary>
        /// Binds the roadmap.
        /// </summary>
        private void BindRoadmap()
        {
            
            List<Issue> roadMapIssues = Project.GetRoadMap(ProjectId);
            roadMapIssues.Sort(delegate(Issue l1, Issue l2)
            {
             
                int r = l1.MilestoneId.CompareTo(l2.MilestoneId);
                switch (SortField)
                {
                    case "Category":
                        if (r == 0 && l1.CategoryName != null)
                            r = l1.CategoryName.CompareTo(l2.CategoryName)  * (SortAscending ? -1 : 1);
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
                    case "Estimation":
                        if(r == 0)
                            r = l1.Estimation.CompareTo(l2.Estimation) * (SortAscending ? -1 : 1);
                        break;
                }
                return r; 
             
            });
            //roadMapIssues.Sort(0,2,new IssueComparer(SortField, SortAscending));
            rptRoadMap.DataSource = roadMapIssues;
            rptRoadMap.DataBind();
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
        /// Handles the ItemCreated event of the rptRoadMap control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void rptRoadMap_ItemCreated(object sender, RepeaterItemEventArgs e)
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
        /// Handles the ItemDataBound event of the rptRoadMap control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void rptRoadMap_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Issue b = (Issue)e.Item.DataItem;
                ((HtmlControl)e.Item.FindControl("HeaderRow")).Visible = false;
                if (e.Item.ItemIndex == 0)
                {
                    //first control set title
                    VersionTitle = b.MilestoneName;
                    ((Label)e.Item.FindControl("lblVersion")).Text = VersionTitle;
                    ((HtmlControl)e.Item.FindControl("HeaderRow")).Visible = true;
                }

                if (VersionTitle != b.MilestoneName)
                {
                    VersionTitle = b.MilestoneName;
                    ((Label)e.Item.FindControl("lblVersion")).Text = VersionTitle;
                    ((HtmlControl)e.Item.FindControl("HeaderRow")).Visible = true;
                }

                int[] values = Project.GetRoadMapProgress(ProjectId, b.MilestoneId);
                ((Label)e.Item.FindControl("lblProgress")).Text = string.Format(GetLocalResourceObject("ProgressMessage").ToString(), values[0], values[1], values[0] * 100 / values[1]);

                ((Label)e.Item.FindControl("lblSummary")).Text = b.Title;
                ((Label)e.Item.FindControl("lblComponent")).Text = b.CategoryName;
                ((Label)e.Item.FindControl("lblStatus")).Text = b.StatusName;
                ((Label)e.Item.FindControl("lblType")).Text = b.IssueTypeName;
                ((Label)e.Item.FindControl("lblAssignedTo")).Text = b.AssignedDisplayName;
                ((Label)e.Item.FindControl("ProgressLabel")).Text = b.Progress.ToString() + "%";
                ((Label)e.Item.FindControl("EstimationLabel")).Text = b.Estimation.ToString();

                //change unassigned user to red
                if (b.AssignedUserId == Guid.Empty)
                    ((Label)e.Item.FindControl("lblAssignedTo")).ForeColor = Color.Red;
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
            BindRoadmap();
        }


        /// <summary>
        /// Sorts the issue id click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SortIssueIdClick(object sender, EventArgs e)
        {

            SortField = "Id";
            BindRoadmap();
        }

        /// <summary>
        /// Sorts the status click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SortStatusClick(object sender, EventArgs e)
        {
            SortField = "Status";
            BindRoadmap();
        }

        /// <summary>
        /// Sorts the assigned user click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SortAssignedUserClick(object sender, EventArgs e)
        {
            SortField = "Assigned";
            BindRoadmap();
        }

        /// <summary>
        /// Sorts the progress click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SortProgressClick(object sender, EventArgs e)
        {
            SortField = "Progress";
            BindRoadmap();
        }

        /// <summary>
        /// Sorts the title click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SortTitleClick(object sender, EventArgs e)
        {
            SortField = "Title";
            BindRoadmap();
        }

        /// <summary>
        /// Sorts the issue type click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SortIssueTypeClick(object sender, EventArgs e)
        {
            SortField = "IssueType";
            BindRoadmap();
        }

        /// <summary>
        /// Sorts the estimation click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SortEstimationClick(object sender, EventArgs e)
        {
            SortField = "Estimation";
            BindRoadmap();
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
    }
}
