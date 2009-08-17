using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using BugNET.BusinessLogicLayer;
using System.IO;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using BugNET.UserInterfaceLayer;
using BugNET.DataAccessLayer;

namespace BugNET.Install
{
    /// <summary>
    /// BugNET Install Class
    /// </summary>
    public partial class Install : System.Web.UI.Page
    {
        private DateTime StartTime;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
             //Get current Script time-out
            int scriptTimeOut = Server.ScriptTimeout;

            string mode = "";
            if(Request.QueryString["mode"] != null)
            {
                mode = Request.QueryString["mode"].ToLower();
            }

            //Disable Client side caching
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);

            //Check mode is not Nothing
            if(mode == "none")
            {
                NoUpgrade();
            }
            else
            {
                 //Set Script timeout to MAX value
                Server.ScriptTimeout = int.MaxValue;

                switch(GetUpgradeStatus())
                {
                    case Globals.UpgradeStatus.Install:
                        InstallApplication();
                        break;
                    case Globals.UpgradeStatus.Upgrade:
                        UpgradeApplication();
                        break;
                    case Globals.UpgradeStatus.None:
                        NoUpgrade();
                        break;
                }
                 
               //restore Script timeout
                Server.ScriptTimeout = scriptTimeOut;
            }

        }

        /// <summary>
        /// Gets the upgrade status.
        /// </summary>
        /// <returns></returns>
        private Globals.UpgradeStatus GetUpgradeStatus()
        {
            if (GetInstalledVersion() == string.Empty)
                return Globals.UpgradeStatus.Install;
            if(GetInstalledVersion() != GetCurrentVersion())
                return Globals.UpgradeStatus.Upgrade;

            return Globals.UpgradeStatus.None;
        }

        /// <summary>
        /// Gets the currently installed BugNET version.
        /// </summary>
        /// <returns></returns>
        private string GetInstalledVersion()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings[0].ConnectionString);
            try
            {
                string CurrentVersion;
                SqlCommand command = new SqlCommand("SELECT SettingValue FROM HostSettings WHERE SettingName='Version'", connection);

                connection.Open();
                CurrentVersion = (string)command.ExecuteScalar();
                return CurrentVersion;
            }
            catch (SqlException e)
            {
                switch (e.Number)
                {
                    case 4060:
                        WriteMessage(e.Message);
                        return "ERROR";
                }
                return string.Empty;
            }
            finally
            {
                connection.Close();
            }

          }

        /// <summary>
        /// Displayed information if no upgrade is necessary
        /// </summary>
        private void NoUpgrade()
        {
            WriteHeader("none");
            WriteMessage(string.Format("<h2>Current Database Version: {0}</h2>",GetInstalledVersion()));
            WriteMessage(string.Format("<h2>Current Assembly Version: {0}</h2>", GetCurrentVersion()));
            WriteMessage("<br/><br/><h2><a href='../Default.aspx'>Click Here To Access Your BugNET Installation</a></h2><br><br>");
        }

        /// <summary>
        /// Upgrades the application.
        /// </summary>
        private void UpgradeApplication()
        {
            string installationDate = WebConfigurationManager.AppSettings["InstallationDate"];

            if (installationDate == null || installationDate == "")
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
                //update machine key
                SystemWebSectionGroup WebSection = (SystemWebSectionGroup)config.GetSectionGroup("system.web");
                UpdateMachineKey(WebSection);
                config.AppSettings.Settings.Add("InstallationDate", DateTime.Today.ToShortDateString());
                //save
                config.Save(ConfigurationSaveMode.Full);

                Response.Redirect(HttpContext.Current.Request.RawUrl, true);
            }
            else
            {
                StartTime = DateTime.Now;
                WriteHeader("upgrade");
                WriteMessage("<h2>Upgrade Status Report</h2>");
                WriteMessage(string.Format("<h2>Upgrading To Version: {0}</h2>", GetCurrentVersion()));
                UpgradeBugNET();
                WriteMessage("<h2>Upgrade Complete</h2>");
                WriteMessage("<br><br><h2><a href='../Default.aspx'>Click Here To Access Your BugNET Installation</a></h2><br><br>");
                WriteFooter();
            }
        }
        /// <summary>
        /// Upgrades the application.
        /// </summary>
        private void UpgradeBugNET()
        {
            //get current App version
            int AssemblyVersion = Convert.ToInt32(GetCurrentVersion().Replace(".", ""));
            int DatabaseVersion = Convert.ToInt32(GetInstalledVersion().Replace(".", ""));
            //get list of script files
            string strScriptVersion;
            ArrayList arrScriptFiles = new ArrayList();

            //install the membership provider and migrate the users if the 
            //installed version is less than 0.7
            if (DatabaseVersion < 70)
            {
                WriteMessage("Installing Membership Provider...",true);
                ExecuteSqlInFile(Server.MapPath("~/Install/dbscripts/InstallCommon.sql"));
                ExecuteSqlInFile(Server.MapPath("~/Install/dbscripts/InstallMembership.sql"));
                ExecuteSqlInFile(Server.MapPath("~/Install/dbscripts/InstallProfile.sql"));
                ExecuteSqlInFile(Server.MapPath("~/Install/dbscripts/InstallRoles.sql"));
                WriteMessage("Migrating Users...", true);
                BugNET.Install.Upgrade.MigrateUsers();  
            }

            string[] arrFiles = Directory.GetFiles(Server.MapPath("~/Install/dbscripts/"), "*.SqlDataProvider.sql");
            foreach(string File in arrFiles)
            {
                //ignore default scripts
                if (Path.GetFileNameWithoutExtension(File).StartsWith("Install") || Path.GetFileNameWithoutExtension(File).StartsWith("BugNet")
                    || Path.GetFileNameWithoutExtension(File).StartsWith("Latest"))
                {}
                else
                {
                    strScriptVersion = Path.GetFileNameWithoutExtension(File).Substring(0, Path.GetFileNameWithoutExtension(File).LastIndexOf("."));
                    int ScriptVersion = Convert.ToInt32(strScriptVersion.Replace(".", ""));
                    //check if script file is relevant for upgrade
                    if (ScriptVersion > DatabaseVersion && ScriptVersion <= AssemblyVersion)
                    {
                        arrScriptFiles.Add(File);
                    }
                }
            }
            arrScriptFiles.Sort();

            foreach(string strScriptFile in arrScriptFiles)
            {
                strScriptVersion = Path.GetFileNameWithoutExtension(strScriptFile);
                //verify script has not already been run
                if (DatabaseVersion != AssemblyVersion)
                {
                    //execute script file (and version upgrades) for version
                    WriteMessage(string.Format("Running Upgrade Script: {0}", strScriptVersion), true);
                    ExecuteSqlInFile(strScriptFile);    
                }
            }
            List<Role> r = Role.GetRolesForUser("Admin");
            bool found = false;
            foreach (Role role in r)
            {
                if (role.Name.CompareTo(Globals.SuperUserRole) == 0)
                {
                    found = true;
                    break;
                }
            } 
            if(!found)
                Role.AddUserToRole("Admin", 1);

            Upgrade.UpdateDatabaseVersion(GetCurrentVersion());
        }

        /// <summary>
        /// Installs the application.
        /// </summary>
        private void InstallApplication()
        {
            string installationDate = WebConfigurationManager.AppSettings["InstallationDate"];
            string backupFolder = Globals.ConfigFolder + "Backup_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "\\";
            if (installationDate == null | installationDate == "")
            {
                //load config.
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
                string strError = "";
                try
                {
                    if (!(Directory.Exists(Server.MapPath("~") + backupFolder)))
                    {
                        Directory.CreateDirectory(Server.MapPath("~")+ backupFolder);
                    }
                    if (File.Exists(Server.MapPath("~") + "\\web.config"))
                    {
                        File.Copy(Server.MapPath("~") + "\\web.config", Server.MapPath("~") + backupFolder + "web_old.config", true);
                    }
                }
                catch(Exception ex)
                {
                    strError += ex.Message;
                }
                try
                {
                    //update machine key
                    SystemWebSectionGroup WebSection = (SystemWebSectionGroup)config.GetSectionGroup("system.web");
                    UpdateMachineKey(WebSection);
                    config.AppSettings.Settings.Add("InstallationDate", DateTime.Today.ToShortDateString());
                    //save
                    config.Save(ConfigurationSaveMode.Full);
                }
                catch (Exception ex)
                {
                    strError += ex.Message;
                }

                try
                {
                    if (File.Exists(Server.MapPath("~") + "\\web.config"))
                    {
                        File.Copy(Server.MapPath("~") + "\\web.config", Server.MapPath("~") + backupFolder + "web_old.config", true);
                    }
                }
                catch (Exception ex)
                {
                    strError += ex.Message;
                }

                if (strError == "")
                {
                    Response.Redirect(HttpContext.Current.Request.RawUrl, true);
                }
                else
                {
                    StreamReader oStreamReader = new StreamReader(HttpContext.Current.Server.MapPath("~/Install/403-3.htm"));
                    string strHTML = oStreamReader.ReadToEnd();
                    oStreamReader.Close();
                    strHTML = strHTML.Replace("[MESSAGE]", strError);
                    HttpContext.Current.Response.Write(strHTML);
                    HttpContext.Current.Response.End();
                }
            }
            else
            {
                StartTime = DateTime.Now;
                WriteHeader("install");
                WriteMessage(string.Format("<h2>Version: {0}</h2>", GetCurrentVersion()));
                WriteMessage(string.Empty);
                WriteMessage("<h2>Installation Status Report</h2>");
                InstallBugNET();
                WriteMessage("<h2>Installation Complete</h2>");
                WriteMessage("<br/><br/><h2><a href='../Default.aspx'>Click Here To Access Your BugNET Installation</a></h2><br><br>");
                Response.Flush();
            }
            WriteFooter();
        }

        #region Html Utility Functions

        /// <summary>
        /// Writes the footer.
        /// </summary>
        private void WriteFooter()
        {
            Response.Write("</body>");
            Response.Write("</html>");
            Response.Flush();
        }

        /// <summary>
        /// Writes the html header.
        /// </summary>
        /// <param name="mode">The mode.</param>
        private void WriteHeader(string mode)
        {
            //read install page and insert into response stream
            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Install/Install.htm")))
            {
                StreamReader oStreamReader;
                oStreamReader = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/Install/Install.htm"));
                string sHtml = oStreamReader.ReadToEnd();
                oStreamReader.Close();
                Response.Write(sHtml);
            }
            switch (mode)
            {
                case "install":
                    Response.Write("<h1>Installing BugNET</h1>");
                    break;
                case "upgrade":
                    Response.Write("<h1>Upgrading BugNET</h1>");
                    break;
                case "none":
                    Response.Write("<h1>Nothing To Install At This Time</h1>");
                    break;
            }
            Response.Flush();
        }

        /// <summary>
        /// Writes the message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void WriteMessage(string message)
        {
            WriteMessage(message, false);
        }
        /// <summary>
        /// Writes the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="showTime">if set to <c>true</c> [show time].</param>
        private void WriteMessage(string message,bool showTime)
        {
            if (showTime)
            {
                HttpContext.Current.Response.Write(string.Format("{0} - {1}", message, DateTime.Now.Subtract(StartTime)));
            }
            else
            {
                HttpContext.Current.Response.Write(message);
            }
            HttpContext.Current.Response.Write("<br/>");
            HttpContext.Current.Response.Flush();
        }

        /// <summary>
        /// Writes the success error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        private void WriteSuccessErrorMessage(string message, bool success)
        {
            if (success)
            {
                WriteMessage("<font color='green'>Success</font><br>");
            }
            else
            {
                WriteMessage(string.Format("<font color='red'>Error!<br/><br/>{0}<br/><br/></font>", message));
            }
        }
        #endregion 
        
        /// <summary>
        /// Gets the bug net version from the currently running assembly.
        /// </summary>
        /// <returns></returns>
        private string GetCurrentVersion()
        {
            return String.Format("{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        /// <summary>
        /// Installs the BugNET application database.
        /// </summary>
        /// <returns></returns>
        private void InstallBugNET()
        {
            WriteMessage(string.Format("Installing Version: {0}", GetCurrentVersion()));
            WriteMessage("Installing Membership Provider...", true);
            ExecuteSqlInFile(Server.MapPath("~/Install/dbscripts/InstallCommon.sql"));
            ExecuteSqlInFile(Server.MapPath("~/Install/dbscripts/InstallMembership.sql"));
            ExecuteSqlInFile(Server.MapPath("~/Install/dbscripts/InstallProfile.sql"));
            ExecuteSqlInFile(Server.MapPath("~/Install/dbscripts/InstallRoles.sql"));
            WriteMessage("Installing BugNET Database...",true);
            ExecuteSqlInFile(Server.MapPath("~/Install/dbscripts/BugNET.Schema.SqlDataProvider.sql"));
            WriteMessage("Installing BugNET Default Data...",true);
            ExecuteSqlInFile(Server.MapPath("~/Install/dbscripts/BugNET.Data.SqlDataProvider.sql"));
            WriteMessage("Creating Administrator Account...",true);
            //create admin user
            MembershipUser NewUser = Membership.CreateUser("admin","password","admin@yourdomain.com");
            if (NewUser != null)
            {
                //add the admin user to the Super Users role.
                Role.AddUserToRole("Admin", 1); 
				//add user profile information
				WebProfile Profile = new WebProfile().GetProfile("admin");
                Profile.FirstName = "admin";
                Profile.LastName = "admin";
                Profile.DisplayName = "Administrator";
                Profile.Save();
            }
            Upgrade.UpdateDatabaseVersion(GetCurrentVersion());
        }

        #region Database Functions 
        /// <summary>
        /// Executes the SQL in file.
        /// </summary>
        /// <param name="pathToScriptFile">The path to script file.</param>
        /// <returns></returns>
        private bool ExecuteSqlInFile(string pathToScriptFile)
        {
            SqlConnection connection = null;

            try
            {
                StreamReader _reader = null;

                string sql = "";

                if (false == System.IO.File.Exists(pathToScriptFile))
                {
                    throw new Exception("File " + pathToScriptFile + " does not exists");
                }
                using (Stream stream = System.IO.File.OpenRead(pathToScriptFile))
                {
                    _reader = new StreamReader(stream);
                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString);

                    SqlCommand command = new SqlCommand();

                    connection.Open();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;

                    while (null != (sql = ReadNextStatementFromStream(_reader)))
                    {
                        command.CommandText = sql;
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            WriteSuccessErrorMessage("Error in file:" + pathToScriptFile + "<br/>" + "Message:" + ex.Message, false);
                        }
                    }
                    _reader.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                WriteSuccessErrorMessage("Error in file:" + pathToScriptFile + "<br/>" + "Message:" + ex.Message, false);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Reads the next statement from stream.
        /// </summary>
        /// <param name="_reader">The _reader.</param>
        /// <returns></returns>
        private static string ReadNextStatementFromStream(StreamReader _reader)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string lineOfText;

                while (true)
                {
                    lineOfText = _reader.ReadLine();
                    if (lineOfText == null)
                    {

                        if (sb.Length > 0)
                        {
                            return sb.ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }

                    if (lineOfText.TrimEnd().ToUpper() == "GO")
                    {
                        break;
                    }

                    sb.Append(lineOfText + Environment.NewLine);
                }

                return sb.ToString();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Machine Key Functions

        /// <summary>
        /// Gens the random values.
        /// </summary>
        /// <param name="len">The len.</param>
        /// <returns></returns>
        private string GenRandomValues(int len)
        {
            byte[] buff = new byte[len / 2];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buff);
            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < buff.Length; i++)
                sb.Append(string.Format("{0:X2}", buff[i]));

            return sb.ToString();
        }

        /// <summary>
        /// Updates the machine key.
        /// Choose an appropriate key size. The recommended key lengths are as follows: 
        /// For SHA1, set the validationKey to 64 bytes (128 hexadecimal characters). 
        /// For AES, set the decryptionKey to 32 bytes (64 hexadecimal characters). 
        /// For 3DES, set the decryptionKey to 24 bytes (48 hexadecimal characters). 
        /// </summary>
        /// <param name="webSection">The web section.</param>
        /// 
        private void UpdateMachineKey(SystemWebSectionGroup webSection)
        {
            webSection.MachineKey.ValidationKey = GenRandomValues(128);
            webSection.MachineKey.DecryptionKey = GenRandomValues(64);

        }
        #endregion

    }
}
