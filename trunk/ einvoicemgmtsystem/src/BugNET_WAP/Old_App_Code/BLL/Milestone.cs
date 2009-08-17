using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using BugNET.DataAccessLayer;

namespace BugNET.BusinessLogicLayer
{
	/// <summary>
	/// Summary description for Milestone.
	/// </summary>
	public class Milestone
	{
		#region Private Variables
			private int      _Id;
			private int      _ProjectId;
			private string   _Name;
            private int      _SortOrder;
            private string   _ImageUrl;
		#endregion

		#region Constructors
            /// <summary>
            /// Initializes a new instance of the <see cref="T:Milestone"/> class.
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <param name="name">The name.</param>
            /// <param name="sortOrder">The sort order.</param>
			public Milestone(int projectId, string name, int sortOrder,string imageUrl)
                : this(Globals.NewId, projectId, name, sortOrder, imageUrl)
				{}

            /// <summary>
            /// Initializes a new instance of the <see cref="Milestone"/> class.
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <param name="name">The name.</param>
            /// <param name="imageUrl">The image URL.</param>
            public Milestone(int projectId, string name, string imageUrl)
                : this(Globals.NewId, projectId, name, -1, imageUrl)
            { }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:Milestone"/> class.
            /// </summary>
            /// <param name="id">The id.</param>
            /// <param name="projectId">The project id.</param>
            /// <param name="name">The name.</param>
            /// <param name="sortOrder">The sort order.</param>
			public Milestone(int id, int projectId, string name, int sortOrder, string imageUrl) 
			{
				if (projectId<=Globals.NewId && id > Globals.NewId)
					throw (new ArgumentOutOfRangeException("projectId"));

				if (name == null ||name.Length==0 )
					throw (new ArgumentOutOfRangeException("name"));

				_Id           = id;
				_ProjectId    = projectId;
				_Name         = name;
                _SortOrder    = sortOrder;
                _ImageUrl     = imageUrl;
			}
		#endregion

		#region Properties
            /// <summary>
            /// Gets the id.
            /// </summary>
            /// <value>The id.</value>
			public int Id 
			{
				get {return _Id;}
			}


            /// <summary>
            /// Gets or sets the project id.
            /// </summary>
            /// <value>The project id.</value>
			public int ProjectId 
			{
				get {return _ProjectId;}
				set 
				{
					if (value<=Globals.NewId )
						throw (new ArgumentOutOfRangeException("value"));
					_ProjectId=value;
				}
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
				set {_Name=value;}
			}

            /// <summary>
            /// Gets or sets the sort order.
            /// </summary>
            /// <value>The sort order.</value>
            public int SortOrder
            {
                get { return _SortOrder; }
                set { _SortOrder = value; }
            }

            /// <summary>
            /// Gets the image URL.
            /// </summary>
            /// <value>The image URL.</value>
            public string ImageUrl
            {
                get
                {
                    if (_ImageUrl == null || _ImageUrl.Length == 0)
                        return string.Empty;
                    else
                        return _ImageUrl;
                }
                set
                {
                    _ImageUrl = value;
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

                    int TempId = DataProviderManager.Provider.CreateNewMilestone(this);
                    if (TempId > 0)
                    {
                        _Id = TempId;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                   return DataProviderManager.Provider.UpdateMilestone(this);
                }
					
			}
		#endregion
		
		#region Static Methods
            /// <summary>
            /// Deletes the Milestone.
            /// </summary>
            /// <param name="original_id">The original_id.</param>
            /// <returns></returns>
            public static bool DeleteMilestone(int original_id) 
			{
                if (original_id <= Globals.NewId)
                    throw (new ArgumentOutOfRangeException("original_id"));

				

                return DataProviderManager.Provider.DeleteMilestone(original_id);	
			}


            /// <summary>
            /// Gets the Milestone by project id.
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <returns></returns>
			public static List<Milestone> GetMilestoneByProjectId(int projectId) 
			{
				if (projectId <= Globals.NewId )
					return new List<BugNET.BusinessLogicLayer.Milestone>();

				return DataProviderManager.Provider.GetMilestonesByProjectId(projectId);
			}

            /// <summary>
            /// Gets the Milestone by id.
            /// </summary>
            /// <param name="MilestoneId">The Milestone id.</param>
            /// <returns></returns>
			public static Milestone GetMilestoneById(int MilestoneId)
			{
				if (MilestoneId < Globals.NewId )
					throw (new ArgumentOutOfRangeException("MilestoneId"));

				
				return DataProviderManager.Provider.GetMilestoneById(MilestoneId);
			}

          

            /// <summary>
            /// Updates the Milestone.
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <param name="name">The name.</param>
            /// <param name="sortOrder">The sort order.</param>
            /// <param name="original_id">The original_id.</param>
            /// <returns></returns>
            public static bool UpdateMilestone(int projectId, string name, int sortOrder, int original_id)
            {
               
                Milestone v = Milestone.GetMilestoneById(original_id);

                v.Name = name;
                v.SortOrder = sortOrder;
              
                return (DataProviderManager.Provider.UpdateMilestone(v));
            }


            /// <summary>
            /// Inserts the Milestone.
            /// </summary>
            /// <param name="projectId">The project id.</param>
            /// <param name="name">The name.</param>
            /// <param name="sortOrder">The sort order.</param>
            /// <returns></returns>
            //public static bool InsertMilestone(int projectId, string name, int sortOrder, string imageUrl)
            //{
            //    Milestone v = new Milestone(projectId, name, sortOrder, imageUrl);
            //    return v.Save();
            //}

			
		#endregion
	}
}
