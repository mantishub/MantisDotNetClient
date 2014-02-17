//-----------------------------------------------------------------------
// <copyright file="Request.cs" company="Victor Boctor">
//     Copyright (C) All Rights Reserved
// </copyright>
// <summary>
// MantisConnect is copyrighted to Victor Boctor
//
// This program is distributed under the terms and conditions of the GPL
// See LICENSE file for details.
//
// For commercial applications to link with or modify MantisConnect, they require the
// purchase of a MantisConnect commercial license.
// </summary>
//-----------------------------------------------------------------------

namespace Futureware.MantisConnect
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.ServiceModel;

    /// <summary>
    /// A wrapper around <see cref="MantisConnectWebservice"/> to provide a more friendly
    /// interface for the rest of the C# code.
    /// </summary>
    /// <remarks>
    /// Some methods will do do pre or post processing of data to convert them from the
    /// webservice format to one that is easier to access.  For example, the webservice
    /// may return information about project ids/names as a serialised string, which then
    /// gets deserialised by this wrapper into a <see cref="DataTable"/> for easier
    /// access and binding to standard controls.
    /// </remarks>
    public sealed class Request
    {
        /// <summary>
        /// Session to retrieve the user name / password of the current session
        /// from.
        /// </summary>
        private readonly Session session;

        /// <summary>
        /// Webservice auto-generated proxy.
        /// </summary>
        private MantisConnectWebservice.MantisConnectPortTypeClient mc;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="session">The session to use for all communication with the webservice.
        /// The user name and password are used from their to provide such details to the
        /// webservice with each call without exposing such detail to the user of the 
        /// library.</param>
        public Request(Session session)
        {
            this.session = session;

            BasicHttpBinding binding;

            if (this.session.NetworkCredential != null)
            {
                binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            }
            else
            {
                binding = new BasicHttpBinding();
            }

            if (this.session.Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                binding.Security.Mode = BasicHttpSecurityMode.Transport;
                binding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;
            }

            var endpoint = new EndpointAddress(this.session.Url);
            this.mc = new MantisConnectWebservice.MantisConnectPortTypeClient(binding, endpoint);

            if (this.session.NetworkCredential != null)
            {
                this.mc.ClientCredentials.UserName.UserName = this.session.NetworkCredential.UserName;
                this.mc.ClientCredentials.UserName.Password = this.session.NetworkCredential.Password;
            }
        }

        /// <summary>
        /// Add the specified issue to Mantis database.
        /// </summary>
        /// <param name="issue">The issue details.  Issue id is ignored.</param>
        /// <remarks>
        /// TODO: Consider a generic and easy way to time operations.
        /// </remarks>
        /// <returns>The id of the added issue</returns>
        public int IssueAdd(Issue issue)
        {
            ValidateIssue(issue);

            return Convert.ToInt32(this.mc.mc_issue_add(
                this.session.Username,
                this.session.Password,
                issue.ToWebservice()));
        }

        /// <summary>
        /// Update an issue
        /// </summary>
        /// <param name="issue">The issue to be updated.</param>
        /// <returns>true: updated successfully; otherwise false</returns>
        public bool IssueUpdate(Issue issue)
        {
            ValidateIssue(issue);

            if (issue.Id < 1)
            {
                throw new Exception("Can not update issue. Issue ID does not exist");
            }

            return this.mc.mc_issue_update(
                this.session.Username,
                this.session.Password,
                issue.Id.ToString(),
                issue.ToWebservice());
        }

        /// <summary>
        /// Adds a check-in comment and possibly resolves an issue.
        /// </summary>
        /// <param name="issueId">The id of the issue to add the comment to.</param>
        /// <param name="comment">The check-in comment.</param>
        /// <param name="issueFixed">true: check-in fixed the issue, false: check-in is a partial fix.</param>
        public void IssueCheckin(int issueId, string comment, bool issueFixed)
        {
            this.mc.mc_issue_checkin(
                this.session.Username,
                this.session.Password,
                issueId.ToString(),
                comment,
                issueFixed);
        }

        /// <summary>
        /// Delete the issue with the specified id
        /// </summary>
        /// <param name="issueId">Id of issue to delete</param>
        /// <exception cref="ArgumentOutOfRangeException">The issue id is invalid.</exception>
        public void IssueDelete(int issueId)
        {
			ValidateIssueId(issueId);

            this.mc.mc_issue_delete(
                this.session.Username,
                this.session.Password,
                issueId.ToString());
        }

        /// <summary>
        /// Get information related to the specified issue id.
        /// </summary>
        /// <param name="issueId">The id of the issue to retrieve information for.</param>
        /// <returns>The issue details, this does not include related information in other
        /// tables, like issue notes, ...etc.</returns>
		/// <exception cref="ArgumentOutOfRangeException">The issue id is 0 or negative.</exception>
		public Issue IssueGet(int issueId)
        {
			ValidateIssueId(issueId);

			MantisConnectWebservice.IssueData issueData = this.mc.mc_issue_get(
                this.session.Username,
                this.session.Password,
                issueId.ToString());

			return issueData == null ? null : new Issue(issueData);
        }

        /// <summary>
        /// Gets all issues that are visible to the current user independent of the current
        /// project.
        /// </summary>
        /// <param name="pageNumber">The page number (1-based)</param>
        /// <param name="issuesPerPage">The number of issues per page.</param>
        /// <returns>The array of issues.</returns>
        public Issue[] IssueGetAll(int pageNumber, int issuesPerPage)
        {
            return this.ProjectGetIssues(Project.AllProjects, pageNumber, issuesPerPage);
        }

        /// <summary>
        /// Check if there exists an issue with the specified id.
        /// </summary>
        /// <param name="issueId">Id of issue to check for.</param>
        /// <returns>true: exists, false: does not exist</returns>
		/// <exception cref="ArgumentOutOfRangeException">The issue id is 0 or negative.</exception>
		public bool IssueExists(int issueId)
        {
			ValidateIssueId(issueId);
			
			return Convert.ToBoolean(this.mc.mc_issue_exists(
                this.session.Username,
                this.session.Password,
                issueId.ToString()));
        }

        /// <summary>
        /// Search for an issue with the specified summary and return its issue id.
        /// </summary>
        /// <remarks>
        /// This is useful to allow a software which is automatically reporting issues due
        /// to exceptions or whatever reason to check first that the issue was not reported
        /// before.  And if it was, then it knows the issue id and hence is able to add
        /// a note or do whatever with this id.  Other applications may decide to delete 
        /// the issue and create a new one, basically it is up to the client application
        /// to decide how to use the returned issue id.
        /// </remarks>
        /// <param name="summary">The summary to search for.</param>
        /// <returns>0: not found, otherwise issue id</returns>
        /// <exception cref="ArgumentNullException">Summary field is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Summary field is empty or too long.</exception>
        public int IssueGetIdFromSummary(string summary)
        {
            if (string.IsNullOrEmpty(summary))
            {
                throw new ArgumentNullException("summary");
            }

            summary = summary.Trim();

            if (string.IsNullOrEmpty(summary) || summary.Length > 128)
            {
                throw new ArgumentOutOfRangeException("summary");
            }

            return Convert.ToInt32(this.mc.mc_issue_get_id_from_summary(
                this.session.Username,
                this.session.Password,
                summary));
        }

        /// <summary>
        /// Get the id of the last "reported" issue that is accessible to the logged in user.
        /// </summary>
        /// <remarks>
        /// This is useful for applications that need to know when new issues are being submitted
        /// to refresh a certain view or do something with such knowledge.
        /// </remarks>
        /// <param name="projectId">-1: default, 0: all projects, otherwise: project id</param>
        /// <returns>0: no issues accessible to logged in user, otherwise Id of the last reported issue.</returns>
		/// <exception cref="ArgumentOutOfRangeException">The project id is invalid.</exception>
		public int IssueGetLastId(int projectId)
        {
			ValidateProjectId(projectId);

			return Convert.ToInt32(this.mc.mc_issue_get_biggest_id(
                this.session.Username,
                this.session.Password,
                projectId.ToString()));
        }

        /// <summary>
        /// Get projects accessible to the currently logged in user.
        /// </summary>
        /// <remarks>
        /// This returns a table ("Projects") which includes two columns ("project_id", "name").
        /// </remarks>
        /// <returns>An array of projects.</returns>
        public Project[] UserGetAccessibleProjects()
        {
            return Project.ConvertArray(this.mc.mc_projects_get_user_accessible(
                this.session.Username,
                this.session.Password));
        }

        /// <summary>
        /// Gets the filters that are available to the current user and the specified project.
        /// </summary>
        /// <param name="projectId">0: all projects, otherwise project id</param>
        /// <returns>An array of filters.</returns>
		/// <exception cref="ArgumentOutOfRangeException">The project id is invalid.</exception>
		public Filter[] UserGetFilters(int projectId)
        {
			ValidateProjectId(projectId);

			return Filter.ConvertArray(this.mc.mc_filter_get(
                this.session.Username,
                this.session.Password,
                projectId.ToString()));
        }

        /// <summary>
        /// Gets the issues based on the specified filter, page, and number per page.
        /// </summary>
        /// <param name="projectId">The project id to apply the filter ON, or 0 for ALL projects.</param>
        /// <param name="filterId">Stored filter id to use.</param>
        /// <param name="pageNumber">Page number to get the issues for.</param>
        /// <param name="issuesPerPage">Number of issues per page.</param>
        /// <returns>An array of issues.</returns>
        public Issue[] GetIssues(int projectId, int filterId, int pageNumber, int issuesPerPage)
        {
            return Issue.ConvertArray(this.mc.mc_filter_get_issues(
                this.session.Username,
                this.session.Password,
                projectId.ToString(),
                filterId.ToString(),
                pageNumber.ToString(),
                issuesPerPage.ToString()));
        }

        /// <summary>
        /// Adds a project to the Mantis database.
        /// </summary>
        /// <param name="project">The project data, the project id is not required.</param>
        public void ProjectAdd(Project project)
        {
            this.mc.mc_project_add(
                this.session.Username,
                this.session.Password,
                project.ToWebservice());
        }

        /// <summary>
        /// Deletes a project given it's id.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <exception cref="ArgumentOutOfRangeException">Project id is invalid.</exception>
        public void ProjectDelete(int projectId)
        {
            ValidateProjectId(projectId);

            this.mc.mc_project_delete(
                this.session.Username,
                this.session.Password,
                projectId.ToString());
        }

        /// <summary>
        /// Adds a project version and sets the id on the supplied project version instance.
        /// </summary>
        /// <param name="version">The new project version details.</param>
        /// <returns>The version id of the added project version.</returns>
        public int ProjectVersionAdd(ProjectVersion version)
        {
            ValidateProjectId(version.ProjectId);

            if (version.Id != 0)
            {
                throw new ArgumentException("Version already has a version id");
            }

            if (version.Name == null)
            {
                throw new ArgumentNullException("version.Name");
            }

            if (version.Description == null)
            {
                throw new ArgumentNullException("version.Description");
            }

            version.Id = Convert.ToInt32(this.mc.mc_project_version_add(
                this.session.Username,
                this.session.Password,
                version.ToWebservice()));

            return version.Id;
        }

        /// <summary>
        /// Updates the project version data.
        /// </summary>
        /// <param name="version">The project version with the approriate id.</param>
        /// <exception cref="ArgumentOutOfRangeException">Version id or project id is invalid.</exception>
        public void ProjectVersionUpdate(ProjectVersion version)
        {
            ValidateProjectVersionId(version.Id);
            ValidateProjectId(version.ProjectId);

            if (version.Name == null)
            {
                throw new ArgumentNullException("version.Name");
            }

            if (version.Description == null)
            {
                throw new ArgumentNullException("version.Description");
            }

            this.mc.mc_project_version_update(
                this.session.Username,
                this.session.Password,
                version.Id.ToString(),
                version.ToWebservice());
        }

        /// <summary>
        /// Deletes a project version given it's id.
        /// </summary>
        /// <param name="projectVersionId">The project version id.</param>
        /// <exception cref="ArgumentOutOfRangeException">The project version id is invalid.</exception>
        public void ProjectVersionDelete(int projectVersionId)
        {
            ValidateProjectVersionId(projectVersionId);

            this.mc.mc_project_version_delete(
                this.session.Username,
                this.session.Password,
                projectVersionId.ToString());
        }

        /// <summary>
        /// Get categories defined for the project with the specified id.
        /// </summary>
		/// <remarks>
		/// TODO: Support CategoryData and Category.
		/// </remarks>
		/// <param name="projectId">project id (greater than 0)</param>
        /// <returns>An array of categories.</returns>
		/// <exception cref="ArgumentOutOfRangeException">The project id is invalid.</exception>
		public string[] ProjectGetCategories(int projectId)
        {
			ValidateProjectId(projectId);

			return this.mc.mc_project_get_categories(
                this.session.Username,
                this.session.Password,
                projectId.ToString());
        }

        /// <summary>
        /// Get versions defined for the project with the specified id.
        /// </summary>
        /// <param name="projectId">project id (greater than 0)</param>
        /// <returns>An array of project versions.</returns>
		/// <exception cref="ArgumentOutOfRangeException">The project id is invalid.</exception>
		public ProjectVersion[] ProjectGetVersions(int projectId)
        {
			ValidateProjectId(projectId);

			return ProjectVersion.ConvertArray(this.mc.mc_project_get_versions(
                this.session.Username,
                this.session.Password,
                projectId.ToString()));
        }

        /// <summary>
        /// Get released versions defined for the project with the specified id.
        /// </summary>
        /// <param name="projectId">project id (greater than 0)</param>
        /// <returns>An array of released project versions.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The project id is invalid.</exception>
        public ProjectVersion[] ProjectGetVersionsReleased(int projectId)
        {
            ValidateProjectId(projectId);

            return ProjectVersion.ConvertArray(this.mc.mc_project_get_released_versions(
                this.session.Username,
                this.session.Password,
                projectId.ToString()));
        }

        /// <summary>
        /// Get unreleased versions defined for the project with the specified id.
        /// </summary>
        /// <param name="projectId">project id (greater than 0)</param>
        /// <returns>An array of released project versions.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The project id is invalid.</exception>
        public ProjectVersion[] ProjectGetVersionsUnreleased(int projectId)
        {
            ValidateProjectId(projectId);

            return ProjectVersion.ConvertArray(this.mc.mc_project_get_unreleased_versions(
                this.session.Username,
                this.session.Password,
                projectId.ToString()));
        }

        /// <summary>
        /// Gets the issues that are visible to the logged in user and belong to the project
        /// with the specified id.
        /// </summary>
        /// <param name="projectId">The project id or <see cref="Project.AllProjects"/> or <see cref="Project.DefaultProject"/>.</param>
        /// <param name="pageNumber">The page number (1-based)</param>
        /// <param name="issuesPerPage">The number of issues per page.</param>
        /// <returns>The array of issues.</returns>
        public Issue[] ProjectGetIssues(int projectId, int pageNumber, int issuesPerPage)
        {
            ValidateProjectId(projectId);

            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException("pageNumber");
            }

            if (issuesPerPage < 1)
            {
                throw new ArgumentOutOfRangeException("issuesPerPage");
            }

            return Issue.ConvertArray(this.mc.mc_project_get_issues(
                this.session.Username,
                this.session.Password,
                projectId.ToString(),
                pageNumber.ToString(),
                issuesPerPage.ToString()));
        }

        /// <summary>
        /// Get string value of the specified configuration option.
        /// </summary>
        /// <remarks>
        /// If the caller attempts to retrieve sensitive configuration options like 
        /// passwords, database name, ...etc, an exception will be raised.
        /// 
        /// TODO: Overload this method to get more types of configurations.
        /// </remarks>
        /// <param name="config">Name of configuration option (without the $g_ part)</param>
        /// <param name="str">An output parameter to hold the value of the configuration option</param>
        /// <exception cref="ArgumentNullException">config parameter is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">config parameter is empty or blank.</exception>
        public void ConfigGet(string config, out string str)
        {
            ValidateConfigName(config);

            str = this.mc.mc_config_get_string(
                this.session.Username,
                this.session.Password,
                config);
        }

		/// <summary>
		/// Adds a note to the specified issue.
		/// </summary>
		/// <param name="issueId">Issue id to add note to.</param>
		/// <param name="note">The note to add</param>
		/// <remarks>
		/// The user must have write access to the issue and the issue must not be readonly.
		/// </remarks>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">Note is null</exception>
		/// <exception cref="ArgumentOutOfRangeException">The issue id is 0 or negative.  Or note is empty or blank.</exception>
		public int IssueNoteAdd(int issueId, IssueNote note)
		{
			ValidateIssueId(issueId);
            ValidateIssueNote(note);

			return Convert.ToInt32(this.mc.mc_issue_note_add(
                this.session.Username,
                this.session.Password,
                issueId.ToString(CultureInfo.InvariantCulture),
                note.ToWebservice()));
		}

		/// <summary>
		/// Delete the issue note with the specified id
		/// </summary>
		/// <param name="issueNoteId">Id of issue note to delete</param>
		/// <exception cref="ArgumentOutOfRangeException">The issue note id is 0 or negative.</exception>
		public void IssueNoteDelete(int issueNoteId)
		{
			ValidateIssueNoteId(issueNoteId);

			mc.mc_issue_note_delete(
                this.session.Username,
                this.session.Password,
                issueNoteId.ToString());
		}

		/// <summary>
		/// Adds an attachment to an issue.
		/// </summary>
		/// <param name="issueId">The id of the issue to associate the attachment with.</param>
		/// <param name="fileName">The file name of the attachment.  Only the file name dot extension.</param>
		/// <param name="fileType">The mime file type.</param>
		/// <param name="base64Content">A byte array that contains a base 64 encoding of the attachment.</param>
		/// <returns>The attachment id.</returns>
		public int IssueAttachmentAdd(int issueId, string fileName, string fileType, System.Byte[] base64Content)
		{
			return Convert.ToInt32(this.mc.mc_issue_attachment_add(
                this.session.Username,
                this.session.Password,
                issueId.ToString(),
                fileName,
                fileType,
                base64Content));
		}

        /// <summary>
        /// Uploads the specified file to the specified issue.
        /// </summary>
        /// <param name="issueId">The issue id</param>
        /// <param name="filePath">The file path of the file to attach.</param>
        /// <param name="fileName">
        /// The name of the file (without any path) to be associated with the uploaded file 
        /// (if null, will extract name from full path).
        /// </param>
        /// <returns>The attachment id.</returns>
        public int IssueAttachmentAdd(int issueId, string filePath, string fileName)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            return this.IssueAttachmentAdd(
                issueId,
                fileName ?? Path.GetFileName(filePath),
                MimeTypes.GetMimeType(Path.GetExtension(filePath)),
                File.ReadAllBytes(filePath));
        }

		/// <summary>
		/// Deletes an attachment given it's id.
		/// </summary>
		/// <param name="issueAttachmentId">The attachment id.</param>
		public void IssueAttachmentDelete(int issueAttachmentId)
		{
			this.mc.mc_issue_attachment_delete(
                this.session.Username,
                this.session.Password,
                issueAttachmentId.ToString());
		}

		/// <summary>
		/// Gets an attachment given it's id.
		/// </summary>
		/// <param name="issueAttachmentId">The attachment id.  This is returned when attachment is added or when attachments list is returned with the issue details.</param>
		/// <returns>The attachment contents base 64 encoded.</returns>
		public System.Byte[] IssueAttachmentGet(int issueAttachmentId)
		{
			return this.mc.mc_issue_attachment_get(
                this.session.Username,
                this.session.Password,
                issueAttachmentId.ToString());
		}

		/// <summary>
		/// Validates a project id and raises an exception if it is not valid.
		/// </summary>
		/// <param name="projectId">Project Id</param>
		/// <exception cref="ArgumentOutOfRangeException">The project id is smaller than -1.</exception>
		private static void ValidateProjectId(int projectId)
		{
            if (projectId < -1)
            {
                throw new ArgumentOutOfRangeException("projectId");
            }
		}

		/// <summary>
		/// Validates an issue id and raises an exception if it is not valid.
		/// </summary>
		/// <param name="issueId">Issue Id</param>
		/// <exception cref="ArgumentOutOfRangeException">The issue id is 0 or negative.</exception>
		private static void ValidateIssueId(int issueId)
		{
            if (issueId < 1)
            {
                throw new ArgumentOutOfRangeException("issueId");
            }
		}

        /// <summary>
        /// Validates a project version id and raises an exception if it is not valid.
        /// </summary>
        /// <param name="projectVersionId">Project Version Id</param>
        /// <exception cref="ArgumentOutOfRangeException">The project version id is 0 or negative.</exception>
        private static void ValidateProjectVersionId(int projectVersionId)
        {
            if (projectVersionId < 1)
            {
                throw new ArgumentOutOfRangeException("projectVersionId");
            }
        }

        /// <summary>
        /// Validate the issue.
        /// </summary>
        /// <param name="issue">The issue, can't be null.</param>
        /// <exception cref="ArgumentNullException">The issue is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static void ValidateIssue(Issue issue)
        {
            if (issue == null)
            {
                throw new ArgumentNullException("issue");
            }

            if (issue.Summary == null)
            {
                throw new ArgumentNullException("issue.Summary");
            }

            if (issue.Summary.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("issue.Summary");
            }

            if (issue.Description == null)
            {
                throw new ArgumentNullException("issue.Description");
            }

            if (issue.Description.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("issue.Description");
            }

            if (issue.Notes != null)
            {
                foreach (IssueNote note in issue.Notes)
                {
                    ValidateIssueNote(note);
                }
            }
        }

		/// <summary>
		/// Validates an issue note id and raises an exception if it is not valid.
		/// </summary>
		/// <param name="issueNoteId">Issue Note Id</param>
		/// <exception cref="ArgumentOutOfRangeException">The issue note id is 0 or negative.</exception>
		private static void ValidateIssueNoteId(int issueNoteId)
		{
            if (issueNoteId < 1)
            {
                throw new ArgumentOutOfRangeException("issueNoteId");
            }
		}

        /// <summary>
        /// Validates an issue note and raises an exception if it is not valid.
        /// </summary>
        /// <param name="note">The note to be validated</param>
        /// <exception cref="ArgumentNullException">The issue note is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The note is invalid.</exception>
        private static void ValidateIssueNote(IssueNote note)
        {
            if (note == null)
            {
                throw new ArgumentNullException("note");
            }

            if (note.Text == null)
            {
                throw new ArgumentNullException("note.Text");
            }

            if (note.Text.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("note");
            }
        }

        /// <summary>
        /// Validates the name of a configuration option and raises an exception if it is not valid.
        /// </summary>
        /// <param name="config">configuration option</param>
        /// <exception cref="ArgumentOutOfRangeException">The configuration option is invalid.</exception>
        private static void ValidateConfigName(string config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("configOption");
            }

            if (config.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException("configOption");
            }

            char[] invalidChars = new char[] { ' ' };
            if (config.IndexOfAny(invalidChars) != -1)
            {
                throw new ArgumentOutOfRangeException("configOption");
            }
        }
    }
}
