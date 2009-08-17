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
using ZedGraph;
using BugNET.BusinessLogicLayer;
using System.Security.Cryptography;

namespace BugNET.Reports
{
	/// <summary>
	/// Summary description for ProjectStatus.
	/// </summary>
	public partial class ProjectStatus : System.Web.UI.Page
	{
		protected Literal litProjectName;


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{	
				if (Request.QueryString["pid"] != null)
					ProjectId = Convert.ToInt32(Request.Params["pid"]);

				Chart.ImageUrl = "Chart.aspx?id=1&pid=" + ProjectId.ToString();
			}
			
		}

        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
		int ProjectId 
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
			this.ViewChart.Click+=new EventHandler(ViewChart_Click);

		}
		#endregion

        /// <summary>
        /// Handles the Click event of the ViewChart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
		private void ViewChart_Click(object sender, EventArgs e)
		{
			switch(ChartList.SelectedValue)
			{
				case "Issues by Status":
					Chart.ImageUrl = "Chart.aspx?id=1&pid=" + ProjectId.ToString();
					break;
				case "Open Issues by Priority":
					Chart.ImageUrl = "Chart.aspx?id=2&pid=" + ProjectId.ToString();
					break;
				case "Open Issues by Version":
					Chart.ImageUrl = "Chart.aspx?id=3&pid=" + ProjectId.ToString();
					break;
				default:
					Chart.ImageUrl = "Chart.aspx?id=1&pid=" + ProjectId.ToString();
					break;

			}
			
		}
	}
}
