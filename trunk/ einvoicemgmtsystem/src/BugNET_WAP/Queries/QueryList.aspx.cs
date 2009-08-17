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
using BugNET.UserControls;
using System.Collections.Generic;

namespace BugNET.Queries
{
	/// <summary>
    /// This page displays a list of existing queries
	/// </summary>
    public partial class QueryList : BugNET.UserInterfaceLayer.BasePage 
	{
	
		protected DisplayIssues ctlDisplayIssues;
		protected PickQuery dropQueries;

		#region Web Form Designer generated code
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
			this.ctlDisplayIssues.RebindCommand += new System.EventHandler(IssuesRebind);
		}
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
                // Set Project ID from Query String
                if (Request.QueryString["pid"] != null)
                {
                    try
                    {
                        ProjectId = Int32.Parse(Request.QueryString["pid"]);
                    }
                    catch { }
                }

                if (!Page.User.Identity.IsAuthenticated || !ITUser.HasPermission(DefaultValues.GetProjectIdMinValue(), Globals.Permission.ADD_QUERY.ToString()))
                    btnAddQuery.Visible = false;

				BindQueries();
			}
		}

        /// <summary>
        /// Projects the selected index changed.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void ProjectSelectedIndexChanged(Object s, EventArgs e) 
		{
			BindQueries();
		}

        /// <summary>
        /// Issueses the rebind.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void IssuesRebind(Object s, EventArgs e) 
		{
			ExecuteQuery();
		}

        /// <summary>
        /// Binds the queries.
        /// </summary>
		void BindQueries() 
		{         
			dropQueries.DataSource = Query.GetQueriesByUsername(User.Identity.Name,ProjectId);
			dropQueries.DataBind();
           
            if (!Page.User.Identity.IsAuthenticated || !ITUser.HasPermission(ProjectId, Globals.Permission.DELETE_QUERY.ToString()))
                btnDeleteQuery.Visible = false;
		}

        /// <summary>
        /// BTNs the perform query click.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnPerformQueryClick(Object s, EventArgs e) 
		{
			ExecuteQuery();
		}

        /// <summary>
        /// Executes the query.
        /// </summary>
		void ExecuteQuery() 
		{
			if (dropQueries.SelectedValue == 0)
				return;


			try 
			{
				List<Issue> colIssues = Issue.PerformSavedQuery(ProjectId,dropQueries.SelectedValue);
				ctlDisplayIssues.DataSource = colIssues;
                ctlDisplayIssues.RssUrl = string.Format("~/Rss.aspx?pid={1}&q={0}&channel=13",dropQueries.SelectedValue,ProjectId);
			} 
			catch 
			{
                lblError.Text = GetLocalResourceObject("QueryError").ToString();
			}

			ctlDisplayIssues.DataBind();
		}

        /// <summary>
        /// Adds the query.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void AddQuery(Object s, EventArgs e) 
		{
			Response.Redirect(string.Format("QueryDetail.aspx?pid={0}",ProjectId));
		}

        /// <summary>
        /// Deletes the query.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void DeleteQuery(Object s, EventArgs e) 
		{
			if (dropQueries.SelectedValue == 0)
				return;

			Query.DeleteQuery(dropQueries.SelectedValue);
			BindQueries();
		}


	
	}
}
