using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using BugNET.BusinessLogicLayer;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;

namespace BugNET.Webservices
{
    /// <summary>
    /// Summary description for BugNetServices
    /// </summary>
    [WebService(Namespace = "http://bugnetproject.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class BugNetServices : LogInWebService
    {
        
        /// <summary>
        /// Creates the new issue revision.
        /// </summary>
        /// <param name="revision">The revision.</param>
        /// <param name="issueId">The issue id.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="revisionAuthor">The revision author.</param>
        /// <param name="revisionDate">The revision date.</param>
        /// <param name="revisionMessage">The revision message.</param>
        /// <returns>The new id of the revision</returns>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        [WebMethod(EnableSession = true)]
        public bool CreateNewIssueRevision(int revision, int issueId,string repository,string revisionAuthor, string revisionDate, string revisionMessage)
        {
            int projectId = Issue.GetIssueById(issueId).ProjectId;
            //authentication checks against user access to project
            if (Project.GetProjectById(projectId).AccessType == Globals.ProjectAccessType.Private && !Project.IsUserProjectMember(User.Identity.Name, projectId))
                throw new UnauthorizedAccessException(string.Format("The user {0} does not have permission to this project.",User.Identity.Name)); //TODO: Get this from resource string

            IssueRevision issueRevision = new IssueRevision(revision, issueId, revisionAuthor, revisionMessage, repository, revisionDate);
            return issueRevision.Save();
        }


        /// <summary>
        /// Changes the tree node.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <param name="newText">The new text.</param>
        /// <param name="oldText">The old text.</param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        [WebMethod]
        public void ChangeTreeNode(string nodeId, string newText, string oldText)
        {          
            if (Convert.ToInt32(nodeId) == 0)
                return;
            if (string.IsNullOrEmpty(nodeId))
                throw new ArgumentNullException("nodeId");
            if (string.IsNullOrEmpty(newText))
                throw new ArgumentNullException("newText");
            if (string.IsNullOrEmpty(oldText))
                throw new ArgumentNullException("oldText");

            Category c = Category.GetCategoryById(Convert.ToInt32(nodeId));
            if (c != null)
            {
                string UserName = Thread.CurrentPrincipal.Identity.Name;
                if (!ITUser.IsInRole(UserName, c.ProjectId, "Administrators") && !ITUser.IsInRole(UserName, 0, "Super Users"))
                    throw new UnauthorizedAccessException("Access Denied");

                c.Name = newText;
                c.Save();
            }

        }

        /// <summary>
        /// Moves the node.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <param name="oldParentId">The old parent id.</param>
        /// <param name="newParentId">The new parent id.</param>
        /// <param name="index">The index.</param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        [WebMethod]
        public void MoveNode(string nodeId, string oldParentId, string newParentId, string index)
        {          
            if (string.IsNullOrEmpty(nodeId))
                throw new ArgumentNullException("nodeId");
            if (string.IsNullOrEmpty(oldParentId))
                throw new ArgumentNullException("oldParentId");
            if (string.IsNullOrEmpty(newParentId))
                throw new ArgumentNullException("newParentId");
            if (string.IsNullOrEmpty(index))
                throw new ArgumentNullException("index");

            Category c = Category.GetCategoryById(Convert.ToInt32(nodeId));
            if (c != null)
            {
                string UserName = Thread.CurrentPrincipal.Identity.Name;

                if (!ITUser.IsInRole(UserName, c.ProjectId, "Administrators") && !ITUser.IsInRole(UserName, 0, "Super Users"))
                    throw new UnauthorizedAccessException("Access Denied");

                c.ParentCategoryId = Convert.ToInt32(newParentId);
                c.Save();
            }

        }

        /// <summary>
        /// Adds the Category.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <param name="name">The name.</param>
        /// <param name="parentCategoryId">The parent Category id.</param>
        /// <returns>Id value of the created Category</returns>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        [WebMethod(EnableSession = true)]
        public int AddCategory(string projectId, string name, string parentCategoryId)
        {  
            if (string.IsNullOrEmpty(projectId))
                throw new ArgumentNullException("projectId");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(parentCategoryId))
                throw new ArgumentNullException("parentCategoryId");

            string UserName = Thread.CurrentPrincipal.Identity.Name; 

            if (!ITUser.IsInRole(UserName, Convert.ToInt32(projectId), "Administrators") && !ITUser.IsInRole(UserName, 0, "Super Users"))
                throw new UnauthorizedAccessException("Access Denied");

            Category c = new Category(Convert.ToInt32(projectId),Convert.ToInt32(parentCategoryId), name, 0);
            c.Save();
            return c.Id;
        }

        /// <summary>
        /// Deletes the Category.
        /// </summary>
        /// <param name="CategoryId">The Category id.</param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        [WebMethod(EnableSession = true)]
        public void DeleteCategory(string CategoryId)
        {
            if (string.IsNullOrEmpty(CategoryId))
                throw new ArgumentNullException("CategoryId");

            Category c = Category.GetCategoryById(Convert.ToInt32(CategoryId));
            if (c != null)
            {
                string UserName = Thread.CurrentPrincipal.Identity.Name;

                if (!ITUser.IsInRole(UserName, c.ProjectId, "Administrators") && !ITUser.IsInRole(UserName, 0, "Super Users"))
                    throw new UnauthorizedAccessException("Access Denied");

                Category.DeleteCategory(Convert.ToInt32(CategoryId));
            }
        }

       

    }
}
