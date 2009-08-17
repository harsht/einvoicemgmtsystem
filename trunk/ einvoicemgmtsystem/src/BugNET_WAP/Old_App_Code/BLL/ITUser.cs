using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BugNET.DataAccessLayer;
using BugNET.UserInterfaceLayer;
using System.Web.Security;
using System.Web.Profile;
using System.Web;
using BugNET.Providers.MembershipProviders;

namespace BugNET.BusinessLogicLayer
{
	/// <summary>
	/// BugNET user class for working with the membership provider
	/// </summary>
	public class ITUser
	{
        private Guid _Id;
        private string _UserName;
        private string _Email;
        private string _DisplayName;
        private string _FirstName;
        private string _LastName;
        private DateTime _CreationDate;
        private DateTime _LastLoginDate;
        private bool _IsApproved;

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id
        {
            get { return _Id; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is approved.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is approved; otherwise, <c>false</c>.
        /// </value>
        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
        /// <summary>
        /// Gets or sets the last login date.
        /// </summary>
        /// <value>The last login date.</value>
        public DateTime LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>The creation date.</value>
        public DateTime CreationDate
        {
            get { return _CreationDate; }
            set { _CreationDate = value; }
        }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }


        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName
        {
            get
            {
                if (_DisplayName == string.Empty)
                    return _UserName;
                else
                    return _DisplayName;
            }
            set { _DisplayName = value; }
        }


		#region Constructors
            /// <summary>
            /// Initializes a new instance of the <see cref="T:ITUser"/> class.
            /// </summary>
			private ITUser() 
			{ }

            /// <summary>
            /// Initializes a new instance of the <see cref="ITUser"/> class.
            /// </summary>
            /// <param name="userId">The user id.</param>
            /// <param name="userName">Name of the user.</param>
            /// <param name="displayName">The display name.</param>
            public ITUser(Guid userId, string userName,string firstName, string lastName, string displayName, DateTime creationDate,DateTime lastLoginDate,bool isApproved)
            {
                _Id = userId;
                _UserName = userName;
                _DisplayName = displayName;
                _CreationDate = creationDate;
                _FirstName = firstName;
                _LastName = lastName;
                _IsApproved = isApproved;
                _LastLoginDate = lastLoginDate;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="ITUser"/> class.
            /// </summary>
            /// <param name="userId">The user id.</param>
            /// <param name="userName">Name of the user.</param>
            /// <param name="displayName">The display name.</param>
            public ITUser(Guid userId, string userName, string displayName)
                : this(userId, userName, string.Empty, string.Empty, displayName, DateTime.MinValue, DateTime.MinValue, true)
            { }

        
        #endregion

        #region Static Methods

            /// <summary>
            /// Creates a new user.
            /// </summary>
            /// <param name="userName"></param>
            /// <param name="password"></param>
            /// <param name="email"></param>
            public static void CreateUser(string userName, string password, string email)
            {
                MembershipUser user = Membership.CreateUser(userName,password,email);
            }

            /// <summary>
            /// Gets the user.
            /// </summary>
            /// <param name="userProviderKey">The user provider key.</param>
            /// <returns></returns>
            public static MembershipUser GetUser(object userProviderKey)
            {
                if (userProviderKey == null)
                    throw (new ArgumentOutOfRangeException("userProviderKey"));
                return Membership.GetUser(userProviderKey);
            }

            /// <summary>
            /// Gets the user.
            /// </summary>
            /// <param name="userName">Name of the user.</param>
            /// <returns></returns>
            public static MembershipUser GetUser(string userName)
            {
                if (String.IsNullOrEmpty(userName))
                    throw (new ArgumentOutOfRangeException("userName"));


                return Membership.GetUser(userName);
            }
           
            /// <summary>
            /// Gets all users in the application
            /// </summary>
            /// <returns>Collection of membership users</returns>
			public static List<CustomMembershipUser> GetAllUsers()
			{
                //return Membership.GetAllUsers();

                List<CustomMembershipUser> userList = new List<CustomMembershipUser>();
                foreach(CustomMembershipUser u in Membership.GetAllUsers())
                {
                    userList.Add(u);
                }
                return userList;
			}

            /// <summary>
            /// Gets all users.
            /// </summary>
            /// <returns>Authorized Users Only</returns>
            public static List<CustomMembershipUser> GetAllAuthorizedUsers()
            {
                List<CustomMembershipUser> users = ITUser.GetAllUsers();
                List<CustomMembershipUser> AuthenticatedUsers = new List<CustomMembershipUser>();
                foreach (CustomMembershipUser user in users)
                {
                    if (user.IsApproved)
                        AuthenticatedUsers.Add(user);
                }
                users = AuthenticatedUsers;
                return users;
            }

            /// <summary>
            /// Finds users by name
            /// </summary>
            /// <param name="userNameToMatch">The user name to match.</param>
            /// <returns></returns>
            public static List<CustomMembershipUser> FindUsersByName(string userNameToMatch)
            {
                List<CustomMembershipUser> userList = new List<CustomMembershipUser>();
                foreach (CustomMembershipUser u in Membership.FindUsersByName(userNameToMatch))
                {
                    userList.Add(u);
                }
                return userList;
            }

            /// <summary>
            /// Finds the users by email.
            /// </summary>
            /// <param name="emailToMatch">The email to match.</param>
            /// <returns></returns>
            public static List<CustomMembershipUser> FindUsersByEmail(string emailToMatch)
            {
                List<CustomMembershipUser> userList = new List<CustomMembershipUser>();
                foreach (CustomMembershipUser u in Membership.FindUsersByEmail(emailToMatch))
                {
                    userList.Add(u);
                }
                return userList;
            }
            /// <summary>
            /// Updates the user.
            /// </summary>
            /// <param name="user">The user.</param>
            public static void UpdateUser(MembershipUser user)
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                Membership.UpdateUser(user);
            }

            /// <summary>
            /// Determines whether [is in role] [the specified project id].
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <param name="roleName">Name of the role.</param>
            /// <returns>
            /// 	<c>true</c> if [is in role] [the specified project id]; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsInRole(int projectId, string roleName)
            {
                if (projectId <= Globals.NewId)
                    throw new ArgumentOutOfRangeException("projectId");
                if (String.IsNullOrEmpty(roleName))
                    throw new ArgumentNullException("roleName");

                return ITUser.IsInRole(HttpContext.Current.User.Identity.Name, projectId, roleName);
            }

            /// <summary>
            /// Determines whether [is in role] [the specified role name].
            /// </summary>
            /// <param name="roleName">Name of the role.</param>
            /// <returns>
            /// 	<c>true</c> if [is in role] [the specified role name]; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsInRole(string roleName)
            {
                if (String.IsNullOrEmpty(roleName))
                    throw new ArgumentNullException("roleName");
                if (HttpContext.Current.User.Identity.Name.Length == 0)
                    return false;

                List<Role> roles = Role.GetRolesForUser(HttpContext.Current.User.Identity.Name);
                return roles.Exists(delegate(Role r) { return r.Name == roleName; });
            }

            /// <summary>
            /// Determines whether [is in role] [the specified user name].
            /// </summary>
            /// <param name="userName">Name of the user.</param>
            /// <param name="projectId">The project id.</param>
            /// <param name="roleName">Name of the role.</param>
            /// <returns>
            /// 	<c>true</c> if [is in role] [the specified user name]; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsInRole(string userName,int projectId, string roleName)
            {
                if (String.IsNullOrEmpty(roleName))
                    throw new ArgumentNullException("roleName");
                if (String.IsNullOrEmpty(userName))
                    throw new ArgumentNullException("userName");

                List<Role> roles = Role.GetRolesForUser(userName, projectId);

                Role role = roles.Find(delegate(Role r) { return r.Name == roleName; });
                if (role != null)
                    return true;

                return false;
            }

            /// <summary>
            /// Determines whether the specified logged on user has permission.
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <param name="permissionKey">The permission key.</param>
            /// <returns>
            /// 	<c>true</c> if the specified project id has permission; otherwise, <c>false</c>.
            /// </returns>
			public static bool HasPermission(int projectId, string permissionKey)
			{
                //if (projectId <= Globals.NewId)
                //    throw new ArgumentOutOfRangeException("projectId");
                if (string.IsNullOrEmpty(permissionKey))
                    throw new ArgumentNullException("permissionKey");

               return ITUser.HasPermission(Security.GetUserName(), projectId, permissionKey);
                
			}

            /// <summary>
            /// Determines whether the specified user name has permission.
            /// </summary>
            /// <param name="userName">Name of the user.</param>
            /// <param name="projectId">The project id.</param>
            /// <param name="permissionKey">The permission key.</param>
            /// <returns>
            /// 	<c>true</c> if the specified user name has permission; otherwise, <c>false</c>.
            /// </returns>
            public static bool HasPermission(string userName,int projectId, string permissionKey)
            {
                if (string.IsNullOrEmpty(userName))
                    throw new ArgumentNullException("userName");
                //if (projectId <=Globals.NewId)
                //    throw new ArgumentOutOfRangeException("projectId");
                if (string.IsNullOrEmpty(permissionKey))
                    throw new ArgumentNullException("permissionKey");

                //return true for all permission checks if the user is in the super users role.
                if (ITUser.IsInRole(Globals.SuperUserRole))
                  return true;

                List<Role> roles = Role.GetRolesForUser(userName, projectId);

                foreach (Role r in roles)
                {
                    if (Role.RoleHasPermission(projectId, r.Name, permissionKey))
                        return true;
                }
                
                return false;
            }

            /// <summary>
            /// Gets the display name of the user.
            /// </summary>
            /// <param name="userName">Name of the user.</param>
            /// <returns></returns>
            public static string GetUserDisplayName(string userName)
            {
                if (string.IsNullOrEmpty(userName))
                    throw new ArgumentNullException("userName");

                string DisplayName = new WebProfile().GetProfile(userName).DisplayName;
                if(!string.IsNullOrEmpty(DisplayName))
                {
                    return DisplayName;
                }else
                {
                    return userName;
                }
            }

            /// <summary>
            /// Gets the users by project id.
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <returns></returns>
            public static List<ITUser> GetUsersByProjectId(int projectId)
            {
                return DataProviderManager.Provider.GetUsersByProjectId(projectId);
            }


            /// <summary>
            /// Sends the user password reminder.
            /// </summary>
            /// <param name="username">The username.</param>
            /// <returns></returns>
            public static void SendUserPasswordReminderNotification(MembershipUser user)
            {
                if (user == null)
                    throw new ArgumentNullException("user");
                
                //load notification plugins 
                NotificationManager nm = new NotificationManager();
                nm.LoadNotificationTypes();

                //load template and replace the tokens
                string template = nm.LoadNotificationTemplate("PasswordReminder");
                string subject = nm.LoadNotificationTemplate("PasswordReminderSubject");
                string displayname = ITUser.GetUserDisplayName(Security.GetUserName());

                nm.SendNotification(user.UserName, subject, String.Format(template, user.GetPassword()));

            }

            /// <summary>
            /// Sends the user registered notification.
            /// </summary>
            /// <param name="user">The user.</param>
            public static void SendUserRegisteredNotification(MembershipUser user)
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                //load notification plugins 
                NotificationManager nm = new NotificationManager();
                nm.LoadNotificationTypes();

                //load template and replace the tokens
                string template = nm.LoadNotificationTemplate("UserRegistered");       
                string subject = nm.LoadNotificationTemplate("UserRegisteredSubject");
                nm.ReplaceTokens(ref template, user);

                //all admin notifications sent to admin user defined in host settings, 
                string AdminNotificationUsername =  HostSetting.GetHostSetting("AdminNotificationUsername");
                
                nm.SendNotification(AdminNotificationUsername, subject, template);
            }

            /// <summary>
            /// Determines whether [is notification type enabled] [the specified username].
            /// </summary>
            /// <param name="username">The username.</param>
            /// <param name="notificationType">Type of the notification.</param>
            /// <returns>
            /// 	<c>true</c> if [is notification type enabled] [the specified username]; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsNotificationTypeEnabled(string username, string notificationType)
            {
                if (string.IsNullOrEmpty(username))
                    throw new ArgumentNullException("username");
                if (string.IsNullOrEmpty(notificationType))
                    throw new ArgumentNullException("notificationType");

                WebProfile profile = new WebProfile().GetProfile(username);

                if (profile != null)
                {
                    string[] notificationTypes = profile.NotificationTypes.Split(';');
                    foreach (string s in notificationTypes)
                    {
                        if (s.Equals(notificationType))
                            return true;
                    }
                }
                return false;
            }
		#endregion
	}
}
