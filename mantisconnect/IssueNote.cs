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

using System;
using System.Collections;

namespace Futureware.MantisConnect
{
	/// <summary>
	/// A class that manages information relating to an issue note.
	/// </summary>
    [Serializable]
    public sealed class IssueNote
	{
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
		internal IssueNote( MantisConnectWebservice.IssueNoteData issueNoteData )
		{
			this.Id = Convert.ToInt32( issueNoteData.id );
			this.Author = new User( issueNoteData.reporter );
			this.Text = StringUtils.WebserviceMultilineToNative( issueNoteData.text );
			this.ViewState = new ObjectRef( issueNoteData.view_state );
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
			note.text = StringUtils.NativeMultilineToWebservice( Text );
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
		internal static IssueNote[] ConvertArray( MantisConnectWebservice.IssueNoteData[] issueNotesData )
		{
			if ( issueNotesData == null )
				return new IssueNote[0];

			IssueNote[] notes = new IssueNote[issueNotesData.Length];

			for ( int i = 0; i < issueNotesData.Length; ++i )
				notes[i] = new IssueNote( issueNotesData[i] );

			return notes;
		}

        /// <summary>
        /// A static method that converts and array of <see cref="IssueNote"/> to an array of 
        /// issue notes in webservice proxy data type.
        /// </summary>
        /// <param name="notes">An array of issue notes.</param>
        /// <returns>An array of issue notes in webservice proxy data type.</returns>
        internal static MantisConnectWebservice.IssueNoteData[] ConvertArrayToWebservice( IssueNote[] notes )
        {
            if ( notes == null )
                return null;

            MantisConnectWebservice.IssueNoteData[] notesForWebservice = new MantisConnectWebservice.IssueNoteData[ notes.Length ];

            int i = 0;
            foreach( IssueNote note in notes )
                notesForWebservice[i++] = note.ToWebservice();

            return notesForWebservice;
        }

		/// <summary>
		/// An issue note id, must be greater than or equal to 1.
		/// </summary>
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		/// <summary>
		/// The user who submitted the issue note.
		/// </summary>
		public User Author
		{
			get { return author; }
			set { author = value; }
		}

		/// <summary>
		/// The text of the issue note.
		/// </summary>
		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		/// <summary>
		/// The view state of the issue note.  This can be private or public.
		/// </summary>
		public ObjectRef ViewState
		{
			get { return viewState; }
			set { viewState = value; }
		}

		/// <summary>
		/// The timestamp on which the issue note was submitted.
		/// </summary>
		public DateTime DateSubmitted
		{
			get { return dateSubmitted; }
			set { dateSubmitted = value; }
		}

		/// <summary>
		/// The timestamp on which the issue note was last modified.
		/// </summary>
		public DateTime LastModified
		{
			get { return lastModified; }
			set { lastModified = value; }
		}

		#region Private Members
		private int id;
		private User author;
		private string text;
		private ObjectRef viewState;
		private DateTime dateSubmitted;
		private DateTime lastModified;
		#endregion
	}
}
