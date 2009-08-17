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
using System.Globalization;
using log4net;
using BugNET.BusinessLogicLayer;
using BugNET.POP3Reader;


namespace BugNET
{
    /// <summary>
    /// Password recovery page
    /// </summary>
    public partial class ForgotPassword : System.Web.UI.Page
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ForgotPassword));

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the Submit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void Submit_Click(object sender, EventArgs e)
        {
            if (CapchaTest.IsValid)
            {
                MembershipUser user = Membership.GetUser(UserName.Text);
                if (user != null)
                {
                   
                    Log.InfoFormat("User {0} requested a password reminder on {0}", user, DateTime.Now);
                    try
                    {
                        ITUser.SendUserPasswordReminderNotification(user);
                        Message1.Visible = true;
                        Message1.IconType = BugNET.UserControls.Message.MessageType.Information;
                        Message1.Text = GetLocalResourceObject("PasswordReminderSuccess").ToString();
                    }
                    catch(Exception ex)
                    {
                        Log.Error(GetLocalResourceObject("EmailPasswordReminderError").ToString(), ex);
                        Message1.Visible = true;
                        Message1.IconType = BugNET.UserControls.Message.MessageType.Error;
                        Message1.Text = GetLocalResourceObject("EmailPasswordReminderError").ToString();
                    }                                   
                }
                else
                {
                    Message1.Visible = true;
                    Message1.IconType = BugNET.UserControls.Message.MessageType.Warning;
                    Message1.Text = GetLocalResourceObject("UserNotFoundError").ToString();
                }
            }
            else
            {
                Message1.Visible = true;
                Message1.IconType = BugNET.UserControls.Message.MessageType.Warning;
                Message1.Text = GetLocalResourceObject("CodeIncorrectError").ToString();                          
            }


        }
    }
}
