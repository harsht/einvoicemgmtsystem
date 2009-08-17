using System;

namespace BugNET.BusinessLogicLayer
{
    /// <summary>
    /// Class to save the state of the IssueList page. An object of this class is saved in the session
    /// so that the IssueList page state can be restored. 
    /// </summary>
	public class IssueListState
	{
		private string _ViewIssues;
		private int _ProjectId;
		private int _IssueListPageIndex;

        /// <summary>
        /// Gets or sets the view issues.
        /// </summary>
        /// <value>The view issues.</value>
		public string ViewIssues
		{
			get { return _ViewIssues; }
			set { _ViewIssues = value; }
		}

        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
		public int ProjectId
		{
			get { return _ProjectId; }
			set { _ProjectId = value; }
		}

        /// <summary>
        /// Gets or sets the index of the issue list page.
        /// </summary>
        /// <value>The index of the issue list page.</value>
		public int IssueListPageIndex
		{
			get { return _IssueListPageIndex; }
			set { _IssueListPageIndex = value; }
		}
	}
}