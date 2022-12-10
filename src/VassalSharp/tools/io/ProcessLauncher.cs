/*
* $Id$
*
* Copyright (c) 2010 by Joel Uckelman
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/

using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics;
using System.IO;

namespace VassalSharp.tools.io
{
	
	/// <summary> 
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public class ProcessLauncher
	{
		private static readonly ILogger logger;
		
		public ProcessLauncher()
		{
		}

		/// <summary> Launches a process.
		/// 
		/// </summary>
		/// <param name="args">the command-line arguments
		/// 
		/// </param>
		/// <throws>  IOException if the process fails to launch </throws>
		public ProcessWrapper launch(params String[] args)
		{
			return launch(null, System.Console.OpenStandardInput(), System.Console.OpenStandardOutput(), System.Console.Error, args);
		}

		/// <summary> Launches a process.
		/// 
		/// </summary>
		/// <param name="workDir">the process' working directory
		/// </param>
		/// <param name="stdout">the stream where the process' STDOUT is redirected
		/// </param>
		/// <param name="stderr">the stream where the process' STDERR is redirected
		/// </param>
		/// <param name="args">the command-line arguments
		/// 
		/// </param>
		/// <throws>  IOException if the process fails to launch </throws>
		public ProcessWrapper launch(FileInfo workDir, Stream stdin, Stream stdout, TextWriter stderr, params String[] args)
		{
			logger.LogInformation("launching " + String.Join(" ", args));

			ProcessStartInfo psi = new ProcessStartInfo();
			psi.WorkingDirectory = workDir.FullName;
			Process  proc = new Process();
			proc.StartInfo = psi;
			proc.Start();

			return new ProcessWrapper(future, proc.StandardInput.BaseStream, proc.StandardError.BaseStream, proc.StandardOutput.BaseStream);
		}
	}
}