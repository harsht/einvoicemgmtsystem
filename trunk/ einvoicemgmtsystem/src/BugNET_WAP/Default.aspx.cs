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
using System.Reflection;
using BugNET.UserInterfaceLayer;

namespace BugNET
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : BugNET.UserInterfaceLayer.BasePage
	{

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!Page.IsPostBack)
            {
                lblApplicationTitle.Text = HostSetting.GetHostSetting("ApplicationTitle");
                WelcomeMessage.Text = HostSetting.GetHostSetting("WelcomeMessage");

            }

			if (!Context.User.Identity.IsAuthenticated)			
			{	
				//get all public available projects here
				if(!Boolean.Parse(HostSetting.GetHostSetting("DisableAnonymousAccess")))
				{
					rptProject.DataSource = Project.GetPublicProjects();
				}
				else
				{
					rptProject.Visible=false;
                    lblMessage.Text = "The administrator has disable anonymous access, please register to view any projects";
                    lblMessage.Visible=true;
				}
                //hide the registration link if we have disabled registration
                if (Boolean.Parse(HostSetting.GetHostSetting("DisableUserRegistration")))
                    LoginView1.FindControl("Login1").FindControl("CreateUserLink").Visible = false;
                
			}
			else
			{
				rptProject.DataSource = Project.GetProjectsByMemberUserName(User.Identity.Name);	
			}

			rptProject.DataBind();

            if (rptProject.Items.Count == 0)
            {
                lblMessage.Text = "You must register and login to view projects on this site.";
                lblMessage.Visible = true;
            }

		
		}

		#region Web Form Designer generated code
        /// <summary>
        /// Overrides the default OnInit to provide a security check for pages
        /// </summary>
        /// <param name="e"></param>
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
			this.rptProject.ItemDataBound+=new RepeaterItemEventHandler(rptProject_ItemDataBound);
		}
		#endregion

        /// <summary>
        /// Handles the ItemDataBound event of the rptProject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
		private void rptProject_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			//check permissions
			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				Project p = (Project)e.Item.DataItem;

				if(!Context.User.Identity.IsAuthenticated || !ITUser.HasPermission(p.Id,Globals.Permission.ADD_ISSUE.ToString()))
					e.Item.FindControl("ReportIssue").Visible=false;

				HyperLink atu = (HyperLink)e.Item.FindControl("AssignedToUser");
				Control AssignedUserFilter = e.Item.FindControl("AssignedUserFilter");
				if(Context.User.Identity.IsAuthenticated && Project.IsUserProjectMember(User.Identity.Name,p.Id))
				{
					atu.NavigateUrl = string.Format("~/Bugs/BugList.aspx?pid={0}&u={1}",p.Id,User.Identity.Name);
				}
				else
				{
					AssignedUserFilter.Visible=false;
				}
			}
		}
	}
}
