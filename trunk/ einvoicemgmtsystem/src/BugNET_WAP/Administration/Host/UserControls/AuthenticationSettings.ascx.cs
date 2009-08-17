using System;
using System.Web.UI;
using BugNET.UserInterfaceLayer;
using BugNET.BusinessLogicLayer;

namespace BugNET.Administration.Host.UserControls
{
    public partial class AuthenticationSettings : System.Web.UI.UserControl, IEditHostSettingControl
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

                HostSetting.UpdateHostSetting("UserAccountSource", UserAccountSource.SelectedValue);
                HostSetting.UpdateHostSetting("ADUserName", ADUserName.Text);
                HostSetting.UpdateHostSetting("ADPath", ADPath.Text);
                HostSetting.UpdateHostSetting("DisableUserRegistration", DisableUserRegistration.Checked.ToString());
                HostSetting.UpdateHostSetting("DisableAnonymousAccess", DisableAnonymousAccess.Checked.ToString());
                //update password if provided
                if (ADPassword.Text.Length > 0)
                    HostSetting.UpdateHostSetting("ADPassword", ADPassword.Text);

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
            UserAccountSource.SelectedValue = HostSetting.GetHostSetting("UserAccountSource");
            ADUserName.Text = HostSetting.GetHostSetting("ADUserName");
            ADPath.Text = HostSetting.GetHostSetting("ADPath");
            DisableUserRegistration.Checked = Boolean.Parse(HostSetting.GetHostSetting("DisableUserRegistration"));
            DisableAnonymousAccess.Checked = Boolean.Parse(HostSetting.GetHostSetting("DisableAnonymousAccess"));
        }

        #endregion
    }
}