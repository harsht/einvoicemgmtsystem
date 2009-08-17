using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using BugNET.DataAccessLayer;
using System.IO;
using System.Web;
using System.Xml.Serialization;

namespace BugNET.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Project.
	/// </summary>
	public class Project
	{
		private string				_Description;
		private int					_Id;
		private string				_Name;
		private string				_Code;
        private Guid _ManagerId;
		private string				_ManagerUserName;
		private DateTime			_DateCreated;
        private string              _CreatorUserName;
		private string				_UploadPath;
		private bool				_Disabled;
		private Globals.ProjectAccessType	_AccessType;
        private bool                _AllowAttachments;
        private IssueAttachmentStorageType _AttachmentStorageType;
        private string _SvnRepositoryUrl;

		#region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="name">The name.</param>
        /// <param name="code">The code.</param>
        /// <param name="description">The description.</param>
        /// <param name="managerUserName">Name of the manager user.</param>
        /// <param name="creatorUserName">Name of the creator user.</param>
        /// <param name="uploadPath">The upload path.</param>
        /// <param name="accessType">Type of the access.</param>
        /// <param name="active">The active.</param>
        /// <param name="allowAttachments">if set to <c>true</c> [allow attachments].</param>
        public Project(int projectId, string name, string code, string description, string managerUserName, string creatorUserName, string uploadPath, Globals.ProjectAccessType accessType, bool disabled, bool allowAttachments, IssueAttachmentStorageType attachmentStorageType, string svnRepositoryUrl)
            : this(projectId, name, code, description, managerUserName, creatorUserName, uploadPath, DateTime.Now, accessType, disabled, allowAttachments, Guid.Empty, attachmentStorageType, svnRepositoryUrl)
    		{}

        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="name">The name.</param>
        /// <param name="code">The code.</param>
        /// <param name="description">The description.</param>
        /// <param name="managerUserName">Name of the manager user.</param>
        /// <param name="creatorUserName">Name of the creator user.</param>
        /// <param name="uploadPath">The upload path.</param>
        /// <param name="dateCreated">The date created.</param>
        /// <param name="accessType">Type of the access.</param>
        /// <param name="active">The active.</param>
        /// <param name="allowAttachments">if set to <c>true</c> [allow attachments].</param>
		public Project(int projectId,string name,string code, string description,string managerUserName,
                string creatorUserName, string uploadPath, DateTime dateCreated, Globals.ProjectAccessType accessType, bool disabled, bool allowAttachments, Guid managerId, IssueAttachmentStorageType attachmentStorageType, string svnRepositoryUrl)
			{
				// Validate Mandatory Fields//
				if (name == null ||name.Length==0 )
					throw (new ArgumentOutOfRangeException("name"));

				_Id                     = projectId;
				_Description            = description;
				_Name                   = name;
				_Code					= code;
				_ManagerUserName        = managerUserName;
                _ManagerId              = managerId;
				_CreatorUserName		= creatorUserName;
				_DateCreated            = dateCreated;
				_UploadPath				= uploadPath;
				_Disabled				= disabled;
				_AccessType				= accessType;
                _AllowAttachments       = allowAttachments;
                _AttachmentStorageType  = attachmentStorageType;
                _SvnRepositoryUrl       = svnRepositoryUrl;
			}
            /// <summary>
            /// Initializes a new instance of the <see cref="Project"/> class.
            /// </summary>
			public Project(){}
		#endregion

		#region Properties
            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            /// <value>The id.</value>
			public int Id
			{
				get {return _Id;}
				set {throw new Exception("Cannot set readonly property Id");}
			}

            /// <summary>
            /// Gets or sets the type of the attachment storage.
            /// </summary>
            /// <value>The type of the attachment storage.</value>
            public IssueAttachmentStorageType AttachmentStorageType 
            {
                get { return _AttachmentStorageType; }
                set { _AttachmentStorageType = value; } 
            }
            /// <summary>
            /// Gets or sets the code.
            /// </summary>
            /// <value>The code.</value>
			public string Code
			{
				get{return _Code;}
				set{_Code = value;}
			}

            /// <summary>
            /// Gets or sets the manager id.
            /// </summary>
            /// <value>The manager id.</value>
            public Guid ManagerId
            {
                get { return _ManagerId; }
                set { _ManagerId = value; }
            }

            /// <summary>
            /// Gets or sets the name of the creator user.
            /// </summary>
            /// <value>The name of the creator user.</value>
			public string CreatorUserName
			{
				get {return _CreatorUserName;}
                set { _CreatorUserName = value; }
			}


            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="Project"/> is disabled.
            /// </summary>
            /// <value><c>true</c> if disabled; otherwise, <c>false</c>.</value>
			public bool Disabled 
			{
				get {return _Disabled;}
                set { _Disabled = value; }
			}

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
			public string Name 
			{
				get
				{
					if (_Name == null ||_Name.Length==0)
						return string.Empty;
					else
						return _Name;
				}
				set {_Name = value;}
			}

            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            /// <value>The description.</value>
			public string Description 
			{
				get 
				{
					if (_Description == null ||_Description.Length==0)
						return string.Empty;
					else
						return _Description;
				}
				set
				{ _Description = value;}
			}

            /// <summary>
            /// Gets or sets the date created.
            /// </summary>
            /// <value>The date created.</value>
			public DateTime DateCreated 
			{
				get {return  _DateCreated;}
				set{throw new Exception("Cannot set readonly property DateCreated");}
			}

            /// <summary>
            /// Gets or sets the name of the manager user.
            /// </summary>
            /// <value>The name of the manager user.</value>
			public string ManagerUserName 
			{
				get {return  _ManagerUserName;}
                set { _ManagerUserName = value; }
			}

            /// <summary>
            /// Gets or sets the upload path.
            /// </summary>
            /// <value>The upload path.</value>
			public string UploadPath
			{
				get 
				{
					if (_UploadPath == null || _UploadPath.Length==0)
						return string.Empty;
					else
						return _UploadPath;
				}
				set {_UploadPath = value;}
			}

            /// <summary>
            /// Gets or sets the type of the access.
            /// </summary>
            /// <value>The type of the access.</value>
			public Globals.ProjectAccessType AccessType
			{
				get
				{
					return _AccessType;

				}
				set
				{
					_AccessType = value;
				}
			}

            /// <summary>
            /// Gets or sets a value indicating whether [allow attachments].
            /// </summary>
            /// <value><c>true</c> if [allow attachments]; otherwise, <c>false</c>.</value>
            public bool AllowAttachments
            {
                get
                {
                    return _AllowAttachments;

                }
                set
                {
                    _AllowAttachments = value;
                }
            }

            /// <summary>
            /// Gets or sets the SVN repository URL.
            /// </summary>
            /// <value>The SVN repository URL.</value>
            public string SvnRepositoryUrl
            {
                get { return _SvnRepositoryUrl; }
                set { _SvnRepositoryUrl = value; }
            }
		#endregion

		#region Public Methods
            /// <summary>
            /// Saves this instance.
            /// </summary>
            /// <returns></returns>
			public bool Save () 
			{
				if (_Id <= 0) 
				{
					    
					int TempId = DataProviderManager.Provider.CreateNewProject(this);
					if (TempId>Globals.NewId) 
					{
						_Id = TempId;
						try
						{	
                            //create default roles for new project.
                            Role.CreateDefaultProjectRoles(_Id);
							//create attachment directory

                            //ONLY DO THIS IF STORAGE TYPE IS FILE SYSETM -- CATCH ERRORS!!
							System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~\\Uploads\\" + _UploadPath));
						}
						catch(Exception ex)
                        {
                            return false;
                        }   
						
						return true;
					}  
					else
						return false;
				}
				else
					return (UpdateProject());
			}		
		#endregion

		#region Private Methods
			

			private bool UpdateProject()
			{
				
				return DataProviderManager.Provider.UpdateProject(this);
			
			}
		#endregion

		#region Static Methods
            /// <summary>
            /// Gets the project by id.
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <returns></returns>
			public static Project GetProjectById(int  projectId) 
			{
				// validate input
				if (projectId <= 0)
					throw (new ArgumentOutOfRangeException("projectId"));

				
				
				return DataProviderManager.Provider.GetProjectById(projectId);
			}

            /// <summary>
            /// Gets the project by code.
            /// </summary>
            /// <param name="projectCode">The project code.</param>
            /// <returns></returns>
			public static Project GetProjectByCode(string  projectCode) 
			{
				// validate input
				if (string.IsNullOrEmpty(projectCode))
					throw (new ArgumentOutOfRangeException("projectCode"));

				
				return DataProviderManager.Provider.GetProjectByCode(projectCode);
			}

            /// <summary>
            /// Gets all projects.
            /// </summary>
            /// <returns></returns>
			public static List<Project> GetAllProjects()
			{				
				return DataProviderManager.Provider.GetAllProjects();
			}

            /// <summary>
            /// Gets the public projects.
            /// </summary>
            /// <returns></returns>
			public static List<Project> GetPublicProjects()
			{
						
				return DataProviderManager.Provider.GetPublicProjects();
			}

            /// <summary>
            /// Gets the name of the projects by user.
            /// </summary>
            /// <param name="userName">Name of the user.</param>
            /// <returns></returns>
			public static List<Project> GetProjectsByMemberUserName(string userName) 
			{
				if (String.IsNullOrEmpty(userName))
					throw (new ArgumentOutOfRangeException("userName"));
				
				return GetProjectsByMemberUserName(userName,  true);
			}

            /// <summary>
            /// Gets the name of the projects by user.
            /// </summary>
            /// <param name="userName">Name of the user.</param>
            /// <param name="activeOnly">if set to <c>true</c> [active only].</param>
            /// <returns></returns>
            public static List<Project> GetProjectsByMemberUserName(string userName, bool activeOnly) 
			{
                if (String.IsNullOrEmpty(userName))
                    throw (new ArgumentOutOfRangeException("userName"));

						
				return DataProviderManager.Provider.GetProjectsByMemberUserName(userName,activeOnly);
				
			}

            /// <summary>
            /// Adds the user to project.
            /// </summary>
            /// <param name="userName">Name of the user.</param>
            /// <param name="projectId">The project id.</param>
            /// <returns></returns>
			public static bool AddUserToProject(string userName, int projectId) 
			{
                if (String.IsNullOrEmpty(userName))
                    throw new ArgumentOutOfRangeException("userName");
                if (projectId <= Globals.NewId)
                    throw new ArgumentOutOfRangeException("projectId");
				
						
				return DataProviderManager.Provider.AddUserToProject(userName,projectId);
			}

            /// <summary>
            /// Removes the user from project.
            /// </summary>
            /// <param name="userName">Name of the user.</param>
            /// <param name="projectId">The project id.</param>
            /// <returns></returns>
			public static bool RemoveUserFromProject(string userName, int projectId) 
			{
                if (String.IsNullOrEmpty(userName))
                    throw new ArgumentOutOfRangeException("userName");
                if (projectId <= Globals.NewId)
                    throw new ArgumentOutOfRangeException("projectId");

						
				return DataProviderManager.Provider.RemoveUserFromProject(userName,projectId);
			}

            /// <summary>
            /// Determines whether [is project member] [the specified user id].
            /// </summary>
            /// <param name="userName">Name of the user.</param>
            /// <param name="projectId">The project id.</param>
            /// <returns>
            /// 	<c>true</c> if [is project member] [the specified user id]; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsUserProjectMember(string userName, int projectId)
            {
                if (String.IsNullOrEmpty(userName))
                    throw new ArgumentOutOfRangeException("userName");
                if (projectId <= Globals.NewId)
                    throw new ArgumentOutOfRangeException("projectId");

                
                return DataProviderManager.Provider.IsUserProjectMember(userName, projectId);
            }

            /// <summary>
            /// Deletes the project.
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <returns></returns>
			public static bool DeleteProject (int projectId) 
			{
				if (projectId <= Globals.NewId )
					throw (new ArgumentOutOfRangeException("projectId"));

				
                string uploadpath = GetProjectById(projectId).UploadPath;

                if (DataProviderManager.Provider.DeleteProject(projectId))
                {
                    try
                    {
                        System.IO.Directory.Delete(HttpContext.Current.Server.MapPath("~\\Uploads\\" + uploadpath), true);
                    }
                    catch { }
                  
                    return true;
                }
                return false;
			}

          

            /// <summary>
            /// Clones the project.
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <param name="projectName">Name of the project.</param>
            /// <returns></returns>
            public static bool CloneProject(int projectId, string projectName)
            {
                if (projectId <= Globals.NewId)
                    throw (new ArgumentOutOfRangeException("projectId"));
                if(string.IsNullOrEmpty(projectName))
                    throw new ArgumentNullException("projectName");

                return DataProviderManager.Provider.CloneProject(projectId, projectName);
            }

            /// <summary>
            /// Gets the road map.
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <returns></returns>
            public static List<Issue> GetRoadMap(int projectId)
            {
                if (projectId <= Globals.NewId)
                    throw (new ArgumentOutOfRangeException("projectId"));
       
                return DataProviderManager.Provider.GetProjectRoadmap(projectId);
            }

            /// <summary>
            /// Gets the road map progress.
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <param name="milestoneId">The milestone id.</param>
            /// <returns>total number of issues and total number of close issues</returns>
            public static int[] GetRoadMapProgress(int projectId, int milestoneId)
            {
                if (projectId <= Globals.NewId)
                    throw (new ArgumentOutOfRangeException("projectId"));
                if (milestoneId < -1)
                    throw new ArgumentNullException("milestoneId");

                return DataProviderManager.Provider.GetProjectRoadmapProgress(projectId,milestoneId);
            }

            /// <summary>
            /// Gets the change log.
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <returns></returns>
            public static List<Issue> GetChangeLog(int projectId)
            {
                if (projectId <= Globals.NewId)
                    throw (new ArgumentOutOfRangeException("projectId"));

                return DataProviderManager.Provider.GetProjectChangeLog(projectId);
            }
			
		#endregion

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
		public override string ToString()
		{
			return _Name;
		}

	
	}
}
