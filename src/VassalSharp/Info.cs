/*
* $Id$
*
* Copyright (c) 2003 by Rodney Kinney
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/

using System;

namespace VassalSharp
{
	
	/// <summary> Class for storing release-related information</summary>
	public static class Info
	{
		private static readonly Version VERSION = new Version(3, 2, 16);

		// Do not allow editing of modules with this revision or later
		private static readonly Version EXPIRY_VERSION = new Version(3, 3);

		// Warn about editing modules, saves, logs written before this version
		private static readonly Version UPDATE_VERSION = new Version(3, 2);

		/// <summary> A valid version format is "w.x.y", where 'w','x',and 'y' are
		/// integers. In the version number, w.x are the major/minor release number,
		/// and y is the bug-fix number.
		/// </summary>
		/// <returns> the version of the VASSAL engine.
		/// </returns>
		public static string Version = VERSION.ToString(3);

        public static readonly string LocalAppDataDir =
            System.IO.Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create),
				"VassalSharp",
				UPDATE_VERSION.ToString(2));

		public static readonly string ConfDir = System.IO.Path.Combine(LocalAppDataDir, "config");

		public static readonly string TempDir = System.IO.Path.GetTempPath();

		public static readonly string PrefsDir = System.IO.Path.Combine(LocalAppDataDir, "prefs");
		
	}
}