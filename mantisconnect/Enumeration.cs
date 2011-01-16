#region Copyright © 2004-2007 Victor Boctor
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
using System.Collections.Generic;
using System.Globalization;

namespace Futureware.MantisConnect
{
	/// <summary>
	/// A class to handle enumerations that are defined in Mantis.
	/// </summary>
	/// <remarks>
	/// The format of Mantis enumerations is as follows:
	/// "10:viewer,25:reporter,40:updater,55:developer,70:manager,90:administrator"
	/// </remarks>
    public sealed class MantisEnum
    {
        /// <summary>
        /// The enumeration string supplied at construction time.
        /// </summary>
        private readonly string enumeration;

        /// <summary>
        /// A dictionary that maps labels to their corresponding code.
        /// </summary>
        private Dictionary<string, int> labelToCodeMap;

        /// <summary>
        /// A dictionary that maps codes to their corresponding labels.
        /// </summary>
        private Dictionary<int, string> codeToLabelMap;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enumeration">Enumeration string to work with.</param>
        public MantisEnum(string enumeration)
        {
            if (String.IsNullOrEmpty(enumeration))
            {
                throw new ArgumentNullException(enumeration);
            }

            this.enumeration = enumeration;

            string[] entries = enumeration.Split(',');

            this.labelToCodeMap = new Dictionary<string, int>(entries.Length);
            this.codeToLabelMap = new Dictionary<int, string>(entries.Length);

            int code;
            string label;

            foreach (string entry in entries)
            {
                string[] details = entry.Split(':');
                if (details.Length != 2)
                {
                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Invalid enumeration '{0}'.", enumeration));
                }

                if (!Int32.TryParse(details[0].Trim(), out code))
                {
                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Invalid code '{0}' in enumeration '{1}'.", details[0], enumeration));
                }

                label = details[1].Trim();
                if (label.Length == 0)
                {
                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Code '{0}' has an empty label in enumeration '{1}'", code, enumeration));
                }

                if (this.labelToCodeMap.ContainsKey(label))
                {
                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Label '{0}' exists more than once in enumeration '{1}'", label, enumeration));
                }

                if (this.codeToLabelMap.ContainsKey(code))
                {
                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Code '{0}' exists more than once in enumeration '{1}'", code, enumeration));
                }

                this.labelToCodeMap[label] = code;
                this.codeToLabelMap[code] = label;
            }
        }

        /// <summary>
        /// Given an id, this indexer returns the corresponding enumeration name.
        /// </summary>
        /// <param name="id">The enumeration value id.</param>
        /// <returns>The enumeration label.</returns>
        public string this[int id]
        {
            get 
            {
                if (this.codeToLabelMap.ContainsKey(id))
                {
                    return this.codeToLabelMap[id];
                }

                return string.Format(CultureInfo.InvariantCulture, "@{0}@", id);
            }
        }

        /// <summary>
        /// Given a name, this indexer returns the corresponding enumeration id.
        /// </summary>
        /// <param name="name">The enumeration value name.</param>
        /// <returns>The enumeration code.</returns>
        public int this [string name]
        {
            get 
            {
                if (this.labelToCodeMap.ContainsKey(name))
                {
                    return this.labelToCodeMap[name];
                }

                return 0;
            }
        }

        /// <summary>
        /// The number of values in the enumeration.
        /// </summary>
        public int Count
        {
            get { return this.labelToCodeMap.Count; }
        }

        /// <summary>
        /// Returns a collection containing the labels in the enumeration.
        /// </summary>
        /// <returns>A collection of the labels.</returns>
        public ICollection<string> GetLabels()
        {
            Dictionary<string, int>.KeyCollection keys = this.labelToCodeMap.Keys;

            string[] labels = new string[keys.Count];

            int i = 0;
            foreach ( string label in keys ) 
            {
                labels[i++] = label;
            }
            
            return labels;
        }

        /// <summary>
        /// Returns a collection containing the codes in the enumeration.
        /// </summary>
        /// <returns>A collection of the codes.</returns>
        public ICollection<int> GetCodes()
        {
            Dictionary<int, string>.KeyCollection keys = this.codeToLabelMap.Keys;

            int[] codes = new int[keys.Count];

            int i = 0;
            foreach (int code in keys)
            {
                codes[i++] = code;
            }

            return codes;
        }
    }
}
