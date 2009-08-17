using System;
using System.Web.UI;
using BugNET.UserInterfaceLayer;
using BugNET.BusinessLogicLayer;

namespace BugNET.Administration.Host.UserControls
{
    public partial class MailSettings : System.Web.UI.UserControl, IEditHostSettingControl
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
        public void Update()
        {
            if (Page.IsValid)
            {
                HostSetting.UpdateHostSetting("HostEmailAddress", HostEmail.Text);
                HostSetting.UpdateHostSetting("SMTPServer", SMTPServer.Text);
                HostSetting.UpdateHostSetting("SMTPAuthentication", SMTPEnableAuthentication.Checked.ToString());
                HostSetting.UpdateHostSetting("SMTPUsername", SMTPUsername.Text);
                HostSetting.UpdateHostSetting("SMTPPassword", SMTPPassword.Text);
                HostSetting.UpdateHostSetting("SMTPPort", SMTPPort.Text);
                HostSetting.UpdateHostSetting("SMTPUseSSL", SMTPUseSSL.Checked.ToString());

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
            HostEmail.Text = HostSetting.GetHostSetting("HostEmailAddress");
            SMTPServer.Text = HostSetting.GetHostSetting("SMTPServer");
            SMTPEnableAuthentication.Checked = Boolean.Parse(HostSetting.GetHostSetting("SMTPAuthentication"));
            SMTPUsername.Text = HostSetting.GetHostSetting("SMTPUsername");
            SMTPPassword.Text = HostSetting.GetHostSetting("SMTPPassword");
            SMTPPort.Text = HostSetting.GetHostSetting("SMTPPort");
            SMTPUseSSL.Checked = Boolean.Parse(HostSetting.GetHostSetting("SMTPUseSSL"));
        }

        #endregion
    }
}