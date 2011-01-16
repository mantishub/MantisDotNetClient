#region Copyright © 2004 Victor Boctor
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
using System.Windows.Forms;

namespace Futureware.MantisNotify
{
	/// <summary>
	/// Application class for Mantis Notify application.
	/// </summary>
	public sealed class MantisNotifyApp
	{
        /// <summary>
        /// Private Constructor, no need to create instances of this class.
        /// </summary>
		private MantisNotifyApp()
		{
		}

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() 
        {
            Application.Run( new MantisNotifyForm() );
        }
    }
}
