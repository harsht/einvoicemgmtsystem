using System;
using System.Data;
using System.Collections.Generic;
using BugNET.DataAccessLayer;
using System.IO;
using log4net;
using log4net.Config;

namespace BugNET.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for IssueAttachment.
	/// </summary>
	public class IssueAttachment
	{
		#region Private Variables
            private int _Id;
            private int _IssueId;
            private string _CreatorUserName;
            private string _CreatorDisplayName;
            private DateTime _DateCreated;
            private string _FileName;
            private string _ContentType;
			private string _Description;
			private int _Size;
            private Byte[] _Attachment;
            private static readonly ILog Log = LogManager.GetLogger(typeof(IssueAttachment));
		#endregion

		#region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="IssueAttachment"/> class.
            /// </summary>
            /// <param name="fileName">Name of the file.</param>
            /// <param name="contentType">Type of the content.</param>
            /// <param name="attachment">The attachment.</param>
            public IssueAttachment(string fileName, string contentType, byte[] attachment)
                : this(Globals.NewId, Globals.NewId, String.Empty, String.Empty, Globals.GetDateTimeMinValue(), fileName, contentType, attachment, 0)
            { }


            /// <summary>
            /// Initializes a new instance of the <see cref="IssueAttachment"/> class.
            /// </summary>
            /// <param name="issueId">The issue id.</param>
            /// <param name="creatorUserName">Name of the creator user.</param>
            /// <param name="fileName">Name of the file.</param>
            /// <param name="contentType">Type of the content.</param>
            /// <param name="attachment">The attachment.</param>
            public IssueAttachment(int issueId, string creatorUserName, string fileName, string contentType, byte[] attachment, int size)
                : this(Globals.NewId, issueId, creatorUserName, String.Empty, Globals.GetDateTimeMinValue(), fileName, contentType, attachment, size)
            { }


            /// <summary>
            /// Initializes a new instance of the <see cref="IssueAttachment"/> class.
            /// </summary>
            /// <param name="attachmentId">The attachment id.</param>
            /// <param name="issueId">The issue id.</param>
            /// <param name="creatorUserName">Name of the creator user.</param>
            /// <param name="creatorDisplayName">Display name of the creator.</param>
            /// <param name="created">The created.</param>
            /// <param name="fileName">Name of the file.</param>
            public IssueAttachment(int attachmentId, int issueId, string creatorUserName, string creatorDisplayName, DateTime created, string fileName, int size)
                : this(attachmentId, issueId, creatorUserName, creatorDisplayName, created, fileName, String.Empty, null,size)
            { }

            /// <summary>
            /// Initializes a new instance of the <see cref="IssueAttachment"/> class.
            /// </summary>
            /// <param name="attachmentId">The attachment id.</param>
            /// <param name="issueId">The issue id.</param>
            /// <param name="creatorUserName">Name of the creator user.</param>
            /// <param name="creatorDisplayName">Display name of the creator.</param>
            /// <param name="created">The created.</param>
            /// <param name="fileName">Name of the file.</param>
            /// <param name="contentType">Type of the content.</param>
            /// <param name="attachment">The attachment.</param>
            public IssueAttachment(int attachmentId, int issueId, string creatorUserName, string creatorDisplayName, DateTime created, string fileName, string contentType, byte[] attachment, int size)
            {

                _Id = attachmentId;
                _IssueId = issueId;
                _CreatorUserName = creatorUserName;
                _CreatorDisplayName = creatorDisplayName;
                _DateCreated = created;
                _FileName = fileName;
                _ContentType = contentType;
                _Attachment = attachment;
                _Size = size;
            }
		#endregion

		#region Properties
            /// <summary>
            /// Gets the id.
            /// </summary>
            /// <value>The id.</value>
            public int Id
            {
                get { return _Id; }
            }


            /// <summary>
            /// Gets or sets the issue id.
            /// </summary>
            /// <value>The issue id.</value>
            public int IssueId
            {
                get { return _IssueId; }
                set
                {
                    if (value <= Globals.NewId)
                        throw (new ArgumentOutOfRangeException("value"));
                    _IssueId = value;
                }
            }


            /// <summary>
            /// Gets the creator username.
            /// </summary>
            /// <value>The creator username.</value>
            public string CreatorUserName
            {
                get
                {
                    if (_CreatorUserName == null || _CreatorUserName.Length == 0)
                        return string.Empty;
                    else
                        return _CreatorUserName;
                }
            }


            /// <summary>
            /// Gets the display name of the creator.
            /// </summary>
            /// <value>The display name of the creator.</value>
            public string CreatorDisplayName
            {
                get
                {
                    if (_CreatorDisplayName == null || _CreatorDisplayName.Length == 0)
                        return string.Empty;
                    else
                        return _CreatorDisplayName;
                }
            }

            /// <summary>
            /// Gets the date created.
            /// </summary>
            /// <value>The date created.</value>
            public DateTime DateCreated
            {
                get { return _DateCreated; }
            }


            /// <summary>
            /// Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public string FileName
            {
                get
                {
                    if (_FileName == null || _FileName.Length == 0)
                        return string.Empty;
                    else
                        return _FileName;
                }
            }



            /// <summary>
            /// Gets the type of the content.
            /// </summary>
            /// <value>The type of the content.</value>
            public string ContentType
            {
                get
                {
                    if (_ContentType == null || _ContentType.Length == 0)
                        return string.Empty;
                    else
                        return _ContentType;
                }
            }


            /// <summary>
            /// Gets the attachment.
            /// </summary>
            /// <value>The attachment.</value>
            public Byte[] Attachment
            {
                get { return _Attachment; }
            }

    
            ///<summary>
            /// Gets the size.
            /// </summary>
            /// <value>The size.</value>
			public int Size 
			{
				get{return _Size;}
			}
            /// <summary>
            /// Gets the description.
            /// </summary>
            /// <value>The description.</value>
			public string Description
			{
				get{return _Description;}
			}

            

		#endregion

		#region Instance Methods
            /// <summary>
            /// Saves this instance.
            /// </summary>
            /// <returns></returns>
			public bool Save() 
			{
				

				if (Id <= Globals.NewId) 
				{
					int TempId = DataProviderManager.Provider.CreateNewIssueAttachment(this);
					if (TempId>0) 
					{
						_Id = TempId;
						return true;
					} 
					else
						return false;
				} 
				return false;
			}
		#endregion

		#region Static Methods
			/// <summary>
			/// Gets all IssueAttachments for a bug
			/// </summary>
			/// <param name="bugId"></param>
			/// <returns></returns>
			public static List<IssueAttachment> GetIssueAttachmentsByIssueId(int issueId)
			{
                // validate input
                if (issueId <= Globals.NewId)
                    throw (new ArgumentOutOfRangeException("issueId"));

				return DataProviderManager.Provider.GetIssueAttachmentsByIssueId(issueId);
			}

            /// <summary>
            /// Gets an IssueAttachment by id
            /// </summary>
            /// <param name="IssueAttachmentId">The IssueAttachment id.</param>
            /// <returns></returns>
			public static IssueAttachment GetIssueAttachmentById(int attachmentId)
			{
                // validate input
                if (attachmentId <= Globals.NewId)
                    throw (new ArgumentOutOfRangeException("attachmentId"));

                return DataProviderManager.Provider.GetIssueAttachmentById(attachmentId);
			}

            /// <summary>
            /// Deletes the IssueAttachment.
            /// </summary>
            /// <param name="IssueAttachmentId">The IssueAttachment id.</param>
            /// <returns></returns>
            public static bool DeleteIssueAttachment(int IssueAttachmentId)
            {
                
                
                IssueAttachment att = IssueAttachment.GetIssueAttachmentById(IssueAttachmentId);
                //TODO: The upload path for the project should really be apart of 
                //IssueAttachment class when gotten from the database
                Issue b = Issue.GetIssueById(att.IssueId);
                Project p = Project.GetProjectById(b.ProjectId);


                if (DataProviderManager.Provider.DeleteIssueAttachment(IssueAttachmentId))
                {
                    //delete IssueAttachment from file system.
                    try
                    {
                        File.Delete(System.Web.HttpContext.Current.Server.MapPath(string.Format("~\\Uploads\\{0}\\{1}",p.UploadPath,att.FileName)));   
                    }
                    catch (Exception ex)
                    {
                        //set user to log4net context, so we can use %X{user} in the appenders
                        if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                            MDC.Set("user", System.Web.HttpContext.Current.User.Identity.Name);
 
                        if(Log.IsErrorEnabled)
                            Log.Error(String.Format("Error Deleting IssueAttachment - {0}",string.Format("{0}\\{1}",p.UploadPath,att.FileName)), ex);

                        //TODO: Get this from a resource string.
                        throw new ApplicationException("An error has occured deleing an IssueAttachment", ex);
                    }
                }
                return true;
               
            }
		
		#endregion

	}
}
