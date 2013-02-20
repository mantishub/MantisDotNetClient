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

namespace Futureware.MantisConnect
{
    using System;

    /// <summary>
	/// A class that manages information relating to a Mantis filter.
	/// </summary>
    [Serializable]
    public sealed class Filter
	{
        /// <summary>
        /// The filter id.
        /// </summary>
        private readonly int id;

        /// <summary>
        /// The project id the filter is associated with.
        /// </summary>
        private readonly int projectId;

        /// <summary>
        /// A flag indicating whether the filter is public or not.
        /// </summary>
        private readonly bool isPublic;

        /// <summary>
        /// The name of the filter.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The filter string.
        /// </summary>
        private readonly string filterString;

        /// <summary>
        /// The owner of the filter.
        /// </summary>
        private User owner;

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="filterData">The filter data stored in the webservice proxy data type.</param>
		internal Filter(MantisConnectWebservice.FilterData filterData)
		{
			this.id = Convert.ToInt32(filterData.id);
			this.owner = new User(filterData.owner);
			this.projectId = Convert.ToInt32(filterData.project_id);
			this.isPublic = filterData.is_public;
			this.name = filterData.name;
			this.filterString = filterData.filter_string;
		}

		/// <summary>
		/// Converts an array of filters from webservice data type to instances of <see cref="Filter"/> class.
		/// </summary>
		/// <param name="filtersData">An array of filters stored in webservice proxy data type.</param>
		/// <returns>An array of <see cref="Filter"/> instances.</returns>
		internal static Filter[] ConvertArray(MantisConnectWebservice.FilterData[] filtersData)
		{
            if (filtersData == null)
            {
                return null;
            }

			Filter[] filters = new Filter[filtersData.Length];

            for (int i = 0; i < filtersData.Length; ++i)
            {
                filters[i] = new Filter(filtersData[i]);
            }

			return filters;
		}

		/// <summary>
		/// Gets the filter Id, must be greater than or equal to 1.
		/// </summary>
		public int Id
		{
			get { return this.id; }
		}

		/// <summary>
		/// Gets the user who defined the filter.
		/// </summary>
		public User Owner
		{
			get { return this.owner; }
		}

		/// <summary>
		/// Gets the project to which the filter belongs, or 0 for All Projects.
		/// </summary>
		public int ProjectId
		{
			get { return this.projectId; }
		}

		/// <summary>
		/// Gets a value indicating whether the filter is public (available to all users) or
		/// private (available only to the user who created it).
		/// </summary>
		public bool IsPublic
		{
			get { return this.isPublic; }
		}

		/// <summary>
		/// Gets the name of the filter
		/// </summary>
		public string Name
		{
			get { return this.name; }
		}

		/// <summary>
		/// Gets the string that defines the filter.  At the moment this format is treated as 
		/// a blackbox and is not interpretted by MantisConnect.
		/// </summary>
		public string FilterString
		{
			get { return this.filterString; }
		}
	}
}
