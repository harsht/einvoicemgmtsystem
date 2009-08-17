using System;
using System.Collections;
using System.Collections.Generic;
using BugNET.DataAccessLayer;

namespace BugNET.BusinessLogicLayer
{
    /// <summary>
    /// IssueComment Class
    /// </summary>
	public class IssueComment
	{
		#region Private Variables
			private int _Id;
			private int _IssueId;
			private string _CreatorUserName;
			private Guid _CreatorUserId;
			private string _CreatorEmail;
            private string _CreatorDisplayName;
            private string _Comment;
            private DateTime _DateCreated;
		#endregion

		#region Constructors


            /// <summary>
            /// Initializes a new instance of the <see cref="IssueComment"/> class.
            /// </summary>
            /// <param name="issueId">The issue id.</param>
            /// <param name="comment">The comment.</param>
            /// <param name="creatorUsername">The creator username.</param>
            public IssueComment(int issueId, string comment, string creatorUsername)
                : this(Globals.NewId, issueId, comment, creatorUsername,Guid.Empty, String.Empty, DateTime.MinValue)
            { }

            /// <summary>
            /// Initializes a new instance of the <see cref="IssueComment"/> class.
            /// </summary>
            /// <param name="commentId">The comment id.</param>
            /// <param name="issueId">The issue id.</param>
            /// <param name="comment">The comment.</param>
            /// <param name="creatorUsername">The creator username.</param>
            /// <param name="creatorDisplayName">Display name of the creator.</param>
            /// <param name="created">The created.</param>
            public IssueComment(int commentId, int issueId, string comment, string creatorUserName,Guid creatorUserId, string creatorDisplayName, DateTime created)
            {
                if (comment == null || comment.Length == 0)
                    throw (new ArgumentOutOfRangeException("comment"));

                if (issueId <= Globals.NewId)
                    throw (new ArgumentOutOfRangeException("IssueId"));

                _Id = commentId;
                _IssueId = issueId;
                _CreatorUserName = creatorUserName;
                _CreatorDisplayName = creatorDisplayName;
                _Comment = comment;
                _DateCreated = created;
                _CreatorUserId = creatorUserId;
            }
		#endregion

		#region Properties


            /// <summary>
            /// Gets the creator user id.
            /// </summary>
            /// <value>The creator user id.</value>
			public Guid CreatorUserId 
			{
				get {return _CreatorUserId;}
			}

            /// <summary>
            /// Gets or sets the comment.
            /// </summary>
            /// <value>The comment.</value>
            public string Comment
            {
                get
                {
                    if (_Comment == null || _Comment.Length == 0)
                        return string.Empty;
                    else
                        return _Comment;
                }

                set { _Comment = value; }
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
		#endregion

		#region Instance Methods
            /// <summary>
            /// Saves this instance.
            /// </summary>
            /// <returns></returns>
			public bool Save () 
			{
                if (Id <= Globals.NewId)
                {
                    int TempId = DataProviderManager.Provider.CreateNewIssueComment(this);
                    if (TempId > Globals.NewId)
                    {
                        _Id = TempId;
                        IssueNotification.SendNewIssueCommentNotification(this.IssueId, IssueComment.GetIssueCommentById(this.Id));
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                   return DataProviderManager.Provider.UpdateIssueComment(this);
                }			
			}
		#endregion

		#region Static Methods
			/// <summary>
			/// Gets all comments for a issue
			/// </summary>
			/// <param name="issueId"></param>
			/// <returns>List of Comment Objects</returns>
			public static List<IssueComment> GetIssueCommentsByIssueId(int issueId)
			{
				return DataProviderManager.Provider.GetIssueCommentsByIssueId(issueId);
			}

			/// <summary>
			/// Delete a comment by Id
			/// </summary>
			/// <param name="commentId"></param>
			/// <returns>True if successful</returns>
			public static bool DeleteIssueCommentById(int commentId)
			{
				return DataProviderManager.Provider.DeleteIssueCommentById(commentId);
			}

            /// <summary>
            /// Gets the issue comment by id.
            /// </summary>
            /// <param name="issueCommentId">The issue comment id.</param>
            /// <returns></returns>
            public static IssueComment GetIssueCommentById(int issueCommentId)
            {
                return DataProviderManager.Provider.GetIssueCommentById(issueCommentId);
            }
		#endregion
	}
}
