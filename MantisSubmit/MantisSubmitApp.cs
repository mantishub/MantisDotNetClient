//-----------------------------------------------------------------------
// <copyright file="MantisSubmitApp.cs" company="Victor Boctor">
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
using System.Windows.Forms;
using System.Diagnostics;

namespace Futureware.MantisSubmit
{
    /// <summary>
    /// Summary description for MantisConnectSampleApp.
    /// </summary>
    public sealed class MantisSubmitApp
    {
        private MantisSubmitApp()
        {
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run( new SubmitIssueForm() );
        }
    }
}
