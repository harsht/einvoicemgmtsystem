using System;
using System.Configuration;
using log4net;
using System.Data.SqlTypes;

namespace BugNET.BusinessLogicLayer
{
	/// <summary>
	/// Global constants, enumerations and properties
	/// </summary>
	public class Globals
	{

		#region Public Constants
		    //Cookie Constants
		    public const string UserCookie = "BugNETUser";  
            public const string ConfigFolder  = "\\Config\\";
            public const string IssueColumns = "issuecolumns";
		    /// <summary>
		    /// Constant assigned to value for new bugs
		    /// </summary>
		    public const int NewBugAssignedTo = 0;
		    public const int NewIssueStatusId = 1;
		    public const int NewIssueResolutionId = 1;
            public const string SkipProjectIntro = "skipprojectintro";
		    public const string UnassignedDisplayText = "none";
		    public const int NewId = 0;
            public const int DefaultId = -1;

            public static string SuperUserRole = "Super Users";
            public static string[] DefaultRoles = {"Project Administrators", "Read Only", "Reporter", "Developer", "Quality Assurance" };
            public static string ProjectAdminRole = DefaultRoles[0];

            public static DateTime GetDateTimeMinValue()
            {
                DateTime MinValue = (DateTime)SqlDateTime.MinValue;
                MinValue.AddYears(1);
                return (MinValue);
            }

            /// <summary>
            /// Upgrade Status Enumeration
            /// </summary>
            public enum UpgradeStatus: int{
                Upgrade = 0,
                Install = 1,
                None = 2
            }
        
            /// <summary>
            /// Default read only role permissions
            /// </summary>
            public static int[] ReadOnlyPermissions = { 
                (int)Permission.SUBSCRIBE_ISSUE 
            };

            /// <summary>
            /// Default reporter role permissions
            /// </summary>
            public static int[] ReporterPermissions = { 
                (int)Permission.ADD_ISSUE, 
                (int)Permission.ADD_COMMENT, 
                (int)Permission.OWNER_EDIT_COMMENT, 
                (int)Permission.SUBSCRIBE_ISSUE, 
                (int)Permission.ADD_ATTACHMENT, 
                (int)Permission.ADD_RELATED
            };

            /// <summary>
            /// Default developer role permissions
            /// </summary>
            public static int[] DeveloperPermissions = { 
                (int)Permission.ADD_ISSUE, 
                (int)Permission.ADD_COMMENT,
                (int)Permission.ADD_ATTACHMENT,
                (int)Permission.ADD_RELATED,
                (int)Permission.ADD_TIME_ENTRY, 
                (int)Permission.OWNER_EDIT_COMMENT, 
                (int)Permission.SUBSCRIBE_ISSUE,
                (int)Permission.EDIT_ISSUE,
                (int)Permission.ASSIGN_ISSUE
            };

            /// <summary>
            /// Default QA role permissions
            /// </summary>
            public static int[] QualityAssurancePermissions = { 
                (int)Permission.ADD_ISSUE, 
                (int)Permission.ADD_COMMENT,
                (int)Permission.ADD_ATTACHMENT,
                (int)Permission.ADD_RELATED,
                (int)Permission.ADD_TIME_ENTRY, 
                (int)Permission.OWNER_EDIT_COMMENT, 
                (int)Permission.SUBSCRIBE_ISSUE,
                (int)Permission.EDIT_ISSUE,
                (int)Permission.EDIT_ISSUE_TITLE,
                (int)Permission.ASSIGN_ISSUE,
                //(int)Permission.REOPEN_ISSUE,
                (int)Permission.CLOSE_ISSUE,
                (int)Permission.DELETE_ISSUE
            };

            /// <summary>
            /// Default project administrator role permissions
            /// </summary>
            public static int[] AdministratorPermissions = { 
                (int)Permission.ADD_ISSUE, 
                (int)Permission.ADD_COMMENT,
                (int)Permission.ADD_ATTACHMENT,
                (int)Permission.ADD_RELATED,
                (int)Permission.ADD_TIME_ENTRY,
                (int)Permission.OWNER_EDIT_COMMENT, 
                (int)Permission.SUBSCRIBE_ISSUE,
                (int)Permission.EDIT_ISSUE,
                (int)Permission.EDIT_COMMENT,
                (int)Permission.EDIT_ISSUE_DESCRIPTION,
                (int)Permission.EDIT_ISSUE_TITLE,
                (int)Permission.DELETE_ATTACHMENT,
                (int)Permission.DELETE_COMMENT,
                (int)Permission.DELETE_ISSUE,
                (int)Permission.DELETE_RELATED,
                (int)Permission.DELETE_TIME_ENTRY,
                (int)Permission.ASSIGN_ISSUE,
                //(int)Permission.REOPEN_ISSUE,
                (int)Permission.CLOSE_ISSUE,
                (int)Permission.ADMIN_EDIT_PROJECT
            };

		#endregion

		#region Public Enumerations
        
