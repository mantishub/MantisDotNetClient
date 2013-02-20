//-----------------------------------------------------------------------
// <copyright file="Issue.cs" company="Victor Boctor">
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
    using System.Text;

    /// <summary>
    /// A class to manage information relating to an issue.
    /// </summary>
    /// <remarks>
    /// This class does not contain information like issue notes, attachments, ...etc
    /// which is stored in different tables in Mantis database.
    /// </remarks>
    [Serializable]
    public sealed class Issue
    {
        /// <summary>
        /// The issue id.
        /// </summary>
        private int id;

        /// <summary>
        /// The project reference.
        /// </summary>
        private ObjectRef project = new ObjectRef();

        /// <summary>
        /// The total of all sponsorships on this issue.
        /// </summary>
        private int sponsorshipTotal;

        /// <summary>
        /// The reproducibility of the issue.
        /// </summary>
        private ObjectRef reproducibility = new ObjectRef();

        /// <summary>
        /// The status of the issue.
        /// </summary>
        private ObjectRef status = new ObjectRef();

        /// <summary>
        /// The priority of the issue.
        /// </summary>
        private ObjectRef priority = new ObjectRef();

        /// <summary>
        /// The severity of the issue.
        /// </summary>
        private ObjectRef severity = new ObjectRef();

        /// <summary>
        /// The resolution of the issue.
        /// </summary>
        private ObjectRef resolution = new ObjectRef();

        /// <summary>
        /// The projection of the issue.
        /// </summary>
        private ObjectRef projection = new ObjectRef();

        /// <summary>
        /// The eta of the issue.
        /// </summary>
        private ObjectRef eta = new ObjectRef();

        /// <summary>
        /// The view state of the issue.
        /// </summary>
        private ObjectRef viewState = new ObjectRef();

        /// <summary>
        /// The category of the issue.
        /// </summary>
        private ObjectRef category = new ObjectRef();

        /// <summary>
        /// The summary of the issue.
        /// </summary>
        private string summary = string.Empty;

        /// <summary>
        /// The description of the issue.
        /// </summary>
        private string description = string.Empty;

        /// <summary>
        /// The steps to reproduce for the issue.
        /// </summary>
        private string stepsToReproduce = string.Empty;

        /// <summary>
        /// The additional information for the issue.
        /// </summary>
        private string additionalInformation = string.Empty;

        /// <summary>
        /// The person to which the issue is assigned.
        /// </summary>
        private User assignedTo = new User();

        /// <summary>
        /// The person who reported the issue.
        /// </summary>
        private User reportedBy = new User();

        /// <summary>
        /// The product version on which the issue was found.
        /// </summary>
        private string productVersion = string.Empty;

        /// <summary>
        /// The product build on which the issue was found.
        /// </summary>
        private string productBuild = string.Empty;

        /// <summary>
        /// The product version in which the issue was resolved.
        /// </summary>
        private string fixedInVersion = string.Empty;

        /// <summary>
        /// The OS on which the issue is found.
        /// </summary>
        private string os = string.Empty;

        /// <summary>
        /// The OS version on which the issue was found.
        /// </summary>
        private string osBuild = string.Empty;

        /// <summary>
        /// The platform on which the issue was found.
        /// </summary>
        private string platform = string.Empty;

        /// <summary>
        /// The submission timestamp of the issue.
        /// </summary>
        private DateTime dateSubmitted = DateTime.Now;

        /// <summary>
        /// The last updated timestamp of the issue.
        /// </summary>
        private DateTime lastUpdated = DateTime.Now;

        /// <summary>
        /// The notes associated with the issue.
        /// </summary>
        private IssueNote[] notes = new IssueNote[0];

        /// <summary>
        /// The relationships associated with the issue.
        /// </summary>
        private IssueRelationship[] relationships = new IssueRelationship[0];

        /// <summary>
        /// The attachments associated with the issue.
        /// </summary>
        private Attachment[] attachments = new Attachment[0];

        /// <summary>
        /// Initializes a new instance of the <see cref="Issue"/> class.
        /// </summary>
        public Issue()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Issue"/> class.
        /// </summary>
        /// <param name="issueData">Issue data from Webservice proxy.</param>
		internal Issue(MantisConnectWebservice.IssueData issueData)
		{
			this.Id = Convert.ToInt32(issueData.id);
			this.Project = new ObjectRef(issueData.project);
			this.Category = new ObjectRef(issueData.category);
			this.Summary = issueData.summary;
			this.Description = StringUtils.WebserviceMultilineToNative(issueData.description);
			this.StepsToReproduce = issueData.steps_to_reproduce == null ? String.Empty : StringUtils.WebserviceMultilineToNative(issueData.steps_to_reproduce);
			this.AdditionalInformation = issueData.additional_information == null ? String.Empty : StringUtils.WebserviceMultilineToNative(issueData.additional_information);
			this.AssignedTo = issueData.handler == null ? null : new User(issueData.handler);
			this.ReportedBy = new User(issueData.reporter);
			this.ProductVersion = issueData.version == null ? String.Empty : issueData.version;
			this.ProductBuild = issueData.build == null ? String.Empty : issueData.build;
			this.Os = issueData.os == null ? String.Empty : issueData.os;
			this.OsBuild = issueData.os_build == null ? String.Empty : issueData.os_build;
			this.Platform = issueData.platform == null ? String.Empty : issueData.platform;
			this.FixedInVersion = issueData.fixed_in_version == null ? String.Empty : issueData.fixed_in_version;
			this.SponsorshipTotal = Convert.ToInt32(issueData.sponsorship_total);
			this.Reproducibility = new ObjectRef(issueData.reproducibility);
			this.Resolution = new ObjectRef(issueData.resolution);
			this.Eta = new ObjectRef(issueData.eta);
			this.Status = new ObjectRef(issueData.status);
			this.Priority = new ObjectRef(issueData.priority);
			this.Severity = new ObjectRef(issueData.severity);
			this.Projection = new ObjectRef(issueData.projection);
			this.ViewState = new ObjectRef(issueData.view_state);

			this.Notes = IssueNote.ConvertArray(issueData.notes);
			this.Attachments = Attachment.ConvertArray(issueData.attachments);
			this.Relationships = IssueRelationship.ConvertArray(issueData.relationships);
		}

        /// <summary>
        /// Convert this instance to the type supported by the webservice proxy.
        /// </summary>
        /// <returns>A copy of this instance in the webservice proxy type.</returns>
		internal MantisConnectWebservice.IssueData ToWebservice()
		{
			MantisConnectWebservice.IssueData issueData = new MantisConnectWebservice.IssueData();

			issueData.id = this.Id.ToString();
			issueData.category = this.Category.Name;
			issueData.summary = this.Summary;
			issueData.description = StringUtils.NativeMultilineToWebservice(this.Description);
			issueData.additional_information = StringUtils.NativeMultilineToWebservice(this.AdditionalInformation);
			issueData.steps_to_reproduce = StringUtils.NativeMultilineToWebservice(this.StepsToReproduce);
			issueData.build = this.ProductBuild;
			issueData.version = this.ProductVersion;
			issueData.os = this.Os;
			issueData.os_build = this.OsBuild;
			issueData.platform = this.Platform;
			issueData.sponsorship_total = this.SponsorshipTotal.ToString();
			issueData.fixed_in_version = this.FixedInVersion;
			issueData.view_state = this.ViewState.ToWebservice();
			issueData.projection = this.Projection.ToWebservice();
			issueData.eta = this.Eta.ToWebservice();
			issueData.priority = this.Priority.ToWebservice();
			issueData.severity = this.Severity.ToWebservice();
			issueData.project = this.Project.ToWebservice();
			issueData.reproducibility = this.Reproducibility.ToWebservice();
			issueData.resolution = this.Resolution.ToWebservice();
			issueData.status = this.Status.ToWebservice();
			issueData.reporter = this.ReportedBy.ToWebservice();
			issueData.handler = this.AssignedTo == null ? null : this.AssignedTo.ToWebservice();
			issueData.date_submitted = this.dateSubmitted;
			issueData.last_updated = this.lastUpdated;

			issueData.notes = IssueNote.ConvertArrayToWebservice(this.Notes);

			// TODO: Attachments
			// TODO: Relationships

			return issueData;
		}

        /// <summary>
        /// Converts an array of instances from webservice proxy type to this type.
        /// </summary>
        /// <param name="issuesData">Issues data in webservice proxy type.</param>
        /// <returns>The array converted to this type.</returns>
		internal static Issue[] ConvertArray(MantisConnectWebservice.IssueData[] issuesData)
		{
            if (issuesData == null)
            {
                return null;
            }

			Issue[] issues = new Issue[issuesData.Length];

            for (int i = 0; i < issuesData.Length; ++i)
            {
                issues[i] = new Issue(issuesData[i]);
            }

			return issues;
		}

        /// <summary>
        /// Gets or sets the issue id.
        /// </summary>
        /// <value>A value greater than or equal to 0.</value>
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// Gets or sets the project to which this issue belongs.  Null means un-assigned yet.
        /// </summary>
        /// <value>A value greater than 0.</value>
        public ObjectRef Project
        {
            get { return this.project; }
            set { this.project = value; }
        }

        /// <summary>
        /// Gets or sets the name of category to which the issue belongs.  This field is mandatory.
        /// </summary>
        public ObjectRef Category
        {
            get { return this.category; }
            set { this.category = value; }
        }

        /// <summary>
        /// Gets or sets the summary of the issue.  This field is mandatory.
        /// </summary>
        public string Summary
        {
            get { return this.summary; }
            set { this.summary = value; }
        }

        /// <summary>
        /// Gets or sets the description of the issue.  This field is mandatory.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// Gets or sets the steps to reproduce the issue.
        /// </summary>
        public string StepsToReproduce
        {
            get { return this.stepsToReproduce; }
            set { this.stepsToReproduce = value; }
        }

        /// <summary>
        /// Gets or sets the additional information about the issue.
        /// </summary>
        public string AdditionalInformation
        {
            get { return this.additionalInformation; }
            set { this.additionalInformation = value; }
        }

        /// <summary>
        /// Gets or sets the id of user handling the issue.
        /// </summary>
        /// <value>User id >= 0</value>
        public User AssignedTo
        {
            get { return this.assignedTo; }
            set { this.assignedTo = value; }
        }

        /// <summary>
        /// Gets or sets the id of user who reported the issue.
        /// </summary>
        /// <value>User id > 0</value>
        public User ReportedBy
        {
            get { return this.reportedBy; }
            set { this.reportedBy = value; }
        }

        /// <summary>
        /// Gets or sets the product version
        /// </summary>
        public string ProductVersion
        {
            get { return this.productVersion; }
            set { this.productVersion = value; }
        }

        /// <summary>
        /// Gets or sets the product build
        /// </summary>
        public string ProductBuild
        {
            get { return this.productBuild; }
            set { this.productBuild = value; }
        }

        /// <summary>
        /// Gets or sets the Operating System
        /// </summary>
        public string Os
        {
            get { return this.os; }
            set { this.os = value; }
        }

        /// <summary>
        /// Gets or sets the Operating System Build
        /// </summary>
        public string OsBuild
        {
            get { return this.osBuild; }
            set { this.osBuild = value; }
        }

        /// <summary>
        /// Gets or sets the Platform (eg: i386)
        /// </summary>
        public string Platform
        {
            get { return this.platform; }
            set { this.platform = value; }
        }

        /// <summary>
        /// Gets or sets the fixed in version field.
        /// </summary>
        /// <value>
        /// Has to be the name of a version that is defined for the project to
        /// which this issue is being added.
        /// </value>
        public string FixedInVersion
        {
            get { return this.fixedInVersion; }
            set { this.fixedInVersion = value; }
        }

        /// <summary>
        /// Gets or sets the Total sponsorships for this issue.
        /// </summary>
        /// <remarks>
        /// This field is usually populated when information is retrieved about an issue.
        /// </remarks>
        public int SponsorshipTotal
        {
            get { return this.sponsorshipTotal; }
            set { this.sponsorshipTotal = value; }
        }

        /// <summary>
        /// Gets or sets the Issue Reproducibility (eg: always, random, not tried, N/A, ...etc)
        /// </summary>
        /// <remarks>
        /// This matches the ids defined in the reproducibility enumeration in Mantis configuration
        /// file.
        /// </remarks>
        public ObjectRef Reproducibility
        {
            get { return this.reproducibility; }
            set { this.reproducibility = value; }
        }

        /// <summary>
        /// Gets or sets the Issue Resolution (eg: fixed, duplicate, not an issue)
        /// </summary>
        /// <remarks>
        /// This matches the ids defined in the resolution enumeration in Mantis configuration
        /// file.
        /// </remarks>
        public ObjectRef Resolution
        {
            get { return this.resolution; }
            set { this.resolution = value; }
        }

        /// <summary>
        /// Gets or sets the Estimated Time of Arrival
        /// </summary>
        /// <remarks>
        /// This matches the ids defined in the eta enumeration in Mantis configuration
        /// file.
        /// </remarks>
        public ObjectRef Eta
        {
            get { return this.eta; }
            set { this.eta = value; }
        }

        /// <summary>
        /// Gets or sets the issue status
        /// </summary>
        /// <remarks>
        /// This matches the ids defined in the status enumeration in Mantis configuration
        /// file.
        /// </remarks>
        public ObjectRef Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        /// <summary>
        /// Gets or sets the Issue Priority
        /// </summary>
        /// <remarks>
        /// This matches the ids defined in the priority enumeration in Mantis configuration
        /// file.
        /// </remarks>
        public ObjectRef Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }

        /// <summary>
        /// Gets or sets the Issue Severity
        /// </summary>
        /// <remarks>
        /// This matches the ids defined in the severity enumeration in Mantis configuration
        /// file.
        /// </remarks>
        public ObjectRef Severity
        {
            get { return this.severity; }
            set { this.severity = value; }
        }

        /// <summary>
        /// Gets or sets the Projection for the scope of the work associated with resolving the issue.
        /// </summary>
        public ObjectRef Projection
        {
            get { return this.projection; }
            set { this.projection = value; }
        }

        /// <summary>
        /// Gets or sets the view state of the issue (eg: private vs. public)
        /// </summary>
        public ObjectRef ViewState
        {
            get { return this.viewState; }
            set { this.viewState = value; }
        }

		/// <summary>
		/// Gets or sets the timestamp when issue was submitted.
		/// </summary>
		public DateTime DateSubmitted
		{
			get { return this.dateSubmitted; }
			set { this.dateSubmitted = value; }
		}

		/// <summary>
		/// Gets or sets the timestamp when issue was last updated.
		/// </summary>
		public DateTime LastUpdated
		{
			get { return this.lastUpdated; }
			set { this.lastUpdated = value; }
		}

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
		public IssueNote[] Notes
		{
			get { return this.notes; }
			set { this.notes = value; }
		}

        /// <summary>
        /// Gets or sets the relationships.
        /// </summary>
		public IssueRelationship[] Relationships
		{
			get { return this.relationships; }
			set { this.relationships = value; }
		}

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
		public Attachment[] Attachments
		{
			get { return this.attachments; }
			set { this.attachments = value; }
		}
		
		/// <summary>
        /// Dumps the issue information to a string.
        /// </summary>
        /// <returns>String include all issue information.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Id = '{0}'\n", this.id);
            sb.AppendFormat("Category = '{0}'\n", this.category == null ? "null" : this.category.ToString());
            sb.AppendFormat("Summary = '{0}'\n", this.summary);
            sb.AppendFormat("Description = '{0}'\n", this.description);
            sb.AppendFormat("Product Version = '{0}'\n", this.ProductVersion);
            sb.AppendFormat("Product Build = '{0}'\n", this.ProductBuild);
            sb.AppendFormat("Steps to Reproduce = '{0}'\n", this.stepsToReproduce);
            sb.AppendFormat("Additional Info = '{0}'\n", this.AdditionalInformation);

            sb.AppendFormat("Project = {0}\n", this.project == null ? "null" : this.project.ToString());
            sb.AppendFormat("Reported By = {0}\n", this.ReportedBy == null ? "null" : this.ReportedBy.ToString());
            sb.AppendFormat("Assigned To = {0}\n", this.AssignedTo == null ? "null" : this.AssignedTo.ToString());
            sb.AppendFormat("Reproducibility = {0}\n", this.reproducibility == null ? "null" : this.reproducibility.ToString());
            sb.AppendFormat("Status = {0}\n", this.status == null ? "null" : this.status.ToString());
            sb.AppendFormat("Priority = {0}\n", this.priority == null ? "null" : this.priority.ToString());
            sb.AppendFormat("Severity = {0}\n", this.severity == null ? "null" : this.severity.ToString());
            sb.AppendFormat("Resolution = {0}\n", this.resolution == null ? "null" : this.resolution.ToString());
            sb.AppendFormat("Projection = {0}\n", this.projection == null ? "null" : this.projection.ToString());
            sb.AppendFormat("Eta = {0}\n", this.eta == null ? "null" : this.eta.ToString());
            sb.AppendFormat("View State = {0}\n", this.viewState == null ? "null" : this.viewState.ToString());

            sb.AppendFormat("Sponsorship Total = '{0}'\n", this.sponsorshipTotal);
            sb.AppendFormat("Fixed in Version = '{0}'\n", this.FixedInVersion);
            sb.AppendFormat("OS = '{0}'\n", this.Os);
            sb.AppendFormat("OS Build = '{0}'\n", this.OsBuild);
            sb.AppendFormat("Platform = '{0}'\n", this.Platform);

			sb.AppendFormat("Date Submitted = '{0}'\n", this.DateSubmitted);
			sb.AppendFormat("Last Updated = '{0}'\n", this.LastUpdated);

			return sb.ToString();
        }
    }
}