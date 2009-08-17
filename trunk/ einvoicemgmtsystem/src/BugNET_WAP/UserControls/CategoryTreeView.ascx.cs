namespace BugNET.UserControls
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using BugNET.BusinessLogicLayer;

    public partial class CategoryTreeView : System.Web.UI.UserControl
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        { }

        /// <summary>
        /// Gets or sets a value indicating whether [show bug count].
        /// </summary>
        /// <value><c>true</c> if [show bug count]; otherwise, <c>false</c>.</value>
        public bool ShowIssueCount
        {
            get
            {
                if (ViewState["ShowIssueCount"] == null)
                    return true;
                else
                    return (bool)ViewState["ShowIssueCount"];
            }
            set { ViewState["ShowIssueCount"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show root].
        /// </summary>
        /// <value><c>true</c> if [show root]; otherwise, <c>false</c>.</value>
        public bool ShowRoot
        {
            get
            {
                if (ViewState["ShowRoot"] == null)
                    return false;
                else
                    return (bool)ViewState["ShowRoot"];
            }
            set { ViewState["ShowRoot"] = value; }
        }

        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
        public int ProjectId
        {
            get
            {
                if (ViewState["ProjectId"] == null)
                    return -1;
                else
                    return (int)ViewState["ProjectId"];
            }
            set { ViewState["ProjectId"] = value; }
        }

        /// <summary>
        /// Gets the component count.
        /// </summary>
        /// <value>The component count.</value>
        public int CategoryCount
        {
            get
            {

                if (ShowRoot && tvCategory.Nodes.Count > 1)
                {
                    return tvCategory.Nodes.Count - 1;
                }
                else
                {
                    return tvCategory.Nodes.Count;
                }

            }

        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            tvCategory.Nodes.Clear();
            List<Category> categories = Category.GetRootCategoriesByProjectId(ProjectId);

            if (ShowRoot)
            {
                tvCategory.Nodes.Add(new TreeNode("Root Category", ""));
                PopulateNodes(categories, tvCategory.Nodes[0].ChildNodes);
            }
            else
            {
                PopulateNodes(categories, tvCategory.Nodes);
            }

            tvCategory.ExpandAll();
        }
        /// <summary>
        /// Populates the nodes.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="nodes">The nodes.</param>
        private void PopulateNodes(List<Category> list, TreeNodeCollection nodes)
        {
            foreach (Category c in list)
            {
                TreeNode tn = new TreeNode();

                if (ShowIssueCount)
                {
                    tn.Text = String.Format("{0}</a></td><td style='width:100%;text-align:right;'><a>{1}", c.Name, Issue.GetIssueCountByProjectAndCategory(ProjectId, c.Id));
                    tn.NavigateUrl = String.Format("~/Issues/IssueList.aspx?pid={0}&c={1}", ProjectId, c.Id);
                }
                else
                {
                    tn.Text = c.Name;
                }
                tn.Value = c.Id.ToString();
                nodes.Add(tn);

                //If node has child nodes, then enable on-demand populating
                tn.PopulateOnDemand = (c.ChildCount > 0);
            }
        }

        /// <summary>
        /// Gets the selected node.
        /// </summary>
        /// <value>The selected node.</value>
        public TreeNode SelectedNode
        {
            get { return tvCategory.SelectedNode; }
        }

        /// <summary>
        /// Gets the selected value.
        /// </summary>
        /// <value>The selected value.</value>
        public string SelectedValue
        {
            get { return tvCategory.SelectedValue; }
        }
        /// <summary>
        /// Populates the sub level.
        /// </summary>
        /// <param name="parentid">The parentid.</param>
        /// <param name="parentNode">The parent node.</param>
        private void PopulateSubLevel(int parentid, TreeNode parentNode)
        {
            PopulateNodes(Category.GetChildCategoriesByCategoryId(parentid), parentNode.ChildNodes);
        }

        /// <summary>
        /// Handles the TreeNodePopulate event of the tvComponent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs"/> instance containing the event data.</param>
        protected void tvCategory_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            PopulateSubLevel(Int32.Parse(e.Node.Value), e.Node);
        }
    }
}
