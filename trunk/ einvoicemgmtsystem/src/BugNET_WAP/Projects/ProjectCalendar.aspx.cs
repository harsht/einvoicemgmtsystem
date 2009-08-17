using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BugNET.UserInterfaceLayer;
using BugNET.BusinessLogicLayer;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BugNET.Projects
{
    /// <summary>
    /// Page that displays a project calendar
    /// </summary>
    public partial class ProjectCalendar : BugNET.UserInterfaceLayer.BasePage 
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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
                        //dropProjects.SelectedValue = Int32.Parse(Request.QueryString["pid"]);
                    }
                    catch { }
                }

                //List<Project> projects;

                //if (User.Identity.IsAuthenticated)
                //    projects = Project.GetProjectsByMemberUserName(User.Identity.Name);
                //else
                //    projects = Project.GetPublicProjects();

                //// Bind projects to dropdownlist
                //dropProjects.DataSource = projects;
                //dropProjects.DataBind();

                // If no projects, redirect to no project page
                //if (dropProjects.SelectedValue == 0)
                //    Response.Redirect("~/NoProjects.aspx");

                BindCalendar();
            }

            //lblProjectName.Text = dropProjects.SelectedItem.Text;
        }

        /// <summary>
        /// Views the selected index changed.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ViewSelectedIndexChanged(Object s, EventArgs e)
        {
            BindCalendar();
        }

        /// <summary>
        /// Calendars the view selected index changed.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void CalendarViewSelectedIndexChanged(Object s, EventArgs e)
        {
            BindCalendar();
        }


        /// <summary>
        /// Binds the calendar.
        /// </summary>
        private void BindCalendar()
        {
            prjCalendar.SelectedDate = DateTime.Today;
            prjCalendar.VisibleDate = DateTime.Today;

        }

        /// <summary>
        /// Handles the DayRender event of the prjCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.DayRenderEventArgs"/> instance containing the event data.</param>
        protected void prjCalendar_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
        {

            string onmouseoverStyle = "this.style.backgroundColor='#D4EDFF'";
            string onmouseoutStyle = "this.style.backgroundColor='@BackColor'";
            string rowBackColor = string.Empty;

    
            if(!e.Day.IsWeekend)
            {
                e.Cell.Attributes.Add("onmouseover", onmouseoverStyle);
                if(!e.Day.IsSelected)
                    e.Cell.Attributes.Add("onmouseout", onmouseoutStyle.Replace("@BackColor", rowBackColor));
                else
                    e.Cell.Attributes.Add("onmouseout", onmouseoutStyle.Replace("@BackColor", "#FFFFC1"));
            }

            if (e.Day.IsToday)
            {
                //TODO: If issues are due today in 7 days or less then create as red, else use blue?
            }
           
           
            List<QueryClause> queryClauses = new List<QueryClause>();
            QueryClause q = new QueryClause("AND", "IssueDueDate", "=", e.Day.Date.ToShortDateString(), SqlDbType.DateTime, false);
            queryClauses.Add(q);
            
            List<Issue> issues = Issue.PerformQuery(ProjectId, queryClauses);
            foreach (Issue issue in issues)
            {
                string cssClass = string.Empty;

                if (issue.DueDate <= DateTime.Today)
                    cssClass = "calIssuePastDue";   
                else
                    cssClass = "calIssue";

                string title = string.Format(@"<div id=""issue"" class=""{3}""><a href=""../Issues/IssueDetail.aspx?id={2}"">{0} - {1}</a></div>", issue.FullId.ToUpper(), issue.Title, issue.Id,cssClass);
                e.Cell.Controls.Add(new LiteralControl(title));
            }

            //Set the calendar to week mode only showing the selected week.
            if (dropCalendarView.SelectedValue == "Week")
            {
                if (Week(e.Day.Date) != Week(prjCalendar.VisibleDate))
                { 
                    e.Cell.Visible = false;
                }
                e.Cell.Height = new Unit("100%"); 
            }
            else
            {
                e.Cell.Height = new Unit("70px");
                e.Cell.Width = new Unit("70px");
            }
               
        }



        /// <summary>
        /// Handles the PreRender event of the prjCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void prjCalendar_PreRender(object sender, EventArgs e)
        {
         
         
        }

        /// <summary>
        /// Handles the Click event of the btnNext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (dropCalendarView.SelectedValue == "Week")
            {
                prjCalendar.VisibleDate = prjCalendar.VisibleDate.AddDays(7);           
            }
            else
            {
                prjCalendar.VisibleDate = prjCalendar.VisibleDate.AddMonths(1);
            }

        }

        /// <summary>
        /// Handles the Click event of the btnPrevious control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            if (dropCalendarView.SelectedValue == "Week")
            {
                prjCalendar.VisibleDate = prjCalendar.VisibleDate.AddDays(-7);
            }
            else
            {
                prjCalendar.VisibleDate = prjCalendar.VisibleDate.AddMonths(-1);
            }

        }
      
        /// <summary>
        /// Weeks the specified td date.
        /// </summary>
        /// <param name="tdDate">The td date.</param>
        /// <returns></returns>
        private static int Week(DateTime tdDate)
        {
            CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.Calendar Cal = ci.Calendar;
            System.Globalization.CalendarWeekRule CWR = ci.DateTimeFormat.CalendarWeekRule;
            DayOfWeek FirstDOW = ci.DateTimeFormat.FirstDayOfWeek;
            return Cal.GetWeekOfYear(tdDate, CWR, FirstDOW);
        }
    }
}
