//-----------------------------------------------------------------------
// <copyright file="IssueNote.cs" company="Victor Boctor">
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
    using System.Collections;

    /// <summary>
	/// A class that manages information relating to an issue note.
	/// </summary>
    [Serializable]
    public sealed class IssueNote
	{
        /// <summary>
        /// The issue note id.
        /// </summary>
        private int id;

        /// <summary>
        /// The author of the issue note.
        /// </summary>
        private User author;

        /// <summary>
        /// The issue note text.
        /// </summary>
        private string text;

        /// <summary>
        /// The view state of the issue note (e.g. private vs. public).
        /// </summary>
        private ObjectRef viewState;

        /// <summary>
        /// The date the issue note was submitted.
        /// </summary>
        private DateTime dateSubmitted;

        /// <summary>
        /// The time stamp at which the issue note was last modified.
        /// </summary>
        private DateTime lastModified;
        
        /// <summary>
		/// Default Constructor
		/// </summary>
		public IssueNote()
		{
		}

		/// <summary>
		/// A constructs that creates an <see cref="IssueNote"/> from the corresponding 
		/// type in the webservice proxy.
		/// </summary>
		/// <param name="issueNoteData">The type defined by the webservice proxy for issue notes.</param>
		internal IssueNote(MantisConnectWebservice.IssueNoteData issueNoteData)
		{
			this.Id = Convert.ToInt32(issueNoteData.id);
			this.Author = new User(issueNoteData.reporter);
			this.Text = StringUtils.WebserviceMultilineToNative(issueNoteData.text);
			this.ViewState = new ObjectRef(issueNoteData.view_state);
			this.DateSubmitted = issueNoteData.date_submitted;
			this.LastModified = issueNoteData.last_modified;
		}

		/// <summary>
		/// Converts the this instance into the webservice issue note type.
		/// </summary>
		/// <returns>An instance of webservice issue note.</returns>
		internal MantisConnectWebservice.IssueNoteData ToWebservice()
		{
			MantisConnectWebservice.IssueNoteData note = new Futureware.MantisConnect.MantisConnectWebservice.IssueNoteData();

			note.id = Id.ToString();
			note.reporter = Author != null ? Author.ToWebservice() : null;
			note.text = StringUtils.NativeMultilineToWebservice(Text);
			note.view_state = ViewState != null ? ViewState.ToWebservice() : null;
			note.date_submitted = DateSubmitted;
			note.last_modified = LastModified;

			return note;
		}

		/// <summary>
		/// A static method that converts an array of issue notes from webservice proxy format
		/// to an array of <see cref="IssueNote"/>
		/// </summary>
		/// <param name="issueNotesData">An array of issue notes in webservice proxy format.</param>
		/// <returns>An array of <see cref="IssueNote"/>.</returns>
		internal static IssueNote[] ConvertArray(MantisConnectWebservice.IssueNoteData[] issueNotesData)
		{
            if (issueNotesData == null)
            {
                return new IssueNote[0];
            }

			IssueNote[] notes = new IssueNote[issueNotesData.Length];

            for (int i = 0; i < issueNotesData.Length; ++i)
            {
                notes[i] = new IssueNote(issueNotesData[i]);
            }

			return notes;
		}

        /// <summary>
        /// A static method that converts and array of <see cref="IssueNote"/> to an array of 
        /// issue notes in webservice proxy data type.
        /// </summary>
        /// <param name="notes">An array of issue notes.</param>
        /// <returns>An array of issue notes in webservice proxy data type.</returns>
        internal static MantisConnectWebservice.IssueNoteData[] ConvertArrayToWebservice(IssueNote[] notes)
        {
            if (notes == null)
            {
                return null;
            }

            MantisConnectWebservice.IssueNoteData[] notesForWebservice = new MantisConnectWebservice.IssueNoteData[notes.Length];

            int i = 0;

            foreach (IssueNote note in notes)
            {
                notesForWebservice[i++] = note.ToWebservice();
            }

            return notesForWebservice;
        }

		/// <summary>
		/// Gets or sets the issue note id, must be greater than or equal to 1.
		/// </summary>
		public int Id
		{
			get { return this.id; }
			set { this.id = value; }
		}

		/// <summary>
		/// Gets or sets the user who submitted the issue note.
		/// </summary>
		public User Author
		{
			get { return this.author; }
			set { this.author = value; }
		}

		/// <summary>
		/// Gets or sets the text of the issue note.
		/// </summary>
		public string Text
		{
			get { return this.text; }
			set { this.text = value; }
		}

		/// <summary>
		/// Gets or sets the view state of the issue note.  This can be private or public.
		/// </summary>
		public ObjectRef ViewState
		{
			get { return this.viewState; }
			set { this.viewState = value; }
		}

		/// <summary>
		/// Gets or sets the timestamp on which the issue note was submitted.
		/// </summary>
		public DateTime DateSubmitted
		{
			get { return this.dateSubmitted; }
			set { this.dateSubmitted = value; }
		}

		/// <summary>
		/// Gets or sets the timestamp on which the issue note was last modified.
		/// </summary>
		public DateTime LastModified
		{
			get { return this.lastModified; }
			set { this.lastModified = value; }
		}
	}
}
