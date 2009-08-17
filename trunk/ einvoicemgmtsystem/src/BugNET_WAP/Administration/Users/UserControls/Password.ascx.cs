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

namespace BugNET.Administration.Users.UserControls
{
	public partial class Password : System.Web.UI.UserControl
	{
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the cmdChangePassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void cmdChangePassword_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            if (cvPasswords.IsValid)
            {
                MembershipUser objUser = ITUser.GetUser(UserId);
                if (objUser != null)
                {
                    try
                    {
                        objUser.ChangePassword(objUser.GetPassword(), NewPassword.Text);
                        lblMessage.Visible = true;
                        lblMessage.Text = "Password changed succesfully";
                    }
                    catch (Exception ex)
                    {
                        //TODO: Log this error
                        lblMessage.Visible = true;
                        lblMessage.Text = "The password could not be changed, please verify that the password is 7 characters or more.";
                    }

                }
            }

        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public Guid UserId
        {
            get
            {
                if (Request.QueryString["user"] != null || Request.QueryString["user"].Length != 0)
                    try
                    {
                        return new Guid(Request.QueryString["user"].ToString());
                    }
                    catch
                    {
                        throw new Exception("The user querystring parameter is not properly formed");
                    }
                else
                    return Guid.Empty;
            }
        }

        /// <summary>
        /// Handles the Click event of the cmdResetPassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void cmdResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                MembershipUser objUser = ITUser.GetUser(UserId);
                objUser.ResetPassword();
                //TODO: Email the password to the user.
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
	}
}