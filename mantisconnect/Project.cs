//-----------------------------------------------------------------------
// <copyright file="Project.cs" company="Victor Boctor">
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
using System.Collections.Generic;
namespace Futureware.MantisConnect
{
    using System;

    /// <summary>
	/// A class that captures information relating to a project.
	/// </summary>
    [Serializable]
    public sealed class Project
	{
        /// <summary>
        /// The project id.
        /// </summary>
        private int id;

        /// <summary>
        /// The project name.
        /// </summary>
        private string name;

        /// <summary>
        /// The project development status.
        /// </summary>
        private ObjectRef status;

        /// <summary>
        /// A flag indicating whether the project is enabled or not.
        /// </summary>
        private bool enabled;

        /// <summary>
        /// The project view state (e.g. private vs. public).
        /// </summary>
        private ObjectRef viewState;

        /// <summary>
        /// The minimum access level to access the project.
        /// </summary>
        private ObjectRef accessMin;

        /// <summary>
        /// The upload file path.
        /// </summary>
        private string filePath;

        /// <summary>
        /// The project description.
        /// </summary>
        private string description;

        /// <summary>
        /// The subprojects
        /// </summary>
        private List<Project> subprojects = new List<Project>();

        /// <summary>
        /// The Id to be used when calling APIs to indicate the default project for the logged
        /// in user.
        /// </summary>
        public const int DefaultProject = -1;

        /// <summary>
        /// The id to be used when calling APIs to indicate all projects.
        /// </summary>
        public const int AllProjects = 0;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Project()
		{
		}

		/// <summary>
		/// Constructor to create a project from the web service project data.
		/// </summary>
		/// <param name="projectData">An instance returned by the webservice.</param>
		internal Project(MantisConnectWebservice.ProjectData projectData)
		{
			this.Id = Convert.ToInt32(projectData.id);
			this.Name = projectData.name;
			this.Status = new ObjectRef(projectData.status);
			this.Enabled = projectData.enabled;
			this.ViewState = new ObjectRef(projectData.view_state);
			this.AccessMin = new ObjectRef(projectData.access_min);
			this.FilePath = projectData.file_path;
			this.Description = projectData.description;
            for (int i = 0; i < projectData.subprojects.Length; ++i)
            {
                this.subprojects.Add( new Project(projectData.subprojects[i]));
            }
            
		}

        /// <summary>
        /// Convert this instance to the type supported by the webservice proxy.
        /// </summary>
        /// <returns>A copy of this instance in the webservice proxy type.</returns>
		internal MantisConnectWebservice.ProjectData ToWebservice()
		{
			MantisConnectWebservice.ProjectData projectData = new MantisConnectWebservice.ProjectData();

            projectData.id = this.Id.ToString();
            projectData.name = this.Name;
            projectData.description = this.Description;
            projectData.enabled = this.Enabled;
            projectData.enabledSpecified = true;
            projectData.file_path = this.FilePath;
            projectData.view_state = this.ViewState.ToWebservice();
            projectData.status = this.Status.ToWebservice();
            
            // TODO: At the moment this is not used by project_add() or Mantis?!
            projectData.access_min = new MantisConnectWebservice.ObjectRef();
            projectData.access_min.id = "0";

            return projectData;
		}

        /// <summary>
        /// Converts an array of projects from webservice type to this type.
        /// </summary>
        /// <param name="projectData">Project data.</param>
        /// <returns>An array of projects in this class type.</returns>
		internal static Project[] ConvertArray( MantisConnectWebservice.ProjectData[] projectData )
		{
            if (projectData == null)
            {
                return null;
            }
            
			Project[] projects = new Project[projectData.Length];
            for (int i = 0; i < projectData.Length; ++i)
            {
                projects[i] = new Project(projectData[i]);
            }

			return projects;
		}

        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>Greater than or equal to 1.</value>
		public int Id
		{
			get { return this.id; }
			set { this.id = value; }
		}

        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        /// <value>Must not be empty or null.</value>
        public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

        /// <summary>
        /// Gets or sets the project status.
        /// </summary>
        /// <value>For example, in development, released, etc.</value>
        public ObjectRef Status
		{
			get { return this.status; }
			set { this.status = value; }
		}

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Project"/> is enabled.
        /// </summary>
        /// <value>
        /// 	<see langword="true"/> if enabled; otherwise, <see langword="false"/>.
        /// </value>
		public bool Enabled
		{
			get { return this.enabled; }
			set { this.enabled = value; }
		}

        /// <summary>
        /// Gets or sets the whether the project is private or public.
        /// </summary>
        /// <value>Private / Public.</value>
        public ObjectRef ViewState
		{
			get { return this.viewState; }
			set { this.viewState = value; }
		}

        /// <summary>
        /// Gets or sets the access min.
        /// </summary>
        /// <value>TODO: include more details.</value>
        public ObjectRef AccessMin
		{
			get { return this.accessMin; }
			set { this.accessMin = value; }
		}

        /// <summary>
        /// Gets or sets the file path for uploaded files.
        /// </summary>
        /// <value>
        /// The path of the folder (on the server) in which the attachments are stored.
        /// The path may be relative to Mantis folder on the server.
        /// </value>
		public string FilePath
		{
			get { return this.filePath; }
			set { this.filePath = value; }
		}

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>Can be empty or null.</value>
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

        public List<Project> Subprojects
        {
            get { return this.subprojects; }
            set { this.subprojects = value; }
        }
	}
}
