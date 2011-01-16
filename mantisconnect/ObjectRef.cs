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
	/// A class that is used on entites to reference other entities.
	/// </summary>
	/// <remarks>
	/// An example is where an issue refers to the project is belongs to.  In some cases it can know 
	/// the id, in others, the name, or sometimes both.
	/// </remarks>
    [Serializable]
    public sealed class ObjectRef
	{
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ObjectRef()
            : this( 0, string.Empty )
        {
        }

        /// <summary>
        /// Constructor specifying id and name
        /// </summary>
        /// <param name="id">Id of the entity referenced</param>
        /// <param name="name">Name of the entity referenced.</param>
		public ObjectRef( int id, string name )
		{
            this.Id = id;
            this.Name = name;
		}

        /// <summary>
        /// Constructor from webservice object ref object.
        /// </summary>
        /// <param name="objRefFromWs">Object reference from webservice</param>
        public ObjectRef( MantisConnectWebservice.ObjectRef objRefFromWs )
        {
            if ( objRefFromWs != null )
            {
                this.Id = Convert.ToInt32( objRefFromWs.id );
                this.Name = objRefFromWs.name;
            }
            else
            {
                this.Id = 0;
                this.Name = string.Empty;
            }
        }

        /// <summary>
        /// Constructor specifying id.
        /// </summary>
        /// <param name="id">Id of the entity referenced</param>
        public ObjectRef( int id )
            : this( id, string.Empty )
        {
        }

        /// <summary>
        /// Constructor specifying name.
        /// </summary>
        /// <param name="name">Name of the entity referenced.</param>
        public ObjectRef( string name )
            : this( 0, name )
        {
        }

        /// <summary>
        /// Converts the current object into the Webservice object.
        /// </summary>
        /// <returns>Webservice object reference</returns>
        internal MantisConnectWebservice.ObjectRef ToWebservice()
        {
            MantisConnectWebservice.ObjectRef wsor = new MantisConnectWebservice.ObjectRef();
            wsor.id = Id.ToString();
            wsor.name = Name;

            return wsor;
        }

        /// <summary>
        /// Checks if the current object reference instance doesn't reference anything.
        /// </summary>
        /// <value>true: references something, false: doesn't reference anything.</value>
        public bool Empty
        {
            get { return ( id == 0 ) && ( Name.Trim().Length == 0 ); }
        }

        /// <summary>
        /// Id of the referenced entity or 0 if unknown
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Name of the referenced entity or 0 if unknown
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value == null ? string.Empty : value; }
        }

        /// <summary>
        /// String representation of ObjectRef object.
        /// </summary>
        /// <returns>String displaying internal state.</returns>
        public override string ToString()
        {
            return string.Format( "ObjectRef( {0}, '{1}' )", Id, Name );
        }

        #region Private
        private int id;
        private string name = string.Empty;
        #endregion
	}
}
