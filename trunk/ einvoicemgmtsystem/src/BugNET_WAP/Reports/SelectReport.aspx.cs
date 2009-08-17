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
using BugNET.BusinessLogicLayer;
using Microsoft.Reporting.WebForms;

namespace BugNET.Reports
{
    /// <summary>
    /// Select Report Class
    /// </summary>
    public partial class SelectReport : BugNET.UserInterfaceLayer.BasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //get project id
                if (Request.QueryString["pid"] != null)
                    ProjectId = Convert.ToInt32(Request.Params["pid"]);

                SelectUser.DataSource = Project.GetProjectMembers(ProjectId);
                SelectUser.DataBind();
                SelectUser.Items.Insert(0, new ListItem("--Select One--", "0"));
               // SelectUser.Items.Insert(1, new ListItem("[All Project Users]", "-1"));               
            }
        }

        /// <summary>
        /// Handles the Click event of the SelectReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void SelectReport_Click(object sender, EventArgs e)
        {
            ReportDataSource DataSource;
            ControlCollection coll = ReportViewer1.Parent.Controls;
            int oldIndex = coll.IndexOf(ReportViewer1);
            coll.Remove(ReportViewer1);
            ReportViewer1 = new ReportViewer();
            
            ReportViewer1.Width = Unit.Parse("100%");
            //ReportViewer1.DocumentMapWidth = Unit.Parse("13%");
            //ReportViewer1.Height = Unit.Parse("600px");
            ReportViewer1.ShowDocumentMapButton = false;
            ReportViewer1.ShowFindControls = false;
            ReportViewer1.ShowZoomControl = false;
            ReportViewer1.DocumentMapCollapsed = true;
            ReportViewer1.SizeToReportContent = true;

            coll.AddAt(oldIndex, ReportViewer1);

            switch (Reports.SelectedValue)
            {
                case "1":
                    SelectUserPanel.Visible = false;
                    ReportViewer1.LocalReport.ReportPath = "Reports\\BugsByStatus.rdlc";
                    ReportViewer1.LocalReport.DataSources.Clear();
                    DataSource = new ReportDataSource();
                    DataSource.DataSourceId = "BugsByStatusDataSource";
                    DataSource.Name = "Bug";
                    ReportViewer1.LocalReport.DataSources.Add(DataSource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.EnableHyperlinks = true;
                    ReportViewer1.DataBind();
                    break;
                case "2":
                    SelectUserPanel.Visible = false;
                    Response.Redirect(string.Format("~/Reports/ProjectStatus.aspx?pid={0}",ProjectId));
                    break;
                case "3":
                    SelectUserPanel.Visible = false;
                    ReportViewer1.LocalReport.ReportPath = "Reports\\ProjectUserReport.rdlc";
                    ReportViewer1.LocalReport.DataSources.Clear();
                    DataSource = new ReportDataSource();
                    DataSource.Name = "BugNET_BusinessLogicLayer_WorkReport";
                    DataSource.Value = WorkReport.GetWorkReportByProjectId(ProjectId);
                    ReportViewer1.LocalReport.DataSources.Add(DataSource);
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.EnableHyperlinks = true;
                    ReportViewer1.DataBind();
                    break;
                case "4":
                    ReportId = "4";
                    SelectUserPanel.Visible = true;
                    break;

            }
        }

        /// <summary>
        /// Handles the Click event of the ViewReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void ViewReport_Click(object sender, EventArgs e)
        {
            if (SelectUser.SelectedIndex != 0)
            {
                switch (ReportId)
                {
                    case "4":
                        ReportViewer1.LocalReport.ReportPath = "Reports\\ProjectUserReport.rdlc";
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportDataSource DataSource = new ReportDataSource();
                        DataSource.Name = "BugNET_BusinessLogicLayer_WorkReport";
                        DataSource.Value = WorkReport.GetWorkReportByUser(ProjectId, SelectUser.SelectedValue);
                        ReportViewer1.LocalReport.DataSources.Add(DataSource);
                        ReportViewer1.LocalReport.EnableExternalImages = true;
                        ReportViewer1.LocalReport.EnableHyperlinks = true;
                        ReportViewer1.DataBind();
                        break;
                }
            }

        }

        /// <summary>
        /// Handles the Selecting event of the ReportDataSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs"/> instance containing the event data.</param>
        protected void BugsByStatusDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["projectId"] = ProjectId;
        }

        #region Properties
        string ReportId
        {
            get
            {
                if (ViewState["ReportId"] == null)
                    return string.Empty;
                else
                    return (string)ViewState["ReportId"];
            }
            set { ViewState["ReportId"] = value; }
        }

        #endregion	
    }
}
