using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using BugNET.Providers.ResourceProviders;
using System.Web.Security;
using System.Threading;


namespace BugNET.BusinessLogicLayer
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationManager
    {
        private List<INotificationType> plugins = null;
        private string _Username;
        private string _Subject;
        private string _BodyText;

        /// <summary>
        /// Loads the notification types from the current assembly.
        /// </summary>
        public List<INotificationType> LoadNotificationTypes()
        {
            plugins = new List<INotificationType>();
            Assembly asm = this.GetType().Assembly;

            foreach (Type t in asm.GetTypes())
            {
                foreach (Type iface in t.GetInterfaces())
                {
                    if (iface.Equals(typeof(INotificationType)))
                    {
                        try
                        {
                            INotificationType notificationType = (INotificationType)Activator.CreateInstance(t);
                            plugins.Add(notificationType);
                            //Console.WriteLine("Type: " + notificationType.Name + " Enabled: " + notificationType.Enabled);
                            break;
                        }
                        catch (Exception e) { }
                    }
                }
            }
            return plugins;
        }

        /// <summary>
        /// Recurses the loaded notification plugins and if enabled will send the notifications using the plugin.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="bodyText">The body text.</param>
        public void SendNotification(string username,string subject, string bodyText)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");
            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException("subject");
            if (string.IsNullOrEmpty(bodyText))
                throw new ArgumentNullException("bodyText");

            _Username = username;
            _Subject = subject;
            _BodyText = bodyText;

            Thread thread = new Thread(new System.Threading.ThreadStart(SendNotification_Thread));
            thread.Priority = ThreadPriority.Lowest;
            thread.Start();
        }


        /// <summary>
        /// Sends the notification_ thread.
        /// </summary>
        private void SendNotification_Thread()
        {

            foreach (INotificationType nt in plugins)
            {
                //if plugin is enabled globably though application settings
                if (nt.Enabled)
                {
                    nt.SendNotification(new NotificationContext(_Username,_Subject,_BodyText));
                }
            }
        }


        /// <summary>
        /// Loads the notification template.
        /// </summary>
        public string LoadNotificationTemplate(string templateName)
        {
            DBResourceProvider res = new DBResourceProvider("Notification");
            return res.GetObject(templateName, System.Threading.Thread.CurrentThread.CurrentUICulture).ToString();             
        }     

        /// <summary>
        /// Replaces the issue tokens.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="issue">The issue.</param>
        private void ReplaceIssueTokens(ref string template, Issue issue)
        {
            if (issue != null)
            {
                template = template.Replace("[Issue_AssignedDisplayName]", issue.AssignedDisplayName);
                template = template.Replace("[Issue_Title]", issue.Title);
                template = template.Replace("[Issue_Description]", issue.Description);
                template = template.Replace("[Issue_Id]", issue.Id.ToString());
                template = template.Replace("[Issue_FullId]", issue.FullId);
                template = template.Replace("[Issue_Project]", issue.ProjectName);
                template = template.Replace("[Issue_CreatorDisplayName]", issue.CreatorDisplayName);
                template = template.Replace("[Issue_CreatedDate]", issue.DateCreated.ToShortDateString());
            }
        }


        /// <summary>
        /// Replaces the comment tokens.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="issueComment">The issue comment.</param>
        private void ReplaceCommentTokens(ref string template, IssueComment issueComment)
        {

            if (issueComment != null)
            {
                template = template.Replace("[Comment_CreatorDisplayName]", issueComment.CreatorDisplayName);
                template = template.Replace("[Comment_DateCreated]", issueComment.DateCreated.ToShortDateString());
                template = template.Replace("[Comment]", issueComment.Comment);
            }
        }


        /// <summary>
        /// Replaces the user tokens.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="user">The user.</param>
        private void ReplaceUserTokens(ref string template, MembershipUser user)
        {
            if (user != null)
            {
                template = template.Replace("[User_DateCreated]", user.CreationDate.ToShortDateString());
                template = template.Replace("[User_Email]", user.Email);
                template = template.Replace("[User_Username]", user.UserName);
                template = template.Replace("[User_DisplayName]", ITUser.GetUserDisplayName(user.UserName));                
            }
        }

        /// <summary>
        /// Replaces the common tokens.
        /// </summary>
        /// <param name="template">The template.</param>
        private void ReplaceCommonTokens(ref string template)
        {
            string desktopDefaultUrl = Globals.DefaultUrl;
            template = template.Replace("[DefaultUrl]", desktopDefaultUrl);
            template = template.Replace("\\n", Environment.NewLine);
        }

        /// <summary>
        /// Replaces the tokens for a issue, issue comment and membership user
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="issue">The issue.</param>
        /// <param name="issueComment">The issue comment.</param>
        /// <param name="user">The user.</param>
        public void ReplaceTokens(ref string template, Issue issue, IssueComment issueComment, MembershipUser user)
        {
            ReplaceIssueTokens(ref template, issue);
            ReplaceCommentTokens(ref template, issueComment);
            ReplaceUserTokens(ref template, user);
            ReplaceCommonTokens(ref template);
        }

        /// <summary>
        /// Replaces the tokens for a issue
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="issue">The issue.</param>
        public void ReplaceTokens(ref string template, Issue issue)
        {
            ReplaceTokens(ref template, issue, null, null);
        }

        /// <summary>
        /// Replaces the tokens for a issue comment
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="issueComment">The issue comment.</param>
        public void ReplaceTokens(ref string template, IssueComment issueComment)
        {

            ReplaceTokens(ref template, null, issueComment, null);
        }

        /// <summary>
        /// Replaces the tokens for a user
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="user">The user.</param>
        public void ReplaceTokens(ref string template, MembershipUser user)
        {
            ReplaceTokens(ref template, null, null, user);
        }

        /// <summary>
        /// Determines whether [is notification type enabled] [the specified notification type].
        /// </summary>
        /// <param name="notificationType">Type of the notification.</param>
        /// <returns>
        /// 	<c>true</c> if [is notification type enabled] [the specified notification type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNotificationTypeEnabled(string notificationType)
        {
            if (string.IsNullOrEmpty(notificationType))
                throw new ArgumentNullException("notificationType");

            string[] notificationTypes = HostSetting.GetHostSetting("EnabledNotificationTypes").Split(';');
            foreach (string s in notificationTypes)
            {
                if (s.Equals(notificationType))
                    return true;
            }
            return false;
        }
    }
}
