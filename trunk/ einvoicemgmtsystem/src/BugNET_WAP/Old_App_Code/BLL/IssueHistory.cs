using System;
using BugNET.DataAccessLayer;
using System.Collections;
using System.Collections.Generic;

namespace BugNET.BusinessLogicLayer
{
	/// <summary>
	/// Issue History
	/// </summary>
	public class IssueHistory
	{

		#region Private Variables
			private int _Id;
			private int _IssueId;
			private string _CreatedUserName;
			private string _FieldChanged;
			private string _OldValue; 
			private string _NewValue;
			private DateTime _DateChanged;
            private string _CreatorDisplayName;
		#endregion

		#region Constructors
            /// <summary>
            /// Initializes a new instance of the <see cref="BugHistory"/> class.
            /// </summary>
            /// <param name="id">The id.</param>
            /// <param name="issueId">The issue id.</param>
            /// <param name="createdUserName">Name of the created user.</param>
            /// <param name="creatorDisplayName">Display name of the creator.</param>
            /// <param name="fieldChanged">The field changed.</param>
            /// <param name="oldValue">The old value.</param>
            /// <param name="newValue">The new value.</param>
            /// <param name="dateChanged">The date changed.</param>
            public IssueHistory(int id, int  issueId, string createdUserName, string creatorDisplayName, string fieldChanged, string oldValue, string newValue, DateTime dateChanged)
			{
				_Id = id;
				_IssueId= issueId;
				_CreatedUserName= createdUserName;
                _CreatorDisplayName = creatorDisplayName;
				_FieldChanged = fieldChanged;
				_OldValue = oldValue;
				_NewValue = newValue;
				_DateChanged = dateChanged;
			}
            /// <summary>
            /// Initializes a new instance of the <see cref="BugHistory"/> class.
            /// </summary>
            /// <param name="bugId">The bug id.</param>
            /// <param name="createdUserName">Name of the created user.</param>
            /// <param name="fieldChanged">The field changed.</param>
            /// <param name="oldValue">The old value.</param>
            /// <param name="newValue">The new value.</param>
			public IssueHistory(int issueId, string createdUserName,string fieldChanged,string oldValue,string newValue)
				: this
				(
				Globals.NewId,
				issueId,
				createdUserName,
                string.Empty,
				fieldChanged,
				oldValue,
				newValue,
				DateTime.Now
				)
			{}
            /// <summary>
            /// Initializes a new instance of the <see cref="BugHistory"/> class.
            /// </summary>
			//public IssueHistory(){}
		#endregion

		#region Properties
            /// <summary>
            /// Gets the id.
            /// </summary>
            /// <value>The id.</value>
			public int Id 
			{
				get{return (_Id);}
			}

            /// <summary>
            /// Gets the bug id.
            /// </summary>
            /// <value>The bug id.</value>
			public int IssueId 
			{
				get{return (_IssueId);}
			}

            /// <summary>
            /// Gets the name of the created user.
            /// </summary>
            /// <value>The name of the created user.</value>
			public string CreatedUserName
			{
				get{return (_CreatedUserName);}
			}
            /// <summary>
            /// Gets the field changed.
            /// </summary>
            /// <value>The field changed.</value>
			public string FieldChanged 
			{
				get{return (_FieldChanged);}
			}

            /// <summary>
            /// Gets the old value.
            /// </summary>
            /// <value>The old value.</value>
			public string OldValue 
			{
				get{return (_OldValue);}
			}
            /// <summary>
            /// Gets the new value.
            /// </summary>
            /// <value>The new value.</value>
			public string NewValue
			{
				get{return (_NewValue);}
			}
            /// <summary>
            /// Gets the date changed.
            /// </summary>
            /// <value>The date changed.</value>
			public DateTime DateChanged
			{
				get{return (_DateChanged);}
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
		#endregion

		#region Instance Methods
            /// <summary>
            /// Saves this instance.
            /// </summary>
            /// <returns></returns>
			public bool Save()
			{
				if (_Id <= Globals.NewId) 
				{
                    int TempId = DataProviderManager.Provider.CreateNewIssueHistory(this);
					if (TempId>0) 
					{
						_Id = TempId;
						return true;
					} 
					else
						return false;
				} 
				else
					return false;
			}
		#endregion

		#region Static Methods
            /// <summary>
            /// Gets the BugHistory by bug id.
            /// </summary>
            /// <param name="bugId">The bug id.</param>
            /// <returns></returns>
			public static List<IssueHistory> GetIssueHistoryByIssueId(int issueId)
			{
				if (issueId <= Globals.NewId)
					throw (new ArgumentOutOfRangeException("issueId"));
                
                return DataProviderManager.Provider.GetIssueHistoryByIssueId(issueId);
			}
		#endregion
		
	}

	
}
