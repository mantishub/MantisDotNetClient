//-----------------------------------------------------------------------
// <copyright file="Attachment.cs" company="Victor Boctor">
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

    /// <summary>
	/// A class that manages the information about an attachment.  The class does not manage
	/// the attachment itself.
	/// </summary>
	[Serializable]
	public class Attachment
	{
        /// <summary>
        /// The attachment id.
        /// </summary>
        private readonly int id;

        /// <summary>
        /// The attachment file name.
        /// </summary>
        private readonly string fileName;

        /// <summary>
        /// The attachment size in bytes.
        /// </summary>
        private readonly int size;

        /// <summary>
        /// The attachment mime type.
        /// </summary>
        private readonly string contentType;

        /// <summary>
        /// The timestamp the attachment was submitted.
        /// </summary>
        private readonly DateTime dateSubmitted;

        /// <summary>
        /// The download url.
        /// </summary>
        private readonly string downloadUrl;

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="attachmentData">The webservice type that captures attachment information.</param>
		internal Attachment(MantisConnectWebservice.AttachmentData attachmentData)
		{
			this.id = Convert.ToInt32(attachmentData.id);
			this.fileName = attachmentData.filename;
			this.size = Convert.ToInt32(attachmentData.size);
			this.contentType = attachmentData.content_type;
			this.dateSubmitted = attachmentData.date_submitted;
			this.downloadUrl = attachmentData.download_url;
		}

		/// <summary>
		/// A static method that converts an array of attachments from the webservice type
		/// to this instances of this class.
		/// </summary>
		/// <param name="attachmentData">An array of attachments in webservice data type</param>
		/// <returns>Array of Attachment instances.</returns>
		internal static Attachment[] ConvertArray(MantisConnectWebservice.AttachmentData[] attachmentData)
		{
			if (attachmentData == null)
			{
				return new Attachment[0];
			}

			Attachment[] attachments = new Attachment[attachmentData.Length];

            for (int i = 0; i < attachmentData.Length; ++i)
            {
                attachments[i] = new Attachment(attachmentData[i]);
            }

			return attachments;
		}

		/// <summary>
		/// Gets the attachment id, must be greater than or equal to 1.
		/// </summary>
		public int Id
		{
			get { return this.id; }
		}

		/// <summary>
		/// Gets the filename of the attachment.
		/// </summary>
		/// <remarks>
		/// This is the original name of the file that was uploaded by the user.  It is not 
		/// the name of the attachment file on the server.
		/// </remarks>
		public string FileName
		{
			get { return this.fileName; }
		}

		/// <summary>
		/// Gets the file size in bytes.
		/// </summary>
		public int Size
		{
			get { return this.size; }
		}

		/// <summary>
		/// Gets the MIME content type.
		/// </summary>
		public string ContentType
		{
			get { return this.contentType; }
		}

		/// <summary>
		/// Gets the date where the attachment was uploaded to Mantis.
		/// </summary>
		public DateTime DateSubmitted
		{
			get { return this.dateSubmitted; }
		}

		/// <summary>
		/// Gets the URL from which the attachment can be downloaded.
		/// </summary>
		/// <remarks>
		/// At the moment downloading of attachments via MantisConnect is 
		/// not supported.  This URL can be used to download attachments 
		/// if the user is logged into Mantis, and has the appropriate
		/// access rights.  The user will have to do the download via
		/// the web browser.
		/// </remarks>
		public string DownloadUrl
		{
			get { return this.downloadUrl; }
		}
	}
}
