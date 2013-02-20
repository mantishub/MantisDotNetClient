//-----------------------------------------------------------------------
// <copyright file="SubmitTask.cs" company="Victor Boctor">
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

using System;
using System.Globalization;
using System.IO;
using System.Text;

using NAnt.Core;
using NAnt.Core.Types;
using NAnt.Core.Attributes;

using Futureware.MantisConnect;

namespace NAnt.Contrib.Tasks.MantisConnect
{
    /// <summary>
    /// Used to submit issues to a Mantis Bugtracker installation.
    /// </summary>
    /// <remarks>
    /// This is useful to allow the build automatically report problems into Mantis which can then be 
    /// handled like defects.  Mantis installation may even have a special category "Build" which can
    /// automatically be assigned to a team member.
    /// 
    /// The implementation of this task is following the coding standards for NAnt rather than 
    /// MantisConnect.
    /// </remarks>
    /// <example>
    ///   <code><![CDATA[
    ///   <mantis-submit
    ///         url="http://www.example.com/mantis/mc/mantisconnect.php"
    ///         username=""         ---- this can be empty if installation supports anonymous logins
    ///         password=""         ---- same here.
    ///         summary="Build 2004-09-18: Unit test error"
    ///         description="Error details should go here."
    ///         additionalInformation="stack trace, ...etc."
    ///         category="Build"
    ///         stepsToReproduce="Checkout tag '20040918' and run the build."
    ///     />
    ///   ]]></code>
    /// </example>
    [TaskName("mantis-submit")]
    public class MantisSubmitTask : BaseTask 
    {
        #region Private Instance Fields

        private Issue issue = new Issue();

        #endregion Private Instance Fields

        #region Public Instance Properties
        
        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("project")]
        public string ProjectName
        {
            get { return issue.Project.Name; }
            set { issue.Project = new ObjectRef( value ); }
        }

        /// <summary>
        /// Issue Summary
        /// </summary>
        [StringValidator(AllowEmpty=false)]
        [TaskAttribute("summary")]
        public string Summary 
        {
            get { return issue.Summary; }
            set { issue.Summary = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [StringValidator(AllowEmpty=false)]
        [TaskAttribute("description")]
        public string Description
        {
            get { return issue.Description; }
            set { issue.Description = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [StringValidator(AllowEmpty=false)]
        [TaskAttribute("category")]
        public string Category
        {
            get { return issue.Category.Name; }
            set { issue.Category = new ObjectRef( value ); }
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("productVersion")]
        public string ProductVersion
        {
            get { return issue.ProductVersion; }
            set { issue.ProductVersion = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("productBuild")]
        public string ProductBuild
        {
            get { return issue.ProductBuild; }
            set { issue.ProductBuild = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("additionalInformation")]
        public string AdditionalInformation
        {
            get { return issue.AdditionalInformation; }
            set { issue.AdditionalInformation = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("stepsToReproduce")]
        public string StepsToReproduce
        {
            get { return issue.StepsToReproduce; }
            set { issue.StepsToReproduce = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("priority")]
        public string Priority
        {
            get { return issue.Priority.Name; }
            set { issue.Priority = new ObjectRef( value ); }
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("severity")]
        public string Severity
        {
            get { return issue.Severity.Name; }
            set { issue.Severity = new ObjectRef( value ); }
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("status")]
        public string Status
        {
            get { return issue.Status.Name; }
            set { issue.Status = new ObjectRef( value ); }
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("assignedTo")]
        public string AssignedTo
        {
            get { return issue.AssignedTo.Name; }
            set
			{
				issue.AssignedTo = new User();
				issue.AssignedTo.Name = value;
			}
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("reproducibility")]
        public string Reproducibility
        {
            get { return issue.Reproducibility.Name; }
            set { issue.Reproducibility = new ObjectRef( value ); }
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("viewState")]
        public string ViewState
        {
            get { return issue.ViewState.Name; }
            set { issue.ViewState = new ObjectRef( value ); }
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("platform")]
        public string Platform
        {
            get { return issue.Platform; }
            set { issue.Platform = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("os")]
        public string Os
        {
            get { return issue.Os; }
            set { issue.Os = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("osVersion")]
        public string OsVersion
        {
            get { return issue.OsBuild; }
            set { issue.OsBuild = value; }
        }

        #endregion Public Instance Properties
        
        #region Override implementation of Task
        
        /// <summary>
        /// Main task execution method
        /// </summary>
        protected override void ExecuteTask() 
        {
            try
            {
                Connect();

                Log( Level.Verbose, "Submitting the following issue into Mantis:\n{0}", issue.ToString() );
                
                int id = Session.Request.IssueAdd( issue );
                Log( Level.Info, "Submitted '#{0}: {1}'.", id, issue.Summary );
            } 
            catch (Exception ex) 
            {
                throw new BuildException(string.Format(CultureInfo.InvariantCulture,
                    "Failure connecting to Mantis.\n{0}", issue.ToString() ),
                    Location, ex);
            }
        }
        
        #endregion Override implementation of Task
    }
}
