// According to http://msdn2.microsoft.com/en-us/library/system.web.httppostedfile.aspx
// "Files are uploaded in MIME multipart/form-data format. 
// By default, all requests, including form fields and uploaded files, 
// larger than 256 KB are buffered to disk, rather than held in server memory."
// So we can use an HttpHandler to handle uploaded files and not have to worry
// about the server recycling the request do to low memory. 
// don't forget to increase the MaxRequestLength in the web.config.
// If you server is still giving errors, then something else is wrong.
// I've uploaded a 1.3 gig file without any problems. One thing to note, 
// when the SaveAs function is called, it takes time for the server to 
// save the file. The larger the file, the longer it takes.
// So if a progress bar is used in the upload, it may read 100%, but the upload won't
// be complete until the file is saved.  So it may look like it is stalled, but it
// is not.

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using BugNET.BusinessLogicLayer;
using log4net;

namespace BugNET.UserInterfaceLayer
{
    /// <summary>
    /// Upload handler for uploading files.
    /// </summary>
    public class UploadAttachment : IHttpHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UploadAttachment));
       
        /// <summary>
        /// Initializes a new instance of the <see cref="Upload"/> class.
        /// </summary>
        public UploadAttachment()
        {
        }

        #region IHttpHandler Members

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context)
        {
            // Example of using a passed in value in the query string to set a Id
            // Now you can do anything you need to witht the file.
            int IssueId = 0;
            if (context.Request.QueryString["id"] != null)
            {
                try
                {
                    IssueId = Convert.ToInt32(context.Request.QueryString["id"]);
                }
                catch (Exception err)
                {
                    IssueId = 0;
                }
            }

            if (IssueId == 0)
                return;

            if (context.Request.Files.Count > 0)
            {
                // get the applications path
                string tempFile = context.Request.PhysicalApplicationPath;
                // loop through all the uploaded files
                for (int j = 0; j < context.Request.Files.Count; j++)
                {
                    // get the current file
                    HttpPostedFile uploadFile = context.Request.Files[j];
                    // if there was a file uploded
                    if (uploadFile.ContentLength > 0)
                    {
                        bool isFileOk = false;
                        string[] AllowedFileTypes = HostSetting.GetHostSetting("AllowedFileExtensions").Split(new char[';']);
                        string fileExt = System.IO.Path.GetExtension(uploadFile.FileName);
                        if (AllowedFileTypes.Length > 0 && AllowedFileTypes[0].CompareTo("*.*") == 0)
                        {
                            isFileOk = true;
                        }
                        else
                        {

                            foreach (string fileType in AllowedFileTypes)
                            {
                                string newfileType = fileType.Substring(fileType.LastIndexOf("."));
                                if (newfileType.CompareTo(fileExt) == 0)
                                    isFileOk = true;

                            }
                        }

                        //if the file is ok save it.
                        if (isFileOk)
                        {
                            // save the file to the upload directory
                            int projectId = Issue.GetIssueById(IssueId).ProjectId;
                            Project p = Project.GetProjectById(projectId);

                            if (p.AllowAttachments)
                            {
                                IssueAttachment attachment;

                                if (p.AttachmentStorageType == IssueAttachmentStorageType.Database)
                                {
                                    int fileSize = uploadFile.ContentLength;
                                    byte[] fileBytes = new byte[fileSize];
                                    System.IO.Stream myStream = uploadFile.InputStream;
                                    myStream.Read(fileBytes, 0, fileSize);
                                    attachment = new IssueAttachment(IssueId, context.User.Identity.Name, uploadFile.FileName, uploadFile.ContentType, fileBytes, fileSize);
                                    attachment.Save();
                                }
                                else
                                {
                                    string ProjectPath = p.UploadPath;

                                    try
                                    {
                                        if (ProjectPath.Length == 0)
                                            throw new ApplicationException(string.Format(PresentationUtils.GetErrorMessageResource("UploadPathNotDefined"), p.Name));

                                        string UploadedFileName = String.Format("{0:0000}_", IssueId) + System.IO.Path.GetFileName(uploadFile.FileName);
                                        string UploadedFilePath = context.Server.MapPath("~\\Uploads\\" + ProjectPath) + "\\" + UploadedFileName;
                                        attachment = new IssueAttachment(IssueId, context.User.Identity.Name, UploadedFileName, uploadFile.ContentType, null, uploadFile.ContentLength);
                                        if (attachment.Save())
                                        {
                                            uploadFile.SaveAs(UploadedFilePath);
                                        }

                                    }
                                    catch (DirectoryNotFoundException ex)
                                    {
                                        if (Log.IsErrorEnabled) Log.Error(string.Format(PresentationUtils.GetErrorMessageResource("UploadPathNotFound"), ProjectPath), ex);
                                        throw;
                                    }
                                    catch (Exception ex)
                                    {
                                        if (Log.IsErrorEnabled) Log.Error(ex.Message, ex);
                                        throw;
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (Log.IsErrorEnabled) Log.Error(string.Format(PresentationUtils.GetErrorMessageResource("InvalidFileType"), uploadFile.FileName));
                        }
                                                  
                    }
                }
            }
            // Used as a fix for a bug in mac flash player that makes the 
            // onComplete event not fire
            HttpContext.Current.Response.Write(" ");
        }

        #endregion
    }

}