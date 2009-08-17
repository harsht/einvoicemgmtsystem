namespace BugNET.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BugNET.BusinessLogicLayer;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Collections.Generic;
    using BugNET.UserInterfaceLayer;


	/// <summary>
	///	Display Issues grid
	/// </summary>
	public partial class DisplayIssues : System.Web.UI.UserControl
	{
        /// <summary>
        /// Datasource 
        /// </summary>
		private List<Issue> _DataSource;
        /// <summary>
        /// Event that fires on a databind
        /// </summary>
		public event EventHandler RebindCommand;
        /// <summary>
        /// Array of issue columns
        /// </summary>
        private string[] _arrIssueColumns = new string[] { "4", "5", "6", "7", "8", "9", "10","11","12","13","14","15","16","17","18","19"};

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, System.EventArgs e)
        {
            if ((Request.Cookies[Globals.IssueColumns] != null) && (Request.Cookies[Globals.IssueColumns].Value != String.Empty))
                _arrIssueColumns = Request.Cookies[Globals.IssueColumns].Value.Split();

            gvIssues.PageSize = PageSize;
           
        }

        /// <summary>
        /// Sets the RSS  URL.
        /// </summary>
        /// <value>The RSS  URL.</value>
        public string RssUrl
        {
            set { lnkRSS.NavigateUrl = value; }
        }
   
        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
		public List<Issue> DataSource 
		{
			get { return _DataSource; }
			set { _DataSource = value; }
		}

        /// <summary>
        /// Binds a data source to the invoked server control and all its child controls.
        /// </summary>
		public override void DataBind() 
		{
			if(this.DataSource.Count >0)
			{
                _DataSource.Sort(new IssueComparer(SortField, SortAscending));
				gvIssues.Visible=true;
                DisplayColumns();
                gvIssues.DataSource = this.DataSource;
                gvIssues.DataBind();

                Table tb = (Table)gvIssues.Controls[0];
                TableRow pagerRow = tb.Rows[tb.Rows.Count - 1]; //last row in the table is for bottom pager
                pagerRow.Cells[0].Attributes.Remove("colspan");
                pagerRow.Cells[0].Attributes.Add("colspan",_arrIssueColumns.Length.ToString() + 4);
                //pagerRow.Cells[1].Attributes.Remove("colspan");
                tblOptions.Visible = true;
				lblResults.Visible=false;

                int projectId = ((Issue)_DataSource[0]).ProjectId;
                dropCategory.DataSource = Category.GetCategoriesByProjectId(projectId);
                dropCategory.DataBind();
                dropMilestone.DataSource = Milestone.GetMilestoneByProjectId(projectId);
                dropMilestone.DataBind();
                dropOwner.DataSource = ITUser.GetUsersByProjectId(projectId);
                dropOwner.DataBind();
                dropPriority.DataSource = Priority.GetPrioritiesByProjectId(projectId);
                dropPriority.DataBind();
                dropStatus.DataSource = Status.GetStatusByProjectId(projectId);
                dropStatus.DataBind();
                dropType.DataSource = IssueType.GetIssueTypesByProjectId(projectId);
                dropType.DataBind();
                dropAssigned.DataSource = ITUser.GetUsersByProjectId(projectId);
                dropAssigned.DataBind();
                dropResolution.DataSource = Resolution.GetResolutionsByProjectId(projectId);
                dropResolution.DataBind();

                //lnkRSS.NavigateUrl = string.Format("~/Rss.aspx?{0}&channel=7", Request.QueryString.ToString());
			}
			else
			{
                BulkEditPanel.Visible = false;
                tblOptions.Visible = false;
				lblResults.Visible=true;
				gvIssues.Visible=false;
			}
			
		}

        /// <summary>
        /// Handles the Click event of the ExportExcelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ExportExcelButton_Click(object sender, EventArgs e)
        {
            GridViewExportUtil.Export("Issues.xls", this.gvIssues);
        }

        /// <summary>
        /// Displays the columns.
        /// </summary>
        private void DisplayColumns()
        {
            // Hide all the DataGrid columns
            for (int index = 4; index < gvIssues.Columns.Count; index++)
                gvIssues.Columns[index].Visible = false;

            // Display columns based on the _arrIssueColumns array (retrieved from cookie)
            foreach (string colIndex in _arrIssueColumns)
                gvIssues.Columns[Int32.Parse(colIndex)].Visible = true;
        }

        /// <summary>
        /// Selects the columns click.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SelectColumnsClick(Object s, EventArgs e)
        {
            pnlSelectColumns.Visible = true;
            foreach (string colIndex in _arrIssueColumns)
            {
                ListItem item = lstIssueColumns.Items.FindByValue(colIndex);
                if (item != null)
                    item.Selected = true;
            }
            OnRebindCommand(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the Click event of the SaveIssues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SaveIssues_Click(object sender, EventArgs e)
        {
            //TODO: Ajax progress bar when this is running;
            string ids = GetSelectedIssueIds();
            if (ids.Length > 0)
            {
                foreach (string s in ids.Split(new char[] { ',' }))
                {
                    Issue issue = Issue.GetIssueById(Convert.ToInt32(s));
                    if (issue != null)
                    {
                        //modify properties if selected
                        if (dropCategory.SelectedValue != 0)
                            issue.CategoryId = dropCategory.SelectedValue;
                        if (dropMilestone.SelectedValue != 0)
                            issue.MilestoneId = dropMilestone.SelectedValue;
                        if (dropOwner.SelectedValue != string.Empty)
                            issue.OwnerUserName = dropOwner.SelectedValue;
                        if (dropPriority.SelectedValue != 0)
                            issue.PriorityId = dropPriority.SelectedValue;
                        if (dropResolution.SelectedValue != 0)
                            issue.ResolutionId = dropResolution.SelectedValue;
                        if (dropStatus.SelectedValue != 0)
                            issue.StatusId = dropStatus.SelectedValue;
                        if (dropAssigned.SelectedValue != string.Empty)
                            issue.AssignedUserName = dropAssigned.SelectedValue;

                        //save issue
                        issue.Save();
                    }
                }
            }

            OnRebindCommand(EventArgs.Empty);         
        }
        /// <summary>
        /// Gets the selected issues.
        /// </summary>
        /// <returns></returns>
        private string GetSelectedIssueIds()
        {
            string ids = string.Empty;
             foreach (GridViewRow gvr in gvIssues.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    if (((CheckBox)gvr.Cells[0].Controls[1]).Checked)
                        ids += gvIssues.DataKeys[gvr.DataItemIndex].Value.ToString() + ",";
                }
            }
             return ids.EndsWith(",") == true ? ids.TrimEnd(new char[] { ',' }) : ids;
        }
       
        /// <summary>
        /// Saves the click.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SaveClick(Object s, EventArgs e)
        {
            string strIssueColumns = " 0";
            foreach (ListItem item in lstIssueColumns.Items)
                if (item.Selected)
                    strIssueColumns += " " + item.Value;
            strIssueColumns = strIssueColumns.Trim();

            _arrIssueColumns = strIssueColumns.Split();

            Response.Cookies[Globals.IssueColumns].Value = strIssueColumns;
            Response.Cookies[Globals.IssueColumns].Path = "/";
            Response.Cookies[Globals.IssueColumns].Expires = DateTime.MaxValue;

            pnlSelectColumns.Visible = false;

            OnRebindCommand(EventArgs.Empty);
        }

        /// <summary>
        /// Cancels the click.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void CancelClick(Object s, EventArgs e)
        {
            pnlSelectColumns.Visible = false;
            OnRebindCommand(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the rebind command event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
		void OnRebindCommand(EventArgs e) 
		{
			if (RebindCommand != null)
				RebindCommand(this, e);
		}

        /// <summary>
        /// Gets or sets the index of the current page.
        /// </summary>
        /// <value>The index of the current page.</value>
		public int CurrentPageIndex 
		{
			get { return gvIssues.PageIndex; }
			set { gvIssues.PageIndex = value; }
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
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize
        {
            get { return gvIssues.PageSize; }
            set { gvIssues.PageSize = value; }
        }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass
        {
            get { return gvIssues.CssClass; }
            set { gvIssues.CssClass = value; }
        }

        /// <summary>
        /// Gets or sets the header CSS class.
        /// </summary>
        /// <value>The header CSS class.</value>
        public string HeaderCssClass
        {
            get { return gvIssues.HeaderStyle.CssClass; }
            set { gvIssues.HeaderStyle.CssClass = value; }
        }

        /// <summary>
        /// Gets or sets the color of the header back.
        /// </summary>
        /// <value>The color of the header back.</value>
        public Color HeaderBackColor
        {
            get { return gvIssues.HeaderStyle.BackColor; }
            set { gvIssues.HeaderStyle.BackColor = value; }
        }

        /// <summary>
        /// Handles the RowCreated event of the gvIssues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gvIssues_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 1; i < e.Row.Cells.Count; i++)
                {
                    TableCell tc = e.Row.Cells[i];
                    if (tc.HasControls())
                    {
                        // search for the header link  
                        LinkButton lnk = (LinkButton)tc.Controls[0];
                        if (lnk != null)
                        {
                            // inizialize a new image
                            System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                            // setting the dynamically URL of the image
                            img.ImageUrl = "~/images/" + (SortAscending ? "bullet_arrow_up" : "bullet_arrow_down") + ".gif";
                            img.CssClass = "icon";
                            // checking if the header link is the user's choice
                            if (SortField == lnk.CommandArgument)
                            {
                                // adding a space and the image to the header link
                                //tc.Controls.Add(new LiteralControl(" "));
                                tc.Controls.Add(img);
                            }


                        }
                    }
                }
            }
        }

        protected void gvIssues_PreRender(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the RowDataBound event of the gvIssues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gvIssues_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Find the checkbox control in header and add an attribute
                ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
            }         
            if (e.Row.RowType == DataControlRowType.Pager)
            {
               
                 PresentationUtils.SetPagerButtonStates(gvIssues, e.Row, this.Page);

                //e.Row.Cells.AddAt(0, new TableCell());
                //e.Row.Cells[0].Text = "Total Issues: " + _DataSource.Count;
                //e.Row.Attributes.Remove("colspan");
                //e.Row.Cells[0].ColumnSpan = 2;
                //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                //e.Row.Cells[0].ColumnSpan -= 2;
                //e.Row.Cells[1].ColumnSpan -= 0;
                //e.Row.Cells[1].HorizontalAlign = gvIssues.PagerStyle.HorizontalAlign;
                //e.Row.Cells[1].BackColor = gvIssues.PagerStyle.BackColor;
                //e.Row.Cells[1].ForeColor = gvIssues.PagerStyle.ForeColor;
                // get your controls from the gridview
                //DropDownList ddlPages = (DropDownList)e.Row.Cells[1].FindControl("ddlPages");
                //Label lblPageCount = (Label)e.Row.Cells[1].FindControl("lblPageCount");

                //if (ddlPages != null)
                //{
                //    // populate pager
                //    for (int i = 0; i < gvIssues.PageCount; i++)
                //    {
                //        int intPageNumber = i + 1;
                //        ListItem lstItem =
                //            new ListItem(intPageNumber.ToString());

                //        if (i == gvIssues.PageIndex)
                //            lstItem.Selected = true;

                //        ddlPages.Items.Add(lstItem);
                //    }
                //}

                //// populate page count
                //if (lblPageCount != null)
                //    lblPageCount.Text = gvIssues.PageCount.ToString();

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.background='#F7F7EC'");

                if( e.Row.RowState == DataControlRowState.Normal)
                    e.Row.Attributes.Add("onmouseout", "this.style.background=''");
                else if( e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.Attributes.Add("onmouseout", "this.style.background='#fafafa'");
 
                Issue b = ((Issue)e.Row.DataItem);

                //Private issue check
                if (b.Visibility == 1 && b.AssignedDisplayName != Page.User.Identity.Name && b.CreatorDisplayName != Page.User.Identity.Name && (!ITUser.IsInRole(Globals.SuperUserRole) || !ITUser.IsInRole(Globals.DefaultRoles[0])))
                    e.Row.Visible = false;

                e.Row.FindControl("imgPrivate").Visible = b.Visibility == 0 ? false : true;

                double warnPeriod = 7; //TODO: Add this to be configurable in the users profile
                bool isDue = b.DueDate <= DateTime.Now.AddDays(warnPeriod) && b.DueDate > DateTime.MinValue;
                bool noOwner = b.AssignedUserId == Guid.Empty;
                //if (noOwner || isDue)
                //{
                //    e.Row.Attributes.Add("style", "background-color:#ffdddc");
                //    e.Row.Attributes.Add("onmouseout", "this.style.background='#ffdddc'");
                //}
                ((HtmlControl)e.Row.FindControl("ProgressBar")).Attributes.CssStyle.Add("width", b.Progress.ToString() + "%");
                //((HtmlControl)e.Row.FindControl("ProgressBar")).Controls.Add(new LiteralControl(b.Progress.ToString()));
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlPages control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlPages_SelectedIndexChanged(Object sender, EventArgs e)
        {
            gvIssues.PageIndex = ((DropDownList)sender).SelectedIndex;
            OnRebindCommand(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the RowCommand event of the gvIssues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void gvIssues_RowCommand(object sender, CommandEventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the Sorting event of the gvIssues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void gvIssues_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortField = e.SortExpression;
            OnRebindCommand(EventArgs.Empty);
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the gvIssues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvIssues_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIssues.PageIndex = e.NewPageIndex;
            OnRebindCommand(EventArgs.Empty);
        }
	}
}
