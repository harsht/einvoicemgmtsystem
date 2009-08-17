using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using BugNET.BusinessLogicLayer;
using BugNET.DataAccessLayer;

namespace BugNET.Install
{
    /// <summary>
    /// Upgrade helper class
    /// </summary>
    public class Upgrade
    {
        /// <summary>
        /// Migrates the users to the .NET 2.0 membership provider.
        /// </summary>   
        /// <returns>[true] if successful</returns>
        public static bool MigrateUsers()
        {
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[0].ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users", conn);
                conn.Open();
                SqlDataReader dr = command.ExecuteReader();
              
                while (dr.Read())
                {
                    MembershipUser NewUser;
                    //create new membership user 
                    if ((string)dr["UserName"] == "Admin")
                    {
                        NewUser = Membership.CreateUser((string)dr["UserName"], (string)dr["Password"], (string)dr["Email"]);
                    }
                    else
                    {
                        string password = (string)dr["Password"];
                        if (password.Length < 7)
                        {
                            password = Membership.GeneratePassword(7, 0);
                            //todo: email this password to the user.
                        }
                        NewUser = Membership.CreateUser((string)dr["UserName"], password, (string)dr["Email"]);
                    }
                 
                    if (NewUser != null)
                    {
                        if (dr["Active"].ToString() == "0")
                        {
                            NewUser.IsApproved = false;
                        }
                    }
                }
                return true;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Upgrades the database version.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public static bool UpdateDatabaseVersion(string version)
        {
            return HostSetting.UpdateHostSetting("Version", version);
        }
    }
}
