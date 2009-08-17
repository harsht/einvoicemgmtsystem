using System;
using System.Collections.Generic;
using System.Text;

namespace BugNET.BusinessLogicLayer
{
    public class MsnNotificationType : INotificationType
    {
        private bool _Enabled;

        #region INotificationType Members

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return "MSN"; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="MsnNotificationType"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled
        {
            get { return _Enabled; }
        }

        /// <summary>
        /// Sends the notification.
        /// </summary>
        /// <param name="context">The context.</param>
        public void SendNotification(INotificationContext context)
        {
            try
            {
                System.Web.Security.MembershipUser user = ITUser.GetUser(context.Username);

                //check if this user had this notifiction type enabled in his/her profile.
                if (user != null && ITUser.IsNotificationTypeEnabled(context.Username, this.Name))
                {
                    //TODO: Send the notification using a .NET msn library       
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion
    }
}
