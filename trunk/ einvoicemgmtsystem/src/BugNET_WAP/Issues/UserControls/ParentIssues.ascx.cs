namespace BugNET.Issues.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BugNET.BusinessLogicLayer;
    using BugNET.UserInterfaceLayer;

	/// <summary>
	///		Summary description for ParentBugs.
	/// </summary>
	public partial class ParentIssues : System.Web.UI.UserControl, IIssueTab
	{
		
        private int _IssueId = 0;
        private int _ProjectId = 0;

        /// <summary>
        /// Binds the related.
        /// </summary>
		protected void BindRelated() 
		{
			IssuesDataGrid.DataSource = RelatedIssue.GetParentIssues(_IssueId);
            IssuesDataGrid.DataKeyField = "IssueId";
            IssuesDataGrid.DataBind();
		}


        /// <summary>
        /// GRDs the bugs item data bound.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.DataGridItemEventArgs"/> instance containing the event data.</param>
		protected void grdIssueItemDataBound(Object s, DataGridItemEventArgs e) 
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				RelatedIssue currentBug = (RelatedIssue)e.Item.DataItem;

                Label lblIssueId = (Label)e.Item.FindControl("IssueIdLabel");
				lblIssueId.Text = currentBug.IssueId.ToString();
			}
		}

        /// <summary>
        /// GRDs the bugs item command.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.DataGridCommandEventArgs"/> instance containing the event data.</param>
		protected void grdBugsItemCommand(Object s, DataGridCommandEventArgs e) 
		{
            int currentIssueId = (int)IssuesDataGrid.DataKeys[e.Item.ItemIndex];
            RelatedIssue.DeleteParentIssue(_IssueId, currentIssueId);
			BindRelated();
		}

        /// <summary>
        /// Adds the related bug.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void AddRelatedIssue(Object s, EventArgs e) 
		{
			if (IssueIdTextBox.Text == String.Empty)
				return;

			if (Page.IsValid) 
			{
				RelatedIssue.CreateNewParentIssue(_IssueId, Int32.Parse(IssueIdTextBox.Text) );
                IssueIdTextBox.Text = String.Empty;
				BindRelated();
			}
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
            BindRelated();
        }


        #endregion
    }
}
