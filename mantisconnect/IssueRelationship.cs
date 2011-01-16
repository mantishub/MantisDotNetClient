#region Copyright © 2004-2005 Victor Boctor
//
// MantisConnect is copyrighted to Victor Boctor
//
// This program is distributed under the terms and conditions of the GPL
// See LICENSE file for details.
//
// For commercial applications to link with or modify MantisConnect, they require the
// purchase of a MantisConnect commerical license.
//
#endregion

using System;

namespace Futureware.MantisConnect
{
	/// <summary>
	/// A type that stores information relating to a relationship between two issues.
	/// </summary>
    [Serializable]
    public sealed class IssueRelationship
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueRelationship"/> class.
        /// </summary>
        /// <param name="relationshipData">Relationship data in webservice proxy type.</param>
		internal IssueRelationship( MantisConnectWebservice.RelationshipData relationshipData )
		{
			this.Type = new ObjectRef( relationshipData.type );
			this.IssueId = Convert.ToInt32( relationshipData.target_id );
		}

        /// <summary>
        /// Converts an array of relationships from webservice proxy type to this type.
        /// </summary>
        /// <param name="relationshipsData">Relationships data.</param>
        /// <returns>An array of relationships in this type.</returns>
		internal static IssueRelationship[] ConvertArray( MantisConnectWebservice.RelationshipData[] relationshipsData )
		{
			if ( relationshipsData == null )
				return null;

			IssueRelationship[] relationships = new IssueRelationship[relationshipsData.Length];

			for ( int i = 0; i < relationshipsData.Length; ++i )
				relationships[i] = new IssueRelationship( relationshipsData[i] );

			return relationships;
		}

        /// <summary>
        /// Gets or sets the relationship type.
        /// </summary>
        /// <value></value>
		public ObjectRef Type
		{
			get { return type; }
			set { type = value; }
		}

        /// <summary>
        /// Gets or sets the issue id of the other issues involved in the relationship.  Note
        /// that relationships are stored as part of an issue, hence, one of the issues involved
        /// in a relationship is known implicitly.
        /// </summary>
        /// <value>Greater than or equal to 1.</value>
		public int IssueId
		{
			get { return issueId; }
			set { issueId = value; }
		}

		#region Private Members
		private ObjectRef type;
		private int issueId;
		#endregion
	}
}
