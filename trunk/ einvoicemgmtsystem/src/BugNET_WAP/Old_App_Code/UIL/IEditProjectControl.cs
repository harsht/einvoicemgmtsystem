using System;

namespace BugNET.UserInterfaceLayer
{
	/// <summary>
	/// Summary description for IEditProjectControl.
	/// </summary>
		public interface IEditProjectControl 
		{
            /// <summary>
            /// Gets or sets the project id.
            /// </summary>
            /// <value>The project id.</value>
			int ProjectId 
			{
				get;
				set;
			}

            /// <summary>
            /// Updates this instance.
            /// </summary>
            /// <returns></returns>
			bool Update();

            /// <summary>
            /// Inits this instance.
            /// </summary>
			void Initialize();

		}
}

