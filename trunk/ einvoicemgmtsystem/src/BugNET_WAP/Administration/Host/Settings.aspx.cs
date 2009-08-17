using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BugNET.BusinessLogicLayer;
using BugNET.UserInterfaceLayer;

namespace BugNET.Administration.Host
{
	/// <summary>
	/// Administration page that controls the application configuration
	/// </summary>
	public partial class Settings : System.Web.UI.Page
	{
        Control ctlHostSettings;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!ITUser.IsInRole(Globals.SuperUserRole))
                Response.Redirect("~/AccessDenied.aspx");

            if (!Page.IsPostBack)
            {
                tvAdminMenu.Nodes.Add(new TreeNode("Basic", "UserControls/BasicSettings.ascx", "~/images/page_white_gear.gif"));
                tvAdminMenu.Nodes.Add(new TreeNode("Authentication", "UserControls/AuthenticationSettings.ascx", "~/images/lock.gif"));
                tvAdminMenu.Nodes.Add(new TreeNode("Mail / SMTP", "UserControls/MailSettings.ascx", "~/images/email.gif"));
                tvAdminMenu.Nodes.Add(new TreeNode("Logging", "UserControls/LoggingSettings.ascx", "~/images/page_white_error.gif"));
                tvAdminMenu.Nodes.Add(new TreeNode("Subversion", "UserControls/SubversionSettings.ascx", "~/images/svnLogo_sm.jpg"));
                tvAdminMenu.Nodes.Add(new TreeNode("Notifications", "UserControls/NotificationSettings.ascx", "~/images/email_go.gif"));
                tvAdminMenu.Nodes.Add(new TreeNode("Attachments", "UserControls/AttachmentSettings.ascx", "~/images/attach.gif"));
            }          

             LoadSettingsControl();       
		}

        /// <summary>
        /// Gets or sets the tab.
        /// </summary>
        /// <value>The tab.</value>
        string Tab
        {
            get
            {
                if (ViewState["Tab"] == null)
                    return "UserControls/BasicSettings.ascx";
                else
                    return (string)ViewState["Tab"];
            }

            set { ViewState["Tab"] = value; }
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
          
              
        }

         ///<summary>
         ///Handles the SelectedNodeChanged event of the tvAdminMenu control.
         ///</summary>
         ///<param name="sender">The source of the event.</param>
         ///<param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void tvAdminMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            Tab = tvAdminMenu.SelectedValue;
            LoadSettingsControl();
        }

        /// <summary>
        /// Loads the settings control.
        /// </summary>
        private void LoadSettingsControl()
        {
            ctlHostSettings = Page.LoadControl(Tab);
            ctlHostSettings.ID = "ctlHostSetting";
            plhSettingsControl.Controls.Clear();
            plhSettingsControl.Controls.Add(ctlHostSettings);
            ((IEditHostSettingControl)ctlHostSettings).Initialize();
        }

        /// <summary>
        /// Handles the Click event of the cmdUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void cmdUpdate_Click(object sender, EventArgs e)
		{
            ((IEditHostSettingControl)ctlHostSettings).Update();         
		}
	}
}
