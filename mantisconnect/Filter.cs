//-----------------------------------------------------------------------
// <copyright file="Filter.cs" company="Victor Boctor">
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

namespace Futureware.MantisConnect
{
	/// <summary>
	/// A class that manages information relating to a Mantis filter.
	/// </summary>
    [Serializable]
    public class Filter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="filterData">The filter data stored in the webservice proxy data type.</param>
		internal Filter( MantisConnectWebservice.FilterData filterData )
		{
			this.id = Convert.ToInt32( filterData.id );
			this.owner = new User( filterData.owner );
			this.projectId = Convert.ToInt32( filterData.project_id );
			this.isPublic = filterData.is_public;
			this.name = filterData.name;
			this.filterString = filterData.filter_string;
		}

		/// <summary>
		/// Converts an array of filters from webservice data type to instances of <see cref="Filter"/> class.
		/// </summary>
		/// <param name="filtersData">An array of filters stored in webservice proxy data type.</param>
		/// <returns>An array of <see cref="Filter"/> instances.</returns>
		internal static Filter[] ConvertArray( MantisConnectWebservice.FilterData[] filtersData )
		{
			if ( filtersData == null )
				return null;

			Filter[] filters = new Filter[filtersData.Length];

			for ( int i = 0; i < filtersData.Length; ++i )
				filters[i] = new Filter( filtersData[i] );

			return filters;
		}

		/// <summary>
		/// Filter Id, must be greater than or equal to 1.
		/// </summary>
		public int Id
		{
			get { return id; }
		}

		/// <summary>
		/// The user who defined the filter.
		/// </summary>
		public User Owner
		{
			get { return owner; }
		}

		/// <summary>
		/// The project to which the filter belongs, or 0 for All Projects.
		/// </summary>
		public int ProjectId
		{
			get { return projectId; }
		}

		/// <summary>
		/// A boolean that indicates whether the filter is public (available to all users) or
		/// private (available only to the user who created it).
		/// </summary>
		public bool IsPublic
		{
			get { return isPublic; }
		}

		/// <summary>
		/// The name of the filter
		/// </summary>
		public string Name
		{
			get { return name; }
		}

		/// <summary>
		/// The string that defines the filter.  At the moment this format is treated as 
		/// a blackbox and is not interpretted by MantisConnect.
		/// </summary>
		public string FilterString
		{
			get { return filterString; }
		}

		#region Private Members
		private readonly int id;
		private User owner;
		private readonly int projectId;
		private readonly bool isPublic;
		private readonly string name;
		private readonly string filterString;
		#endregion
	}
}
