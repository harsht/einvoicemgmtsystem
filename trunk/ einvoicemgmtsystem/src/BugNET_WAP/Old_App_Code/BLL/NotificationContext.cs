using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;


namespace BugNET.BusinessLogicLayer
{
    public class NotificationContext : INotificationContext
    {
        private string _Username;
        private string _BodyText;
        private string _Subject;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationContext"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="bodyText">The body text.</param>
        public NotificationContext(string username,string subject, string bodyText )
		{
            _Username = username;
            _Subject = subject;
            _BodyText = bodyText;
		}

        #region INotificationContext Members

     
        /// <summary>
        /// Gets or sets the message to send
        /// </summary>
        /// <value>The message.</value>
        public string BodyText
        {
            get
            {
                return _BodyText;
            }
            set
            {
                _BodyText = value;
            }
        }

        /// <summary>
        /// Gets or sets the send to address.
        /// </summary>
        /// <value>The send to address.</value>
        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
            }
        }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public string Subject
        {
            get
            {
                return _Subject;
            }
            set
            {
                _Subject = value;
            }
        }

        #endregion
    }
}
