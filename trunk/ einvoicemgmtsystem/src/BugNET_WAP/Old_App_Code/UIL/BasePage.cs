using System;
using System.Web.UI;
using BugNET.BusinessLogicLayer;
using System.Web.Security;
using System.Web;

namespace BugNET.UserInterfaceLayer
{
	/// <summary>
	/// Summary description for BasePage.
	/// </summary>
	public class BasePage : System.Web.UI.Page 
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="T:BasePage"/> class.
        /// </summary>
		public BasePage()
		{}

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"></see> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            Page.Title = string.Format("{0} - {1}", HostSetting.GetHostSetting("ApplicationTitle"), Page.Title);        
        }

        /// <summary>
        /// Returns to previous page.
        /// </summary>
        public void ReturnToPreviousPage()
        {
            if(Session["ReferrerUrl"] != null)
                Response.Redirect((string)Session["ReferrerUrl"]);
        }

        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
        public virtual int ProjectId
        {
            get
            {
                if (ViewState["ProjectId"] == null)
                    return -1;
                else
                    return (int)ViewState["ProjectId"];
            }
            set { ViewState["ProjectId"] = value; }
        }

		/// <summary>
		/// Overrides the default OnInit to provide a security check for pages
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit (e);

            //Check for session timeouts
            //if (Context.Session != null)
            //{
            //    //check whether a new session was generated
            //    if (Session.IsNewSession)
            //    {
            //            //check whether a cookies had already been associated with this request
            //            HttpCookie sessionCookie = Request.Cookies["ASP.NET_SessionId"];
            //            if (sessionCookie != null)
            //            {
            //                    string sessionValue = sessionCookie.Value;
            //                    if (!string.IsNullOrEmpty(sessionValue))
            //                    {
            //                         // we have session timeout condition!
            //                         Response.Redirect("SessionTimeout.htm",true);
            //                    }
            //            }
            //     }
            //}

			//Security check using the following rules:
			//1. Application must allow anonymous identification (DisableAnonymousAccess HostSetting)
			//2. User must be athenticated if anonymous identification is false
			//3. Default page is not protected so the unauthenticated user may login
            if (Boolean.Parse(HostSetting.GetHostSetting("DisableAnonymousAccess")) && !User.Identity.IsAuthenticated && !Request.Url.LocalPath.EndsWith("Default.aspx"))
            {
                TransferToErrorPage();
            }
            else if(Request.QueryString["pid"] != null)
            {
                int ProjectId = Convert.ToInt16(Request.QueryString["pid"]);

                //Security check using the following rules:
                //1. Anonymous user
                //2. The project type is private
                if (!User.Identity.IsAuthenticated && Project.GetProjectById(ProjectId).AccessType == Globals.ProjectAccessType.Private)
                {
                    TransferToErrorPage();
                }
                //Security check using the following rules:
                //1. Authenticated user
                //2. The project type is private 
                //3. The user is not a project member
                else if (User.Identity.IsAuthenticated && Project.GetProjectById(ProjectId).AccessType == Globals.ProjectAccessType.Private && !Project.IsUserProjectMember(User.Identity.Name, ProjectId) )
                {
                    TransferToErrorPage();
                }
            }    

		}

        /// <summary>
        /// Transfers to error page.
        /// </summary>
        private void TransferToErrorPage()
        {
            Response.Redirect("~/Errors/AccessDenied.aspx", true);
        }


	}

}
