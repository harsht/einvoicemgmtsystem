namespace BugNET.UserControls
{

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
using BugNET.UserInterfaceLayer;
using BugNET.UserControls;

public partial class Banner : System.Web.UI.UserControl
{

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //hide user registration if disabled in host settings
        if (!Page.User.Identity.IsAuthenticated && Boolean.Parse(HostSetting.GetHostSetting("DisableUserRegistration"))){
            if(LoginView1.FindControl("lnkRegister") != null)
                LoginView1.FindControl("lnkRegister").Visible = false;
        }

        ddlProject.DataTextField = "Name";
        ddlProject.DataValueField = "Id";

        if (!Page.IsPostBack)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                ddlProject.DataSource = Project.GetProjectsByMemberUserName(Security.GetUserName(), true);
                ddlProject.DataBind();
                ddlProject.Items.Insert(0, new ListItem("-- Select Project --"));
            }
            else if (!Page.User.Identity.IsAuthenticated && !Boolean.Parse(HostSetting.GetHostSetting("DisableAnonymousAccess")))
            {
                ddlProject.DataSource = Project.GetPublicProjects();
                ddlProject.DataBind();
                ddlProject.Items.Insert(0, new ListItem("-- Select Project --"));
            }
            else
            {
                pnlHeaderNav.Visible = false;
            }

            if (Request.QueryString["pid"] != null)
            {
                try
                {
                    ddlProject.SelectedValue = Request.QueryString["pid"].ToString();
                }
                catch { }
            }

        }

       

            //if (Request.QueryString["pid"] != null)
            //{
            //    try
            //    {
            //        ddlProject.SelectedValue = Request.QueryString["pid"].ToString();
            //    }
            //    catch { }
            //}
            //else if (Request.QueryString["bid"] != null)
            //{
            //    try
            //    {
            //        ddlProject.SelectedValue = Issue.GetIssueById(int.Parse(Request.QueryString["bid"])).ProjectId.ToString();
            //    }
            //    catch { }
            //}
        
        //ddlProject.Attributes.Add("onchange","location.href='" + Page.ResolveUrl("~/Bugs/ProjectSummary.aspx") + "?pid=' + this.options[this.selectedIndex].value + ''");
    }

    /// <summary>
    /// Handles the Click event of the Profile control.
    /// </summary>
    /// <param name="s">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Profile_Click(object s, EventArgs e)
    {
        Response.Redirect(string.Format("~/UserProfile.aspx?referrerurl={0}", Request.RawUrl));

    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlProject control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlProject.SelectedIndex != 0)
            Response.Redirect(string.Format("~/Projects/ProjectSummary.aspx?pid={0}", ddlProject.SelectedValue));
    }

    /// <summary>
    /// Handles the Click event of the btnSearch control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtIssueId.Text.Trim().Length != 0)
        {
            int IssueId;
            int.TryParse(txtIssueId.Text.Trim(),out IssueId);
            if (IssueId != 0)
            {
              Response.Redirect(string.Format("~/Bugs/BugDetail.aspx?&bid={0}",IssueId.ToString()));
            }
        }



    }

    
}

}