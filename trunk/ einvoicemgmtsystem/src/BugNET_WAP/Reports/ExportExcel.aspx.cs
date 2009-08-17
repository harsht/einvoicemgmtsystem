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
namespace BugNET.Reports
{
	/// <summary>
	/// Summary description for ExportExcel.
	/// </summary>
	public partial class ExportExcel : System.Web.UI.Page
	{

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (Page.IsPostBack)
				return;
			string Report = string.Empty;
			int ProjectId = 0;
			int userId = 0;

			if (Request.QueryString["report"] != null)
				Report= Request.Params["report"];
			if (Request.QueryString["pid"] != null)
				ProjectId = Convert.ToInt32(Request.Params["pid"]);
			if (Request.QueryString["uid"] != null)
				userId = Convert.ToInt32(Request.QueryString["uid"]);


			DataSet ds = new DataSet();

			this.EnableViewState = false;
			Response.Clear();
			Response.Buffer= true;
			Response.ContentType = "application/vnd.ms-excel";
			Response.Charset = "";
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			byte[] preamble = System.Text.Encoding.UTF8.GetPreamble();
			Response.OutputStream.Write(preamble, 0, preamble.Length);
			System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(Response.Output);

			switch(Report)
			{	
				case "BugsByStatus.aspx":
                    //ds = Bug.GetBugsByProjectId(ProjectId);
                    //dgTimeTrack.Visible=false;
                    //dgBugs.Visible=true;
                    //dgBugs.DataSource = ds.Tables[0];
                    //dgBugs.DataBind();
                    //dgBugs.RenderControl(htmlWriter);
					break;
				case "ProjectUserReport.aspx":
                    ds = null;// TimeEntry.GetWorkReportByProjectId(ProjectId, userId);
					dgTimeTrack.Visible=true;
					dgBugs.Visible=false;
					dgTimeTrack.DataSource = ds.Tables[0];
					dgTimeTrack.DataBind();
					dgTimeTrack.RenderControl(htmlWriter);
					break;
				case "BugList.aspx":
					dgFullBugs.DataSource = (IssueCollection)Session["BugList"];
					dgTimeTrack.Visible=false;
					dgFullBugs.Visible=true;
					dgFullBugs.DataBind();
					dgFullBugs.RenderControl(htmlWriter);
					break;

			}
			Response.End();
		}
	
	}
}
