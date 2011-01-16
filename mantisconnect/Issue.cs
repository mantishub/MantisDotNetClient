//-----------------------------------------------------------------------
// <copyright file="Issue.cs" company="Microsoft Corporation">
//     Copyright (C) 2004-2011 All Rights Reserved
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

#region Copyright © 2004-2005 Victor Boctor
#endregion

using System;
using System.Text;

namespace Futureware.MantisConnect
{
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
				return null;

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
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Gets or sets the project to which this issue belongs.  Null means un-assigned yet.
        /// </summary>
        /// <value>A value greater than 0.</value>
        public ObjectRef Project
        {
            get { return project; }
            set { project = value; }
        }

        /// <summary>
        /// Gets or sets the name of category to which the issue belongs.  This field is mandatory.
        /// </summary>
        public ObjectRef Category
        {
            get { return category; }
            set { category = value; }
        }

        /// <summary>
        /// Gets or sets the summary of the issue.  This field is mandatory.
        /// </summary>
        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }

        /// <summary>
        /// Gets or sets the description of the issue.  This field is mandatory.
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Gets or sets the steps to reproduce the issue.
        /// </summary>
        public string StepsToReproduce
        {
            get { return stepsToReproduce; }
            set { stepsToReproduce = value; }
        }

        /// <summary>
        /// Gets or sets the additional information about the issue.
        /// </summary>
        public string AdditionalInformation
        {
            get { return additionalInformation; }
            set { additionalInformation = value; }
        }

        /// <summary>
        /// Gets or sets the id of user handling the issue.
        /// </summary>
        /// <value>User id >= 0</value>
        public User AssignedTo
        {
            get { return assignedTo; }
            set { assignedTo = value; }
        }

        /// <summary>
        /// Gets or sets the id of user who reported the issue.
        /// </summary>
        /// <value>User id > 0</value>
        public User ReportedBy
        {
            get { return reportedBy; }
            set { reportedBy = value; }
        }

        /// <summary>
        /// Gets or sets the product version
        /// </summary>
        public string ProductVersion
        {
            get { return productVersion; }
            set { productVersion = value; }
        }

        /// <summary>
        /// Gets or sets the product build
        /// </summary>
        public string ProductBuild
        {
            get { return productBuild; }
            set { productBuild = value; }
        }

        /// <summary>
        /// Gets or sets the Operating System
        /// </summary>
        public string Os
        {
            get { return os; }
            set { os = value; }
        }

        /// <summary>
        /// Gets or sets the Operating System Build
        /// </summary>
        public string OsBuild
        {
            get { return osBuild; }
            set { osBuild = value; }
        }

        /// <summary>
        /// Gets or sets the Platform (eg: i386)
        /// </summary>
        public string Platform
        {
            get { return platform; }
            set { platform = value; }
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
            get { return fixedInVersion; }
            set { fixedInVersion = value; }
        }

        /// <summary>
        /// Gets or sets the Total sponsorships for this issue.
        /// </summary>
        /// <remarks>
        /// This field is usually populated when information is retrieved about an issue.
        /// </remarks>
        public int SponsorshipTotal
        {
            get { return sponsorshipTotal; }
            set { sponsorshipTotal = value; }
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
            get { return reproducibility; }
            set { reproducibility = value; }
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
            get { return resolution; }
            set { resolution = value; }
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
            get { return eta; }
            set { eta = value; }
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
            get { return status; }
            set { status = value; }
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
            get { return priority; }
            set { priority = value; }
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
            get { return severity; }
            set { severity = value; }
        }

        /// <summary>
        /// Gets or sets the Projection for the scope of the work associated with resolving the issue.
        /// </summary>
        public ObjectRef Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        /// <summary>
        /// Gets or sets the view state of the issue (eg: private vs. public)
        /// </summary>
        public ObjectRef ViewState
        {
            get { return viewState; }
            set { viewState = value; }
        }

		/// <summary>
		/// Gets or sets the timestamp when issue was submitted.
		/// </summary>
		public DateTime DateSubmitted
		{
			get { return dateSubmitted; }
			set { dateSubmitted = value; }
		}

		/// <summary>
		/// Gets or sets the timestamp when issue was last updated.
		/// </summary>
		public DateTime LastUpdated
		{
			get { return lastUpdated; }
			set { lastUpdated = value; }
		}

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value></value>
		public IssueNote[] Notes
		{
			get { return notes; }
			set { notes = value; }
		}

        /// <summary>
        /// Gets or sets the relationships.
        /// </summary>
        /// <value></value>
		public IssueRelationship[] Relationships
		{
			get { return relationships; }
			set { relationships = value; }
		}

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value></value>
		public Attachment[] Attachments
		{
			get { return attachments; }
			set { attachments = value; }
		}
		
		/// <summary>
        /// Dumps the issue information to a string.
        /// </summary>
        /// <returns>String include all issue information.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Id = '{0}'\n", id);
            sb.AppendFormat("Category = '{0}'\n", category == null ? "null" : category.ToString());
            sb.AppendFormat("Summary = '{0}'\n", summary);
            sb.AppendFormat("Description = '{0}'\n", description);
            sb.AppendFormat("Product Version = '{0}'\n", this.ProductVersion);
            sb.AppendFormat("Product Build = '{0}'\n", this.ProductBuild);
            sb.AppendFormat("Steps to Reproduce = '{0}'\n", this.stepsToReproduce);
            sb.AppendFormat("Additional Info = '{0}'\n", this.AdditionalInformation);

            sb.AppendFormat("Project = {0}\n", project == null ? "null" : project.ToString());
            sb.AppendFormat("Reported By = {0}\n", ReportedBy == null ? "null" : ReportedBy.ToString());
            sb.AppendFormat("Assigned To = {0}\n", AssignedTo == null ? "null" : AssignedTo.ToString());
            sb.AppendFormat("Reproducibility = {0}\n", reproducibility == null ? "null" : reproducibility.ToString());
            sb.AppendFormat("Status = {0}\n", status == null ? "null" : status.ToString());
            sb.AppendFormat("Priority = {0}\n", priority == null ? "null" : priority.ToString());
            sb.AppendFormat("Severity = {0}\n", severity == null ? "null" : severity.ToString());
            sb.AppendFormat("Resolution = {0}\n", resolution == null ? "null" : resolution.ToString());
            sb.AppendFormat("Projection = {0}\n", projection == null ? "null" : projection.ToString());
            sb.AppendFormat("Eta = {0}\n", eta == null ? "null" : eta.ToString());
            sb.AppendFormat("View State = {0}\n", viewState == null ? "null" : viewState.ToString());

            sb.AppendFormat("Sponsorship Total = '{0}'\n", sponsorshipTotal);
            sb.AppendFormat("Fixed in Version = '{0}'\n", this.FixedInVersion);
            sb.AppendFormat("OS = '{0}'\n", this.Os);
            sb.AppendFormat("OS Build = '{0}'\n", this.OsBuild);
            sb.AppendFormat("Platform = '{0}'\n", this.Platform);

			sb.AppendFormat("Date Submitted = '{0}'\n", this.DateSubmitted);
			sb.AppendFormat("Last Updated = '{0}'\n", this.LastUpdated);

			return sb.ToString();
        }

        #region Private Members
        private int id;
        private ObjectRef project = new ObjectRef();
        private int sponsorshipTotal;
        private ObjectRef reproducibility = new ObjectRef();
        private ObjectRef status = new ObjectRef();
        private ObjectRef priority = new ObjectRef();
        private ObjectRef severity = new ObjectRef();
        private ObjectRef resolution = new ObjectRef();
        private ObjectRef projection = new ObjectRef();
        private ObjectRef eta = new ObjectRef();
        private ObjectRef viewState = new ObjectRef();
        private ObjectRef category = new ObjectRef();
        private string summary = string.Empty;
        private string description = string.Empty;
        private string stepsToReproduce = string.Empty;
        private string additionalInformation = string.Empty;
        private User assignedTo = new User();
        private User reportedBy = new User();
        private string productVersion = string.Empty;
        private string productBuild = string.Empty;
        private string fixedInVersion = string.Empty;
        private string os = string.Empty;
        private string osBuild = string.Empty;
        private string platform = string.Empty;
		private DateTime dateSubmitted = DateTime.Now;
		private DateTime lastUpdated = DateTime.Now;
		private IssueNote[] notes = new IssueNote[0];
		private IssueRelationship[] relationships = new IssueRelationship[0];
		private Attachment[] attachments = new Attachment[0];
        #endregion
    }
}