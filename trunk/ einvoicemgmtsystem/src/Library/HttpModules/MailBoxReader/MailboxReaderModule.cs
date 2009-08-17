using System;
using System.Web;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using BugNET.BusinessLogicLayer;
using log4net;
using log4net.Config;
using BugNET.POP3Reader;

namespace BugNET.HttpModules
{
    public class MailboxReaderModule : IHttpModule  
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MailboxReaderModule));
        static Timer timer;
        int interval = 10000;

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        public String ModuleName
        {
            get { return "MailboxReaderModule"; }
        }

        #region IHttpModule Members

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"></see>.
        /// </summary>
        public void Dispose()
        {
            timer = null;
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"></see> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication application)
        {
             bool ReaderEnabled = Convert.ToBoolean(HostSetting.GetHostSetting("Pop3ReaderEnabled"));
             interval = Convert.ToInt32(HostSetting.GetHostSetting("Pop3Interval"));

             if (ReaderEnabled)
             {
                 Log.Info("Enabling POP3 Reader");

                // Wire-up application events
                if (timer == null)
                    timer = new Timer(new TimerCallback(ScheduledWorkCallback),
                    application.Context, interval, interval);
             }
        }

        /// <summary>
        /// Scheduleds the work callback.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ScheduledWorkCallback(object sender) {
            //stop the timer
            timer.Change(Timeout.Infinite, Timeout.Infinite);

            HttpContext context = (HttpContext)sender;
            Poll(context);

            //start the timer 
            timer.Change(interval, interval);
        }

        /// <summary>
        /// Polls the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        private void Poll(HttpContext context)
        {
            //poll mailboxes here.
             HttpContext.Current = context;
             try
             {
                 MailboxReader mailboxReader = new MailboxReader(HostSetting.GetHostSetting("Pop3Server"), HostSetting.GetHostSetting("Pop3Username"),
                      HostSetting.GetHostSetting("Pop3Password"),
                      Convert.ToBoolean(HostSetting.GetHostSetting("Pop3InlineAttachedPictures")),
                      HostSetting.GetHostSetting("Pop3BodyTemplate"),
                      Convert.ToBoolean(HostSetting.GetHostSetting("Pop3DeleteAllMessages")),
                      HostSetting.GetHostSetting("Pop3ReportingUsername"));
                 mailboxReader.ReadMail();
             }
             catch (Exception ex)
             {
                 Log.Error("Mailbox reader failed", ex);
             }
        }

        #endregion
    
    }
}