		public enum ProjectAccessType : int 
		{
			None = 0,
			Public = 1,
			Private = 2
		}

      
        /// <summary>
        /// Permissions
        /// </summary>
        public enum Permission : int
        {
            /// <summary>
            /// None
            /// </summary>
            None = 0,
            /// <summary>
            /// Close the issue
            /// </summary>
            CLOSE_ISSUE = 1,
            /// <summary>
            /// Add a new issue
            /// </summary>
            ADD_ISSUE = 2,
            /// <summary>
            /// Assign an issue
            /// </summary>
            ASSIGN_ISSUE = 3,
            /// <summary>
            /// Edit an issue
            /// </summary>
            EDIT_ISSUE = 4,
            /// <summary>
            /// Subscribe to notifications
            /// </summary>
            SUBSCRIBE_ISSUE = 5,
            /// <summary>
            /// Delete issue
            /// </summary>
            DELETE_ISSUE = 6,
            /// <summary>
            /// Add comment
            /// </summary>
            ADD_COMMENT = 7,
            /// <summary>
            /// Edit comment
            /// </summary>
            EDIT_COMMENT = 8,
            /// <summary>
            /// Delete comment
            /// </summary>
            DELETE_COMMENT = 9,
            /// <summary>
            /// Add Attachment
            /// </summary>
            ADD_ATTACHMENT = 10,
            /// <summary>
            /// Delete Attachment
            /// </summary>
            DELETE_ATTACHMENT = 11,
            /// <summary>
            /// Add Related Isuse
            /// </summary>
            ADD_RELATED = 12,
            /// <summary>
            /// Delete Related Issue
            /// </summary>
            DELETE_RELATED = 13,
            /// <summary>
            /// Re-open an issue
            /// </summary>
            //REOPEN_ISSUE = 14, // old
            /// <summary>
            /// Edit comments
            /// </summary>
            OWNER_EDIT_COMMENT = 15,
            /// <summary>
            /// Edit issue description
            /// </summary>
            EDIT_ISSUE_DESCRIPTION = 16,
            /// <summary>
            /// Ediit the issue summary
            /// </summary>
            EDIT_ISSUE_TITLE = 17,
            /// <summary>
            /// Administrate a project
            /// </summary>
            ADMIN_EDIT_PROJECT = 18,
            /// <summary>
            /// Add a time entry
            /// </summary>
            ADD_TIME_ENTRY = 19,
            /// <summary>
            /// Delete a time entry
            /// </summary>
            DELETE_TIME_ENTRY = 20,
            /// <summary>
            /// Create a new project
            /// </summary>
            ADMIN_CREATE_PROJECT = 21,
            /// <summary>
            /// Add query
            /// </summary>
            ADD_QUERY = 22,
            /// <summary>
            /// Delete query
            /// </summary>
            DELETE_QUERY = 23
        }
		#endregion

		#region Public Properties

        public static string DataAccessType
        {
            get
            {
                string str = ConfigurationSettings.AppSettings["DataAccessType"];
                if (str == null || str == String.Empty)
                    throw (new ApplicationException("DataAccessType configuration is missing from you web.config. It should contain  <appSettings><add key=\"DataAccessType\" value=\"data access type\" /></appSettings> "));
                else
                    return (str);
            }
        }

        /// <summary>
        /// Gets the SMTP server.
        /// </summary>
        /// <value>The SMTP server.</value>
		public static string SmtpServer 
		{
			get 
			{
				string str = HostSetting.GetHostSetting("SMTPServer");
				if (String.IsNullOrEmpty(str))
					throw (new ApplicationException("SmtpServer configuration is missing or not set, check the host settings"));
				else
					return (str);
			}
		}


        /// <summary>
        /// Gets the host email address.
        /// </summary>
        /// <value>The host email address.</value>
		public static string HostEmailAddress 
		{
			get 
			{
				
				string str = HostSetting.GetHostSetting("HostEmailAddress");
				if (String.IsNullOrEmpty(str))
					throw (new ApplicationException("Host email address is not set, check the host settings."));
				else
					return (str);
			}
		}

        /// <summary>
        /// Gets the user account source.
        /// </summary>
        /// <value>The user account source.</value>
		public static string UserAccountSource 
		{
			get 
			{
				string str = HostSetting.GetHostSetting("UserAccountSource");
				if (String.IsNullOrEmpty(str))
					throw (new ApplicationException("UserAccountSource configuration is not set properly, check the host settings."));
				else
					return (str);
			}
		}
        /// <summary>
        /// Gets the default URL.
        /// </summary>
        /// <value>The default URL.</value>
		public static string DefaultUrl 
		{
			get 
			{
				string str = HostSetting.GetHostSetting("DefaultUrl");
                if (String.IsNullOrEmpty(str))
                {
                    throw (new ApplicationException("DefaultUrl configuration is not set, check the host settings."));
                }
                else
                {
                    if (str.EndsWith("/"))
                    {
                        return (str);
                    }
                    else
                    {
                        return (str.Insert(str.Length, "/"));
                    }
                }
			}
		}
        /// <summary>
        /// Parses the full bug id.
        /// </summary>
        /// <param name="fullId">The full id.</param>
        /// <returns></returns>
		public static int ParseFullIssueId(string fullId)
		{
			string[] split = fullId.Split('-');

			if(split.Length > 1)
				return  Convert.ToInt32(split[1]);

            try
            {
                return Convert.ToInt32(split[0]);
            }
            catch
            {
                return -1;
            }
		}
		#endregion

	}
}
