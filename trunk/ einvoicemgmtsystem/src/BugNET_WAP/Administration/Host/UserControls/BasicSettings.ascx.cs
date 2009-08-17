using System;
using System.Web.UI;
using BugNET.UserInterfaceLayer;
using BugNET.BusinessLogicLayer;

namespace BugNET.Administration.Host.UserControls
{
    public partial class BasicSettings : System.Web.UI.UserControl, IEditHostSettingControl
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region IEditHostSettingControl Members

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <returns></returns>
        public void Update()
        {
            if (Page.IsValid)
            {
                if (WelcomeMessageHtmlEditor.Text.Length > 500)
                {
                    Message1.Text = "Please enter a welcome message that is less than 500 characters.";
                    Message1.IconType = BugNET.UserControls.Message.MessageType.Error;
                    Message1.Visible = true;
                    return;
                }
                HostSetting.UpdateHostSetting("WelcomeMessage", WelcomeMessageHtmlEditor.Text.Trim());
                HostSetting.UpdateHostSetting("ApplicationTitle", ApplicationTitle.Text.Trim());
                HostSetting.UpdateHostSetting("DefaultUrl", DefaultUrl.Text);
                Message1.Text = "The settings have been updated successfully.";
                Message1.IconType = BugNET.UserControls.Message.MessageType.Information;
                Message1.Visible = true;
            }
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        public void Initialize()
        {
            //get hostsettings for this control here and bind them to ctrls
            ApplicationTitle.Text = HostSetting.GetHostSetting("ApplicationTitle");
            WelcomeMessageHtmlEditor.Text = HostSetting.GetHostSetting("WelcomeMessage");
            DefaultUrl.Text = HostSetting.GetHostSetting("DefaultUrl");
        }

        #endregion
    }
}