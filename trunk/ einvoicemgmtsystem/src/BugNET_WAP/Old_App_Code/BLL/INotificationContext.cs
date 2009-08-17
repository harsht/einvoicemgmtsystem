using System;
using System.Collections.Generic;
using System.Text;

namespace BugNET.BusinessLogicLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface INotificationContext
    {


        /// <summary>
        /// Gets or sets the body text.
        /// </summary>
        /// <value>The body text.</value>
        string BodyText { get;set;}

        /// <summary>
        /// Gets or sets the send to address.
        /// </summary>
        /// <value>The send to address.</value>
        string Username { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        string Subject { get; set; }
    }
}
