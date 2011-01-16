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
    /// A class that includes a set of string utility methods.
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Converts a multiline string from webservice format to native format that
        /// is used by the local OS and its controls.
        /// </summary>
        /// <remarks>
        /// This is to be called on fields that support multiline string after
        /// reading them from the webservice and before setting them in controls
        /// like textboxes.
        /// 
        /// <seealso cref="NativeMultilineToWebservice"/>
        /// </remarks>
        /// <param name="webserviceMultiline">The string in webservice format.</param>
        /// <returns>The string in native format</returns>
        public static string WebserviceMultilineToNative(string webserviceMultiline)
        {
            return webserviceMultiline.Replace("\n", Environment.NewLine);
        }

        /// <summary>
        /// Converts a multiline string from native format to webservice format.
        /// </summary>
        /// <remarks>
        /// This is to be called on fields that support multiline strings before 
        /// sending them to the webservice.
        /// 
        /// <seealso cref="WebserviceMultilineToNative"/>
        /// </remarks>
        /// <param name="nativeMultiline"></param>
        /// <returns></returns>
        public static string NativeMultilineToWebservice(string nativeMultiline)
        {
            return nativeMultiline.Replace(Environment.NewLine, "\n");
        }
    }
}