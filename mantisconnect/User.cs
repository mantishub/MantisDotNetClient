//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Victor Boctor">
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
	/// A class that includes information relating to a Mantis user account.
	/// </summary>
    [Serializable]
    public sealed class User
	{
        /// <summary>
        /// The user id.
        /// </summary>
        private int id;

        /// <summary>
        /// The user name.
        /// </summary>
        private string name;

        /// <summary>
        /// The user real name.
        /// </summary>
        private string realName;

        /// <summary>
        /// The user email address.
        /// </summary>
        private string email;
 
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
		public User()
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="accountData">Account data.</param>
		internal User(MantisConnectWebservice.AccountData accountData)
		{
			this.Id = Convert.ToInt32(accountData.id);
			this.Name = accountData.name;
			this.RealName = accountData.real_name;
			this.Email = accountData.email;
		}

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
		public override string ToString()
		{
			return this.Name;
		}

        /// <summary>
        /// Converts this instance to the webservice proxy type.
        /// </summary>
        /// <returns>An equivalent object in the webservice proxy type.</returns>
		internal MantisConnectWebservice.AccountData ToWebservice()
		{
			MantisConnectWebservice.AccountData accountData = new MantisConnectWebservice.AccountData();
			accountData.id = this.Id.ToString();
			accountData.name = this.Name;
			accountData.real_name = this.RealName;
			accountData.email = this.Email;

			return accountData;
		}

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>Greater than or equal to 1.</value>
		public int Id
		{
			get { return this.id; }
			set { this.id = value; }
		}

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>Must not be empty or null.</value>
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

        /// <summary>
        /// Gets or sets the real name of the user.
        /// </summary>
        /// <value>Can be empty or null.</value>
		public string RealName
		{
			get { return this.realName; }
			set { this.realName = value; }
		}

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>Can be empty or null.</value>
		public string Email
		{
			get { return this.email; }
			set { this.email = value; }
		}
	}
}
