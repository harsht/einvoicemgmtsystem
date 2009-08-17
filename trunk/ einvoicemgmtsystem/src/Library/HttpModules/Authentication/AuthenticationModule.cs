using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.DirectoryServices;
using BugNET.BusinessLogicLayer;
using log4net;
using log4net.Config;

namespace BugNET.HttpModules
{
    /// <summary>
    /// BugNET Authentication HttpModule
    /// </summary>
    public class AuthenticationModule : IHttpModule
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuthenticationModule));
        private const string _filter = "(&(ObjectClass=Person)(SAMAccountName={0}))";
        private static string _path = String.Empty;

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName
        {
            get { return "AuthenticationModule"; }
        }

        #region IHttpModule Members

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"></see>.
        /// </summary>
        public void Dispose()
        {}

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"></see> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(context_AuthenticateRequest);
        }

        /// <summary>
        /// Handles the AuthenticateRequest event of the context control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        void context_AuthenticateRequest(object sender, EventArgs e)
        {

            // Start of changes by Stewart Moss
            // 18 Oct 2008 15:53
            //
            // Made changes to the conditions under which a new user account is created.
            // Refer to:
            // http://www.bugnetproject.com/Forums/tabid/54/forumid/9/threadid/1632
            //
            // Also improved the logging to ensure nobody is monkeying about without admin's permission.
            // Added assigning of auto roles as per 
            // http://bugnetproject.com/Forums/tabid/54/forumid/-1/threadid/1836/scope/posts/Default.aspx

            //get host settings
            bool enabled = HostSetting.GetHostSetting("UserAccountSource") == "ActiveDirectory" || HostSetting.GetHostSetting("UserAccountSource") == "WindowsSAM";

            //check if windows authentication is enabled in the host settings
            if (enabled)
            {
                // This was moved from outside "if enabled" to only happen when we need it.
                HttpRequest request = HttpContext.Current.Request;

               // not needed to be removed
               // HttpResponse response = HttpContext.Current.Response;

                if (request.IsAuthenticated)
                {
                    if ((request.LogonUserIdentity.AuthenticationType == "NTLM" || request.LogonUserIdentity.AuthenticationType == "Negotiate"))
                    {
                        //check if the user exists in the database 
                        MembershipUser user = ITUser.GetUser(HttpContext.Current.User.Identity.Name);

                        if (user == null)
                        {
                            try
                            {
                                UserProperties userprop = GetUserProperties(HttpContext.Current.User.Identity.Name);
                                //create a new user with the current identity and a random password.
                                ITUser.CreateUser(HttpContext.Current.User.Identity.Name, Membership.GeneratePassword(7, 2), userprop.Email);
                                user = ITUser.GetUser(HttpContext.Current.User.Identity.Name);
                            }
                            catch (Exception ex)
                            {
                                Log.Error(String.Format("Unable to add new user '{0}' to BugNET application. Authentication Type='{1}'.", HttpContext.Current.User.Identity.Name, request.LogonUserIdentity.AuthenticationType), ex);
                            }

                            try
                            {
                                //auto assign user to roles
                                List<Role> roles = Role.GetAllRoles();
                                foreach (Role r in roles)
                                {
                                    if (r.AutoAssign)
                                        Role.AddUserToRole(user.UserName, r.Id);
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error(String.Format("Unable to auto assign roles to user '{0}'.", user.UserName), ex);
                            }

                        }
                        else
                        {
                            //update the user's last login date.
                            user.LastLoginDate = DateTime.Now;
                            Membership.UpdateUser(user);
                        }
                    }
                    else
                    {
                        // Warning:
                        // This line may generate too many log entries!!
                        // We will have to see in practice.
                        Log.Error(String.Format("Unknown Authentication Type '{0}'.", request.LogonUserIdentity.Name));
                    }
                }
                else 
                {
                    // if ! IsAuthenticated then log
                    Log.Error(String.Format("User '{0}' is not Authenticated.", request.LogonUserIdentity.Name));
                }
            }
            // End of changes by Stewart Moss
            // 18 Oct 2008
        }

        /// <summary>
        /// Gets the users properties from the specified user store.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <returns>
        /// Class of user properties
        /// </returns>
        public UserProperties GetUserProperties(string identification)
        {
            UserProperties userprop = new UserProperties();
            userprop.FirstName = identification;
      
            // Determine which method to use to retrieve user information

            // WindowsSAM
            if (Globals.UserAccountSource == "WindowsSAM")
            {
                // Extract the machine or domain name and the user name from the
                // identification string
                string[] samPath = identification.Split(new char[] { '\\' });
                _path = String.Format("{0}{1}{3}", "WinNT://", HostSetting.GetHostSetting("ADPath"), samPath[0]);

                try
                {
                    // Find the user
                    DirectoryEntry entryRoot = new DirectoryEntry(_path);
                    DirectoryEntry userEntry = entryRoot.Children.Find(samPath[1], "user");
                    userprop.FirstName = userEntry.Properties["FullName"].Value.ToString();
                    return userprop;
                }
                catch
                {
                    return userprop;
                }
            }

            // Active Directory
            else if (Globals.UserAccountSource == "ActiveDirectory")
            {
                DirectoryEntry entry = new DirectoryEntry(String.Format("{0}{1}","GC://",HostSetting.GetHostSetting("ADPath")), HostSetting.GetHostSetting("ADUserName"), HostSetting.GetHostSetting("ADPassword"), AuthenticationTypes.Secure);

                // Setup the filter
                identification = identification.Substring(identification.LastIndexOf(@"\") + 1,
                    identification.Length - identification.LastIndexOf(@"\") - 1);
                string userNameFilter = string.Format(_filter, identification);

                // Get a Directory Searcher to the LDAPPath
                DirectorySearcher searcher = new DirectorySearcher(entry);
                if (searcher == null)
                {
                    return userprop;
                }

                // Add the propierties that need to be retrieved
                searcher.PropertiesToLoad.Add("givenName");
                searcher.PropertiesToLoad.Add("mail");
                searcher.PropertiesToLoad.Add("sn");

                // Set the filter for the search
                searcher.Filter = userNameFilter;


                try
                {
                    // Execute the search
                    SearchResult search = searcher.FindOne();

                    if (search != null)
                    {
                        userprop.FirstName = SearchResultProperty(search, "givenName"); //firstname
                        userprop.LastName = SearchResultProperty(search, "sn"); //lastname
                        userprop.Email =  SearchResultProperty(search, "mail"); //email address

                        //TODO: add new properties here to fill the profile.
                        return userprop;
                    }
                    else
                        return userprop;
                }
                catch
                {
                    return userprop;
                }
            }
            else
            {
                // The user has not choosen an UserAccountSource or UserAccountSource as None
                // Usernames will be displayed as "Domain/Username"
                return userprop;
            }
        }

        /// <summary>
        /// Searches the result property.
        /// </summary>
        /// <param name="sr">The sr.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        private static String SearchResultProperty(SearchResult sr, string field)
        {
            if (sr.Properties[field] != null)
            {
                return (String)sr.Properties[field][0];
            }

            return null;
        }

        #endregion

    }
}
